'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Site/Patients/PatientsApp/AppNamespace',
    'Controllers/Helpers',
    'Controllers/Constants',
    'moment'
], function ( $, _, Backbone, app, Helpers, Constants, moment ) {
    return Backbone.Model.extend({

        defaults: {
            name: null, 
            due: null, 
            eventTz: null,
            expireMinutes: null,
            expireHours: null,
            isNeverExpiring: false,
            startDate: moment().format('YYYY-MM-DD'),
            startDateDp: moment().format('MM/DD/YYYY'),
            // sessionTime: "07:30",
            // sessionTimeTp: "7:30 AM",
            timeType: "defaultTime",
            protocols: [],
            recurrenceRules: [],
            isRecurring: false,
            isDefault: false,
            isEdit: false
        },

        initialize: function () {

        },

        url: function () {

            return '/' + app.siteId + '/Patients/CalendarItems?patientId=' + app.patientId;
        },

        sync: function(method, model, options){

            if( method == 'update' || method == 'delete' ){
                
                options.url = '/' + app.siteId + '/Patients/CalendarItems?patientId=' + app.patientId +
                '&calendarItemId=' + this.get('id');
            }

            // if( this.get('isDefault') && method == 'create' ){
            //     options.url = '/' + app.siteId + '/Patients/DefaultSessions?patientId=' + app.patientId
            // }

            return Backbone.sync(method, model, options);
        },
        
        parse: function (response, options) {
            var data = Helpers.convertKeysToCamelCase(response);

            if (data.expireMinutes === null) {
                data.isNeverExpiring = true;
            }

            return data;
        },

        reset: function () {
            this.clear({silent: true});
            this.set(this.defaults);
        },

        validation: {
            name: [{
                required: true,
                msg: 'Name is required'
            }],

            startDate: [{
                required: true,
                msg: 'Start Date is required'
            }],

            sessionTime: function(value) {
                if (!value) {
                    return 'Session Time is required';
                }

                var startDatetime = this.get('startDate') + ' ' + value;
                var patientTimeZone = app.models.patientModel.get('timeZone');
                var startMoment = moment.tz(startDatetime, patientTimeZone);
                if (startMoment.isBefore(moment().tz(patientTimeZone)) && !this.get('isDefault')) {
                    return 'Session time cannot be in the past, please select a Session time in the future (in patient\'s timezone).';
                }
            },

            expireHours: function(value) {
                if (!this.get('isNeverExpiring') && !this.get('isDefault')) {
                    if (!value) {
                        return 'Expire Hours is required';
                    } else if (value < 1 || value > 10000) {
                        return 'Expire Hours can be only positive integer number';
                    }
                }
            }

        }
    });
});