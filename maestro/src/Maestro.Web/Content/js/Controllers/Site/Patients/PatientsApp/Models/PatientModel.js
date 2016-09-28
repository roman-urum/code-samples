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
            address1: null,
            address2: null,
            address3: null,
            birthDate: null,
            birthDateParsed: null,
            careManagers: [],
            categoriesOfCare: [],
            city: null,
            effectiveDate: null,
            email: null,
            firstName: null,
            gender: '1',
            identifiers: [],
            language: 'en',
            lastName: null,
            middleInitial: null,
            phoneHome: null,
            phoneWork: null,
            preferredSessionTime: '9:00 AM',
            siteId: app.siteId,
            state: null,
            status: '1',
            timeZone: null,
            zipCode: null
        },

        initialize: function () {
            
            _.extend(this, new Backbone.Memento(this));

            // this.sortIdentifiers();

        },

        // sortIdentifiers: function() {
        //     this.attributes.Identifiers = _.sortBy(this.attributes.Identifiers, function(identifier) {
        //         return [!identifier.IsRequired, identifier.Value];
        //     });
        // },

        url: function () {
            return '/' + app.siteId + '/Patients/Patient?patientId=' + app.patientId;
        },

        sync: function(method, model, options){
            if( method == 'update' || method == 'create' ){
                
                var preferredSessionTime = moment( this.get('preferredSessionTime') , ["h:mm A"]).format("HH:mm");
                this.set('preferredSessionTime', preferredSessionTime );
                options.url = '/' + app.siteId + '/Patients/Patient';
            }
            return Backbone.sync(method, model, options);
        },

        parse: function (response, options) {
            if( typeof response === 'object' ) {

                if(response.BirthDate) {
                    response.BirthDateParsed = moment(response.BirthDate).format('MM/DD/YYYY');
                }

                if( response.PreferredSessionTime ){
                    var hours = response.PreferredSessionTime.Hours,
                        minutes = response.PreferredSessionTime.Minutes,
                        time = moment({
                            minutes: minutes,
                            hours: hours
                        });

                    response.PreferredSessionTime = time.format('HH:mm');
                }
            }

            return Helpers.convertKeysToCamelCase(response);
        },

        validation: {

            firstName: [{
                required: true,
                msg: 'First Name is required'
            }, {
                maxLength: 50,
                msg: 'The field *First Name: must be a string with a maximum length of 50'
            }],

            middleInitial: [{
                required: false
            }, {
                maxLength: 50,
                msg: 'The field Middle Initial: must be a string with a maximum length of 50'
            }],

            lastName: [{
                required: true,
                msg: 'Last Name is required'
            }, {
                maxLength: 50,
                msg: 'The field *Last Name: must be a string with a maximum length of 50'
            }],

            birthDate: function (value) {

                if ( !value ) {
                    return 'Date Of Birth is required';
                }
                if ( value.split('-')[0] < 1800) {
                    return 'Your age exceeds 200 years? Stop lying!';
                }
                if (new Date(value) > new Date()) {
                    return 'Date of birth should not be in the future';
                }
            },

            // id1: [{
            //     required: true,
            //     msg: 'ID 1 is required'
            // }, {
            //     maxLength: 50,
            //     msg: 'The field *ID 1 (Primary): must be a string with a maximum length of 50'
            // }],

            // id2: [{
            //     required: false
            // }, {
            //     maxLength: 50,
            //     msg: 'The field ID 2: must be a string with a maximum length of 50'
            // }],

            // id3: [{
            //     required: false
            // }, {
            //     maxLength: 50,
            //     msg: 'The field ID 3: must be a string with a maximum length of 50'
            // }],
            
            address1: [{
                required: true,
                msg: 'Address 1 is required'
            }, {
                maxLength: 250,
                msg: 'The field *Address 1: must be a string with a maximum length of 250'
            }],

            address2: [{
                required: false
            }, {
                maxLength: 250,
                msg: 'The field Address 2: must be a string with a maximum length of 250'
            }],

            address3: [{
                required: false
            }, {
                maxLength: 250,
                msg: 'The field Address 3: must be a string with a maximum length of 250'
            }],

            city: [{
                required: true,
                msg: 'City is required'
            }, {
                maxLength: 50,
                msg: 'The field *City: must be a string with a maximum length of 50'
            }],

            state: [{
                required: true,
                msg: 'State is required'
            }, {
                maxLength: 100,
                msg: 'The field *State: must be a string with a maximum length of 100'
            }],

            zipCode: [{
                required: true,
                msg: 'Zip is required'
            }, {
                maxLength: 10,
                msg: 'The field *Zip: must be a string with a maximum length of 10'
            }],
            
            phoneHome: [{
                required: true,
                msg: 'Home Phone Number is required'
            }, {
                pattern: /^(\(\d{3}\))\s(\d{3})\-(\d{4})$/,
                msg: 'Home Phone Number doesn\'t match pattern (###) ###-####'
            }],

            phoneWork: [{
                required: false,
                msg: 'Home Phone Number is required'
            }, {
                pattern: /^(\(\d{3}\))\s(\d{3})\-(\d{4})$/,
                msg: 'Home Phone Number doesn\'t match pattern (###) ###-####'
            }],

            email: [{
                required: false
            }, {
                pattern: 'email',
                msg: 'The Patient Email is not a valid'
            }, {
                maxLength: 250,
                msg: 'The field Patient Email: must be a string with a maximum length of 250'
            }],
            
            language: [{
                required: true,
                msg: 'Language is required'
            }],

            timeZone: [{
                required: true,
                msg: 'Time Zone is required'
            },{
                maxLength: 40,
                msg: 'The field Time Zone: must be a string with a maximum length of 40'
            }],

            preferredSessionTime: [{
                required: true,
                msg: 'Preferred Session Time is required'
            }]

        }

    });
});