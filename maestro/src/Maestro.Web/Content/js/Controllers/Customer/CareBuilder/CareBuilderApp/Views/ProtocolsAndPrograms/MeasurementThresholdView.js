'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'BackboneModelBinder',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Models/ProtocolsAndPrograms/MeasurementThresholdModel'
], function (
    $,
    _,
    Backbone,
    BackboneModelBinder,
    MeasurementThresholdModel
) {
    return Backbone.View.extend({
        model: MeasurementThresholdModel,

        className: 'measurement-threshold-value-ci',

        template: _.template($("#measurementThresholdTemplate").html()),

        modelBinder: undefined,

        initialize: function (options) {
            this.isMultipleThresholds = options.isMultipleThresholds;
            this.modelBinder = new BackboneModelBinder();
        },

        collectionBinder: undefined,

        render: function () {
            this.$el.html(this.template(this.model.attributes));
            this.modelBinder.bind(this.model, this.el, BackboneModelBinder.createDefaultBindings(this.el, 'data-name'));

            if (this.isMultipleThresholds === true) {
                this.$el.find('.js-ignore-threshold').removeClass('hide');
            }

            return this;
        }
    });
});