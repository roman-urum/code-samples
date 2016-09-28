'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Collections/SelectionAnswerChoiceCollection',
    'Controllers/Constants',
    'Controllers/Helpers'
], function (
    $,
    _,
    Backbone,
    SelectionAnswerChoiceCollection,
    Constants,
    Helpers
) {
    return Backbone.Model.extend({
        url: function () {
            if (this.id !== undefined) {
                return '/CareBuilder/SelectionAnswerSet?id=' + this.get('id') + '&customerId=' + Constants.customer.id;
            }

            return '/CareBuilder/SelectionAnswerSet/?customerId=' + Constants.customer.id;
        },

        initialize: function () {
            // Remember that in JavaScript, objects are passed by reference,
            // so if you include an object as a default value, it will be shared
            // among all instances. Instead, define defaults as a function.

            if (this.get('selectionAnswerChoices') === undefined) {
                this.set('selectionAnswerChoices', new SelectionAnswerChoiceCollection());
            }

            // Memento initializing
            _.extend(this, new Backbone.Memento(this));
        },

        defaults: {
            name: '',
            isMultipleChoice: false,
            answerSetType: 'SelectionAnswerSet',
            tags: []
        },

        validation: {
            name: [{
                required: true,
                msg: 'Please enter AnswerSet name'
            }, {
                maxLength: 50,
                msg: 'AnswerSet name shouldn\'t exceed 50 letters'
            }],
            selectionAnswerChoices: function (value, attr, computedState) {
                var errors = value.map(function (entry) {
                    return entry.isValid(true);
                });

                if (value.length < 2) {
                    return 'AnswerSet should contain at least two answers';
                }

                if (_.indexOf(errors, false) !== -1) {
                    return 'Invalid collection of ' + attr;
                }
            },
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
            if (options.skipParseOnSuccess) {
                return this.attributes;
            }

            response = Helpers.convertKeysToCamelCase(response);

            response.selectionAnswerChoices = new SelectionAnswerChoiceCollection(response.selectionAnswerChoices);

            return response;
        }
    });
});