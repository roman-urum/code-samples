'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Site/Patients/PatientsApp/AppNamespace',
    'Controllers/Helpers'
], function ($, _, Backbone, app, Helpers) {
    return Backbone.Model.extend({
        defaults: {
            calendarProgramId: null,
            terminationUtc: null
        },

        initialize: function () {

        },

        url: function () {
            var patientId = app.models.patientModel.get('id'),
                calendarProgramId = this.get('calendarProgramId');

            return '/' + app.siteId + '/Patients/TerminateProgram?patientId=' + patientId +
            		'&calendarProgramId=' + calendarProgramId;
        },

        parse: function (response, options) {
            return Helpers.convertKeysToCamelCase(response);
        }

    });
});