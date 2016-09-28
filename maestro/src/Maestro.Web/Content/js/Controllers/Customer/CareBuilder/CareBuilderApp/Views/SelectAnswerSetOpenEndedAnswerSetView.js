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

        className: 'panel panel-default',

        template: _.template($('#openEndedAnswerSetTemplate').html()),

        render: function () {
            this.$el.html(this.template(this.model.attributes));

            return this;
        }
    });
});