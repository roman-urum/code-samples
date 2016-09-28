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

        template: _.template($("#protocolTreeAnswerOpenEndedTemplate").html()),

        render: function () {
            this.$el.html(this.template());

            return this;
        }
    });
});