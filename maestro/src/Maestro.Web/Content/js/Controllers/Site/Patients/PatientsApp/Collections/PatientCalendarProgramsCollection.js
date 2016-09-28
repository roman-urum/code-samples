'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Site/Patients/PatientsApp/AppNamespace',
    'Controllers/Helpers',
    'Controllers/Site/Patients/PatientsApp/Models/PatientCalendarProgramModel'
], function ($, _, Backbone, app, Helpers, PatientCalendarProgramModel) {
    return Backbone.Collection.extend({

        model: PatientCalendarProgramModel

    });
});