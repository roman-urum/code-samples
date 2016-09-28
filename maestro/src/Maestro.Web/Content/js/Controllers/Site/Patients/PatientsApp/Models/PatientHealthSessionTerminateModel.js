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
            calendarItemId: null,
            terminationUtc: null
        },

        initialize: function () {

        },

        url: function () {
            var calendarItemId = this.get('calendarItemId');

            return '/' + app.siteId + '/Patients/TerminateCalendarItem?patientId=' + app.patientId +
            		'&calendarItemId=' + calendarItemId;
        },

        parse: function (response, options) {
            return Helpers.convertKeysToCamelCase(response);
        }

    });
});