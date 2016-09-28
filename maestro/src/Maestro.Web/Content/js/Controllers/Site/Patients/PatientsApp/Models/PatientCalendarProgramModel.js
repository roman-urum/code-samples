'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'moment',
    'Controllers/Site/Patients/PatientsApp/AppNamespace',
    'Controllers/Helpers'
], function ($, _, Backbone, moment, app, Helpers) {
    return Backbone.Model.extend({
        defaults: {
            programId: null,
            startDate: null,
            startDay: '1',
            endDay: null,
            programTime: '08:30',
            expireMinutes: '60',
            validHours: 1,
            timeType: 'defaultTime'
        },

        initialize: function () {
            // this.attributes.programTimeFormatted = this.get('startDate').length > 10 ?
            //     moment(this.attributes.startDate).format('hh:mm A') :
            //     app.models.patientModel.get('preferredSessionTime');
            _.extend(this, new Backbone.Memento(this));
        },

        url: function () {
            var patientId = app.models.patientModel.get('id'),
                calendarProgramId = this.get('id');

            return '/' + app.siteId + '/Patients/Programs?patientId=' + patientId +
                    '&calendarProgramId=' + calendarProgramId;
        },

        sync: function (method, model, options) {
            var patientId = app.models.patientModel.get('id');

            if (method == 'create') {
                options.url = '/' + app.siteId + '/Patients/Programs?patientId=' + patientId;
            }
            return Backbone.sync(method, model, options);
        },

        parse: function (response, options) {
            return Helpers.convertKeysToCamelCase(response);
        },

        validation: {

            startDate: [{
                required: true,
                msg: 'Start Date is required'
            }],

            startDay: function (value) {

                if (value == '0' || value * 1 < 0) {
                    return 'Start Day can be only positive integer number';
                }
                if (!value * 1) {
                    return 'Start Day is required';
                }
                if (value * 1 > this.get('endDay')) {
                    return 'Start Day can be lower than End Day';
                }
            },

            endDay: [{
                required: true,
                msg: 'End Day is required'
            }],

            validHours: [{
                required: true,
                msg: 'Valid Hours is required'
            },
            {
                range: [1, 10000],
                msg: 'Valid Hours can be only positive integer number'
            }]


        },


    });
});