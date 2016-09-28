'use strict';

define([
    'underscore',
    'backbone'
], function (_, Backbone) {
    return Backbone.Model.extend({
        defaults: {
            id: null,
            "startDay": 1,
            "endDay": 1,
            "everyDays": 1
        },

        validation: {
            startDay: [
                {
                    required: true,
                    msg: 'Please enter start day'
                },
                {
                    pattern: /^\+?([1-9]\d*)$/,
                    msg: "Please enter positive integer value"
                }
            ],
            endDay: [
                {
                    required: true,
                    msg: 'Please enter end day'
                },
                {
                    pattern: /^\+?([1-9]\d*)$/,
                    msg: "Please enter positive integer value"
                },
                {
                    fn: 'validateEndDay'
                }
            ],
            everyDays: [
                {
                    required: true,
                    msg: 'Please enter the frequency of repetition'
                },
                {
                    pattern: /^\+?([1-9]\d*)$/,
                    msg: "Please enter positive integer value"
                }
            ]
        },

        validateEndDay: function (value, attr, computedState) {
            if (parseInt(value, 10) <= parseInt(this.get('startDay'), 10)) {
                return 'End day number must be after the start day number.';
            }
        },

        getRecurrenceDays: function () {
            var days = [],
                startDay = parseInt(this.get('startDay'), 10),
                endDay = parseInt(this.get('endDay'), 10),
                everyDays = parseInt(this.get('everyDays'), 10);

            for (var i = startDay; i <= endDay; i = i + everyDays) {
                days.push(i);
            }

            return days;
        }
    });
});