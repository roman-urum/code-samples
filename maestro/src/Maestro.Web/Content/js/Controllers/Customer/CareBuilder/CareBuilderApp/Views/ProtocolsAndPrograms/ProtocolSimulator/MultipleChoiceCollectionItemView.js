'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Customer/CareBuilder/CareBuilderApp/AppGlobalVariables'
], function (
    $,
    _,
    Backbone,
    app
) {
    return Backbone.View.extend({
        tagName: 'a',

        className: 'multiple-choice-answer js-select-answer',

        template: _.template('<div class="answer-text"><%=answerString.value%></div>'),

        render: function () {
            this.$el.html(this.template(this.model.attributes));
            this.$el.data('id', this.model.get('id')).attr('data-id', this.model.get('id'));

            return this;
        }
    });
});