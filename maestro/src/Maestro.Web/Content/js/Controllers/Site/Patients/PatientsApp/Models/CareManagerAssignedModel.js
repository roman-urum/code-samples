'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Helpers'
], function ($, _, Backbone, Helpers) {
    return Backbone.Model.extend({
        // defaults: {},

        url: function () {

        },

        parse: function (response, options) {
            return Helpers.convertKeysToCamelCase(response);
        }


    });
});