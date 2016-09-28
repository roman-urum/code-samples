'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'BackboneModelBinder',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Models/ScaleAnswerChoiceModel'
], function ($, _, Backbone, BackboneModelBinder, ScaleAnswerChoiceModel) {
    return Backbone.View.extend({

        model: ScaleAnswerChoiceModel,

        className: 'list-group-item',

        template: _.template($('#questionModalScaleAnswerChoiceTemplate').html()),

        modelBinder: undefined,

        initialize: function () {
            this.modelBinder = new BackboneModelBinder();
            Backbone.Validation.bind(this);
        },

        render: function () {
            this.$el.html(this.template(this.model.attributes));
            this.modelBinder.bind(this.model, this.el, BackboneModelBinder.createDefaultBindings(this.el, 'data-name'));

            return this;
        },

        close: function () {
            this.modelBinder.unbind();
        }
    });
});