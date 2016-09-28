'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'backbone-nested'
], function ($, _, Backbone) {
    return Backbone.NestedModel.extend({
        defaults: function () {
            return {
                answerString: {
                    value: '',
                    pronunciation: '',
                    audioFileMedia: null
                },
                isRemovable: true, // for local purposes
                isOpenEnded: false,
                index: 1,
                externalId: '',
                externalScore: null,
                internalId: '',
                internalScore: null
            }
        },

        initialize: function () {
            // Memento initializing
            _.extend(this, new Backbone.Memento(this));
        },

        validation: {
            'answerString.value': [
                {
                    required: true,
                    msg: 'Please enter answer string'
                }, {
                    maxLength: 75,
                    msg: 'Answer string shouldn\'t exceed 75 letters'
                }
            ],
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