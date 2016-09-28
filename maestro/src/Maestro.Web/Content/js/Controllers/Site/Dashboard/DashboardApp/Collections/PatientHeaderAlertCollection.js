'use strict';

define([
    'underscore',
    'Controllers/Site/Dashboard/DashboardApp/Models/PatientHeaderAlertModel'
], function (_, PatientHeaderAlertModel) {
    return Backbone.Collection.extend({
        model: PatientHeaderAlertModel
    });
});