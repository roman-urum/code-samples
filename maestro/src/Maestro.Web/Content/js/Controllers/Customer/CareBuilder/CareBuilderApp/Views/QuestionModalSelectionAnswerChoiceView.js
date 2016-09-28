'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'BackboneModelBinder',
    'Controllers/Customer/CareBuilder/CareBuilderApp/AppGlobalVariables',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Models/SelectionAnswerChoiceModel'
], function ($, _, Backbone, BackboneModelBinder, AppGlobalVariables, SelectionAnswerChoiceModel) {
    return Backbone.View.extend({
        model: SelectionAnswerChoiceModel,

        className: 'panel panel-default',

        template: _.template($('#questionModalSelectionAnswerChoiceTemplate').html()),

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