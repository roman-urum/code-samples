'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Customer/CareBuilder/CareBuilderApp/AppGlobalVariables',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Models/QuestionElementModel'
], function ($, _, Backbone, AppGlobalVariables, QuestionElementModel) {
    return Backbone.View.extend({
        className: 'protocol-search-result-item-ci',

        model: QuestionElementModel,

        template: _.template($('#protocolQuestionListItemTemplate').html()),

        render: function () {
            this.$el.html(this.template(this.model.toJSON()));
            this.$el.attr('data-id', this.model.attributes.id).data('id', this.model.attributes.id);

            return this;
        }
    });
});