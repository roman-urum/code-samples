'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Customer/CareBuilder/CareBuilderApp/AppGlobalVariables',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Models/AssessmentModel'
], function ($, _, Backbone, AppGlobalVariables, AssessmentModel) {
    return Backbone.View.extend({
        className: 'protocol-search-result-item-ci',

        model: AssessmentModel,

        template: _.template($('#protocolAssessmentListItemTemplate').html()),

        render: function () {
            this.$el.html(this.template(this.model.attributes));
            this.$el.attr('data-elementType', this.model.attributes.type).data('elementType', this.model.attributes.type);
            this.$el.attr('data-id', this.model.attributes.id).data('id', this.model.attributes.id);

            return this;
        }
    });
});