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

        template: _.template('<h4><%=name %></h4>'),

        render: function () {
            this.$el.html(this.template(this.model.attributes));

            return this;
        }
    });
});