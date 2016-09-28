'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Site/Patients/PatientsApp/AppNamespace',
    'BackboneModelBinder',
    'BackboneBootstrapModal'
], function ($, _, Backbone, app, BackboneModelBinder, BackboneBootstrapModal) {
    return Backbone.View.extend({

        template: _.template($('#devicesManagePinTemplate').html()),

        el: '#manage-pin-form',

        modelBinder: new BackboneModelBinder(),

        initialize: function () {
            Backbone.Validation.bind(this);
        },

        render: function () {
            var self = this;

            self.managePinModal = new BackboneBootstrapModal({
                title: "Manage Pin",
                content: self.template(self.model.attributes),
                modalOptions: {
                    backdrop: 'static',
                    keyboard: false
                }
            });

            self.listenTo(self.managePinModal, "ok", function () { self.onSave(self.managePinModal, self); });
            self.listenTo(self.managePinModal, "cancel", function () { self.onCancel(self.managePinModal, self); });

            self.managePinModal.open();
            self.el = self.managePinModal.$el.find('#manage-pin-form')[0];
            self.$el = self.managePinModal.$el.find('#manage-pin-form');
            var bindings = {
                'settings.pinCode': '[name="pinCode"]',
                'settings.isPinCodeRequired': '[name="isPinCodeRequired"]'
            };


            self.model.store();
            self.modelBinder.bind(self.model, self.managePinModal.el, bindings);
            self.managePinModal.$el.find('[name="isPinCodeRequired"]').bootstrapSwitch();

            self.managePinModal.$el.find('[name="isPinCodeRequired"]').on('switchChange.bootstrapSwitch', function (event, state) {
                self.isPinCodeRequiredChange(event.currentTarget, state);
            });
        },

        onSave: function (e) {
            e.preventClose();

            var self = this;

            if (self.model.isValid(true)) {


                self.managePinModal.$el.find('.btn.ok').attr('data-loading-text', 'Updating...').button('loading');

                self.model.store();
                self.model.save(null, {
                    success: function () {
                        self.model.set('settings.pinCode', ''); // we dont want to display pin code on UI since it's saved to db
                        app.collections.devicesCollection.store();
                        self.close();
                    }
                });
            }
        },

        onCancel: function (e) {
            e.preventClose();
            this.model.restore();
            this.close();
        },

        isPinCodeRequiredChange: function (e, state) {
            this.model.set('settings.isPinCodeRequired', state);

            this.managePinModal.$el.find('#pinCode').prop('disabled', !state);

        },

        close: function () {
            console.log('close manage pin');
            this.managePinModal.close();
            this.remove();
        }

    });
});

