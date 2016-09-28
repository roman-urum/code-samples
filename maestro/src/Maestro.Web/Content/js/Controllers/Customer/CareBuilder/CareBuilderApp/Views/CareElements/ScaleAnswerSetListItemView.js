'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'BackboneBootstrapModal',
    'Controllers/Customer/CareBuilder/CareBuilderApp/AppGlobalVariables',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Models/ScaleAnswerSetModel',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Views/ScaleAnswerSetFormView',
    'Controllers/Constants',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Views/CareElements/ScaleAnswersControlView',
    'Controllers/Helpers'
], function (
    $,
    _,
    Backbone,
    BackboneBootstrapModal,
    app,
    ScaleAnswerSetModel,
    ScaleAnswerSetFormView,
    Constants,
    ScaleAnswersControlView,
    Helpers
) {
    return Backbone.View.extend({
        className: 'panel panel-default',

        model: ScaleAnswerSetModel,

        template: _.template($('#scaleAnswerSetListItemTemplate').html()),

        render: function () {
            this.$el.html(this.template(this.model.attributes));

            return this;
        },

        initialize: function () {
            this.isCollapsed = true;
        },

        events: {
            'click .js-collapsed': 'loadDetailedModel',
            'click .js-edit-scale-answer-set-item': 'editScaleAnswerSet'
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
                        var scaleAnswersView = new ScaleAnswersControlView({ model: $this.model });

                        $this.$el.find('.panel-body').html(scaleAnswersView.render().$el);

                        $("input.scale").slider({
                            handle: 'custom',
                            tooltip: 'hide'
                        });
                    }
                });
            }
        },

        editScaleAnswerSet: function (e) {
            var scaleAnswerSet = this.model;

            // Saving original model state
            scaleAnswerSet.store();

            e.preventDefault();
            $('.js-edit-scale-answer-set-item').addClass('disabled');

            app.views.editScaleAnswerSetFormView = new ScaleAnswerSetFormView({ model: scaleAnswerSet });

            var modalView = new BackboneBootstrapModal({
                content: app.views.editScaleAnswerSetFormView,
                title: 'Edit content',
                modalOptions: {
                    backdrop: 'static',
                    keyboard: false
                }
            });

            modalView.open();

            modalView.$el.find('.js-change-answerset-type').addClass('hidden');

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

                app.views.editScaleAnswerSetFormView.model.set('tags', newTags);

                if (!app.views.editScaleAnswerSetFormView.isCustomValid(true)) {
                    modalView.preventClose();

                    return;
                }

                modalView.$el.find('.ok').attr('disabled', true);

                app.views.editScaleAnswerSetFormView.model.save(
                    null,
                    {
                        success: function (model, response, options) {
                            modalView.$el.find('.js-alert-update-success').removeClass('hidden');
                            setTimeout(function () {
                                modalView.close();
                                modalView.$el.find('.js-alert-update-success').addClass('hidden');

                                app.vent.trigger("reRenderCareElements", {careElementType: "2"});

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
                $('.js-edit-scale-answer-set-item').removeClass('disabled');

                modalView.close();
                // Restoring original model state
                scaleAnswerSet.restart();
            });

            app.models.scaleAsnwerSetModel = scaleAnswerSet;
        }
    });
});