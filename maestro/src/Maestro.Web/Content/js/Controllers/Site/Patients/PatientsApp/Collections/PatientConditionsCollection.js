'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Site/Patients/PatientsApp/AppNamespace',
    'Controllers/Customer/Settings/CustomerSettingsApp/Models/ConditionsCollection',
    'Controllers/Helpers'
], function ($, _, Backbone, app, ConditionsCollection, Helpers) {
    return ConditionsCollection.extend({

        url: function () {
            return '/' + app.siteId + '/Patients/PatientConditions';
        },

        sync: function (method, model, options) {
            if (method == 'read') {
                var patientId = app.models.patientModel.get('id');
                options.url = this.url() + '?patientId=' + patientId;
            }

            return Backbone.sync(method, model, options);
        }
    });
});