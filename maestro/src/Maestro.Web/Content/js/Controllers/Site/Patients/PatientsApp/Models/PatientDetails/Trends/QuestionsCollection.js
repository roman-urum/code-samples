'use strict';

define([
    'jquery',
    'underscore',
    'async',
    'backbone',
    'Controllers/Site/Patients/PatientsApp/AppNamespace',
    'Controllers/Helpers',
    'moment'
], function ($, _, async, Backbone, app, Helpers, moment) {
    return Backbone.Collection.extend({

        requestDateFormat: 'YYYY-MM-DD',

        url: function () {
            //hardcoded 1 month range
            var endDate = moment();
            var startDate = moment().subtract(1, 'month');

            return '/' + app.siteId + '/patients/AssessmentChartQuestions' +
                '?patientId=' + app.patientId +
                '&startDate=' + startDate.format(this.requestDateFormat) +
                '&endDate=' + endDate.format(this.requestDateFormat);
        },

        initialize: function () {
            this.isFetched = false;
        },

        fetch: function (options) {
            options = options || {};

            var self = this;
            var successCb = options.success;
            options.success = function () {
                self.isFetched = true;
                if (_.isFunction(successCb)) {
                    successCb.apply(self, arguments)
                }
            };

            return Backbone.Collection.prototype.fetch.call(this, options);
        },

        parse: function (response) {
            response = Helpers.convertKeysToCamelCase(response);
            return response;
        }

    });
});