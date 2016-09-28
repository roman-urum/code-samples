'use strict';

define([
    'jquery',
    'underscore',
    'backbone'
], function ($, _, Backbone) {
    return Backbone.View.extend({

        template: _.template($("#selectionAnswerSetFormTemplate").html()),

        render: function () {
            this.$el.html(this.template());

            return this;
        }
    });
});