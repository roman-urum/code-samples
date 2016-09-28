'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'BackboneModelBinder',
    'Controllers/Customer/CareBuilder/CareBuilderApp/AppGlobalVariables',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Models/ProtocolsAndPrograms/RecurrenceModel'
], function (
    $,
    _,
    Backbone,
    BackboneModelBinder,
    AppGlobalVariables,
    RecurrenceModel
) {
    return Backbone.View.extend({
        model: RecurrenceModel,

        template: _.template($('#recurrenceModalTemplate').html()),

        modelBinder: undefined,

        initialize: function () {
            this.modelBinder = new BackboneModelBinder();

            Backbone.Validation.bind(this);
        },

        render: function () {
            this.$el.html(this.template(this.model.toJSON()));
            this.modelBinder.bind(this.model, this.el, BackboneModelBinder.createDefaultBindings(this.el, 'data-name'));

            return this;
        }
    });
});