'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Root/Thresholds/AppNamespace',
    'Controllers/Root/Thresholds/Views/ThresholdsSeveritiesLayoutCollectionItemView'
], function ($, _, Backbone, app, ThresholdsSeveritiesLayoutCollectionItemView) {
    return Backbone.View.extend({

        tagName: 'tr',

        template: _.template('<td colspan="2" class="td-nopadding"></td>'),

        events: {},

        initialize: function () {

        },

        render: function () {
            this.$el.html(this.template());
            this.collection.each(this.renderThresholdLabel, this);

            return this;
        },

        renderThresholdLabel: function (threshold) {
            app.views.thresholdsSeveritiesLayoutCollectionItemView = new ThresholdsSeveritiesLayoutCollectionItemView({ model: threshold });
            this.$el.find('td[colspan="2"]').append(app.views.thresholdsSeveritiesLayoutCollectionItemView.render().el);

            return this;
        }
    });
});