'use strict';

define([
    'underscore',
    'Controllers/Site/Dashboard/DashboardApp/Models/PatientDetailAlertModel'
], function (_, PatientDetailAlertModel) {
    return Backbone.Collection.extend({
        model: PatientDetailAlertModel
    });
});