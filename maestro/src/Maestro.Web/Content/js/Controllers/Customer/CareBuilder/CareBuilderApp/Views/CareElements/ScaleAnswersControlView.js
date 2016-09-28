'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Models/ScaleAnswerSetModel'
], function (
    $,
    _,
    Backbone,
    ScaleAnswerSetModel
) {
    return Backbone.View.extend({
        model: ScaleAnswerSetModel,

        template: _.template($('#scaleAnswersControlTemplate').html()),

        render: function () {
            this.$el.html(this.template(this.model.attributes));

            return this;
        }
    });
});