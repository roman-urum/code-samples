'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Customer/CareBuilder/CareBuilderApp/AppGlobalVariables',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Models/MeasurementModel'
], function ($, _, Backbone, AppGlobalVariables, MeasurementModel) {
    return Backbone.View.extend({
        className: 'protocol-search-result-item-ci',

        model: MeasurementModel,

        template: _.template($('#protocolMeasurementListItemTemplate').html()),

        render: function () {
            this.$el.html(this.template(this.model.attributes));
            this.$el.attr('data-elementType', this.model.attributes.type).data('elementType', this.model.attributes.type);
            this.$el.attr('data-id', this.model.attributes.id).data('id', this.model.attributes.id);

            return this;
        }
    });
});