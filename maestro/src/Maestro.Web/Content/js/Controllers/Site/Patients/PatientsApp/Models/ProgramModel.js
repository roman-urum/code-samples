'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Site/Patients/PatientsApp/AppNamespace',
    'Controllers/Helpers',
    'Controllers/Constants',
], function ($, _, Backbone, app, Helpers, Constants) {
    return Backbone.Model.extend({

        initialize: function () {
            // Memento initializing
            // _.extend(this, new Backbone.Memento(this));
        },

        url: function () {
            return '/CareBuilder/Program/' + this.get('programId') + '?language=en-US';
        },

        parse: function (response, options) {
            return Helpers.convertKeysToCamelCase(response);
        }

    });
});