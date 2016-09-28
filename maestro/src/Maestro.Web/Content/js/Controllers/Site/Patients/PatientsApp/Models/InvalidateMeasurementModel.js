'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Site/Patients/PatientsApp/AppNamespace'
], function ($, _, Backbone, app) {
    return Backbone.Model.extend({

        defaults: {
            patientId: null,
            measurementId: null
        },

        url: function () {
            return '/' + app.siteId + '/Patients/InvalidateMeasurement';
        }


    });
});