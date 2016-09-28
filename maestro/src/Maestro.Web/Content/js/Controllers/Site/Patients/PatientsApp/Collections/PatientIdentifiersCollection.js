'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Site/Patients/PatientsApp/AppNamespace',
    'Controllers/Site/Patients/PatientsApp/Models/PatientIdentifierModel',
    'Controllers/Helpers'
], function ($, _, Backbone, app, PatientIdentifierModel, Helpers) {
    return Backbone.Collection.extend({

        model: PatientIdentifierModel

    });
});