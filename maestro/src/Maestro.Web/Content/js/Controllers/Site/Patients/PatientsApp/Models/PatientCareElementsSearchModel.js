'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Helpers'
], function (
    $,
    _,
    Backbone,
    Helpers
) {
    return Backbone.Model.extend({
        initialize: function () {
            // Memento initializing
            _.extend(this, new Backbone.Memento(this));
        },

        defaults: {
            id: '',
            name: '',
            type: '',
            tags: [],
            isDisplay: true,
            isAdded: false
        },

        parse: function (response, options) {
            return Helpers.convertKeysToCamelCase(response);
        }
    });
});