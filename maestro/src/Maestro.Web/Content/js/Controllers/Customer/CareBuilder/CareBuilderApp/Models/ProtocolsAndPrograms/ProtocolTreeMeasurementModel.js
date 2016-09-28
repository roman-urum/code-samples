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
            return '/CareBuilder/Measurement?id=' + this.get('id');
        },
        parse: function (response, options) {
            return Helpers.convertKeysToCamelCase(response);
        }
    });
});