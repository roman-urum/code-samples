'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Site/Patients/PatientsApp/AppNamespace',
    'Controllers/Site/Patients/PatientsApp/Models/CareManagerModel',
    'Controllers/Helpers'
], function ($, _, Backbone, app, CareManagerModel, Helpers) {
    return Backbone.Collection.extend({

        model: CareManagerModel,

        initialize: function () {

            _.extend(this, new Backbone.Memento(this));

        },

        url: function () {
            return '/' + app.siteId + '/Patients/CareManagers';
        },

        parse: function (response, options) {
            return Helpers.convertKeysToCamelCase(response);
        }

    });
});