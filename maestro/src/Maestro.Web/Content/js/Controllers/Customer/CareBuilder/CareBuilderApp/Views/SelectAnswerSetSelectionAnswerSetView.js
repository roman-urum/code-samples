'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Models/SelectionAnswerSetModel',
    'Controllers/Helpers'
], function (
    $,
    _,
    Backbone,
    SelectionAnswerSetModel,
    Helpers) {
    return Backbone.View.extend({

        model: SelectionAnswerSetModel,

        modelBinder: undefined,

        collectionBinder: undefined,

        className: 'panel panel-default',

        template: _.template($("#selectionAnswerSetTemplate").html()),

        templateAnswers: _.template($("#selectionAnswerSetAnswersTemplate").html()),

        events: {
            'click .panel-toggle': 'loadAnswers'
        },

        render: function () {
            this.$el.html(this.template(this.model.attributes));

            return this;
        },

        loadAnswers: function () {
            var $container = this.$el.find('.js-answers'),
                self = this;

            if ($container.hasClass('.js-loaded')) {
                return;
            }

            Helpers.renderSpinner($container);

            this.model.fetch({
                success: function () {
                    console.log(self.model);
                    $container.html(self.templateAnswers(self.model.attributes));
                    $container.addClass('.js-loaded');
                }
            });
        }
    });
});