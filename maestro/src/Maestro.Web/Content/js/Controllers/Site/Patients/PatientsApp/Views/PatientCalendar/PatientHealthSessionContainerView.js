'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'async',
    'Controllers/Site/Patients/PatientsApp/AppNamespace',
    'Controllers/Helpers',
    'BackboneModelBinder',
    'BackboneBootstrapModal',
    'BackboneBootstrapAlert',
    'moment',
    'Controllers/Site/Patients/PatientsApp/Collections/PatientCareElementsSearchCollection',
    'Controllers/Site/Patients/PatientsApp/Views/PatientCalendar/PatientProtocolSearchCollectionView',
    'Controllers/Site/Patients/PatientsApp/Views/PatientCalendar/PatientHealthSessionProtocolCollectionView',
    'Controllers/Site/Patients/PatientsApp/Views/PatientCalendar/PatientHealthSessionRecurringRulesView',
    'Controllers/Site/Patients/PatientsApp/Models/PatientHealthSessionRecurringRulesModel',
    'Controllers/Site/Patients/PatientsApp/Collections/PatientConditionsCollection'
], function (
    $,
    _,
    Backbone,
    async,
    app,
    Helpers,
    BackboneModelBinder,
    BackboneBootstrapModal,
    BackboneBootstrapAlert,
    moment,
    PatientCareElementsSearchCollection,
    PatientProtocolSearchCollectionView,
    PatientHealthSessionProtocolCollectionView,
    PatientHealthSessionRecurringRulesView,
    PatientHealthSessionRecurringRulesModel,
    PatientConditionsCollection
) {
    return Backbone.View.extend({
        className: 'b-session-container-content-ci',

        modelBinder: new BackboneModelBinder(),

        template: _.template($('#patientHealthSessionContainerTemplate').html()),

        templateNoSessionProtocol: _.template('<div class="no-protocol-placeholder">No Protocol Selected</div>'),

        templateRecurringSessionMonth: _.template('<label class="btn btn-default btn-sm btn-sm-square-ci"><input type="checkbox" value="<%=day%>" name="monthDays"><%=day%></label>'),

        selectors: {
            validForOverlay:    '.js-valid-for-overlay',
            submitBtn:          '.js-create-session',
            occurrencesErrMsg:  '.help-block-session-occurrences'
        },

        events: {
            'change #search-protocol-elements-keyword': 'filterProtocolSearchList',
            'keyup #search-protocol-elements-keyword': 'filterProtocolSearchList',
            'change #search-protocol-elements-tags': 'filterProtocolSearchList',
            'click #isNeverExpiring': 'onToggleExpiring',
            'click .js-search-protocol-clear': 'searchClear',
            'click .js-create-session': 'createSession',
            'click .js-cancel-session': 'resetSession',
            'chosen:updated #search-protocol-elements-tags': 'calculateClearSearchTagsButtonHeight'
        },

        initialize: function() {
            _.bindAll(this, "patientProtocolSearchCollectionViewRender", "resetSession", "createDefaultSession", "resetSession");

            this.listenTo(this.model, 'change:expireHours', this.updateExpireMinutes);
            this.listenTo(this.model, 'change:isRecurring', this.toggleRecurringForm);
            this.listenTo(this.model, 'change:isDefault', this.toggleDefaultForm);
            this.listenTo(this.model, 'change:timeType', this.updateSessionTimeInputState);
            this.listenTo(this.model, 'change:isNeverExpiring', this.renderOverlay);

            Backbone.Validation.bind(this);

            app.vent.bind('patient-health-session:resetSession', this.resetSession);
        },

        render: function() {
            var self = this;

            self.$el.html(self.template(self.model.attributes));

            self.initializeSessionProtocolCollection();

            self.modelBinder.bind(self.model, self.el);

            self.initializeDatetimepicker();

            self.initializeValidForInput();

            self.renderRecurringSessionRules();

            self.updateSessionTimeInputState();
            
            self.loadPatientConditions();

            self.renderOverlay();

            var workers = [];
            workers.push(function (cb) {
                Helpers.initTags(null, self.$el.find('#search-protocol-elements-tags'), function () {
                    cb(null, null);
                });
            });
            workers.push(function (cb) {
                Helpers.renderSpinner(self.$el.find('#patient-protocol-search-result'));

                if (!app.collections.patientProtocolSearchCollection) {
                    app.collections.patientProtocolSearchCollection = new PatientCareElementsSearchCollection();
                    app.collections.patientProtocolSearchCollection.isFetched = false;
                }

                if (!app.collections.patientProtocolSearchCollection.isFetched) {
                    app.collections.patientProtocolSearchCollection.fetch({
                        data: {
                            categories: "protocol",
                            q: self.getSearchKeyword(),
                            tags: self.getSearchTags()
                        },
                        success: function() {
                            cb(null, null);
                        }
                    });
                } else {
                    cb(null, null);
                }


            });
            workers.push(function (cb) {

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

            async.parallel(workers, function (err, results) {
                self.renderProtocolSearchList();
                self.renderSessionProtocolCollection();
                self.renderPartientsConditionsTags.call(self);
                self.filterProtocolSearchList.call(self);
            });

            return self;
        },

        renderProtocolSearchList: function(isReset) {
            var self = this;

            var $searchResultContainer = self.$el.find('#patient-protocol-search-result');

            Helpers.renderSpinner($searchResultContainer);

            if (app.collections.patientProtocolSearchCollection) {
                app.collections.patientProtocolSearchCollection = !isReset ? app.collections.patientProtocolSearchCollection : null;
            }

            if (!app.collections.patientProtocolSearchCollection) {

                self.$el.find('[href="#tab-session-schedule"]').addClass('disabled');

                app.collections.patientProtocolSearchCollection = new PatientCareElementsSearchCollection();
                app.collections.patientProtocolSearchCollection.isFetched = false;
                app.collections.patientProtocolSearchCollection.fetch({
                    data: {
                        categories: "protocol",
                        q: self.getSearchKeyword(),
                        tags: self.getSearchTags()
                    },
                    success: function() {
                        self.filterProtocolSearchList.call(self);
                    }
                });
            } else {
                if (app.collections.patientProtocolSearchCollection.isFetched) {
                    self.filterProtocolSearchList.call(self);
                }
            }
        },

        initializeSessionProtocolCollection: function() {
            app.models.patientHealthSessionProtocolModel = Backbone.Model.extend({});

            app.collections.patientHealthSessionProtocolCollection = Backbone.Collection.extend({
                model: app.models.patientHealthSessionProtocolModel,
                comparator: 'order'
            });

            this.model.set('protocols', new app.collections.patientHealthSessionProtocolCollection());

            this.listenTo(this.model.get('protocols'), 'add', this.renderSessionProtocolCollection);
            this.listenTo(this.model.get('protocols'), 'remove', this.renderSessionProtocolCollection);
        },

        renderSessionProtocolCollection: function() {
            var sessionProtocolsCount = this.model.get('protocols').length;

            if (sessionProtocolsCount) {
                app.views.patientHealthSessionProtocolCollectionView = new PatientHealthSessionProtocolCollectionView({ collection: this.model.get('protocols') });
                this.$el.find('#session-protocol-collection').html(app.views.patientHealthSessionProtocolCollectionView.render().el);

                this.$el.find('.js-create-session').removeClass('disabled');
                this.$el.find('#session-protocol-count').removeClass('hidden').text('(' + sessionProtocolsCount + ')');
            } else {
                this.$el.find('#session-protocol-collection').html(this.templateNoSessionProtocol());

                this.$el.find('.js-create-session').addClass('disabled');
                this.$el.find('#session-protocol-count').addClass('hidden').empty();
            }
        },

        onToggleExpiring: function (e) {
            //var isNeverExpiring = !!$(e.target).prop('checked');
            //this.model.set('isNeverExpiring', isNeverExpiring);
            //this.renderOverlay();
        },

        renderOverlay: function () {
            var isNeverExpiring = this.model.get('isNeverExpiring');
            if (isNeverExpiring) {
                this.$(this.selectors.validForOverlay).show();
            } else {
                this.$(this.selectors.validForOverlay).hide();
            }
        },

        validateHealthSessionOccurrences: function () {
            var occurrencesErrorMsg =  app.models.patientHealthSessionRecurringRulesModel.validateOccurrences();

            if (occurrencesErrorMsg) {
                this.$(this.selectors.submitBtn).addClass('disabled');
                this.$(this.selectors.occurrencesErrMsg).text(occurrencesErrorMsg).removeClass('hidden');
            } else {
                this.$(this.selectors.submitBtn).removeClass('disabled');
                this.$(this.selectors.occurrencesErrMsg).addClass('hidden');
            }

            return occurrencesErrorMsg;
        },

        createSession: function () {

            this.isEdit = this.model.get('isEdit');

            if (this.model.get('isDefault')) {
                if (!this.model.get('startDate')) {
                    this.model.set('startDate', moment(this.model.defaults.startDate).format("YYYY-MM-DD"));
                    this.model.set('startDateDp', moment(this.model.defaults.startDate).format("MM/DD/YYYY"));
                    this.$el.find('#startDateDp').val(this.model.get('startDateDp'));
                }

                if (!this.model.get('sessionTime')) {
                    this.model.set('sessionTime', moment(app.models.patientModel.get('preferredSessionTime'), ["HH:mm"]).format("HH:mm"));
                    this.model.set('sessionTimeTp', moment(app.models.patientModel.get('preferredSessionTime'), ["HH:mm"]).format("h:mm A"));
                    this.$el.find('#sessionTimeTp').val(this.model.get('sessionTimeTp'));
                }

                if (!this.model.get('expireHours')) {
                    this.model.set('expireHours', this.model.defaults.expireHours);
                }
            }

            if (this.model.get('isNeverExpiring')) {
                this.model.set({expireHours: null, expireMinutes: null}, {silent: true});
            }

            if (this.healthSessionIsValid()) {
                this.model.get('protocols').each(function(model) {
                    model.set('protocolId', model.get('id'));
                });

                this.$el.find('.js-create-session').data('loading-text', (this.isEdit) ? 'Updating...' : 'Creating...').button('loading');
                this.$el.find('.js-cancel-session').addClass('disabled');

                if (this.model.get('isDefault')) {
                    if (app.models.patientHealthSessionDefaultModel.get('id') && !this.isEdit) {

                        var confirmModal = new BackboneBootstrapModal({
                                title: 'Replace Current Default Health Session?',
                                content: '<div class="alert alert-danger hidden" role="alert"></div><h3>The patient already has a Default Health Session assigned.<br>Replace the current Default Health Session?</h3>',
                                modalOptions: {
                                    backdrop: 'static',
                                    keyboard: false
                                }
                            })
                            .open()
                            .on('ok', this.createDefaultSession)
                            .on('cancel', this.resetSession);

                    } else {
                        this.createDefaultSession();
                    }
                } else {
                    this.createRegularSession();
                }
            }
        },

        createRegularSession: function() {
            var self = this,
                startDate = this.model.get('startDate'),
                sessionTime = this.model.get('sessionTime'),
                isRecurring = this.model.get('isRecurring'),
                recurringFrequency = app.models.patientHealthSessionRecurringRulesModel.get('frequency'),
                recurringInterval;

            app.models.patientHealthSessionRecurringRulesModel.set('startDate', startDate + ' ' + sessionTime);
            app.models.patientHealthSessionRecurringRulesModel.set('endDate', app.models.patientHealthSessionRecurringRulesModel.get('endDate') + ' ' + '23:59:59');

            switch (recurringFrequency) {
                // case 'daily':
                //   interval = this.model.get('intervalDaily');
                //   break
            case 'weekly':
                recurringInterval = app.models.patientHealthSessionRecurringRulesModel.get('intervalWeekly');
                break;
            case 'monthly':
                recurringInterval = app.models.patientHealthSessionRecurringRulesModel.get('intervalMonthly');
                break;
            default:
                recurringInterval = app.models.patientHealthSessionRecurringRulesModel.get('intervalDaily');
            }

            app.models.patientHealthSessionRecurringRulesModel.set('interval', recurringInterval);

            if (isRecurring) {
                this.model.set('recurrenceRules', [app.models.patientHealthSessionRecurringRulesModel.toJSON()]);
                this.model.set('due', null);
            } else {
                this.model.set('due', startDate + ' ' + sessionTime);
            }

            this.model.save(null, {
                success: function(model, response, options) {
                    app.vent.trigger("updateCurrentMonth");

                    self.$el.find('.js-create-session').button('reset');
                    self.$el.find('.js-cancel-session').removeClass('disabled');

                    self.resetSession();

                    var messageMethod = (self.isEdit) ? 'updated' : 'created';

                    var alert = new BackboneBootstrapAlert({
                            alert: 'success',
                            message: 'Session was successfully ' + messageMethod,
                            autoClose: true
                        })
                        .show();
                },
                error: function(model, xhr, options) {
                    self.$el.find('.js-create-session').button('reset');
                    self.$el.find('.js-cancel-session').removeClass('disabled');

                    var alert = new BackboneBootstrapAlert({
                            alert: 'danger',
                            title: 'Error: ',
                            message: xhr.responseJSON.ErrorMessage
                        })
                        .show();
                }
            });

        },

        createDefaultSession: function() {
            var self = this;

            if (!self.isEdit)
                app.models.patientHealthSessionDefaultModel.reset();

            app.models.patientHealthSessionDefaultModel.set({
                name: this.model.get('name'),
                protocols: this.model.get('protocols').toJSON()
            });

            app.models.patientHealthSessionDefaultModel.save(null, {
                success: function (model, response, options) {
                    app.vent.trigger("updateCurrentMonth");

                    app.models.patientHealthSessionDefaultModel.trigger('updated');

                    self.$el.find('.js-create-session').button('reset');
                    self.$el.find('.js-cancel-session').removeClass('disabled');

                    self.resetSession();

                    var messageMethod = (self.isEdit) ? 'updated' : 'created';

                    var alert = new BackboneBootstrapAlert({
                            alert: 'success',
                            message: 'Default Session was successfully ' + messageMethod,
                            autoClose: true
                        })
                        .show();

                },
                error: function(model, xhr, options) {

                    self.$el.find('.js-create-session').button('reset');
                    self.$el.find('.js-cancel-session').removeClass('disabled');

                    var alert = new BackboneBootstrapAlert({
                            alert: 'danger',
                            title: 'Error: ',
                            message: xhr.responseJSON.ErrorMessage,
                        })
                        .show();
                }
            });
        },

        resetSession: function() {
            // this.$el.find('[href="#tab-session-creation"]').tab('show');

            this.model.reset();

            // this.renderSessionProtocolCollection();

            app.collections.patientProtocolSearchCollection.each(function(model) {
                model.set('isAdded', false);
            });

            app.collections.patientHealthSessionRecurrungWeekCollection.each(function(model) {
                model.set('isSelected', false);
            });

            app.collections.patientHealthSessionRecurrungMonthCollection.each(function(model) {
                model.set('isSelected', false);
            });


            // this.initializeSessionProtocolCollection();

            app.models.patientHealthSessionRecurringRulesModel.reset();

            app.views.patientCalendarView.renderHealthSession();

            app.views.patientCalendarView.calculateLeftPanelHeight();

            // setTimeout(function(){
            //     self.$el.find('.js-create-session').addClass('disabled');
            // },500);
        },

        initializeDatetimepicker: function() {
            var self = this;

            this.$el.find('#startDate-datetimepicker').datetimepicker({
                format: "MM/DD/YYYY",
                minDate: moment().format('YYYY-MM-DD'),
                widgetPositioning: {
                    horizontal: 'right',
                    vertical: 'top'
                }
            }).on('dp.change', function(e) {
                var startDate = $(this).find('#startDateDp').val();
                var value = startDate ? moment(startDate, ["MM/DD/YYYY"]).format("YYYY-MM-DD") : startDate;

                if (e.date) {
                    self.$el.find('#recurring-session-rules #endDate-datetimepicker').data("DateTimePicker").minDate(e.date);
                }

                $(this).siblings('#startDate').val(value).trigger('change');

            }).on('click', function(e) {
                $(this).siblings('.help-block-error').addClass('hidden');
                $(this).closest('.has-error').removeClass('has-error');
            });

            this.$el.find('#startDateDp').inputmask({
                mask: '99/99/9999',
                showMaskOnHover: false,
                clearIncomplete: true
            });

            this.$el.find('#sessionTime-time').datetimepicker({
                format: 'LT',
                widgetPositioning: {
                    horizontal: 'right',
                    vertical: 'top'
                }
            }).on('dp.change', function(e) {
                var time = $(this).find('#sessionTimeTp').val();
                var value = time ? moment(time, ["h:mm A"]).format("HH:mm") : time;

                console.log('on change session time new value: ', value, 'sibbling sessionTime: ', $(this).siblings('#sessionTime'));

                $(this).siblings('#sessionTime').val(value).trigger('change');

            }).on('click', function(e) {
                $(this).siblings('.help-block-error').addClass('hidden');
                $(this).closest('.has-error').removeClass('has-error');
            });

            this.$el.find('#sessionTimeTp').inputmask({
                mask: "h:s t\\m",
                placeholder: "hh:mm xm",
                alias: "datetime",
                hourFormat: "12",
                showMaskOnHover: false,
                clearIncomplete: true
            });
        },

        initializeValidForInput: function() {
            this.$el.find('#expireHours').inputmask(
                'integer',
                {
                    min: 1,
                    max: 24,
                    step: 1,
                    allowPlus: false,
                    allowMinus: false,
                    unmaskAsNumber: true
                }
            );
        },

        updateExpireMinutes: function() {
            var expireHours = this.model.get('expireHours');
            this.model.set('expireMinutes', expireHours * 60);
        },

        toggleRecurringForm: function() {
            if (this.model.get('isRecurring')) {
                this.$el.find('#recurring-session-rules').removeClass('hidden');
            } else {
                this.$el.find('#recurring-session-rules').addClass('hidden');
            }
        },

        toggleDefaultForm: function() {
            if (this.model.get('isDefault')) {
                this.$el.find('#regular-health-session-fields').addClass('hidden');
            } else {
                this.$el.find('#regular-health-session-fields').removeClass('hidden');
            }
        },

        updateSessionTimeInputState: function () {            
            if (this.model.get('timeType') == 'defaultTime') {
                this.$el.find('#sessionTimeTp').prop('disabled', true);
                this.$el.find('#sessionTimeTp').val(moment(app.models.patientModel.get('preferredSessionTime'), ["HH:mm"]).format("h:mm A")).trigger('change');
                //console.log('when update #sessionTimeTp, trigger change');
                this.$el.find('#sessionTime-time').trigger('dp.change');
            } else {
                this.$el.find('#sessionTimeTp').prop('disabled', false);
            }
        },

        renderRecurringSessionRules: function() {
            var reccurringRulesModel = app.models.patientHealthSessionRecurringRulesModel;
            if (reccurringRulesModel) this.stopListening(reccurringRulesModel);

            app.models.patientHealthSessionRecurringRulesModel = new PatientHealthSessionRecurringRulesModel();
            reccurringRulesModel = app.models.patientHealthSessionRecurringRulesModel;

            this.listenTo(reccurringRulesModel, 'change:startDate', this.validateHealthSessionOccurrences);
            this.listenTo(reccurringRulesModel, 'change:endDate', this.validateHealthSessionOccurrences);
            this.listenTo(reccurringRulesModel, 'change:weekDays', this.validateHealthSessionOccurrences);
            this.listenTo(reccurringRulesModel, 'change:monthDays', this.validateHealthSessionOccurrences);
            this.listenTo(reccurringRulesModel, 'change:frequency', this.validateHealthSessionOccurrences);

            var updateReccurringRulesStartTime = function () {
                app.models.patientHealthSessionRecurringRulesModel.set(
                    'startDate',
                    this.model.get('startDate') + ' ' + this.model.get('sessionTime')
                );
            }

            this.listenTo(this.model, 'change:startDate', updateReccurringRulesStartTime.call(this));
            this.listenTo(this.model, 'change:sessionTime', updateReccurringRulesStartTime.call(this));

            app.views.patientHealthSessionRecurringRulesView = new PatientHealthSessionRecurringRulesView({ model: reccurringRulesModel });
            this.$el.find('#recurring-session-rules').html(app.views.patientHealthSessionRecurringRulesView.render().el);
        },

        filterProtocolSearchList: function () {
            var self = this;
            var searchString = self.getSearchKeyword().trim(),
                pattern = new RegExp(searchString, "gi"),
                searchTags = self.getSearchTags(),
                $searchResultContainer = self.$el.find('#patient-protocol-search-result');

            Helpers.renderSpinner($searchResultContainer);

            app.collections.patientProtocolSearchCollection.each(function(model) {
                var isDisplayFilterText = pattern.test(model.get("name"));                
                var isDisplayFilterTags = self.showSuggestedContentLabel
                    ? _.intersection(model.get("tags"), searchTags).length > 0
                    : searchTags.length <= 0 || _.intersection(model.get("tags"), searchTags).length > 0;
                var isDisplay = isDisplayFilterText && isDisplayFilterTags;
                var highlight = isDisplayFilterText ? searchString : false;

                if (searchString === '' && searchTags.length === 0) {
                    isDisplay = true;
                }

                model.set('isDisplay', isDisplay);
                model.set('highlight', highlight);
            }, self);

            self.patientProtocolSearchCollectionViewRender();

            self.$el.find('#search-protocol-elements-tags').trigger('chosen:updated');
        },

        showSuggestedContentLabel: true,

        patientProtocolSearchCollectionViewRender: function() {
            this.$el.find('[href="#tab-session-schedule"]').removeClass('disabled');

            app.collections.patientProtocolSearchCollection.isFetched = true;
            app.collections.patientProtocolSearchCollection.trigger('fetched');
            app.views.patientProtocolSearchCollectionView = new PatientProtocolSearchCollectionView({ collection: app.collections.patientProtocolSearchCollection });
            this.$el.find('#patient-protocol-search-result').html(app.views.patientProtocolSearchCollectionView.render().el);

            if (this.showSuggestedContentLabel) {
                this.showSuggestedContentLabel = false;
                this.$el.find('#patient-protocol-search-result').prepend(
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

        getSearchKeyword: function() {
            return this.$el.find('#search-protocol-elements-keyword').val();
        },

        getSearchTags: function() {
            var searchTags = this.$el.find('#search-protocol-elements-tags').val();
            searchTags = (searchTags && searchTags !== '0') ? searchTags : [];

            searchTags = searchTags.filter(function(element, index) {
                return element !== '';
            });

            if (searchTags.length) {
                this.$el.find('.js-search-protocol-clear').prop('disabled', false);
            } else {
                this.$el.find('.js-search-protocol-clear').prop('disabled', true);
            }

            return searchTags.filter(function(tag) { return tag !== '' });
        },

        searchClear: function() {
            this.$el.find('#search-protocol-elements-tags')
                .val(0)
                .trigger("chosen:updated")
                .change();
        },

        healthSessionIsValid: function() {
            var sessionIsValid = this.model.isValid(true);
            var validationealthSessionOccurrencesMsg = this.validateHealthSessionOccurrences();
            var recurringRulesIsValid = true;

            if (this.model.get('isRecurring') && !this.model.get('isDefault')) {
                recurringRulesIsValid = app.models.patientHealthSessionRecurringRulesModel.isValid(true)
                                        && !validationealthSessionOccurrencesMsg;
            }

            if (sessionIsValid && recurringRulesIsValid)
                return true;

            return false;
        },

        loadPatientConditions: function() {

            var self = this;

            if (app.collections.patientConditionsCollection) {
                if (app.collections.patientConditionsCollection.isFetched) {
                    //self.renderPartientsConditionsTags.call(self);
                } else {
                    app.collections.patientConditionsCollection.bind('sync', function() {
                        //self.renderPartientsConditionsTags.call(self);
                    });
                }
            } else {

                app.collections.patientConditionsCollection = new PatientConditionsCollection();
                app.collections.patientConditionsCollection.isFetched = false;
                app.collections.patientConditionsCollection.fetch({
                    success: function() {

                        //self.renderPartientsConditionsTags.call(self);
                    }
                });
            }
        },

        renderPartientsConditionsTags: function () {

            var self = this;

            self.renderPartientsConditionsTagsInProgress = true;

            var conditionsTags = self.getConditionsTags();

            _.each(conditionsTags, function (tag) {

                var protocolsWithTag = app.collections.patientProtocolSearchCollection.where(function (model) {
                    return _.intersection(model.get('tags'), [tag]).length > 0;
                });

                if (protocolsWithTag.length <= 0) return;

                var tagOption = self.$el.find('#search-protocol-elements-tags').find('option').filter(function (index, option) {                                                
                    return $(option).text() === tag;
                });

                if (tagOption) {
                    tagOption.attr('selected', 'selected');
                } else {
                    tagOption = $('<option>');
                    tagOption.attr('selected', 'selected');
                    tagOption.text(tag);

                    self.$el.find('#search-protocol-elements-tags').append(tagOption);
                }

                self.$el.find('#search-protocol-elements-tags').trigger("chosen:updated");

            });
        },

        initPopover: function () {

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
        },

        getConditionsTags: function () {
            var conditionsTags = [];

            app.collections.patientConditionsCollection.each(function (model) {
                _.each(model.get('tags'), function (tag) { conditionsTags.push(tag) });
            });

            return conditionsTags;
        },

        calculateClearSearchTagsButtonHeight: function () {

            var newHeight = this.$el.find('#search_protocol_elements_tags_chosen').height() - 14;

            if (newHeight > 0) {
                this.$el.find('.js-search-protocol-clear').height(newHeight);
            }

        }

    });
});