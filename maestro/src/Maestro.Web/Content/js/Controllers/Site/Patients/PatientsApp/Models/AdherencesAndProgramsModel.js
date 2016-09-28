'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Site/Patients/PatientsApp/AppNamespace',
    'Controllers/Helpers',
    'moment'
], function ($, _, Backbone, app, Helpers, moment) {
    return Backbone.Model.extend({

        initialize: function (options) {
            this.year = options.year;
            this.month = options.month;

            if (this.month * 1 > 11) {
                this.nextMonth = 1;
                this.nextYear = this.year + 1;
            } else {
                this.nextMonth = this.month * 1 + 1;
                this.nextYear = this.year;
            }

            this.nextMonth = (this.nextMonth < 10 ? '0' : '') + this.nextMonth;
        },

        url: function (options) {
            var patientId = app.models.patientModel.get('id');

            this.month = (this.month < 10 ? '0' : '') + this.month;
            
            return '/' + app.siteId + '/Patients/AdherencesAndPrograms?patientId=' + patientId +
                    '&scheduledAfter=' + moment.tz(this.year + '-' + this.month + '-01', app.models.patientModel.get('timeZone')).tz('Etc/UTC').format('MM/DD/YYYY HH:mm') +
                    '&scheduledBefore=' + moment.tz(this.nextYear + '-' + this.nextMonth + '-01', app.models.patientModel.get('timeZone')).tz('Etc/UTC').format('MM/DD/YYYY HH:mm');
        },

        parse: function (response, options) {
            return Helpers.convertKeysToCamelCase(response);
        }
    });
});