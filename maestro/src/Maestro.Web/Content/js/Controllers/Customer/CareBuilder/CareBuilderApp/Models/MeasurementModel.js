'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Helpers',
    'backbone-nested'
], function ($, _, Backbone, Helpers) {
    return Backbone.NestedModel.extend({
        defaults: {
            type: '',
            name: ''
        },

        initialize: function () {
            // Memento initializing
            _.extend(this, new Backbone.Memento(this));
        },

        parse: function (response, options) {
            return Helpers.convertKeysToCamelCase(response);
        }
    });
});