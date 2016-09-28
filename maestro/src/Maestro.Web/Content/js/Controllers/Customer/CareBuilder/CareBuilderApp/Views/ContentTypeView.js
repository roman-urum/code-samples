'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'BackboneBootstrapModal',
    'Controllers/Customer/CareBuilder/CareBuilderApp/AppGlobalVariables',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Views/AnswerSetTypeView',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Views/AddQuestionView',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Models/QuestionElementModel',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Views/TextMediaElementView',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Models/TextMediaElementModel',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Models/MediaElementModel',
    'Controllers/Helpers'
], function ($, _,
    Backbone,
    BackboneBootstrapModal,
    app,
    AnswerSetTypeView,
    AddQuestionView,
    QuestionElementModel,
    TextMediaElementView,
    TextMediaElementModel,
    MediaElementModel,
    Helpers
) {
    return Backbone.View.extend({
        template: _.template($('#contentTypeTemplate').html()),

        render: function () {
            this.$el.html(this.template());

            return this;
        },

        events: {
            'click .js-add-question': 'addQuestion',
            'click .js-add-answer-set': 'addAnswerSet',
            'click .js-add-text-and-media': 'addTextAndMedia',
            'click .js-add-protocols-and-programs': 'addProtocolsAndPrograms'
        },

        addQuestion: function (e) {
            e.preventDefault();

            app.views.contentTypeModalView.once('hidden', function () {
                app.views.addQuestionView = new AddQuestionView({
                    model: new QuestionElementModel()
                });

                var modalView = new BackboneBootstrapModal({
                    content: app.views.addQuestionView,
                    title: 'Add Question',
                    modalOptions: {
                        backdrop: 'static',
                        keyboard: false
                    }
                });

                app.views.addQuestionView.on('OnShowAnswerSetDetails', function () {
                    modalView.$('.modal-footer a.btn-primary').removeClass('disabled');
                });

                app.views.addQuestionView.on('OnShowAnswerSetsList', function () {
                    modalView.$('.modal-footer a.btn-primary').addClass('disabled');
                });

                modalView.open();

                modalView.on("ok", function () {
                    app.views.addQuestionView.OnSubmitQuestion(modalView, true);
                });

                modalView.on("shown", function () {

                    Helpers.initTags($('.creation-tags'), null);
                    Helpers.initTags(null, $('.searching-tags'));

                    $('#answer-type').trigger('change');
                });

                app.views.addQuestionModalView = modalView;

            });

            app.views.contentTypeModalView.close();
        },

        addAnswerSet: function (e) {
            e.preventDefault();

            app.views.contentTypeModalView.once('hidden', function () {
                app.views.answerSetTypeView = new AnswerSetTypeView();

                var modalView = new BackboneBootstrapModal({
                    content: app.views.answerSetTypeView,
                    title: 'Select Answer Set Type',
                    okText: 'Cancel',
                    cancelText: false,
                    modalOptions: {
                        backdrop: 'static',
                        keyboard: false
                    }
                });

                modalView.open();

                app.views.answerSetTypeModalView = modalView;
            });

            app.views.contentTypeModalView.close();
        },

        addTextAndMedia: function (e) {
            e.preventDefault();

            app.views.contentTypeModalView.once('hidden', function () {

                var textMediaElementView = new TextMediaElementView({ model: new TextMediaElementModel() });
                var modalView = textMediaElementView.getModalView();

                // textMediaElementView.on('OnTextMediaElementSaved', function (isNew) {
                //     app.views.careElementsView.loadTextMediaElements();
                // });

                modalView.open();

                app.views.contentTypeModalView.close();
            });

            app.views.contentTypeModalView.close();
        },

        addProtocolsAndPrograms: function (e) {
            e.preventDefault();

            if (!e.altKey && !e.ctrlKey && !e.metaKey && !e.shiftKey) {
                var href = $(e.currentTarget).attr('href');

                app.views.contentTypeModalView.close();

                app.router.navigate(href, {
                    trigger: true
                });
            }
        }
    });
});