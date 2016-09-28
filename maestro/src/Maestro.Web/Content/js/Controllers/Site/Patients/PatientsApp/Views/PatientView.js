'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Site/Patients/PatientsApp/AppNamespace',
    'Controllers/Helpers',
    'Controllers/Constants',
    'BackboneBootstrapAlert',
    'Controllers/Site/OneWayChat/OneWayChatModule',
    'Controllers/Site/Patients/PatientsApp/Collections/CareManagersCollection',
    'Controllers/Site/Patients/PatientsApp/Collections/CareManagersAssignedCollection',
    'Controllers/Site/Patients/PatientsApp/Collections/PatientConditionsCollection',
    'Controllers/Site/Patients/PatientsApp/Views/PatientCareManagersAssignedCollectionView',
    'Controllers/Site/Patients/PatientsApp/Views/PatientConditionsView',
    './BaseMaestroView',
    './PatientDetails/Dashboard/PatientDashboardView',
    './PatientDetailedDataView',
    './PatientDetails/Trends/PatientTrendsView',
    'Controllers/Site/Patients/PatientsApp/Views/PatientCalendar/PatientCalendarView',
    'Controllers/Site/Notes/NotesApp',
    'BackboneZoomCalls'
], function (
        $,
        _,
        Backbone,
        app,
        Helpers,
        Constants,
        BackboneBootstrapAlert,
        OneWayChat,
        CareManagersCollection,
        CareManagersAssignedCollection,
        PatientConditionsCollection,
        PatientCareManagersAssignedCollectionView,
        PatientConditionsView,
        BaseMaestroView,
        PatientDashboardView,
        PatientDetailedDataView,
        PatientTrendsView,
        PatientCalendarView,
        notes,
        BackboneZoomCalls
    ) {
    return Backbone.View.extend({

        el: '#patients-container',

        template: _.template($('#patientViewTemplate').html()),

        events: {
            'click .js-patient-edit': '_changeState',
            'click .js-view-patient-tab': 'onClickPatientViewTab',
            'click .js-chat': 'openChat',
            'click .js-video-call': 'videoCall',
            'click .js-patient-additional-info-toggle': 'patientAdditionalInfoToggle',
            'click.show-patient-notes button#show-patient-notes': 'showPatientNotes'
        },

        showPatientNotes: function () {
            notes.isVisible() ? notes.hide() : notes.show(this.model.attributes.id);
        },

        initialize: function () {

            _.bindAll(this, "saveCareManagers", "videoCallStarted", "videoCallTimeout");

            app.vent.bind("saveCareManagers", this.saveCareManagers);

        },

        render: function (tabName) {
            var birthDate = this.model.get('birthDate'),
                gender = this.model.get('gender'),
                identifiers = this.model.get('identifiers'),
                patientCategoriesOfCare = this.model.get('categoriesOfCare'),
                categoriesOfCare = Constants.site.categoriesOfCare,
                patientCategoriesOfCareLabels = [];

            _.each(identifiers, function (id) {
                var constant = _.findWhere(Constants.customer.patientIdentifiers, { name: id.name })
                if (_.findWhere(Constants.customer.patientIdentifiers, { name: id.name })) {
                    id.isRequired = constant.isRequired;
                }
            });
            identifiers.sort(Helpers.dynamicSort("name"));
            var idMain = _.find(identifiers, function (id) { return id.isPrimary; });

            _.each(patientCategoriesOfCare, function (categoryId) {
                var category = _.findWhere(categoriesOfCare, { id: categoryId });
                if (category)
                    patientCategoriesOfCareLabels.push(category.Name);
            });

            this.model.set('age', Helpers.getAgeByBirthday(birthDate));
            this.model.set('genderName', Helpers.getGenderName(gender));
            this.model.set('patientCategoriesOfCareLabels', patientCategoriesOfCareLabels);
            this.model.set('identifiers', identifiers);
            this.model.set('idMain', idMain);

            this.$el.html(this.template(this.model.attributes));

            this.getCareManagers();
            this.getPatientConditions();

            $('#' + tabName).tab('show');

            this['show' + tabName]();

            return this;
        },

        getCareManagers: function () {
            var self = this,
                assignedCareManagers = this.model.get('careManagers');

            if (assignedCareManagers.length) {

                Helpers.renderSpinner(this.$el.find('#assigned-care-managers-container'));

                if (app.collections.careManagersCollection) {

                    if (app.collections.careManagersCollection.isFetched)
                        self.renderCareManagers();

                } else {
                    app.collections.careManagersCollection = new CareManagersCollection();
                    app.collections.careManagersCollection.isFetched = false;
                    app.collections.careManagersCollection.fetch({
                        success: function () {

                            app.collections.careManagersCollection.isFetched = true;
                            self.renderCareManagers();

                        }
                    });
                }
            }

        },

        renderCareManagers: function () {
            var assignedCareManagers = this.model.get('careManagers'),
                assignedCareManagersCollection = [];

            app.collections.careManagersCollection.restore();
            app.collections.careManagersCollection.store();

            app.collections.careManagersCollection.each(function (model) {
                if ($.inArray(model.get('userId'), assignedCareManagers) + 1) {
                    assignedCareManagersCollection.push(model);
                }
            });

            app.collections.careManagersCollection.remove(assignedCareManagersCollection);

            app.collections.careManagerAssignedsCollection = new CareManagersAssignedCollection(assignedCareManagersCollection);
            app.views.patientCareManagersAssignedCollectionView = new PatientCareManagersAssignedCollectionView({ collection: app.collections.careManagerAssignedsCollection });
            this.$el.find('#assigned-care-managers-container').html(app.views.patientCareManagersAssignedCollectionView.render().el);

            return this;

        },

        saveCareManagers: function () {
            var self = this,
                managers = [];

            app.collections.careManagerAssignedsCollection.each(function (model) {
                managers.push(model.get('userId'));
            });

            this.model.set('careManagers', managers)

            if (this.model.isValid(true)) {

                this.model.save({ siteId: app.siteId }, {
                    success: function (model, response, options) {

                        self.model.store()
                        app.collections.careManagerAssignedsCollection.store();
                        app.collections.careManagerAssignedsCollection.trigger('change');

                    },
                    error: function (model, xhr, options) {

                        var alert = new BackboneBootstrapAlert({
                            alert: 'danger',
                            title: 'Error: ',
                            message: xhr.responseJSON.ErrorMessage,
                            fixed: true
                        })
                            .show();

                        app.collections.careManagerAssignedsCollection.restore();
                        app.collections.careManagerAssignedsCollection.trigger('change');

                    }
                });
            }

        },

        getPatientConditions: function () {
            var self = this;

            if (app.collections.patientConditionsCollection) {
                self.renderPatientConditions();
            } else {
                Helpers.renderSpinner(this.$el.find('#js-patient-conditions-container'));

                app.collections.patientConditionsCollection = new PatientConditionsCollection();
                app.collections.patientConditionsCollection.isFetched = false;
                app.collections.patientConditionsCollection.fetch({
                    success: function () {

                        app.collections.patientConditionsCollection.isFetched = true;
                        self.renderPatientConditions();
                    }
                });
            }
        },

        renderPatientConditions: function() {
            var $conditionsContainer = this.$el.find('#js-patient-conditions-container'),
                conditionsView = new PatientConditionsView({ collection: app.collections.patientConditionsCollection });
            
            $conditionsContainer.html(conditionsView.render().$el);
        },

        patientAdditionalInfoToggle: function (e) {
            $(e.currentTarget).find('.fa').toggleClass('fa-angle-up fa-angle-down');
            this.$el.find('.b-patient-additional-info-ci').slideToggle(300);
        },

        _changeState: function (e) {
            e.preventDefault();

            app.models.patientModel = app.models.patientModel || this.model;

            if (!e.altKey && !e.ctrlKey && !e.metaKey && !e.shiftKey) {
                var href = $(e.currentTarget).attr('href');

                app.router.navigate(href, {
                    trigger: true
                });
            }
        },

        onClickPatientViewTab: function (e) {
            e.preventDefault();

            if (!e.altKey && !e.ctrlKey && !e.metaKey && !e.shiftKey) {
                var href = $(e.currentTarget).data('href');

                app.router.navigate(href, {
                    trigger: true
                });
            }
        },

        showDashboard: function () {
            if (!(app.views.patientDashboardView && app.views.patientDashboardView.patientId === app.patientId)) {
                app.views.patientDashboardView = new PatientDashboardView({ model: this.model });
            }
            app.views.patientDashboardView.undelegateEvents();
            app.views.patientDashboardView.render();
            app.views.patientDashboardView.delegateEvents();
            this.$el.find('#dashboard').html(app.views.patientDashboardView.$el);
        },

        showDetailedData: function () {
            if (!(app.views.patientDetailedDataView && app.views.patientDetailedDataView.patientId === app.patientId)) {
                app.views.patientDetailedDataView = new PatientDetailedDataView({ model: this.model });
            }
            app.views.patientDetailedDataView.undelegateEvents();
            app.views.patientDetailedDataView.render();
            app.views.patientDetailedDataView.delegateEvents();
            this.$el.find('#detailedData').html(app.views.patientDetailedDataView.$el);
        },

        showTrends: function () {
            if (!(app.views.patientTrendsView && app.views.patientTrendsView.patientId === app.patientId)) {
                app.views.patientTrendsView = new PatientTrendsView({ model: this.model });
            }
            app.views.patientTrendsView.undelegateEvents();
            app.views.patientTrendsView.render();
            app.views.patientTrendsView.delegateEvents();
            this.$el.find('#trends').html(app.views.patientTrendsView.$el);
        },

        showHistory: function () {
            if (!(app.views.patientHistoryView && app.views.patientHistoryView.patientId === app.patientId)) {
                app.views.patientHistoryView = new PatientHistoryView({ model: this.model });
            }
            app.views.patientHistoryView.undelegateEvents();
            app.views.patientHistoryView.render();
            app.views.patientHistoryView.delegateEvents();
            this.$el.find('#history').html(app.views.patientHistoryView.$el);
        },

        onBeforeRemove: function () { },

        showSchedule: function () {

            var PatientCalendarModel = Backbone.Model.extend({});
            var day = new Date();

            app.models.patientCalendarModel = new PatientCalendarModel();

            app.models.patientCalendarModel.set({
                'year': day.getFullYear(),
                'month': day.getMonth()
            });

            app.views.patientCalendarView = new PatientCalendarView({ model: app.models.patientCalendarModel });
            this.$el.find("#schedule").append(app.views.patientCalendarView.render().el);

        },

        getIdMain: function (identifiers) {
            var isRequiredArray = [];

            if (!identifiers.length) {
                return '';
            } else if (identifiers.length == 1) {
                return identifiers[0];
            } else {

                _.each(identifiers, function (id) {
                    if (id.isRequired)
                        isRequiredArray.push(id);
                });

                if (!isRequiredArray.length) {
                    identifiers.sort(Helpers.dynamicSort("name"));
                    return identifiers[0];
                } else if (isRequiredArray.length == 1) {
                    return isRequiredArray[0];
                } else {
                    isRequiredArray.sort(Helpers.dynamicSort("name"));
                    return isRequiredArray[0];
                }
            }

        },

        openChat: function () {
            OneWayChat.showModal({
                siteId: app.siteId,
                patientModel: app.models.patientModel,
                patientId: app.models.patientModel.get('id'),
                patientFirstName: app.models.patientModel.get('firstName'),
                patientLastName: app.models.patientModel.get('lastName')
            });
        },

        videoCall: function (e) {
            this.videoCallBtn = $(e.currentTarget);
            this.videoCallBtn.attr('data-loading-text', 'Calling...').button('loading');

            var zoomCalls = new BackboneZoomCalls({
                siteId: app.siteId,
                patientId: app.patientId,
                started: this.videoCallStarted,
                timeout: this.videoCallTimeout
            });

            zoomCalls.createMeeting();

        },

        videoCallStarted: function () {
            var alertMessage = new BackboneBootstrapAlert({
                alert: 'success',
                message: 'Meeting started successfully',
                autoClose: true
            })
            .show();

            this.videoCallBtn.button('reset');

        },

        videoCallTimeout: function () {
            var alertMessage = new BackboneBootstrapAlert({
                alert: 'danger',
                title: 'Timeout Error: ',
                message: 'Please confirm that Zoom is installed and try again.',
            })
            .show();
            this.videoCallBtn.button('reset');
        }
    });
});