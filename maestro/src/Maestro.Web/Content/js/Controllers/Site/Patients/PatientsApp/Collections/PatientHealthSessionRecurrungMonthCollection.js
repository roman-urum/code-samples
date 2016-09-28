'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Site/Patients/PatientsApp/Models/PatientHealthSessionRecurrungMonthModel'
], function ($, _, Backbone, PatientHealthSessionRecurrungMonthModel) {
    return Backbone.Collection.extend({
        model: PatientHealthSessionRecurrungMonthModel,

    });
});