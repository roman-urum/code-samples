'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Site/Patients/PatientsApp/AppNamespace',
    'Controllers/Helpers',
    'Controllers/Site/Patients/PatientsApp/Models/PatientCalendarModel'
], function ($, _, Backbone, app, Helpers, PatientCalendarModel) {
    return Backbone.Collection.extend({

        model: PatientCalendarModel

    });
});