'use strict';

define([
    'jquery',
    'underscore',
    'backbone'
], function (
    $,
    _,
    Backbone
) {
    return Backbone.View.extend({
        tagName: 'li',

        className: 'protocol-item',

        template: _.template($("#protocolTreeQuestionItemTemplate").html()),

        events: {

        },

        render: function () {
            this.$el.html(this.template(this.model.attributes));
            this.$el.attr('id', this.model.attributes.id);

            return this;
        }
    });
});