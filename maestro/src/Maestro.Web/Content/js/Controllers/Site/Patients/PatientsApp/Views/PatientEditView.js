'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'BackboneBootstrapAlert',
    'Controllers/Site/Patients/PatientsApp/AppNamespace',
    'Controllers/Helpers',
    'Controllers/Constants',
    'Controllers/Site/Patients/PatientsApp/Views/PatientEditInfoView',
    'Controllers/Site/Patients/PatientsApp/Views/PatientEditCareManagersView',
    'Controllers/Site/Patients/PatientsApp/Views/PatientEditConditionsView',
    'Controllers/Site/Patients/PatientsApp/Views/PatientEditDevicesView',
    'Controllers/Site/Patients/PatientsApp/Views/PatientEditMeasurementsView',
    'Controllers/Site/Patients/PatientsApp/Collections/CareManagersCollection',
    'Controllers/Customer/Settings/CustomerSettingsApp/Models/ConditionsCollection',
    'Controllers/Site/Patients/PatientsApp/Collections/ConditionsCollection',
    'Controllers/Site/Patients/PatientsApp/Collections/DevicesCollection',
    'Controllers/Root/Thresholds/ThresholdsApp'
], function (
        $,
        _,
        Backbone,
        BackboneBootstrapAlert,
        app,
        Helpers,
        Constants,
        PatientEditInfoView,
        PatientEditCareManagersView,
        PatientEditConditionsView,
        PatientEditDevicesView,
        PatientEditMeasurementsView,
        CareManagersCollection,
        CustomerConditionsCollection,
        ConditionsCollection,
        DevicesCollection,
        ThresholdsApp
    ) {
    return Backbone.View.extend({

        el: '#patients-container',

        template: _.template($('#patientEditTemplate').html()),

        events: {
            'click .js-edit-patient-tab': 'onClickPatientTab'
        },

        initialize: function () {

        },

        render: function (tabName) {

            var self = this;

            this.model.restore();
            this.model.store();

            self.renderTabs(tabName);

            return this;
        },

        renderTabs: function (tabName) {

            if (tabName) {

                var gender = this.model.get('gender').toString(),
                    status = this.model.get('status').toString(),
                    identifiers = this.model.get('identifiers');

                _.each(identifiers, function (id) {
                    var constant = _.findWhere(Constants.customer.patientIdentifiers, { name: id.name })
                    if (_.findWhere(Constants.customer.patientIdentifiers, { name: id.name })) {
                        id.isRequired = constant.isRequired;
                    }
                });

                identifiers.sort(Helpers.dynamicSort("name"));
                var idMain = _.find(identifiers, function (id) { return id.isPrimary; });

                this.model.set({
                    'gender': gender,
                    'status': status,
                    'identifiers': identifiers,
                    'idMain': idMain
                });

                this.$el.html(this.template(this.model.attributes));

                $('#' + tabName).tab('show');

                if (!app.collections.devicesCollection) {
                    app.collections.devicesCollection = new DevicesCollection();
                    app.collections.devicesCollection.isFetched = false;
                    app.collections.devicesCollection.fetch({
                        success: function () {
                            app.collections.devicesCollection.isFetched = true;
                            app.collections.devicesCollection.trigger('fetched');
                        }
                    });
                }

                if (!app.collections.careManagersCollection) {
                    app.collections.careManagersCollection = new CareManagersCollection();
                    app.collections.careManagersCollection.isFetched = false;
                    app.collections.careManagersCollection.fetch({
                        success: function () {

                            app.collections.careManagersCollection.isFetched = true;
                            app.collections.careManagersCollection.trigger('fetched');

                        }
                    });
                }

                this.initConditionsData();

                switch (tabName) {
                    case 'PatientInfo':{

                        if (app.views.patientEditInfoView)
                            app.views.patientEditInfoView.undelegateEvents();
                        app.views.patientEditInfoView = new PatientEditInfoView({ model: app.models.patientModel });
                        app.views.patientEditInfoView.render();

                        break;
                    }
                    case 'Conditions':{

                        app.views.patientEditConditionsView = app.views.patientEditConditionsView || new PatientEditConditionsView({ model: app.models.patientModel });
                        app.views.patientEditConditionsView.render();

                        break;
                    }
                    case 'CareManagers':{

                        if (app.views.patientEditCareManagersView)
                            app.views.patientEditCareManagersView.undelegateEvents();
                        app.views.patientEditCareManagersView = new PatientEditCareManagersView({ model: app.models.patientModel });
                        app.views.patientEditCareManagersView.render();

                        break;
                    }
                    case 'Devices':{

                        app.views.patientEditDevicesView = app.views.patientEditDevicesView || new PatientEditDevicesView();
                        app.views.patientEditDevicesView.render();

                        break;
                    }
                    case 'Measurements':{

                        app.views.patientEditMeasurementsView = app.views.patientEditMeasurementsView || new PatientEditMeasurementsView();
                        app.views.patientEditMeasurementsView.render();

                        var href = '',
                            config = {

                                siteId: app.siteId,

                                patientId: app.patientId,

                                vent: app.vent,

                                crud: {
                                    get: '/Patients/Thresholds',
                                    save: '/Patients/Threshold'
                                },

                                allowUnusedButton: true,

                                initCollections: {
                                    //thresholdsCollection: app.collections.patientThresholdsCollection,
                                    devicesCollection: app.collections.devicesCollection
                                },

                                events: {

                                    onModelChanged: function() {
                                        app.vent.trigger('patientEditMeasurementsChanged');
                                    },
                                    beforeSave: function() {
                                        $saveThresholdSaveButton.data('loading-text', 'Updating...').button('loading');
                                    },
                                    afterSave: function() {
                                        app.vent.trigger('saveMeasurementCollection');
                                        app.vent.trigger('hidePopover');

                                        app.vent.on('measurementSavedSuccess', function() {
                                            $saveThresholdSaveButton.button('reset');

                                            if(href) {
                                                app.vent.trigger('hidePopover');
                                                app.router.navigate(href, {
                                                    trigger: true
                                                });
                                            }
                                        });


                                    },
                                    afterLoad: function() {
                                        $saveThresholdSaveButton.on('click', function(e) {
                                            e.preventDefault();

                                            href = $(this).attr('href');
                                            thresholdsApp.save();
                                        });

                                        app.collections.patientThresholdsCollection = thresholdsApp.getCollection('thresholdsCollection');
                                        $thresholdsContainer.html(thresholdsApp.el);
                                    }
                                }

                            };

                        var $thresholdsContainer = this.$el.find('#thresholds-container'),
                            $saveThresholdSaveButton = this.$el.find('a.js-patient-measurements-save');

                        //if(!app.collections.patientThresholdsCollection) {
                            Helpers.renderSpinner($thresholdsContainer);
                        //}

                        var thresholdsApp = new ThresholdsApp(config);

                        break;
                    }
                }

            } else {
                app.vent.trigger('hidePopover');
                app.router.navigate('EditPatient/' + app.patientId + '/PatientInfo/', {
                    trigger: true
                });
            }

        },

        initConditionsData: function () {
            if (app.collections.customerConditionsCollection &&
                app.collections.assignedConditionsCollection &&
                app.collections.assignedConditionsCollection.patientId === this.model.get('id')) return;

            var customerConditions = new CustomerConditionsCollection();
            customerConditions.isFetched = false;
            app.collections.customerConditionsCollection = customerConditions;

            var assignedConditions = new ConditionsCollection({patientId: this.model.get('id')});
            assignedConditions.isFetched = false;
            app.collections.assignedConditionsCollection = assignedConditions;

            var availableConditions = new ConditionsCollection();
            availableConditions.isFetched = false;
            app.collections.availableConditionsCollection = availableConditions;

            customerConditions.fetch({
                success: function () {
                    customerConditions.isFetched = true;
                    customerConditions.trigger('fetched');
                    populateAvailableConditions();
                }
            });

            assignedConditions.fetch({
                success: function () {
                    assignedConditions.isFetched = true;
                    assignedConditions.trigger('fetched');
                    populateAvailableConditions();
                }
            });

            function populateAvailableConditions() {
                if (customerConditions.isFetched === true &&
                    assignedConditions.isFetched === true) {

                    var assignedIds = assignedConditions.pluck('id');
                    var differArr = customerConditions.reject(function (model) {
                        return _.contains(assignedIds, model.get('id'));
                    });
                    availableConditions.set(differArr);
                    availableConditions.isFetched = true;
                    availableConditions.trigger('fetched');

                }
            }
        },

        onClickPatientTab: function (e) {
            e.preventDefault();

            if (!e.altKey && !e.ctrlKey && !e.metaKey && !e.shiftKey) {
                var href = $(e.currentTarget).data('href');
                app.vent.trigger('hidePopover');
                app.router.navigate(href, {
                    trigger: true
                });
            }
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
        }
    });
});