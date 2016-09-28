'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Helpers'
], function ($, _, Backbone, Helpers) {
    return Backbone.Collection.extend({
        model: Backbone.Model.extend({}),

        url: '/CareBuilder/AlertSeverities',

        parse: function (response, options) {
            return Helpers.convertKeysToCamelCase(response);
        },

        comparator: function (alertSeverity) {
            return -alertSeverity.get("severity");
        }
    });
});