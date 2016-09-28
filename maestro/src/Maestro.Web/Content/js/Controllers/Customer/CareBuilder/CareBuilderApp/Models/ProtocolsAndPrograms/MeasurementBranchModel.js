'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Customer/CareBuilder/CareBuilderApp/AppGlobalVariables',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Models/ProtocolsAndPrograms/MeasurementThresholdModel',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Collections/ProtocolsAndPrograms/MeasurementThresholdsCollection',
    'Controllers/Helpers',
    'backbone-nested'
], function ($, _, Backbone, app, MeasurementThresholdModel, MeasurementThresholdsCollection, Helpers) {
    return Backbone.NestedModel.extend({
        defaults: {
            thresholdAlertSeverityId: null,
            thresholdAlertSeverityName: null
        },

        initialize: function () {
            var self = this,
                measurementType = this.get('measurementType'),
                thresholdTypes = this.measurementThresholdsMap[measurementType],
                thresholdsCollection = new MeasurementThresholdsCollection(),
                conditions = this.get('conditions');

            $.each(thresholdTypes, function (index, thresholdType) {
                var threshold = {
                    type: thresholdType,
                    name: self.measurementThresholdNamesMap[thresholdType]
                },
                    condition = _.findWhere(conditions, { operand: thresholdType });

                if (condition != undefined) {
                    threshold['value'] = self.measurementOperatorsMap[condition.operator];
                }

                thresholdsCollection.add(threshold);
            });

            this.initAlertSeverity();
            this.set('thresholds', thresholdsCollection);
        },

        parse: function (response, options) {
            return Helpers.convertKeysToCamelCase(response);
        },

        initAlertSeverity: function () {
            var thresholdAlertSeverityId = this.get('thresholdAlertSeverityId');

            // Name of severity initialized in model in case if severity id specified.
            if (thresholdAlertSeverityId) {
                var alertSeverity = app.collections.alertSeverities.where({ id: thresholdAlertSeverityId })[0];

                if (alertSeverity) {
                    this.set('thresholdAlertSeverityName', alertSeverity.get('name'));
                }
            }
        },

        isValid: function () {
            var isValid = false;

            this.get('thresholds').each(function (threshold) {
                var value = threshold.get('value');

                if (value != null && value !== 'DoesNotApply') {
                    isValid = true;
                }
            });

            return isValid;
        },

        getBranchJson: function () {
            return {
                id: this.get('id'),
                nextProtocolElementId: this.get('nextProtocolElementId'),
                conditions: this.getConditionsJson(),
                thresholdAlertSeverityId: this.get('thresholdAlertSeverityId')
            }
        },

        getConditionsJson: function () {
            var conditions = [],
                self = this;

            this.get('thresholds').each(function (threshold) {
                var type = threshold.get('type');

                switch (threshold.get('value')) {
                    case 'Low':
                        conditions.push({
                            operand: type,
                            operator: 2,
                            value: "Patient" + self.measurementThresholdCodesMap[type] + "LowerLimit"
                        });
                        break;

                    case 'High':
                        conditions.push({
                            operand: type,
                            operator: 4,
                            value: "Patient" + self.measurementThresholdCodesMap[type] + "HigherLimit"
                        });
                        break;

                    case 'WithinLimit':
                        conditions.push({
                            operand: type,
                            operator: 3,
                            value: "Patient" + self.measurementThresholdCodesMap[type] + "HigherLimit"
                        });
                        conditions.push({
                            operand: type,
                            operator: 5,
                            value: "Patient" + self.measurementThresholdCodesMap[type] + "LowerLimit"
                        });
                        break;

                    default:
                }
            });

            return conditions;
        },

        measurementThresholdNamesMap: {
            1: 'Blood Glucose',
            2: 'Oxygen Saturation',
            3: 'Blood Pressure',
            4: 'Heart Rate',
            5: 'Temperature',
            6: 'Forced Expiatory Volume',
            7: 'Peak Expiratory Flow',
            8: 'Weight',
            11: 'Walking Steps',
            12: 'Running Steps',
            13: 'Forced Vital Capacity',
            14: 'FEV1/FVC'
        },

        measurementThresholdCodesMap: {
            1: 'BloodGlucose',
            2: 'OxygenSaturation',
            3: 'BloodPressure',
            4: 'HeartRate',
            5: 'Temperature',
            6: 'ForcedExpiratoryVolume',
            7: 'PeakExpiratoryFlow',
            8: 'Weight',
            11: 'WalkingSteps',
            12: 'RunningSteps',
            13: 'ForcedVitalCapacity',
            14: 'FEV1_FVC'
        },

        // MeasurementTypeId: [ MeasurementThresholds ]
        measurementThresholdsMap: {
            '1': [1],
            '2': [2, 4],
            '3': [3, 4],
            '4': [5],
            '5': [6, 7, 13, 14],
            '6': [8],
            '7': [11, 12]
        },

        measurementOperatorsMap: {
            2: 'Low',
            3: 'WithinLimit',
            4: 'High',
            5: 'WithinLimit'
        }
    });
});