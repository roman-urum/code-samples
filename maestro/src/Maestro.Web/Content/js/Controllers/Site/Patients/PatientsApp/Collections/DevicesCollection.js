'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Site/Patients/PatientsApp/AppNamespace',
    'Controllers/Site/Patients/PatientsApp/Models/DeviceModel',
    'Controllers/Helpers'
], function ($, _, Backbone, app, DeviceModel, Helpers) {
    return Backbone.Collection.extend({

        model: DeviceModel,

        initialize: function () {

            _.extend(this, new Backbone.Memento(this));

        },

        url: function () {
            return '/' + app.siteId + '/Patients/Devices';
        },

        sync: function (method, model, options) {
            if (method == 'read') {
                var patientId = app.models.patientModel.get('id');
                options.url = '/' + app.siteId + '/Patients/Devices?patientId=' + patientId;
            }
            return Backbone.sync(method, model, options);
        },

        parse: function (response, options) {

            _.each(response, function (item) {
                item.DeviceIdTypeLabel = Helpers.getDeviceIdType(item.DeviceIdType);
            });

            return Helpers.convertKeysToCamelCase(response);
        }

    });
});