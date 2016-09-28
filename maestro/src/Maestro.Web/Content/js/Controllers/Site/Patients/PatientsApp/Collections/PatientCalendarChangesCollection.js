'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Site/Patients/PatientsApp/AppNamespace',
    'Controllers/Helpers',
    'Controllers/Site/Patients/PatientsApp/Models/PatientCalendarProgramChangeModel',
    'Controllers/Site/Patients/PatientsApp/Models/PatientCalendarItemChangeModel'
], function ($, _, Backbone, app, Helpers, PatientCalendarProgramChangeModel, PatientCalendarItemChangeModel) {
    return Backbone.Collection.extend({
        model: function (attrs, options) {
            return attrs.elementType === 1
                ? new PatientCalendarItemChangeModel(attrs, options)
                : new PatientCalendarProgramChangeModel(attrs, options);
        },

        initialize: function () {
            this.pageSize = 8;
            this.reset();
        },

        url: function () {
            var patientId = app.models.patientModel.get('id');

            return '/' + app.siteId + '/Patients/History?patientId=' + patientId +
                '&skip=' + (this.currentPage - 1) * this.pageSize +
                '&take=' + this.pageSize;
        },

        parse: function (response) {
            this.currentPage++;

            if (response.length < this.pageSize) {
                this.isLastPage = true;
            }

            return Helpers.convertKeysToCamelCase(response);
        },

        reset: function () {
            this.currentPage = 1;
        }
    });
});