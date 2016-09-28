'use strict';

define([
    'jquery',
    'underscore',
    'backbone'
], function ($, _, Backbone) {
    return Backbone.View.extend({

        tagName: 'th',

        className: 'text-center',

        template: _.template('<i style="background-color: <%=colorCode%>"/><%=name%>'),

        events: {},

        initialize: function (options) {

        },

        render: function () {

            this.$el.html(this.template(this.model.attributes));
            return this;
        }
    });
});