'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Root/Thresholds/AppNamespace',
    'Controllers/Root/Thresholds/Views/ThresholdsSeveritiesCollectionItemView'
], function ($, _, Backbone, app, ThresholdsSeveritiesCollectionItemView) {
    return Backbone.View.extend({
        tagName: 'td',

        className: 'td-nopadding',

        template: _.template('<table class="table"></table>'),

        events: {},

        initialize: function (options) {

            this.severity = options.severity;
            this.severityIndex = options.severityIndex;
        },

        render: function (severityIndex) {

            this.$el.html(this.template());
            this.collection.each(function (model, index) {                
                this.renderThreshold(model, 100 * index + severityIndex);
            }, this);
            return this;
        },

        renderThreshold: function (threshold, tabIndex) {

            app.views.thresholdsSeveritiesCollectionItemView = new ThresholdsSeveritiesCollectionItemView({
                model: threshold,
                severity: this.severity,
                tabindex: tabIndex
            });

            var el = app.views.thresholdsSeveritiesCollectionItemView.render().el;

            this.$el.find('table').append(el);
        }
    });
});