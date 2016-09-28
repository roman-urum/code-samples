'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Constants',
    'Controllers/Helpers',
    'backbone-nested'
], function (
    $,
    _,
    Backbone,
    Constants,
    Helpers
) {
    return Backbone.NestedModel.extend({
        url: function () {
            var id = this.get('id');

            if (id == null) {
                return '/CareBuilder/Program?language=en-US';
            }

            return '/CareBuilder/Program/' + this.get('id') + '?language=en-US';
        },

        initialize: function () {
            // Memento initializing
            _.extend(this, new Backbone.Memento(this));
        },

        defaults: {
            id: null,
            name: '',
            tags: []
        },

        parse: function (response, options) {
            response = Helpers.convertKeysToCamelCase(response);

            return response;
        }
    });
});