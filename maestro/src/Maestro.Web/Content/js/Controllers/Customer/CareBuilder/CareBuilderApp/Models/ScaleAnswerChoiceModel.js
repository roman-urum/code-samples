'use strict';

define([
    'jquery',
    'underscore',
    'backbone'
], function ($, _, Backbone) {
    return Backbone.NestedModel.extend({
        defaults: {
            value: 1,
            externalId: '',
            externalScore: null,
            internalId: '',
            internalScore: null
        },

        validation: {
            externalScore: [
                {
                    required: false
                },
                {
                    pattern: /^\d+$/,
                    msg: "Please enter integer value"
                }
            ],
            internalScore: [
                {
                    required: false
                },
                {
                    pattern: /^\d+$/,
                    msg: "Please enter integer value"
                }
            ]
        }
    });
});