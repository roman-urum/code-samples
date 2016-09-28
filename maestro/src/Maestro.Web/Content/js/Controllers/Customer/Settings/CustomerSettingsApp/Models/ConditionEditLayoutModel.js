'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    './BaseModel'
], function (
    $,
    _,
    Backbone,
    BaseModel
) {
    return BaseModel.extend({
        defaults: {
            condition: null,
            thresholds: null,
            content: null
        }
    });
});