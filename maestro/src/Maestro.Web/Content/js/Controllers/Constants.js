'use strict';

define(['Controllers/Helpers'], function (Helpers) {
    var constants;
    var constantsStr = $('#constants').val();

    if (constantsStr != undefined && constantsStr !== "") {
        constants = JSON.parse(constantsStr);
    }

    return {
        customer: constants != undefined &&
            constants.customer != undefined ?
            Helpers.convertKeysToCamelCase(constants.customer) :
            null,

        site: constants != undefined &&
            constants.site != undefined ?
            Helpers.convertKeysToCamelCase(constants.site) :
            null,

        daysInWeek: 7,

        deviceStatuses: {
            NOT_ACTIVATED: 0,
            ACTIVATED: 1,
            DECOMISSION_REQUESTED: 2,
            DECOMISSION_ACKNOWLEDGED: 3,
            DECOMISSION_STARTED: 4,
            DECOMISSION_COMPLETED: 5
        },

        deviceTypes: {
            iOS: 'iOS',
            Android: 'Android',
            Other: 'Other',
            IVR: 'IVR'
        },

        monthNames: [
            'January',
            'February',
            'March',
            'April',
            'May',
            'June',
            'July',
            'August',
            'September',
            'October',
            'November',
            'December'
        ],

        dayNames: [
            'Sunday',
            'Monday',
            'Tuesday',
            'Wednesday',
            'Thursday',
            'Friday',
            'Saturday'
        ],

        dayLetterNames: [
            'S',
            'M',
            'T',
            'W',
            'Th',
            'F',
            'Sa'
        ],

        patientStatuses: {
            INACTIVE: 0,
            ACTIVE: 1,
            INTRAINING: 2
        },

        thresholdAppModes: {
            PATIENT: 'Patient',
            DEFAULT: 'Default',
            CONDITION: 'Condition',
            READONLY: 'ReadOnly'
        },

        defaultThresholdTypes: {
            DEFAULT: 1,
            CONDITION: 2
        },

        maxMediaSize: 150, // max allowed size in bytes
        maxMediaNameLength: 100,

        currentCareManager: constants != undefined && constants.currentCareManager != undefined
            ? Helpers.convertKeysToCamelCase(constants.currentCareManager) :
            null
    };
});