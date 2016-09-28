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
        defaults: {
            name: '',
            type: null,
            value: 'DoesNotApply'
        }
    });
});