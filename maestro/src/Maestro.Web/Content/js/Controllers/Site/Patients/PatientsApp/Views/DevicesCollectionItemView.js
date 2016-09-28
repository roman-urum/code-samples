'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Site/Patients/PatientsApp/AppNamespace',
    'Controllers/Helpers',
    'Controllers/Constants',
    'BackboneBootstrapModal',
    'Controllers/Site/Patients/PatientsApp/Views/DeviceManagePinView'
], function ($, _, Backbone, app, Helpers, Constants, BackboneBootstrapModal, DeviceManagePinView) {
    return Backbone.View.extend({

        tagName: 'tr',

        template: _.template($('#devicesListItemTemplate').html()),
        templateRemoveModal: _.template($('#devicesConfirmRemoveTemplate').html()),
        templateDecomissionModal: _.template($('#devicesConfirmDecomissionTemplate').html()),

        events: {
            'click #remove-device-btn': 'onClickRemoveDeviceButton',
            'click #request-decomission-btn': 'onClickDecomissionDeviceButton',
            'click #manage-pin-btn': 'onManagePinButton'
        },

        render: function () {

            _.extend(this.model.attributes, {
                deviceStatusesStrings: Helpers.getDeviceStatuses(),
                deviceStatuses: Constants.deviceStatuses
            });

            this.$el.html(this.template(this.model.attributes));
            return this;
        },

        initialize: function () {

        },

        onClickRemoveDeviceButton: function (e) {
            e.preventDefault();
            var self = this;

            var confirmModal = new BackboneBootstrapModal({
                content: self.templateRemoveModal(),
                modalOptions: {
                    backdrop: 'static',
                    keyboard: false
                }
            });

            confirmModal.on('ok', function () {
                confirmModal.preventClose();

                confirmModal.$el.find('.btn.ok').attr('data-loading-text', 'Updating...').button('loading');

                self.model.destroy({
                    data: { deviceId: self.model.get('id') },
                    processData: true,
                    wait: true,
                    success: function () {
                        app.collections.devicesCollection.store();
                        confirmModal.close();
                    }
                });

            });

            confirmModal.open();
        },

        onClickDecomissionDeviceButton: function (e) {
            e.preventDefault();

            var self = this,
                originalDeviceId = this.model.get('deviceId');

            var confirmModal = new BackboneBootstrapModal({
                content: self.templateDecomissionModal(),
                modalOptions: {
                    backdrop: 'static',
                    keyboard: false
                }
            });

            confirmModal.on('ok', function () {
                confirmModal.preventClose();
                confirmModal.$el.find('.btn.ok').attr('data-loading-text', 'decommisioning...').button('loading');

                self.model.save({ deviceId: self.model.get('id') }, {
                    processData: true,
                    patch: true,
                    wait: true,
                    success: function () {
                        var newStatus = self.model.get('deviceType') === Constants.deviceTypes.IVR ?
                            Constants.deviceStatuses.DECOMISSION_COMPLETED :
                            Constants.deviceStatuses.DECOMISSION_REQUESTED;

                        self.model.set('status', newStatus);
                        self.model.set('deviceId', originalDeviceId);

                        app.collections.devicesCollection.store();
                        confirmModal.close();
                    }
                });

            });

            confirmModal.open();
        },


        onManagePinButton: function (e) {
            e.preventDefault();

            app.views.DeviceManagePinView = new DeviceManagePinView({ model: this.model });
            app.views.DeviceManagePinView.render();
        }
    });

});