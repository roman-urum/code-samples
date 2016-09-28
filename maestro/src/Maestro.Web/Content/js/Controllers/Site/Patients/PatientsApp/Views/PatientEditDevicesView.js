'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Site/Patients/PatientsApp/AppNamespace',
    'Controllers/Helpers',
    'Controllers/Constants',
    'BackboneBootstrapModal',
    'Controllers/Site/Patients/PatientsApp/Models/PatientModel',
    'Controllers/Site/Patients/PatientsApp/Collections/DevicesCollection',
    'Controllers/Site/Patients/PatientsApp/Views/DevicesCollectionView',
    'Controllers/Site/Patients/PatientsApp/Models/DeviceModel'
], function ($, _, Backbone, app, Helpers, Constants, BackboneBootstrapModal, PatientModel, DevicesCollection, DevicesCollectionView, DeviceModel) {
    return Backbone.View.extend({

        el: '#patients-container',

        initialize: function () {
            this.listenTo(app.collections.devicesCollection, 'fetched', this._renderDevicesCollectionView);
        },

        events: {
            'click .js-new-activation': 'onClickNewActivationButton',
            'click .js-patient-device-exit': 'exitPatientEditDevices',
        },

        render: function () {

            var self = this;

            Helpers.renderSpinner(this.$el.find('#devices-list-container'));

            // if( app.collections.devicesCollection ){
            if (app.collections.devicesCollection.isFetched) {
                this._renderDevicesCollectionView();
            }
            // } else {
            //     app.collections.devicesCollection = new DevicesCollection();
            //     this._fetchDevicesCollection();
            // }
            return this;
        },

        _renderDevicesCollectionView: function () {

            app.views.devicesCollectionView = new DevicesCollectionView({ collection: app.collections.devicesCollection });
            this.$el.find('#devices-list-container').html(app.views.devicesCollectionView.render().el);

        },

        _fetchDevicesCollection: function () {
            var self = this;
            app.collections.devicesCollection.isFetched = false;
            app.collections.devicesCollection.restore();
            app.collections.devicesCollection.fetch({
                success: function () {
                    app.collections.devicesCollection.isFetched = true;
                    app.collections.devicesCollection.store();
                    app.collections.devicesCollection.trigger('fetched');
                    // self._renderDevicesCollectionView();
                }
            });

        },

        _showIVRWarning: function () {
            var warningModal = new BackboneBootstrapModal({
                content: _.template($('#devicesIVRErrorTemplate').html())(),
                okText: 'Ok',
                allowCancel: false,
                modalOptions: {
                    backdrop: 'static',
                    keyboard: false
                }
            });

            warningModal.open();
        },

        onClickNewActivationButton: function (e) {
            var self = this,
                deviceType = $(e.target).data('devicetype');

            if (deviceType == Constants.deviceTypes.IVR
                && app.collections.devicesCollection.where({
                deviceType: Constants.deviceTypes.IVR,
                status: Constants.deviceStatuses.ACTIVATED
            }).length) {

                this._showIVRWarning();

                return;
            }

            var confirmModal = new BackboneBootstrapModal({
                content: _.template($('#devicesConfirmAddTemplate').html())(),
                modalOptions: {
                    backdrop: 'static',
                    keyboard: false
                }
            });

            confirmModal.on('ok', function (e) {

                confirmModal.preventClose();
                confirmModal.$el.find('.btn.ok').attr('data-loading-text', 'Updating...').button('loading');

                var newDevice = new DeviceModel({
                    patientId: app.patientId,
                    deviceType: deviceType
                });

                newDevice.save(newDevice.attributes, {
                    success: function () {

                        Helpers.renderSpinner(self.$el.find('#devices-list-container'));

                        self._fetchDevicesCollection();

                        confirmModal.close();

                    },
                    wait: true
                });

            });

            confirmModal.open();

        },

        exitPatientEditDevices: function (e) {
            e.preventDefault();

            if (!e.altKey && !e.ctrlKey && !e.metaKey && !e.shiftKey) {
                var href = $(e.currentTarget).attr('href');

                app.router.navigate(href, {
                    trigger: true
                });
            }
        }
    });
});