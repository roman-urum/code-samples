'use strict';

define([
    'jquery',
    'underscore'
], function($, _) {
    var ALERT = {
        _$window: $(window),
        NOT_EXISTING_ID: 'NOT_EXISTING_ID',
        NOT_EXISTING_COLOR: '#000',
        TYPES: {
            UNDEFINED: {
                NAME: 'UNDEFINED',
                CODE: -1,
                COLOR: '#aeaeae'
            },
            MEASUREMENT: {
                NAME: 'MEASUREMENT',
                CODE: 2,
                COLOR: '#5a8080'
            },
            BEHAVIOR: {
                NAME: 'BEHAVIOR',
                CODE: 3,
                COLOR: '#5a8080'
            },
            ADHERENCE: {
                NAME: 'ADHERENCE',
                CODE: 1,
                COLOR: '#5a8080'
            }
        },
        GET_NAME_BY_CODE: function(CODE) {
            var TYPES = this.TYPES,
                name = _.find(Object.getOwnPropertyNames(TYPES), function(TYPE) {
                    return TYPES[TYPE].CODE === CODE;
                });

            return TYPES[name] || TYPES.UNDEFINED;
        },
        GET_ALL_TYPES: function() {
            var TYPES = this.TYPES,
                names = _.filter(Object.getOwnPropertyNames(TYPES), function(TYPE) {
                    return TYPES[TYPE].CODE !== TYPES.UNDEFINED.CODE;
                });

            return _.map(names, function(name) {
                return TYPES[name];
            });
        },
        DEFAULT_HEIGHT: 165,
        DEFAULT_COLOR: '#808080',
        DATE_TYPE: 'MM/DD/YYYY',
        CURRENT_COUNT: function() {
            return Math.round(ALERT._$window.height() / ALERT.DEFAULT_HEIGHT) || 1;
        },
        DEFAULT_COUNT: function() {
            return Math.round(ALERT._$window.height() / ALERT.DEFAULT_HEIGHT) || 1;
        },
        ACTIONS: {
            ACKNOWLEDGE: 'ACKNOWLEDGE',
            ACKNOWLEDGEAll: 'ACKNOWLEDGEAll',
            IGNORE: 'IGNORE'
        }
    };

    var ADHERENCE = {
        STATUSES: {
            UNDEFINED: {
                NAME: 'UNDEFINED',
                CODE: -1
            },
            SCHEDULED: {
                NAME: 'Scheduled',
                CODE: 1
            },
            STARTED: {
                NAME: 'Started',
                CODE: 2
            },
            PARTIALLYCOMPLETED: {
                NAME: 'Partially Completed',
                CODE: 3
            },
            COMPLETED: {
                NAME: 'Completed',
                CODE: 4
            },
            MISSED: {
                NAME: 'Missed',
                CODE: 5
            }
        },
        GET_STATUS_BY_CODE: function(CODE) {
            var STATUSES = this.STATUSES,
                name = _.find(Object.getOwnPropertyNames(STATUSES), function(STATUS) {
                    return STATUSES[STATUS].CODE === CODE;
                });

            return STATUSES[name] || STATUSES.UNDEFINED;
        },
        GET_ALL_STATUSES: function() {
            var STATUSES = this.STATUSES,
                names = _.filter(Object.getOwnPropertyNames(STATUSES), function(STATUS) {
                    return STATUSES[STATUS].CODE !== STATUSES.UNDEFINED.CODE;
                });

            return _.map(names, function(name) {
                return STATUSES[name];
            });
        }
    };

    var URL = {
        PATIENTS_CARDS: 'dashboard/PatientsCards',
        PATIENT_DETAILS: 'dashboard/PatientCards',
        ACKNOWLEDGE_ALERT: 'dashboard/AcknowledgeAlerts',
        IGNORE_ALERT: 'dashboard/IgnoreReading',
        SITE: window.location.pathname.split('/')[1],
        LINK: window.location.protocol + '//' + window.location.host + '/' + window.location.pathname.split('/')[1]
    };

    var RECENT_READINGS = {
        DATE: 'MM/DD',
        TIME: 'hh:mm A',
        FULL_DATE: 'MM/DD/YYYY hh:mm A',
        VISIBLE_COUNT: 6
    };

    var TEMPLATE = {
        _memory: {},
        renderer: function(name) {
            TEMPLATE._memory[name] = TEMPLATE._memory[name] || _.template($('#' + name).html());

            return TEMPLATE._memory[name];
        }
    };

    return {
        URL: URL,
        ALERT: ALERT,
        ADHERENCE: ADHERENCE,
        TEMPLATE: TEMPLATE.renderer,
        RECENT_READINGS: RECENT_READINGS
    }
});