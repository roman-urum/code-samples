'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Site/Patients/PatientsApp/Models/PatientCareElementsSearchModel'
], function ($, _, Backbone, PatientCareElementsSearchModel) {
    return Backbone.Collection.extend({
        
        model: PatientCareElementsSearchModel,

        url: function () {
            return '/CareBuilder/Search';
        }
    });
});