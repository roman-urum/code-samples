'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'BackboneBootstrapModal',
    'Controllers/Customer/CareBuilder/CareBuilderApp/AppGlobalVariables',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Models/QuestionElementModel',
    'Controllers/Constants',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Models/SelectionAnswerSetModel',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Views/CareElements/QuestionElementSelectionAnswersView',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Models/ScaleAnswerSetModel',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Views/CareElements/QuestionElementScaleAnswersView',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Models/OpenEndedAnswerSetModel',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Views/CareElements/QuestionElementOpenEndedAnswersView',
    'Controllers/Helpers',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Views/AddQuestionView',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Collections/SelectionAnswerChoiceCollection'
], function (
    $,
    _,
    Backbone,
    BackboneBootstrapModal,
    AppGlobalVariables,
    QuestionElementModel,
    Constants,
    SelectionAnswerSetModel,
    QuestionElementSelectionAnswersView,
    ScaleAnswerSetModel,
    QuestionElementScaleAnswersView,
    OpenEndedAnswerSetModel,
    QuestionElementOpenEndedAnswersView,
    Helpers,
    AddQuestionView,
    SelectionAnswerChoiceCollection
) {
    return Backbone.View.extend({
        className: 'panel panel-default',

        model: QuestionElementModel,

        template: _.template($('#questionElementListItemTemplate').html()),

        events: {
            'click .js-collapsed': 'loadAnswerSet',
            'click .js-edit-question-btn': 'onClickEditQuestionBtn'
        },

        render: function () {
            this.$el.html(this.template(this.model.attributes));
            this.$editButton = this.$el.find('.js-edit-question-btn');
            this.$answerSetBox = this.$el.find('.panel-body');

            this.$editButton.addClass("disabled");

            return this;
        },

        // event handlers
        loadAnswerSet: function () {
            var self = this;

            if (self.model.get('answerSet') != undefined) {
                return;
            }

            Helpers.renderSpinner(self.$el.find('.panel-body'));
            this.model.fetch({
                data: {
                    'id': self.model.get('id')
                },
                success: function (response) {
                    if (response == undefined) {
                        console.log('No response when load question details');

                        return;
                    } else if (response.get('answerSet') != undefined && response.get('answerSet').type === 1) {
                        var answerSetModel = new ScaleAnswerSetModel(response.get('answerSet'));
                        var answerSetView = new QuestionElementScaleAnswersView({ model: answerSetModel });

                        self.model.set('answerSet', answerSetModel);
                        self.model.set('answerSetId', answerSetModel.get('id'));
                        self.renderAnswerSetView(answerSetView);
                        $("input.scale").slider({
                            handle: 'custom',
                            tooltip: 'hide'
                        });
                    } else if (response.get('answerSet') != undefined && response.get('answerSet').type === 0) {
                        var answerSetModel = new SelectionAnswerSetModel(response.get('answerSet'));

                        var selectionAnswerChoicesCollection = new SelectionAnswerChoiceCollection();
                        selectionAnswerChoicesCollection.reset(response.get('answerSet').selectionAnswerChoices);

                        answerSetModel.set('selectionAnswerChoices', selectionAnswerChoicesCollection);

                        var answerSetView = new QuestionElementSelectionAnswersView({ model: answerSetModel });

                        self.model.set('answerSet', answerSetModel);
                        self.model.set('answerSetId', answerSetModel.get('id'));
                        self.renderAnswerSetView(answerSetView);
                    } else if (response.get('answerSet') != undefined && response.get('answerSet').type === 2) {

                        var answerSetModel = new OpenEndedAnswerSetModel(response.get('answerSet'));
                        var answerSetView = new QuestionElementOpenEndedAnswersView({ model: answerSetModel });

                        self.model.set('answerSet', answerSetModel);
                        self.model.set('answerSetId', answerSetModel.get('id'));
                        self.renderAnswerSetView(answerSetView);
                    }
                }
            });
        },

        onClickEditQuestionBtn: function (e) {
            var self = this;

            // Saving original model state
            self.model.store();

            var addQuestionView = new AddQuestionView({
                model: this.model
            });

            var modalView = new BackboneBootstrapModal({
                content: addQuestionView,
                title: 'Edit Question',
                modalOptions: {
                    backdrop: 'static',
                    keyboard: false
                }
            });

            addQuestionView
                .on('OnShowAnswerSetDetails', function () {
                    modalView.$('a.btn-primary').removeClass('disabled');
                })
                .on('OnShowAnswerSetsList', function () {
                    modalView.$('a.btn-primary').addClass('disabled');
                });

            modalView
                .open()
                .on("ok", function () {
                    addQuestionView.OnSubmitQuestion(modalView, false);
                })
                .on("shown", function () {
                    $("input.scale").slider({
                        handle: 'custom',
                        tooltip: 'hide'
                    });

                    Helpers.initTags($('.creation-tags'), null);

                })
                .on('cancel', function () {
                    // Restoring original model state
                    self.model.restart();
                });

            addQuestionView.OnSelectAnswerSet();

            return false;
        },

        // loading content
        loadSelectionAnswerSet: function () {
            var self = this;
            var answerSetModel = new SelectionAnswerSetModel({ id: this.model.get('answerSetId') });

            answerSetModel.fetch({
                data: { id: answerSetModel.get('id') },

                success: function () {
                    var answerSetView = new QuestionElementSelectionAnswersView({ model: answerSetModel });

                    self.model.set('answerSet', answerSetModel);
                    self.renderAnswerSetView(answerSetView);
                }
            });
        },

        loadScaleAnswerSet: function () {
            var self = this;
            var answerSetModel = new ScaleAnswerSetModel({ id: this.model.get('answerSetId') });

            answerSetModel.fetch({
                data: { id: answerSetModel.get('id') },
                success: function () {
                    var answerSetView = new QuestionElementScaleAnswersView({ model: answerSetModel });

                    self.model.set('answerSet', answerSetModel);
                    self.renderAnswerSetView(answerSetView);
                    $("input.scale").slider({
                        handle: 'custom',
                        tooltip: 'hide'
                    });
                }
            });
        },

        renderAnswerSetView: function (view) {
            this.$answerSetBox.html(view.render().$el);
            this.$editButton.removeClass('disabled');
        }
    });
});