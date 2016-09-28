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
            protocols: [],
            isDefault: true
        },

        initialize: function () {

        },

        url: function () {

            return '/' + app.siteId + '/Patients/DefaultSessions?patientId=' + app.patientId;
        },

        sync: function(method, model, options){

            if( method == 'update' || method == 'delete' ){
                
                options.url = '/' + app.siteId + '/Patients/DefaultSessions?patientId=' + app.patientId +
                '&defaultSessionId=' + this.get('id');
            }
            return Backbone.sync(method, model, options);
        },


        parse: function (response, options) {

            return Helpers.convertKeysToCamelCase(response);
        },

        reset: function () {
            this.clear({silent: true});
            this.set(this.defaults);
        },

        // validation: {

        //     name: [{
        //         required: true,
        //         msg: 'Name is required'
        //     }],

        //     startDate: [{
        //         required: true,
        //         msg: 'Start Date is required'
        //     }],

        //     sessionTime: [{
        //         required: true,
        //         msg: 'Session Time is required'
        //     }],

        //     expireHours: [{
        //         required: true,
        //         msg: 'Expire Hours is required'
        //         },
        //         {
        //         range: [1, 10000],
        //         msg: 'Expire Hours can be only positive integer number'
        //     }]

        // }


    });
});