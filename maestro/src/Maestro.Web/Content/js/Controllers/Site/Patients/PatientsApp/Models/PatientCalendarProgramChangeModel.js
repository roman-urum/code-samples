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

            this.formatDateField('startDate');
            this.formatDateField('terminationDate');
        }
    });
});