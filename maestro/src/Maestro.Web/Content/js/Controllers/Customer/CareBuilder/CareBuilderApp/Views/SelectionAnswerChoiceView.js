'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'BackboneModelBinder',
    'Controllers/Customer/CareBuilder/CareBuilderApp/AppGlobalVariables',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Models/SelectionAnswerChoiceModel',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Views/AudioOptionsView'
], function ($, _, Backbone, BackboneModelBinder, AppGlobalVariables, SelectionAnswerChoiceModel, AudioOptionsView) {
    return Backbone.View.extend({
        model: SelectionAnswerChoiceModel,

        modelBinder: undefined,

        className: 'form-group',

        template: _.template($('#selectionAnswerChoiceTemplate').html()),

        events: {
            'click .js-remove-answer': 'removeAnswer',
            'click .js-make-open-close-ended': 'makeOpenCloseEnded',
            'fileUploaded': 'onFileUploaded',
            'removeAudioFile': 'onRemoveAudioFile'
        },

        initialize: function () {
            this.modelBinder = new BackboneModelBinder();

            Backbone.Validation.bind(this);
        },

        render: function () {
            var bindings = {
                'answerString.value': '[data-name="answerString.value"]',
                'answerString.pronunciation': '[data-name="pronunciation"]'
            };
            
            this.audioOptionsView = new AudioOptionsView({
                model: this.model.get('answerString'),
                index: this.model.get('index')
            });
            this.$el.html(this.template(this.model.attributes));
            this.$el.append(this.audioOptionsView.render().$el);
            this.modelBinder.bind(this.model, this.el, bindings);

            return this;
        },

        close: function () {
            this.audioOptionsView.stopAudio();
            this.modelBinder.unbind();
        },

        removeAnswer: function (e) {
            e.preventDefault();

            AppGlobalVariables.models.selectionAnswerSetModel.get('selectionAnswerChoices').remove(this.model);
            this.remove();

            var selectionAnswerChoices = AppGlobalVariables.models.selectionAnswerSetModel.get('selectionAnswerChoices');

            if (selectionAnswerChoices.length < 3) {
                _.each(selectionAnswerChoices.models, function (model, i) {
                    model.set({
                        'isRemovable': false,
                        'index': i + 1
                    });
                });
            } else {
                _.each(selectionAnswerChoices.models, function (model, i) {
                    model.set({
                        'isRemovable': true,
                        'index': i + 1
                    });
                });
            }
        },

        makeOpenCloseEnded: function (e) {
            e.preventDefault();

            if (this.model.get('isOpenEnded')) {
                this.model.set('isOpenEnded', false);
            } else {
                this.model.set('isOpenEnded', true);
            }
            this.render();
        },

        onFileUploaded: function (event, audioFileModel) {
            this.model.set('answerString.audioFileMedia', audioFileModel);
        },

        onRemoveAudioFile: function () {
            this.model.set('answerString.audioFileMedia', null);
        }
    });
});