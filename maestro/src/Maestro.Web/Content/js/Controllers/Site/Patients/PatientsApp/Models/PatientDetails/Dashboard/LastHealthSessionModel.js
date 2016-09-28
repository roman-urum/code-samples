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

        url: function () {
            return '/' + app.siteId + '/Patients/LatestInformationDashboard' +
                '?patientId=' + this.patientId;
        },

        initialize: function (attrs, options) {
            if (!options) throw 'Options required';

            this.patientId = options.patientId;
            _.extend(this, new Backbone.Memento(this));
        },

        parse: function (response) {
            response.latestReadings = this.fixBloodPRessureInReadings(response.latestReadings);
            response.latestHealthSessionReadings = this.fixBloodPRessureInReadings(response.latestHealthSessionReadings);
            return response;
        },

        fixBloodPRessureInReadings: function (readings) {
            var diastolicBPReading = _.findWhere(readings, { name: 'Diastolic Blood Pressure' });
            var systolicBPReading = _.findWhere(readings, { name: 'Systolic Blood Pressure' });
            var result = readings;

            if (systolicBPReading && diastolicBPReading) {
                systolicBPReading.value = systolicBPReading.value + '/' + diastolicBPReading.value;
                systolicBPReading.name = 'Blood Pressure';
                systolicBPReading.alert = systolicBPReading.alert || diastolicBPReading.alert;
                result = _.without(readings, diastolicBPReading);
            }

            return result;
        }

    });
});