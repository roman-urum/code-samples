'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Site/Patients/PatientsApp/AppNamespace'
], function ($, _, Backbone, app) {
    return Backbone.Model.extend({
        url: function () {
            return '/' + app.siteId + '/patients/PatientSearchDetails?patientId=' + this.patientId;
        },

        initialize: function (patientId) {
            this.patientId = patientId;

            this.listenTo(this, 'sync', this.sortPrograms);
        },

        sortPrograms: function () {
            if (this.attributes.Programs.length) {
                this.attributes.Programs = _.sortBy(this.attributes.Programs, function (program) {
                    return program.Name;
                });
            }
        }
    });
});