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

        template: _.template( $('#oneWayChatTemplate').html() ),

        className: "one-way-chat-content",

        maxLength: 230,

        events: {
            'input textarea': 'detectMessageMaxLength'
        },

        detectMessageMaxLength: function(e) {
            if(e.currentTarget.value.length > this.maxLength) {
                e.currentTarget.value = e.currentTarget.value.substring(0, this.maxLength);
            }
        },

        render: function () {
            var self = this;

            var data = _.extend({}, this.model.toJSON());
            this.$el.html(this.template(data));

            return this;
        },

        getMessageText: function () {
            return this.$('textarea').val();
        }

    });
});