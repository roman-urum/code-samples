'use strict';

define([
    'Controllers/Site/Dashboard/DashboardApp/Models/PatientHeaderModel'
], function (PatientHeaderModel) {
    return Backbone.Collection.extend({
        model: PatientHeaderModel
    });
});