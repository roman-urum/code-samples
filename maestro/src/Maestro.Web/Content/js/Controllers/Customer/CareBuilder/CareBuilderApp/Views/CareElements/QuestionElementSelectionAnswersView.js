'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Models/SelectionAnswerSetModel'
], function (
    $,
    _,
    Backbone,
    SelectionAnswerSetModel
) {
    return Backbone.View.extend({
        model: SelectionAnswerSetModel,

        template: _.template($('#questionElementSelectionAnswersTemplate').html()),

        render: function () {

            this.$el.html(this.template(this.model.attributes));

            return this;
        }
    });
});