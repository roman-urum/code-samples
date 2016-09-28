'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'BackboneModelBinder',
    'BackboneCollectionBinder',
    'BackboneBootstrapModal',
    'Controllers/Customer/CareBuilder/CareBuilderApp/AppGlobalVariables',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Models/SelectionAnswerSetModel',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Models/SelectionAnswerChoiceModel',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Views/SelectionAnswerChoiceView'
], function ($,
             _,
             Backbone,
             BackboneModelBinder,
             BackboneCollectionBinder,
             BackboneBootstrapModal,
             AppGlobalVariables,
             SelectionAnswerSetModel,
             SelectionAnswerChoiceModel,
             SelectionAnswerChoiceView) {
    return Backbone.View.extend({
        className: 'selection-answer-set-form',

        model: SelectionAnswerSetModel,

        template: _.template($('#selectionAnswerSetFormTemplate').html()),

        modelBinder: undefined,

        collectionBinder: undefined,

        initialize: function (options) {
            this.options = options;

            this.modelBinder = new BackboneModelBinder();

            var elManagerFactory = new BackboneCollectionBinder.ViewManagerFactory(
                function (model) {
                    return new SelectionAnswerChoiceView({ model: model });
                });

            this.collectionBinder = new BackboneCollectionBinder(elManagerFactory);

            Backbone.Validation.bind(this);
        },

        render: function () {
            this.$el.html(this.template(this.model.attributes));

            this.modelBinder.bind(this.model, this.el, BackboneModelBinder.createDefaultBindings(this.el, 'data-name'));
            this.collectionBinder.bind(this.model.get('selectionAnswerChoices'), this.$el.find('.js-answers'));

            if (this.options !== undefined && this.options.isChangeAnswerTypeVisible) {
                this.$el.find('.js-change-answerset-type').removeClass('hidden');
            } else {
                this.$el.find('.js-change-answerset-type').addClass('hidden');
            }

            return this;
        },

        close: function () {
            this.modelBinder.unbind();
            this.collectionBinder.unbind();
            this.$el.find('.creation-tags').tokenfield('destroy');
        },

        events: {
            'click .js-change-answerset-type': 'changeAnswerSetType',
            'click .js-add-answer': 'addSelectionAnswerChoice'
        },

        changeAnswerSetType: function (e) {
            e.preventDefault();

            AppGlobalVariables.views.selectionAnswerSetFormModalView.close();
            AppGlobalVariables.views.selectionAnswerSetFormModalView.$el.on('hidden.bs.modal', function () {
                AppGlobalVariables.views.answerSetTypeView = new AppGlobalVariables.AnswerSetTypeView();

                var modalView = new BackboneBootstrapModal({
                    content: AppGlobalVariables.views.answerSetTypeView,
                    title: 'Select Answer Set Type',
                    okText: 'Cancel',
                    cancelText: false,
                    modalOptions: {
                        backdrop: 'static',
                        keyboard: false
                    }
                });

                AppGlobalVariables.views.answerSetTypeModalView = modalView;

                modalView.open();
            });
        },

        addSelectionAnswerChoice: function (e) {
            e.preventDefault();

            var selectionAnswerChoices = this.model.get('selectionAnswerChoices');
            var additionalSelectionAnswerChoiceModel = new SelectionAnswerChoiceModel();

            if (selectionAnswerChoices.length === 0) {
                additionalSelectionAnswerChoiceModel.set('index', 1);
            } else {
                additionalSelectionAnswerChoiceModel.set('index', selectionAnswerChoices.models[selectionAnswerChoices.length - 1].get('index') + 1);
            }

            selectionAnswerChoices.add(additionalSelectionAnswerChoiceModel);

            if (selectionAnswerChoices.length < 2) {
                _.each(selectionAnswerChoices.models, function (model) {
                    model.set('isRemovable', false);
                });
            } else {
                _.each(selectionAnswerChoices.models, function (model) {
                    model.set('isRemovable', true);
                });
            }
        }
    });
});