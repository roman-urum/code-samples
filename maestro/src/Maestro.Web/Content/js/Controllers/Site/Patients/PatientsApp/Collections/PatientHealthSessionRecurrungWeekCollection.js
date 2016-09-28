'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Site/Patients/PatientsApp/Models/PatientHealthSessionRecurrungWeekModel'
], function ($, _, Backbone, PatientHealthSessionRecurrungWeekModel) {
    return Backbone.Collection.extend({
        model: PatientHealthSessionRecurrungWeekModel,

    });
});