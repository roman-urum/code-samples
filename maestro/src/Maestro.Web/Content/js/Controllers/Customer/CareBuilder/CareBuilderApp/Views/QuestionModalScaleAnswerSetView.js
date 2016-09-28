'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'BackboneCollectionBinder',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Views/CareElements/ScaleAnswersControlView',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Views/QuestionModalScaleAnswerChoiceView'
], function (
    $,
    _,
    Backbone,
    BackboneCollectionBinder,
    ScaleAnswersControlView,
    QuestionModalScaleAnswerChoiceView) {
    return Backbone.View.extend({
        template: _.template($("#questionModalScaleAnswerSetTemplate").html()),

        className: 'panel panel-default',

        // modelBinder: undefined,

        collectionBinder: undefined,

        initialize: function () {
            var elManagerFactory = new BackboneCollectionBinder.ViewManagerFactory(function (model) {
                return new QuestionModalScaleAnswerChoiceView({ model: model });
            });

            // this.modelBinder = new Backbone.ModelBinder();
            this.collectionBinder = new BackboneCollectionBinder(elManagerFactory);
        },

        render: function () {
            var scaleControlView = new ScaleAnswersControlView({ model: this.model });

            this.$el.html(this.template(this.model.attributes));
            this.$el.find('.js-scale-control').html(scaleControlView.render().$el);
            this.collectionBinder.bind(this.model.get('scaleAnswerChoices'), this.$el.find('.js-answers'));


            $("input.scale").slider({
                handle: 'custom',
                tooltip: 'hide'
            });

            return this;
        },

        close: function () {
            // this.modelBinder.unbind();
        }
    });
});