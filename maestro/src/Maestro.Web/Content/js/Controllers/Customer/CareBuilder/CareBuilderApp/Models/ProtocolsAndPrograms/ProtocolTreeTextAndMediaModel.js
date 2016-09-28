'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Constants',
    'Controllers/Helpers'
], function (
    $,
    _,
    Backbone,
    Constants,
    Helpers
) {
    return Backbone.Model.extend({
        url: function () {
            return '/CareBuilder/TextMediaElement/' + this.get('id');
        },
        parse: function (response, options) {
            return Helpers.convertKeysToCamelCase(response);
        }
    });
});