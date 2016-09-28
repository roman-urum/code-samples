'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Site/Dashboard/DashboardApp/AppNamespace'
], function($, _, Backbone, app) {
    return Backbone.View.extend({

        className: 'range-item',

        template: app.OPTIONS.TEMPLATE('alertTypeItemWidgetView'),
        
        events: {
            'click': 'hasCountEvent'
        },

        initialize: function() {
            this.$el.attr('data-code', this.model.attributes.CODE);
            if (!this.model.attributes.COUNT) {
                this.$el.addClass('no-count');
            }
        },

        render: function() {
            this.$el.html(this.template(this.model.attributes));

            return this;
        },

        hasCountEvent: function(e) {

            if (!this.model.attributes.COUNT) {
                e.stopImmediatePropagation();
            }
            
        }

    });
});


