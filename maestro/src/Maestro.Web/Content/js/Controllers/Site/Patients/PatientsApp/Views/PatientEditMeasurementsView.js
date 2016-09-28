'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Site/Patients/PatientsApp/AppNamespace',
    'Controllers/Helpers',
    'BackboneBootstrapModal',
    'Controllers/Site/Patients/PatientsApp/Collections/DevicesCollection',
    'Controllers/Site/Patients/PatientsApp/Models/DeviceModel',
    'Controllers/Site/Patients/PatientsApp/Views/MeasurementCollectionView',
    'Controllers/Site/Patients/PatientsApp/Views/DevicesSelectCollectionView',
    'Controllers/Site/Patients/PatientsApp/Views/DevicesSingleView',
    'Controllers/Site/Patients/PatientsApp/Views/DevicesNoView',
    'Controllers/Site/Patients/PatientsApp/Views/MeasurementCollectionEmptyView'
], function (
        $,
        _,
        Backbone,
        app,
        Helpers,
        BackboneBootstrapModal,
        DevicesCollection,
        DeviceModel,
        MeasurementCollectionView,
        DevicesSelectCollectionView,
        DevicesSingleView,
        DevicesNoView,
        MeasurementCollectionEmptyView
) {
    return Backbone.View.extend({

        el: '#patients-container',

        $popover: $(),

        events: {

            'click .js-patient-measurements-save': 'savePatientEditMeasurement',
            'click .js-patient-measurements-cancel': 'cancelPatientEditMeasurement'

        },

        initialize: function () {
            _.bindAll(this, "patientEditMeasurementsChanged");
            app.vent.bind("patientEditMeasurementsChanged", this.patientEditMeasurementsChanged);

            _.bindAll(this, "measurementSavedSuccess");
            app.vent.bind("measurementSavedSuccess", this.measurementSavedSuccess);

            this.listenTo(app.collections.devicesCollection, 'fetched', this.render);

            app.vent.on('hidePopover', this.hideMeasurementPopover.bind(this));
        },

        hideMeasurementPopover: function() {
            this.$popover.popover('hide');
        },

        render: function () {
            var self = this;

            this.container = this.$el.find('#measurements-container');

            Helpers.renderSpinner(this.container);

            if (app.collections.devicesCollection.isFetched) {
                self._renderMeasurementCollectionView();
            }
                

            this.$popover = $('[data-toggle="popover"]').popover({
                template: '  <div class="popover">\
                                <div class="arrow"></div>\
                                <div class="popover-header">\
                                    <button type="button" class="close" data-dismiss="popover" aria-hidden="true">&times;</button>\
                                    <h3 class="popover-title"></h3>\
                                </div>\
                                <div class="popover-content"></div>\
                            </div>',
                html: true,
                container: 'body',
                placement: 'bottom'
            }).on('shown.bs.popover', function (e) {
                $('[data-dismiss="popover"]')
                    .off('click')
                    .on('click', function () {
                        $(e.currentTarget).click();
                    })
                ;

            });

            return this;
        },


        _renderMeasurementCollectionView: function () {

            var suitableDeviceCount = 0,
                suitableDeviceModel;

            app.collections.devicesCollection.restore();
            app.collections.devicesCollection.store();

            app.collections.devicesCollection.each(function (model) {
                if (model.get('status') == 0 || model.get('status') == 1)
                    suitableDeviceCount++;

            });

            app.vent.unbind("saveMeasurementCollection");

            if (suitableDeviceCount) {
                if (app.views.measurementCollectionView)
                    app.views.measurementCollectionView.remove();
                app.views.measurementCollectionView = new MeasurementCollectionView({ collection: app.collections.devicesCollection });
                this.container.html(app.views.measurementCollectionView.render().el);

            } else {
                if (app.views.measurementCollectionEmptyView)
                    app.views.measurementCollectionEmptyView.remove();

                var deviceModel = new DeviceModel();

                app.views.measurementCollectionEmptyView = new MeasurementCollectionEmptyView({model: deviceModel});
                this.container.html(app.views.measurementCollectionEmptyView.render().el);
            }


            if (suitableDeviceCount == 0) {
                app.views.devicesNoView = app.views.devicesNoView || new DevicesNoView();
                this.$el.find('#measurements-title').append(app.views.devicesNoView.render().el);
            } else if (suitableDeviceCount == 1) {

                suitableDeviceModel = app.collections.devicesCollection.find(function (model) {
                    return model.get('status') == 0 || model.get('status') == 1;
                });

                app.views.devicesSingleView = app.views.devicesSingleView || new DevicesSingleView({ model: suitableDeviceModel });
                this.$el.find('#measurements-title').append(app.views.devicesSingleView.render().el);
            } else {
                app.views.devicesSelectCollectionView = app.views.devicesSelectCollectionView || new DevicesSelectCollectionView({ collection: app.collections.devicesCollection });
                this.$el.find('#measurements-title').append(app.views.devicesSelectCollectionView.render().el);
                $('.selectpicker').selectpicker();
            }

            this.isChanged = false;

        },

        savePatientEditMeasurement: function (e) {
            e.preventDefault();
            this.e = e;
            // app.vent.trigger("savePatientThresholds");
        },

        measurementSavedSuccess: function () {
            
            var href = $(this.e.currentTarget).attr('href');
            this.isChanged = false;

            app.router.navigate(href, {
                trigger: true
            });

        },

        cancelPatientEditMeasurement: function (e) {
            e.preventDefault();

            var self = this;

            if (this.isChanged) {

                var modalView = new BackboneBootstrapModal({
                    content: 'You have unsaved changes. If you leave the page, these changes will be lost.',
                    title: 'Confirm Navigation',
                    okText: 'Leave this page',
                    cancelText: 'Stay on this page',
                    modalOptions: {
                        backdrop: 'static',
                        keyboard: false
                    }
                })
                .open()
                .on('ok', function () {
                    self.cancelTriggerRouter(e);
                });

            } else {
                self.cancelTriggerRouter(e);
            }

        },

        cancelTriggerRouter: function (e) {
            e.preventDefault();

            if (!e.altKey && !e.ctrlKey && !e.metaKey && !e.shiftKey) {
                var href = $(e.currentTarget).attr('href');

                app.router.navigate(href, {
                    trigger: true
                });
            }
        },

        patientEditMeasurementsChanged: function () {
            this.isChanged = true;
        }

    });
});