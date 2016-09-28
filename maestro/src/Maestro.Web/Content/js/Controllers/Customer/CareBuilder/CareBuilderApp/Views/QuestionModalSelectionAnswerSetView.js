'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'BackboneCollectionBinder',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Views/QuestionModalSelectionAnswerChoiceView'
], function (
    $,
    _,
    Backbone,
    BackboneCollectionBinder,
    QuestionModalSelectionAnswerChoiceView) {
    return Backbone.View.extend({
        template: _.template($("#questionModalSelectionAnswerSetTemplate").html()),

        // modelBinder: undefined,

        collectionBinder: undefined,

        initialize: function () {
            var elManagerFactory = new BackboneCollectionBinder.ViewManagerFactory(function (model) {

                return new QuestionModalSelectionAnswerChoiceView({ model: model });
            });

            // this.modelBinder = new Backbone.ModelBinder();
            this.collectionBinder = new BackboneCollectionBinder(elManagerFactory);
        },

        close: function () {
            // this.modelBinder.unbind();
        },

        render: function () {
            this.$el.html(this.template(this.model.attributes));
            this.collectionBinder.bind(this.model.get('selectionAnswerChoices'), this.$el.find('.js-answers'));

            return this;
        }
    });
});