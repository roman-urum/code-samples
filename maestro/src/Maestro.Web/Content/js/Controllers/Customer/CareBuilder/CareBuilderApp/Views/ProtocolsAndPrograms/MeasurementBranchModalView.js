'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'BackboneModelBinder',
    'BackboneCollectionBinder',
    'Controllers/Customer/CareBuilder/CareBuilderApp/AppGlobalVariables',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Models/ProtocolsAndPrograms/MeasurementBranchModel',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Views/ProtocolsAndPrograms/MeasurementThresholdView',

], function (
    $,
    _,
    Backbone,
    BackboneModelBinder,
    BackboneCollectionBinder,
    AppGlobalVariables,
    MeasurementBranchModel,
    MeasurementThresholdView
) {
    return Backbone.View.extend({
        model: MeasurementBranchModel,

        template: _.template($("#measurementBranchModalTemplate").html()),

        modelBinder: undefined,

        collectionBinder: undefined,

        initialize: function () {
            var self = this,
                elManagerFactory = new BackboneCollectionBinder.ViewManagerFactory(function (model) {
                    return new MeasurementThresholdView({
                        model: model,
                        isMultipleThresholds: self.model.get('thresholds').length > 1
                    });
                });

            this.modelBinder = new BackboneModelBinder();
            this.collectionBinder = new BackboneCollectionBinder(elManagerFactory);
        },

        render: function () {
            this.$el.html(this.template(this.model.attributes));
            this.renderAlertSeverities();
            this.modelBinder.bind(this.model, this.el, BackboneModelBinder.createDefaultBindings(this.el, 'data-name'));
            this.collectionBinder.bind(this.model.get('thresholds'), this.$el.find('.js-thresholds'));

            return this;
        },

        renderAlertSeverities: function () {
            var $alertSeveritiesSelectContainer = this.$el.find('.js-alert-severities'),
                $alertSeveritiesSelect = $alertSeveritiesSelectContainer.find("#thresholdAlertSeverityId"),
                alertSeverities = AppGlobalVariables.collections.alertSeverities,
                selectedSeverityId = this.model.get('thresholdAlertSeverityId');

            if (selectedSeverityId == null || !alertSeverities.where({ id: selectedSeverityId }).length) {
                if (alertSeverities.length) {
                    this.model.set('thresholdAlertSeverityId', alertSeverities.first().get('id'));
                } else {
                    this.model.set('thresholdAlertSeverityId', null);
                }
            }

            if (alertSeverities.length) {
                alertSeverities.each(function (item) {
                    $alertSeveritiesSelect.append('<option value="' + item.get('id') + '">' + item.get('name') + '</option>');
                });
                $alertSeveritiesSelect.val(this.model.get('thresholdAlertSeverityId'));
            } else {
                $alertSeveritiesSelectContainer.remove();
            }
        },

        validate: function (currentProtocolElement) {
            var isModelValid = this.model.isValid();

            if (!isModelValid) {
                this.$el.find('.js-value-not-selected').removeClass('hide');
            }

            if (!this.isBranchUnique(currentProtocolElement)) {
                this.$el.find('.js-branch-exists').removeClass('hide');
                isModelValid = false;
            }

            return isModelValid;
        },

        isBranchUnique: function (currentProtocolElement) {
            var newBranch = this.model.getBranchJson(),
                isUniqueConditions = true,
                self = this;

            _.each(currentProtocolElement.branches, function (branch) {
                if (self.isBranchesTheSame(branch, newBranch)) {
                    isUniqueConditions = false;
                }
            });

            return isUniqueConditions;
        },

        isBranchesTheSame: function (branch1, branch2) {
            var isDifferencesExists = false;

            if (branch1.conditions.length !== branch2.conditions.length ||
                branch1.id === branch2.id ||
                branch1.thresholdAlertSeverityId !== branch2.thresholdAlertSeverityId) {
                return false;
            }

            _.each(branch1.conditions, function (branch1Condition) {
                var branch2Condition = _.findWhere(branch2.conditions, {
                    operand: branch1Condition.operand,
                    operator: branch1Condition.operator
                });

                if (branch2Condition == undefined || branch2Condition.value !== branch1Condition.value) {
                    console.log('differences found');
                    isDifferencesExists = true;
                }
            });

            return !isDifferencesExists;
        }
    });
});