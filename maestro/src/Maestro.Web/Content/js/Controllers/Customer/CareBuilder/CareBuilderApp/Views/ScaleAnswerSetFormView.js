'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'BackboneModelBinder',
    'BackboneBootstrapModal',
    'Controllers/Customer/CareBuilder/CareBuilderApp/AppGlobalVariables',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Models/ScaleAnswerSetModel'
], function ($,
             _,
             Backbone,
             BackboneModelBinder,
             BackboneBootstrapModal,
             AppGlobalVariables,
             ScaleAnswerSetModel) {
    return Backbone.View.extend({
        model: ScaleAnswerSetModel,

        template: _.template($('#scaleAnswerSetFormTemplate').html()),

        modelBinder: new BackboneModelBinder(),

        initialize: function () {
            Backbone.Validation.bind(this);
        },

        render: function () {
            this.$el.html(this.template(this.model.attributes));

            this.model.set('highLabel', this.model.get('highLabel').value);
            this.model.set('lowLabel', this.model.get('lowLabel').value);
            if (this.model.get('midLabel')) {
                this.model.set('midLabel', this.model.get('midLabel').value);
            }
            this.modelBinder.bind(this.model, this.el, BackboneModelBinder.createDefaultBindings(this.el, 'data-name'));

            return this;
        },

        close: function () {
            this.modelBinder.unbind();
            this.$el.find('.creation-tags').tokenfield('destroy');
        },

        events: {
            'click .js-change-answerset-type': 'changeAnswerSetType',
        },

        changeAnswerSetType: function (e) {
            e.preventDefault();

            AppGlobalVariables.views.scaleContentAnswerSetModalView.close();
            AppGlobalVariables.views.scaleContentAnswerSetModalView.$el.on('hidden.bs.modal', function () {
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

        isCustomValid: function (flag) {
            var isValid = this.model.isValid(flag);

            if (this.model.get('HighValue') <= this.model.get('LowValue')) {
                var $el = this.$el.find('[data-validate-for="scale-content-high-value"]');
                $el.html('High value must be greater than Low value').removeClass('hidden').closest('div').addClass('has-error');

                $el.on('change', function () {
                    $el.off('click');
                    $el.closest('div').removeClass('has-error');
                    $el.closest('.help-block').html('').addClass('hidden');
                });

                return false;
            }

            return isValid;
        }

    });
});