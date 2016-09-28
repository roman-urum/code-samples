'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Models/ScaleAnswerSetModel',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Views/CareElements/ScaleAnswersControlView'
], function (
    $,
    _,
    Backbone,
    ScaleAnswerSetModel,
    ScaleAnswersControlView
) {
    return Backbone.View.extend({
        model: ScaleAnswerSetModel,

        template: _.template($('#questionElementScaleAnswersTemplate').html()),

        render: function () {
            var scaleAnswersView = new ScaleAnswersControlView({ model: this.model });

            this.$el.html(this.template(this.model.attributes));
            this.$el.find('.js-scale-answers').html(scaleAnswersView.render().$el);

            return this;
        }
    });
});