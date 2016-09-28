'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Site/Patients/PatientsApp/Views/ConditionDetailsThresholdsView',
    'Controllers/Site/Patients/PatientsApp/Views/ConditionDetailsContentView'
], function ($, _, Backbone, ConditionDetailsThresholdsView, ConditionDetailsContentView) {
    return Backbone.View.extend({
        template: _.template($('#conditionDetailsTemplate').html()),

        selectors: {
            conditionThresholds: '#condition-thresholds',
            conditionContent: '#condition-content'
        },

        render: function () {
            this.$el.html(this.template(this.model.attributes));

            var thresholdsView = new ConditionDetailsThresholdsView({model: this.model});
            this.$(this.selectors.conditionThresholds).html(thresholdsView.render().$el);

            var contentView = new ConditionDetailsContentView({model: this.model});
            this.$(this.selectors.conditionContent).html(contentView.render().$el);

            return this;
        }
    });
});