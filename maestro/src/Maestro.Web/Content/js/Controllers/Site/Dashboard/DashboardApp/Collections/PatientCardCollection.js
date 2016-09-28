'use strict';

define([
    'Controllers/Site/Dashboard/DashboardApp/Models/PatientModel'
], function (PatientModel) {
    return Backbone.Collection.extend({
        model: PatientModel
    });
});