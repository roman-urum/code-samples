'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Models/ScaleAnswerSetModel',
    'Controllers/Helpers'
], function (
    $,
    _,
    Backbone,
    ScaleAnswerSetModel,
    Helpers
) {
    return Backbone.View.extend({

        model: ScaleAnswerSetModel,

        modelBinder: undefined,

        collectionBinder: undefined,

        className: 'panel panel-default',

        template: _.template($("#ScaleAnswerSetTemplate").html()),

        templateScaleAnswersControl: _.template($('#scaleAnswersControlTemplate').html()),

        events: {
            'click .panel-toggle': 'loadAnswers'
        },

        render: function () {
            this.$el.html(this.template(this.model.attributes));

            return this;
        },

        loadAnswers: function () {
            var self = this,
                $container = this.$el.find('.js-scale-control');

            if ($container.hasClass('.js-loaded')) {
                return;
            }

            Helpers.renderSpinner($container);

            this.model.fetch({
                success: function () {
                    $container.html(self.templateScaleAnswersControl(self.model.attributes));
                    $container.find("input.scale").slider({
                        handle: 'custom',
                        tooltip: 'hide'
                    });
                    $container.addClass('.js-loaded');
                }
            });
        }
    });
});