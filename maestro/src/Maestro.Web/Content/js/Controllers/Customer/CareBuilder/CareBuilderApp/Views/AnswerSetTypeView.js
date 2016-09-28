'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'BackboneBootstrapModal',
    'Controllers/Customer/CareBuilder/CareBuilderApp/AppGlobalVariables',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Views/SelectionAnswerSetFormView',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Models/SelectionAnswerSetModel',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Views/ScaleAnswerSetFormView',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Models/ScaleAnswerSetModel',
    'Controllers/Helpers'
], function (
    $,
    _,
    Backbone,
    BackboneBootstrapModal,
    app,
    SelectionAnswerSetFormView,
    SelectionAnswerSetModel,
    ScaleAnswerSetFormView,
    ScaleAnswerSetModel,
    Helpers
) {
    app.AnswerSetTypeView = Backbone.View.extend({
        tagName: 'div',
        className: 'row',
        template: _.template($('#answerSetTypeTemplate').html()),

        render: function () {
            this.$el.html(this.template());

            return this;
        },

        events: {
            'click .js-add-selection-answer-set': 'addSelectionAnswerSet',
            'click .js-add-scale-answer-set': 'addScaleAnswerSet'
        },

        addSelectionAnswerSet: function (e) {
            e.preventDefault();

            app.views.answerSetTypeModalView.close();
            app.views.answerSetTypeModalView.$el.on('hidden.bs.modal', function () {
                var selectionAnswerSetModel = new SelectionAnswerSetModel({
                    isMultipleChoice: false
                });
                selectionAnswerSetModel.get('selectionAnswerChoices').add({
                    isRemovable: false,
                    index: 1
                });
                selectionAnswerSetModel.get('selectionAnswerChoices').add({
                    isRemovable: false,
                    index: 2
                });

                app.models.selectionAnswerSetModel = selectionAnswerSetModel;

                app.views.createSelectionAnswerSetFormView = new SelectionAnswerSetFormView({ model: selectionAnswerSetModel, isChangeAnswerTypeVisible: true });

                var modalView = new BackboneBootstrapModal({
                    content: app.views.createSelectionAnswerSetFormView,
                    title: 'Add Content',
                    modalOptions: {
                        backdrop: 'static',
                        keyboard: false
                    }
                });

                modalView.$el.find('.js-change-answerset-type').removeClass('hidden');

                modalView.on('shown', function () {
                    Helpers.initTags($('.creation-tags'), null);
                });

                modalView.on('ok', function () {
                    modalView.$el.find('.alert-danger').addClass('hidden');

                    // Updating model with Tags
                    var newTags = modalView.$el.find('.creation-tags').tokenfield('getTokensList').split(', ');

                    newTags = newTags.filter(function (element, index) {
                        return element !== '';
                    });

                    selectionAnswerSetModel.set('tags', newTags);

                    if (!app.views.createSelectionAnswerSetFormView.model.isValid(true)) {
                        modalView.preventClose();

                        return;
                    }

                    modalView.$el.find('.ok').attr('disabled', true);

                    app.views.createSelectionAnswerSetFormView.model.save(
                        null,
                        {
                            success: function (model, response, options) {
                                modalView.$el.find('.js-alert-create-success').removeClass('hidden');
                                modalView.close();
                                window.setTimeout(function () {
                                    app.vent.trigger("reRenderCareElements", {careElementType: "1"});
                                }, 2000);
                            },
                            error: function (model, xhr, options) {
                                modalView.$el.find('.ok').attr('disabled', false);

                                var errorFrame = modalView.$el.find('.alert-danger');
                                errorFrame.html(xhr.responseJSON.ErrorMessage);
                                errorFrame.removeClass('hidden');
                            },
                            skipParseOnSuccess: true
                        }
                    );

                    modalView.preventClose();
                });

                app.views.selectionAnswerSetFormModalView = modalView;

                modalView.open();
            });
        },

        addScaleAnswerSet: function (e) {
            e.preventDefault();

            app.views.answerSetTypeModalView.close();
            app.views.answerSetTypeModalView.$el.on('hidden.bs.modal', function () {
                var scaleAnswerSetModel = new ScaleAnswerSetModel();
                app.views.scaleContentAnswerSetFormView = new ScaleAnswerSetFormView({ model: scaleAnswerSetModel });

                var modalView = new BackboneBootstrapModal({
                    content: app.views.scaleContentAnswerSetFormView,
                    title: 'Add Content',
                    modalOptions: {
                        backdrop: 'static',
                        keyboard: false
                    }
                });

                modalView.$el.find('.js-change-answerset-type').removeClass('hidden');

                modalView.on('shown', function () {
                    Helpers.initTags($('.creation-tags'), null);
                });

                modalView.on('ok', function () {
                    modalView.$el.find('.alert-danger').addClass('hidden');

                    // Updating model with Tags
                    var newTags = modalView.$el.find('.creation-tags').tokenfield('getTokensList').split(', ');

                    newTags = newTags.filter(function (element, index) {
                        return element !== '';
                    });

                    scaleAnswerSetModel.set('tags', newTags);

                    if (!app.views.scaleContentAnswerSetFormView.isCustomValid(true)) {
                        modalView.preventClose();

                        return;
                    }

                    modalView.$el.find('.ok').attr('disabled', true);
                    modalView.preventClose();

                    app.views.scaleContentAnswerSetFormView.model.save(null, {
                        success: function (model, response, options) {
                            modalView.$el.find('.ok').attr('disabled', false);
                            modalView.$el.find('.js-alert-create-success').removeClass('hidden');
                            modalView.close();
                            window.setTimeout(function () {
                                modalView.$el.find('.js-alert-create-success').addClass('hidden');

                                app.vent.trigger("reRenderCareElements", {careElementType: "2"});

                            }, 2000);

                        },
                        error: function (model, xhr, options) {
                            modalView.$el.find('.ok').attr('disabled', false);

                            var errorFrame = modalView.$el.find('.alert-danger');
                            errorFrame.html(xhr.responseJSON.ErrorMessage);
                            errorFrame.removeClass('hidden');
                        }
                    });

                });

                app.views.scaleContentAnswerSetModalView = modalView;

                modalView.open();
            });
        }
    });

    return app.AnswerSetTypeView;
});