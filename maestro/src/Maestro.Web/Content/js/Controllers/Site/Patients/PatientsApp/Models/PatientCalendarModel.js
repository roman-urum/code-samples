'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'backbone-nested',
    'Controllers/Helpers',
    'Controllers/Site/Patients/PatientsApp/AppNamespace'
], function ($, _, Backbone, BackboneNested, Helpers, app) {
    return Backbone.NestedModel.extend({

        defaults: {
            calendarEvents: [],
            year: null,
            month: null
        },

        initialize: function () {

        },

        url: function () {

        },

        parse: function (response, options) {
            return Helpers.convertKeysToCamelCase(response);
        }

    });
});