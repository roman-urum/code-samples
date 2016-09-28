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
            if (this.get('id') !== undefined) {
                return '/CareBuilder/ScaleAnswerSet?id=' + this.get('id') + '&customerId=' + Constants.customer.id;
            }

            return '/CareBuilder/ScaleAnswerSet/?customerId=' + Constants.customer.id;
        },

        initialize: function () {
            // Memento initializing
            _.extend(this, new Backbone.Memento(this));
        },

        defaults: {
            lowLabel: {
                value: ''
            },
            midLabel: {
                value: ''
            },
            highLabel: {
                value: ''
            },
            lowValue: 0,
            highValue: 0,
            name: '',
            tags: [],
            answerSetType: 'ScaleAnswerSet'
        },

        validation: {
            lowLabel: [
                {
                    required: true,
                    msg: 'Please enter Low Label name',
                }, {
                    maxLength: 50,
                    msg: 'Low Label name shouldn\'t exceed 50 letters'
                }
            ],
            midLabel: [
                { required: false, },
                {
                    maxLength: 50,
                    msg: 'Mid Label name shouldn\'t exceed 50 letters'
                }
            ],
            highLabel: [
                {
                    required: true,
                    msg: 'Please enter High Label name',
                }, {
                    maxLength: 50,
                    msg: 'High Label name shouldn\'t exceed 50 letters'
                }
            ],
            lowValue: function (value, attr) {
                var errMsg,
                    highValue = this.get('highValue');

                if (isNaN(parseInt(value))) {
                    if (value) {
                        errMsg = "Please enter integer value";
                    } else {
                        errMsg = "Please enter Low Value";
                    }
                }
                if (!errMsg) {
                    errMsg = Backbone.Validation.validators.range(value, attr, [0, 100], this);
                }
                if (!errMsg && highValue && value >= highValue * 1) {
                    errMsg = "Low Value should be lower than High Value";
                }
                return errMsg;
            },
            highValue: function (value, attr) {
                var errMsg,
                    lowValue = this.get('lowValue');

                if (isNaN(parseInt(value))) {
                    if (value) {
                        errMsg = "Please enter integer value";
                    } else {
                        errMsg = "Please enter High Value";
                    }
                }
                if (!errMsg) {
                    errMsg = Backbone.Validation.validators.range(value, attr, [1, 100], this);
                }
                if (!errMsg && lowValue && value <= lowValue * 1) {
                    errMsg = "High Value should be higher than Low Value";
                }
                return errMsg;
            },
            name: [{
                required: true,
                msg: 'Please enter AnswerSet name'
            }, {
                maxLength: 50,
                msg: 'AnswerSet name shouldn\'t exceed 50 letters'
            }],
            tags: function (tags) {
                if (!Helpers.isTagsValid(tags)) {
                    return 'Incorrect tag value. Tags can consist of alphanumeric symbols, dot, dash, underscore.';
                }

                if (Helpers.hasDuplicates(tags)) {
                    return globalStrings.DuplicatedTags_ErrorMessage;
                }
            }
        },

        parse: function (response, options) {

            return Helpers.convertKeysToCamelCase(response);
        }
    });
});