'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'backbone-nested',
    'Controllers/Helpers',
    'Controllers/Site/Patients/PatientsApp/AppNamespace',
    'moment'
], function ($, _, Backbone, BackboneNested, Helpers, app, moment) {
    return Backbone.NestedModel.extend({

        DEFAULT_DATERANGE_CASE: '28',
        CUSTOM_DATERANGE_CASE: '0',

        initialize: function (attrs, options) {
            attrs = attrs || {};

            Backbone.NestedModel.prototype.initialize.apply(attrs, options);

            //apply default daterange
            this.setDaterangeCase(this.DEFAULT_DATERANGE_CASE);
        },

        setDaterangeCase: function (caseNumber, startDate, endDate, options) {
            caseNumber = caseNumber || '0';

            var caseStr = caseNumber.toString();
            var end = moment();
            var start;
            switch (caseStr) {
                case '7':
                    start = end.clone().subtract(1, 'weeks');
                    break;
                case '14':
                    start = end.clone().subtract(2, 'weeks');
                    break;
                case '28':
                    start = end.clone().subtract(4, 'weeks');
                    break;
                case '84':
                    start = end.clone().subtract(12, 'weeks');
                    break;
                case '172':
                    start = end.clone().subtract(6, 'months');
                    break;
                case '355':
                    start = end.clone().subtract(12, 'months');
                    break;
                default:
                    if (!startDate || !endDate) throw 'StartDate & EndDate are required';
                    start = moment(startDate);
                    end = moment(endDate);
                    break;
            }

            this.setDaterange(caseNumber, start, end, options);
        },

        setDaterange: function (caseNumber, stardDate, endDate, options) {
            var start = moment(stardDate).startOf('day').add(1, 'second');
            var end = moment(endDate).startOf('day').add(1, 'second');
            this.set({ 'daterangeCase': caseNumber, 'startDate': start, 'endDate': end }, options);
        }
    });
});