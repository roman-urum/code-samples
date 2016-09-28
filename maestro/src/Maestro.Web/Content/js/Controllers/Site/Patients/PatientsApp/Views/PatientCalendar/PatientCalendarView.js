'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'async',
    'Controllers/Site/Patients/PatientsApp/AppNamespace',
    'Controllers/Helpers',
    'moment',
    'Controllers/Constants',
    'calendar',
    'Controllers/Site/Patients/PatientsApp/Collections/PatientCalendarCollection',
    'Controllers/Site/Patients/PatientsApp/Views/PatientCalendar/PatientCalendarCollectionView',
    'Controllers/Site/Patients/PatientsApp/Collections/PatientCareElementsSearchCollection',
    'Controllers/Site/Patients/PatientsApp/Views/PatientCalendar/PatientProgramSearchCollectionView',
    'Controllers/Site/Patients/PatientsApp/Collections/PatientCalendarProgramsCollection',
    'Controllers/Site/Patients/PatientsApp/Views/PatientCalendar/PatientCalendarHistoryView',
    'Controllers/Site/Patients/PatientsApp/Models/AdherencesAndProgramsModel',
    'Controllers/Site/Patients/PatientsApp/Views/PatientCalendar/PatientHealthSessionView',
    'Controllers/Site/Patients/PatientsApp/Views/PatientCalendar/PatientHealthSessionDefaultContainerView',
    'Controllers/Site/Patients/PatientsApp/Collections/PatientConditionsCollection'
], function (
    $,
    _,
    Backbone,
    async,
    app,
    Helpers,
    moment,
    Constants,
    Calendar,
    PatientCalendarCollection,
    PatientCalendarCollectionView,
    PatientCareElementsSearchCollection,
    PatientProgramSearchCollectionView,
    PatientCalendarProgramsCollection,
    PatientCalendarHistoryView,
    AdherencesAndProgramsModel,
    PatientHealthSessionView,
    PatientHealthSessionDefaultContainerView,
    PatientConditionsCollection
) {
    return Backbone.View.extend({
        el: '#schedule',

        template: _.template($('#patientCalendarTemplate').html()),

        templateCalendar: _.template(
            '<header class="header-calendar">\
                <a class="btn btn-primary btn-radius-ci js-prev-month">\
                    <span class="glyphicon glyphicon-chevron-left"></span>\
                </a>\
                <span class="calendar-month-name"><%=monthName%>, <%=year%></span>\
                <a class="btn btn-primary btn-radius-ci js-next-month">\
                    <span class="glyphicon glyphicon-chevron-right"></span>\
                </a>\
            </header>\
            <div id="calendar-content" class="calendar-content"></div>\
            '
        ),

        events: {
            'click .js-prev-month': 'renderPrevMonth',
            'click .js-next-month': 'renderNextMonth',
            'change #search-program-elements-keyword': 'filterProgramSearchList',
            'keyup #search-program-elements-keyword': 'filterProgramSearchList',
            'change #search-program-elements-tags': 'filterProgramSearchList',
            'chosen:updated #search-program-elements-tags': 'calculateClearSearchTagsButtonHeight',
            'click .js-search-program-clear': 'searchClear'
        },

        initialize: function() {
            _.bindAll(this, "updateCurrentMonth", "renderCalendarCollection", "patientProgramSearchCollectionViewRender");
            app.vent.bind("updateCurrentMonth", this.updateCurrentMonth);
            this.listenTo(this.model, 'change:month', this.renderCalendar);            
        },

        render: function () {
            var self = this;
            self.$el.html(self.template());

            var workers = [];
            workers.push(function(cb) {

                Helpers.initTags(null, self.$el.find('#search-program-elements-tags'), function () {
                    cb(null, null);
                });

            });
            workers.push(function(cb) {

                if (!app.collections.patientConditionsCollection) {
                    app.collections.patientConditionsCollection = new PatientConditionsCollection();
                    app.collections.patientConditionsCollection.isFetched = false;
                }

                if (!app.collections.patientConditionsCollection.isFetched) {
                    app.collections.patientConditionsCollection.fetch({
                        success: function () {
                            app.collections.patientConditionsCollection.isFetched = true;
                            cb(null, null);
                        }
                    });
                } else {
                    cb(null, null);
                }
            });
            workers.push(function (cb) {

                if (!app.collections.patientProgramSearchCollection) {
                    app.collections.patientProgramSearchCollection = new PatientCareElementsSearchCollection();
                    app.collections.patientProgramSearchCollection.isFetched = false;                    
                }

                if (!app.collections.patientProgramSearchCollection.isFetched) {
                    app.collections.patientProgramSearchCollection.fetch({
                        data: {
                            categories: "program",
                            q: self.getSearchKeyword(),
                            tags: self.getSearchTags()
                        },
                        success: function () {
                            app.collections.patientProgramSearchCollection.isFetched = true;
                            cb(null, null);
                        }
                    });
                } else {
                    cb(null, null);
                }

            });

            async.parallel(workers, function (err, results) {

                self.renderProgramSearchList();

                setTimeout(function() {
                    self.renderPartientsConditionsTags.call(self);
                }, 100);


            });

            self.renderCalendar();
            self.renderHealthSession();
            self.renderCalendarHistory();
            self.loadPatientConditions();
            return self;
        },

        renderCalendar: function() {
            var month = this.model.get('month'),
                year = this.model.get('year'),
                monthArray = [],
                weekArray = [],
                cal = new Calendar(),
                weeks = cal.monthDaysFull(year, month),
                today = moment().format('YYYY-MM-DD'),
                date;

            this.model.set('monthName', Constants.monthNames[month]);

            this.$el.find('#patient-calendar-container').html(this.templateCalendar(this.model.attributes));

            Helpers.renderSpinner(this.$el.find('#calendar-content'));

            month = month + 1;

            _.each(weeks, function(week) {
                weekArray = [];

                _.each(week, function(day) {
                    date = year + '-' + (day.month < 10 ? '0' : '') + day.month + '-' + (day.day < 10 ? '0' : '') + day.day;

                    day = {
                        date: date,
                        day: day.day,
                        month: (day.month < 10 ? '0' : '') + day.month,
                        year: year,
                        isAvailable: moment(today).isSameOrBefore(date), // isBefore
                        isToday: moment(today).isSame(date),
                        isMonth: month == day.month ? true : false
                    };

                    weekArray.push(day);
                });
                monthArray = monthArray.concat(weekArray);
            });

            app.collections.patientCalendarCollection = new PatientCalendarCollection(monthArray);

            if (!app.models.adherencesAndProgramsModel) {
                app.models.adherencesAndProgramsModel = new AdherencesAndProgramsModel({ year: year, month: month });
                app.models.adherencesAndProgramsModel.isFetched = false;

                if (app.xhr.adherencesAndProgramsModel && app.xhr.adherencesAndProgramsModel.readyState)
                    app.xhr.adherencesAndProgramsModel.abort();

                app.xhr.adherencesAndProgramsModel = app.models.adherencesAndProgramsModel.fetch({
                    success: this.renderCalendarCollection
                });

            } else {
                if (app.models.adherencesAndProgramsModel.isFetched) {
                    this.renderCalendarCollection();
                }
            }

            return this;
        },

        renderCalendarCollection: function() {
            app.models.adherencesAndProgramsModel.isFetched = true;

            app.collections.patientCalendarProgramsCollection = new PatientCalendarProgramsCollection(app.models.adherencesAndProgramsModel.get('programs'));

            app.collections.adherencesCollection = new Backbone.Collection(app.models.adherencesAndProgramsModel.get('adherences'));
            app.collections.adherencesCollection.each(function(adherencesModel) {

                var patientCalendarModel = app.collections.patientCalendarCollection.find(function(model) {
                    return model.get('date') == adherencesModel.get('scheduled').substring(0, 10) && !adherencesModel.get('deleted');
                });

                if (patientCalendarModel) {
                    // var isOneTime = ( !adherencesModel.get('calendarEvent').calendarProgramId && !adherencesModel.get('calendarEvent').programId );

                    // var calendarEvent = {
                    //     name: adherencesModel.get('calendarEvent').name,
                    //     programDay: adherencesModel.get('calendarEvent').programDay,
                    //     status: adherencesModel.get('status'),
                    //     calendarProgramId: adherencesModel.get('calendarEvent').calendarProgramId,
                    //     programId: adherencesModel.get('calendarEvent').programId,
                    //     isOneTime: isOneTime
                    // };

                    // if( isOneTime ){
                    var calendarEvent = adherencesModel.get('calendarEvent');

                    calendarEvent.isOneTime = (!adherencesModel.get('calendarEvent').calendarProgramId && !adherencesModel.get('calendarEvent').programId);
                    calendarEvent.status = adherencesModel.get('status');
                    // }

                    var calendarEventArray = patientCalendarModel.get('calendarEvents');
                    calendarEventArray = calendarEventArray.concat([calendarEvent]);
                    patientCalendarModel.set('calendarEvents', calendarEventArray);
                }
            });

            app.collections.patientCalendarCollection.each(function(model) {
                var eventCollection = new Backbone.Collection(model.get('calendarEvents'));
                model.set('calendarEvents', eventCollection);
            });

            this.renderDefaultHealthSessionContainer();

            this.renderCalendarHeader();

            app.views.patientCalendarCollectionView = new PatientCalendarCollectionView({ collection: app.collections.patientCalendarCollection });
            this.$el.find('#calendar-content').append(app.views.patientCalendarCollectionView.render().el);
            this.calculateLeftPanelHeight();

        },

        renderCalendarHeader: function() {
            this.$el.find('#calendar-content').append('<header class="header-patient-calendar" />');
            _.each(Constants.dayNames, function(month) {
                this.$el.find('.header-patient-calendar').append('<div class="calendar-day-name">' + month + '</div>');
            }, this);
        },

        renderNextMonth: function() {
            var month = this.model.get('month'),
                year = this.model.get('year');

            app.models.adherencesAndProgramsModel = null;

            if (month > 10) {
                month = 0;
                year = year + 1;
            } else {
                month = month + 1;
            }

            this.model.set('year', year);
            this.model.set('month', month);
        },

        renderPrevMonth: function() {
            var month = this.model.get('month'),
                year = this.model.get('year');

            app.models.adherencesAndProgramsModel = null;

            if (month < 1) {
                month = 11;
                year = year - 1;
            } else {
                month = month - 1;
            }

            this.model.set('year', year);
            this.model.set('month', month);
        },

        updateCurrentMonth: function() {
            app.models.adherencesAndProgramsModel = null;
            this.model.trigger('change:month');
            app.collections.PatientCalendarChangesCollection.trigger('reFetch');
        },

        renderProgramSearchList: function (isReset) {

            var $searchResultContainer = this.$el.find('#patient-program-search-result');

            Helpers.renderSpinner($searchResultContainer);

            if (app.collections.patientProgramSearchCollection) {
                app.collections.patientProgramSearchCollection = !isReset ? app.collections.patientProgramSearchCollection : null;
            }

            if (!app.collections.patientProgramSearchCollection) {
                app.collections.patientProgramSearchCollection = new PatientCareElementsSearchCollection();
                app.collections.patientProgramSearchCollection.isFetched = false;

                app.collections.patientProgramSearchCollection.fetch({
                    data: {
                        categories: "program",
                        q: this.getSearchKeyword(),
                        tags: this.getSearchTags()
                    },
                    success: this.patientProgramSearchCollectionViewRender
                });
            } else {
                if (app.collections.patientProgramSearchCollection.isFetched) {
                    this.patientProgramSearchCollectionViewRender();
                }
            }
        },

        filterProgramSearchList: function () {            
            var searchString = this.getSearchKeyword().trim(),
                pattern = new RegExp(searchString, "gi"),
                searchTags = this.getSearchTags(),
                $searchResultContainer = this.$el.find('#patient-program-search-result');

            Helpers.renderSpinner($searchResultContainer);

            app.collections.patientProgramSearchCollection.each(function(model) {
                var isDisplayFilterText = pattern.test(model.get("name"));                
                var isDisplayFilterTags = searchTags.length <= 0 ||  _.intersection(model.get("tags"), searchTags).length > 0;
                var isDisplay = isDisplayFilterText && isDisplayFilterTags;

                if (searchString === '' && searchTags.length === 0) {
                    isDisplay = true;
                }

                model.set('isDisplay', isDisplay);
            }, this);

            this.patientProgramSearchCollectionViewRender();

            this.$el.find('#search-program-elements-tags').trigger('chosen:updated');
        },

        patientProgramSearchCollectionViewRender: function (showSuggestedContentLabel) {

            app.collections.patientProgramSearchCollection.isFetched = true;
            app.views.patientProgramSearchCollectionView = new PatientProgramSearchCollectionView({ collection: app.collections.patientProgramSearchCollection });
            this.$el.find('#patient-program-search-result').html(app.views.patientProgramSearchCollectionView.render().el);

            if (showSuggestedContentLabel) {
                this.$el.find('#patient-program-search-result').prepend(
                    '<div class="alert alert-info suggested-content-alert" role="alert">\
                        <strong>Suggested Content</strong>\
                        <span class="glyphicon glyphicon-question-sign"\
                                data-container="body"\
                                data-toggle="popover"\
                                data-content="This content is suggested for the patient based on the condition(s) assigned to the patient\'s profile"\
                                data-original-title="Default Health Sessions">\
                        </span>\
                    </div>');
                this.initPopover();
            }
        },

        calculateClearSearchTagsButtonHeight: function() {
           
            var self = this;
            setTimeout(function () {
                var newHeight = self.$el.find('#search_program_elements_tags_chosen').height() - 14;
                if (newHeight > 0) {
                    self.$el.find('.js-search-program-clear').height(newHeight);
                }
                
            }, 25);

        },

        getSearchKeyword: function () {
            return this.$el.find('#search-program-elements-keyword').val();
        },

        getSearchTags: function () {
            var searchTags = this.$el.find('#search-program-elements-tags').val();
            searchTags = (searchTags && searchTags !== '0') ? searchTags : [];

            searchTags = searchTags.filter(function (element, index) {
                return element !== '';
            });

            if (searchTags.length) {
                this.$el.find('.js-search-program-clear').prop('disabled', false);
            } else {
                this.$el.find('.js-search-program-clear').prop('disabled', true);
            }

            return searchTags.filter(function (tag) { return tag !== '' });
        },

        searchClear: function () {
            this.$el.find('#search-program-elements-tags')
                .val(0)
                .trigger("chosen:updated")
                .change();
        },

        renderHealthSession: function () {
            if (app.views.patientHealthSessionView)
                app.views.patientHealthSessionView.remove();

            app.views.patientHealthSessionView = new PatientHealthSessionView();
            this.$el.find('#session-container').html(app.views.patientHealthSessionView.render().el);
            // this.calculateLeftPanelHeight();
        },

        renderDefaultHealthSessionContainer: function () {
            if (app.views.patientHealthSessionDefaultContainerView)
                app.views.patientHealthSessionDefaultContainerView.remove();

            app.views.patientHealthSessionDefaultContainerView = new PatientHealthSessionDefaultContainerView();
            this.$el.find('#calendar-content').html(app.views.patientHealthSessionDefaultContainerView.render().el);
        },

        renderCalendarHistory: function () {
            app.views.patientCalendarHistoryView = new PatientCalendarHistoryView();
            app.views.patientCalendarHistoryView.render();
        },

        calculateLeftPanelHeight: function () {
            var height = this.$el.find('#patient-calendar-container #calendar-content').height();

            this.$el.find('.js-left-panel-content').outerHeight(height - 1);
        },

        editHealthSessionCalendar: function (options) {
            this.$el.find('[href="#tab-session"]').tab('show');
            app.views.patientHealthSessionView.editHealthSession(options);
        },

        loadPatientConditions: function() {

            var self = this;

            if (app.collections.patientConditionsCollection) {
                app.collections.patientConditionsCollection.bind('sync', function () {
                    self.renderPartientsConditionsTags.call(self);
                });
            } else {
                
                app.collections.patientConditionsCollection = new PatientConditionsCollection();
                app.collections.patientConditionsCollection.isFetched = false;
                app.collections.patientConditionsCollection.fetch({
                    success: function () {

                        self.renderPartientsConditionsTags.call(self);
                    }
                });
            }

        },

        getConditionsTags: function() {
            var conditionsTags = [];

            app.collections.patientConditionsCollection.each(function (model) {
                _.each(model.get('tags'), function (tag) { conditionsTags.push(tag) });
            });

            return conditionsTags;
        },

        renderPartientsConditionsTags: function () {
            var self = this;
            var conditionsTags = self.getConditionsTags();

            _.each(conditionsTags, function(tag) {

                var programsWithTag = app.collections.patientProgramSearchCollection.where(function(model) {
                    return _.intersection(model.get('tags'), [tag]).length > 0;
                });

                if (programsWithTag.length <= 0) return;

                var tagOption = self.$el.find('#search-program-elements-tags').find('option').filter(function(index, option) {
                    return $(option).text() === tag;
                });

                if (tagOption) {
                    tagOption.attr('selected', 'selected');
                } else {
                    tagOption = $('<option>');
                    tagOption.attr('selected', 'selected');
                    tagOption.text(tag);

                    this.$el.find('#search-program-elements-tags').append(tagOption);
                }

                self.$el.find('#search-program-elements-tags').trigger("chosen:updated");

            });

            app.collections.patientProgramSearchCollection.each(function (model) {
                var isDisplay = _.intersection(conditionsTags, model.get("tags")).length > 0;

                if (conditionsTags.length === 0) {
                    isDisplay = true;
                }

                model.set('isDisplay', isDisplay);
            }, self);

            self.patientProgramSearchCollectionViewRender(true);
        },

        initPopover: function() {

            this.$el.find('[data-toggle="popover"]').popover({
                template: ' <div class="popover">\
                                <div class="arrow"></div>\
                                <div class="popover-header">\
                                    <button type="button" class="close" data-dismiss="popover" aria-hidden="true">&times;</button>\
                                    <h3 class="popover-title"></h3>\
                                </div>\
                                <div class="popover-content"></div>\
                            </div>'
            }).on('shown.bs.popover', function (e) {
                $('[data-dismiss="popover"]').off('click').on('click', function () {
                    $(e.currentTarget).click();
                });
            });
        }
    });
});