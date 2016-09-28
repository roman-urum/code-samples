'use strict';

define([
    'jquery',
    'underscore',
    'backbone'
], function ($, _, Backbone) {
    return Backbone.View.extend({

        tagName: 'li',

        render: function () {
            this.$el.html(this.model.get('name'));

            return this;
        }
    });
});