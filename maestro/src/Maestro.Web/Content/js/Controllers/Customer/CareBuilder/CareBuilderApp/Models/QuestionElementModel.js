'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Helpers',
    'backbone-nested'
], function ($, _, Backbone, Helpers) {
    return Backbone.NestedModel.extend({
        defaults: function() {
            return {
                id: null,
                questionElementString: {
                    value: "",
                    pronunciation: "",
                    audioFileMedia: null
                },
                answerSetId: "",
                externalId: "",
                internalId: "",
                tags: []
            }
        },

        initialize: function () {
            // Memento initializing
            _.extend(this, new Backbone.Memento(this));
        },

        url: function () {
            return '/CareBuilder/QuestionElement';
        },

        toJSON: function () {
            var json = Backbone.Model.prototype.toJSON.call(this);

            var answers = this.getAnswerChoices();

            if (answers == undefined) {
                return json;
            }

            json.answerChoiceIds = answers;

            return json;
        },

        parse: function (response, options) {
            return Helpers.convertKeysToCamelCase(response);
        },

        validation: {
            'questionElementString.value': function () {
                if (!this.get('questionElementString') || !this.get('questionElementString').value)
                    return 'Please enter Question Text';

                if (this.get('questionElementString').value.length > 500) {
                    return 'Question text shouldn\'t exceed 500 letters';
                }
            },
            'tags': function (tags) {
                if (!Helpers.isTagsValid(tags)) {
                    return 'Incorrect tag value. Tags can consist of alphanumeric symbols, dot, dash, underscore.';
                }
                
                if (Helpers.hasDuplicates(tags)) {
                    return globalStrings.DuplicatedTags_ErrorMessage;
                }
            }
        },

        // Validates question and answers.
        isModelValid: function () {
            var result = true;

            if (!this.isValid(true)) {
                result = false;
            }

            var answers = this.getAnswerChoices();

            if (answers == undefined) {
                return result;
            }

            answers.each(function (item) {
                if (!item.isValid(true)) {
                    result = false;
                }
            });

            return result;
        },

        // Returns list of answers from assigned answerset.
        getAnswerChoices: function () {
            var answerSet = this.get('answerSet'),
                answerSetType;

            if (answerSet == undefined) {
                return undefined;
            }

            answerSetType = answerSet.get('answerSetType');

            if (answerSetType === 'SelectionAnswerSet') {
                return answerSet.get('selectionAnswerChoices');
            } else {
                return answerSet.get('scaleAnswerChoices');
            }
        }
    });
});