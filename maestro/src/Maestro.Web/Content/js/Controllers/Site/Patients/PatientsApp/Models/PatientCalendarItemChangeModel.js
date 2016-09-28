'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'backbone-nested',
    'Controllers/Helpers',
    'Controllers/Site/Patients/PatientsApp/AppNamespace',
    'Controllers/Site/Patients/PatientsApp/Models/PatientCalendarChangeModel'
], function ($, _, Backbone, BackboneNested, Helpers, app, PatientCalendarChangeModel) {
    return PatientCalendarChangeModel.extend({

        defaults: {

        },

        initialize: function () {
            PatientCalendarChangeModel.prototype.initialize.apply(this, arguments);

            this.formatDateField('prevDueDate');
            this.formatDateField('dueDate');
            this.formatDateField('recurrenceEndDate');
            this.formatDateField('recurrenceStartDate');
            this.formatDateField('prevRecurrenceEndDate');
            this.formatDateField('prevRecurrenceStartDate');
        }
    });
});