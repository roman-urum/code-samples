'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'backbone-nested',
    'Controllers/Helpers',
    'Controllers/Site/Patients/PatientsApp/AppNamespace'
], function ($, _, Backbone, BackboneNested, Helpers, app) {
    return Backbone.NestedModel.extend({

        defaults: {
            customerId: null,
            deviceId: null,
            deviceIdType: null,
            deviceModel: null,
            deviceType: null,
            patientId: null,
            settings:  {},
            status: null
        },

        url: function () {
            return '/' + app.siteId + '/Patients/Device/';
        },

        parse: function (response, options) {

            if (response.settings) {
                response.settings.bloodGlucosePeripheral = response.settings.bloodGlucosePeripheral ? response.settings.bloodGlucosePeripheral.toString() : "1";
            }
            
            return Helpers.convertKeysToCamelCase(response);
        },

        initialize: function () {
            _.extend(this, new Backbone.Memento(this));
        },

        validation: {

            'settings.pinCode': function (value) {

                if (this.get('settings.isPinCodeRequired') && (!value || value.length <= 0)) {
                    return "Pin Code cannot be blank";
                }

                if (value && value.length && !value.match(/^[0-9]{4,8}$/g)) {
                    return "Pin Code should be numeric string from 4 to 8 symbols";
                }
            }

        }
    });
});