'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Site/Dashboard/DashboardApp/AppNamespace'
], function($, _, Backbone, app) {
    return Backbone.View.extend({

        className: 'range-item',

        template: app.OPTIONS.TEMPLATE('severityItemWidgetView'),

        events: {
            'click': 'hasCountEvent'
        },

        hasCountEvent: function(e) {

            if (!this.model.attributes.count) {
                e.stopImmediatePropagation();
            }
        },

        initialize: function() {
            
        },

        render: function() {

            this.$el.html(this.template(this.model.attributes));

            this.$el.attr('data-id', this.model.attributes.id);

            if (!this.model.attributes.count) {
                this.$el.addClass('no-count');
            }


            if (!this.model.attributes.colorCode) {
                $('.scale-item', this.$el).addClass('with-border');
            }
            return this;

        }
    });
});


