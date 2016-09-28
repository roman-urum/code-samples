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

        template: _.template($("#protocolTreeAnswerScaleItemTemplate").html()),

        events: {

        },

        render: function () {
            this.$el.html(this.template(this.model.attributes));

            return this;
        }
    });
});