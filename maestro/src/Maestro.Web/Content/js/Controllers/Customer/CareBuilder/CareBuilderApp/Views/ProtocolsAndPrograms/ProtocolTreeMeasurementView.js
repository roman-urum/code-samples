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

        template: _.template($("#protocolTreeMeasurementTemplate").html()),

        events: {

        },

        render: function () {
            this.$el.html(this.template(this.model.attributes));
            console.log('rendering of protocol tree view');

            return this;
        }

    });
});