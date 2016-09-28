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
            name: '',
            rate: 0
        }
    });
});