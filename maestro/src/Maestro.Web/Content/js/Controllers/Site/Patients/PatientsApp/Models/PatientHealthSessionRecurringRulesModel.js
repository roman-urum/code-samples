'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Site/Patients/PatientsApp/AppNamespace',
    'Controllers/Helpers',
    'Controllers/Constants',
    'moment'
], function ($, _, Backbone, app, Helpers, Constants, moment) {
    return Backbone.Model.extend({

        defaults: {
            startDate: null,
            endDate: moment().add(2, 'days').format('YYYY-MM-DD'),
            endDateDp: moment().add(2, 'days').format('MM/DD/YYYY'),
            frequency: "daily",
            interval: 1,
            intervalDaily: 4,
            intervalWeekly: 1,
            intervalMonthly: 1,
            weekDays: [],
            monthDays: [],
            count: 0
        },

        initialize: function () {
        },

        parse: function (response, options) {
            return Helpers.convertKeysToCamelCase(response);
        },

        reset: function () {
            this.clear({ silent: true });
            this.set(this.defaults);
        },

        validateOccurrences: function () {
            var startDate = this.get('startDate'),
                endDate = this.get('endDate'),
                weekDays = this.get('weekDays'),
                monthDays = this.get('monthDays'),
                frequency = this.get('frequency'),
                days = [],
                day = moment(startDate).startOf('day'),
                endDay = moment(endDate).startOf('day'),
                errMsg,
                daysArr,
                daysNeeded;

            if (!startDate || !endDate ||
                (!weekDays && !monthDays) ||
                (frequency !== 'weekly' && frequency !== 'monthly')) return errMsg;

            while (!day.isAfter(endDay)) {
                days.push(day.clone());
                day.add(1, 'days');
            }

            if (frequency === 'weekly' && weekDays) {
                daysArr = _.map(days, function (day) { return day.day(); });
                daysNeeded = weekDays;
            } else if (frequency === 'monthly' && monthDays) {
                daysArr = _.map(days, function (day) { return day.date(); });
                daysNeeded = monthDays;
            }

            if (!_.intersection(daysArr, daysNeeded).length) {
                errMsg = 'This session has no occurrences within the date/time selected.';
            }

            return errMsg;
        },

        validation: {
            // interval: [{
            //     required: true,
            //     msg: 'Interval is required'
            // }],

            intervalDaily: function (value) {
                var frequency = this.get('frequency');

                if (frequency == 'daily') {

                    if (!value) {
                        return 'Interval is required';
                    }

                    if (value * 1 < 1) {
                        return 'Interval can be only positive integer number';
                    }

                    if (value * 1 > 999) {
                        return 'Interval can\'t be greater than 999';
                    }
                }
            },

            intervalWeekly: function (value) {
                var frequency = this.get('frequency');

                if (frequency == 'weekly') {

                    if (!value) {
                        return 'Interval is required';
                    }

                    if (value * 1 < 1) {
                        return 'Interval can be only positive integer number';
                    }


                }
            },

            intervalMonthly: function (value) {
                var frequency = this.get('frequency');

                if (frequency == 'monthly') {

                    if (!value) {
                        return 'Interval is required';
                    }

                    if (value * 1 < 1) {
                        return 'Interval can be only positive integer number';
                    }
                }
            },

            weekDays: function (value) {
                var frequency = this.get('frequency');

                if (frequency == 'weekly' && value.length == 0) {
                    return 'Please, select the days';
                }
            },

            monthDays: function (value) {

                var frequency = this.get('frequency');

                if (frequency == 'monthly' && value.length == 0) {
                    return 'Please, select the days';
                }
            },

            endDate: [{
                required: true,
                msg: 'End Date is required'
            }]
        }
    });
});