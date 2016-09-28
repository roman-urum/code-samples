'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'BackboneBootstrapModal',
    'Controllers/Customer/CareBuilder/CareBuilderApp/AppGlobalVariables',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Models/SelectionAnswerSetModel',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Views/SelectionAnswerSetFormView',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Views/CareElements/SelectionAnswerSetListItemBodyView',
    'Controllers/Helpers'
], function (
    $,
    _,
    Backbone,
    BackboneBootstrapModal,
    app,
    SelectionAnswerSetModel,
    SelectionAnswerSetFormView,
    SelectionAnswerSetListItemBodyView,
    Helpers
) {
    return Backbone.View.extend({
        className: 'panel panel-default',

        model: SelectionAnswerSetModel,

        template: _.template($('#selectionAnswerSetListItemTemplate').html()),

        render: function () {
            this.$el.html(this.template(this.model.attributes));

            return this;
        },

        initialize: function () {
            this.isCollapsed = true;
        },

        events: {
            'click .js-collapsed': 'loadDetailedModel',
            'click .js-edit-selection-answer-set-item': 'editSelectionAnswerSet'
        },

        loadDetailedModel: function (e) {
            e.preventDefault();

            var $this = this;

            if (this.isCollapsed) {
                Helpers.renderSpinner(this.$el.find('.panel-body'));

                this.model.fetch({
                    reset: true,
                    success: function () {
                        $this.isCollapsed = false;

                        var selectionAnswerSetListItemBodyView =
                            new SelectionAnswerSetListItemBodyView({ model: $this.model });

                        $this.$el.find('.panel-body').html(selectionAnswerSetListItemBodyView.render().$el);
                    }
                });
            }
        },

        editSelectionAnswerSet: function (e) {
            var selectionAnswerSet = this.model;

            // Saving original model state
            selectionAnswerSet.store();
            selectionAnswerSet.get('selectionAnswerChoices').store();

            e.preventDefault();

            $('.js-edit-selection-answer-set-item').addClass('disabled');

            var selectionAnswerChoices = selectionAnswerSet.get('selectionAnswerChoices');

            if (selectionAnswerChoices !== undefined &&
                selectionAnswerChoices !== null &&
                selectionAnswerChoices.models.length !== 0) {
                $.each(selectionAnswerChoices.models, function (index, el) {
                    el.set('index', selectionAnswerChoices.indexOf(el) + 1);
                });
            }

            app.views.editSelectionAnswerSetFormView =
                new SelectionAnswerSetFormView({ model: selectionAnswerSet });

            var modalView = new BackboneBootstrapModal({
                content: app.views.editSelectionAnswerSetFormView,
                title: 'Edit content',
                modalOptions: {
                    backdrop: 'static',
                    keyboard: false
                }
            });

            modalView.open();

            modalView.on('shown', function () {

                Helpers.initTags($('.creation-tags'), null);

                var selectionAnswerChoices = app.models.selectionAnswerSetModel.get('selectionAnswerChoices');

                if (selectionAnswerChoices.length < 3) {
                    _.each(selectionAnswerChoices.models, function (model) {
                        model.set('isRemovable', false);
                    });
                } else {
                    _.each(selectionAnswerChoices.models, function (model) {
                        model.set('isRemovable', true);
                    });
                }
            });

            modalView.on('ok', function () {
                modalView.$el.find('.alert-danger').addClass('hidden');

                // Updating model with Tags
                var newTags = modalView.$el.find('.creation-tags').tokenfield('getTokensList').split(', ');

                newTags = newTags.filter(function (element, index) {
                    return element !== '';
                });

                app.views.editSelectionAnswerSetFormView.model.set('tags', newTags);

                if (!app.views.editSelectionAnswerSetFormView.model.isValid(true)) {
                    modalView.preventClose();

                    return;
                }

                modalView.$el.find('.ok').attr('disabled', true);

                app.views.editSelectionAnswerSetFormView.model.save(
                    null,
                    {
                        success: function (model, response, options) {
                            modalView.$el.find('.js-alert-update-success').removeClass('hidden');
                            window.setTimeout(function () {
                                $('.js-edit-selection-answer-set-item').removeClass('disabled');
                                modalView.close();
                                modalView.$el.find('.js-alert-update-success').addClass('hidden');
                                
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

            modalView.on('cancel', function () {
                $('.js-edit-selection-answer-set-item').removeClass('disabled');

                // Restoring original model state
                selectionAnswerSet.restart();
                selectionAnswerSet.get('selectionAnswerChoices').restart();
            });

            app.models.selectionAnswerSetModel = selectionAnswerSet;
        }
    });
});