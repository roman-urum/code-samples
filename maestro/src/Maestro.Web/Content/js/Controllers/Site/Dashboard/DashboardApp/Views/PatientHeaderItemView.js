'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Site/Dashboard/DashboardApp/AppNamespace'
], function($, _, Backbone, app) {
    return Backbone.View.extend({
        tagName: 'div',
        className: 'row',
        template: app.OPTIONS.TEMPLATE('patientCardViewTemplateHeaderItem'),

        initialize: function() {
            this.alertType = this.model.alertType;
        },

        render: function() {
            this.collection.each(function(item) {
                var $item = $(this.template(item.attributes))
                    .attr('data-type', this.alertType)
                    .attr('data-id', item.attributes.id);


                this.$el.append($item);
            }, this);

            var count = 12 / this.collection.length;
            count = count - (count % 1);

            this.$el.find('.alert-count-item').addClass('col-sm-' + count);
            this.$el.find('[data-toggle="tooltip"]').tooltip();

            return this;
        }
    });
});