'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Root/Thresholds/AppNamespace',
    'Controllers/Root/Thresholds/Models/ThresholdModel',
    'Controllers/Helpers',
    'Controllers/Constants'
], function ($, _, Backbone, app, ThresholdModel, Helpers, Constants) {
    return Backbone.Collection.extend({

        model: ThresholdModel,

        hasAlerts: false,

        initialize: function () {
            _.extend(this, new Backbone.Memento(this));
        },

        url: function () {
            return (app.siteId ? '/' + app.siteId : '') + app.crud.get;
        },

        sync: function (method, model, options) {
            if (method == 'read') {
                options.url = (app.siteId ? '/' + app.siteId : '') +
                    (app.crud.get) +
                    (app.patientId ?
                        '?patientId=' + app.patientId :
                        (app.mode === Constants.thresholdAppModes.DEFAULT ?
                            '?defaultType=Customer' : ''));
            }

            return Backbone.sync(method, model, options);
        },

        parse: function (response, options) {
            var self = this;

            response = Helpers.convertKeysToCamelCase(response);

            if (!response.thresholds) {
                response.thresholds = response.defaultThresholds;
            }

            _.each(response.thresholds, function (item) {
                item.name = Helpers.getVitalType(item.name - 1);
                item.unit = Helpers.getUnitType(item.unit - 1);
            });

            if (response.alertSeverities.length) {
                this.hasAlerts = true;

                var AlertSeveritiesCollection = Backbone.Collection.extend({

                    sort_key: 'severity',

                    direction: 1,

                    comparator: function (item) {
                        return this.direction * item.get(this.sort_key);
                    },

                    sortDirection: function (direction) {
                        this.direction = direction == 'Desc' ? -1 : 1;
                        this.sort();
                    }

                });

                app.collections.alertSeveritiesCollection = new AlertSeveritiesCollection(response.alertSeverities);

                var ThresholdsSeveritiesCollection = Backbone.Collection.extend({
                    model: ThresholdModel,

                    initialize: function () {
                        _.extend(this, new Backbone.Memento(this));
                    },

                    url: function () {
                        return (app.siteId ? '/' + app.siteId : '') + (app.crud.get);
                    }
                });

                app.collections.thresholdsSeveritiesCollections = {};

                $(response.alertSeverities).each(function () {

                    var alertSeverityId = $(this)[0].id,
                        tempCollection = self.getThresholdsCollection();

                    _.each(tempCollection, function (item) {
                        var responseItem = _.filter(response.thresholds, function (threshold) {
                            var modeCondition = false;

                            switch (app.mode) {
                                case Constants.thresholdAppModes.PATIENT:
                                {
                                    modeCondition = response.patientId && threshold.patientId === response.patientId;

                                    break;
                                }

                                case Constants.thresholdAppModes.DEFAULT:
                                {
                                    modeCondition = threshold.defaultType === Constants.defaultThresholdTypes.DEFAULT;

                                    break;
                                }

                                case Constants.thresholdAppModes.CONDITION:
                                {
                                    modeCondition = threshold.defaultType === Constants.defaultThresholdTypes.CONDITION &&
                                        threshold.conditionId &&
                                        threshold.conditionId === app.conditionId;

                                    break;
                                }

                                default:
                                    modeCondition = response.patientId && threshold.patientId === response.patientId;
                            }

                            return threshold.name === item.name &&
                                modeCondition &&
                                threshold.alertSeverity != undefined &&
                                threshold.alertSeverity.id === alertSeverityId;
                        })[0];

                        if (app.mode === Constants.thresholdAppModes.DEFAULT) {
                            item.defaultType = Constants.defaultThresholdTypes.DEFAULT;
                        }

                        if (app.mode === Constants.thresholdAppModes.CONDITION) {
                            item.defaultType = Constants.defaultThresholdTypes.CONDITION;
                            item.conditionId = app.conditionId;
                        }

                        item.alertSeverityId = alertSeverityId;

                        if (responseItem) {
                            item = _.extend(item, responseItem);
                        }

                        if (app.mode !== Constants.thresholdAppModes.DEFAULT) {

                            item.defaultThreshold = self.detectDefaultThreshold(response.thresholds, item.name, response.patientConditions, alertSeverityId);
                            
                        }
                    });

                    app.collections.thresholdsSeveritiesCollections[alertSeverityId] = new ThresholdsSeveritiesCollection(tempCollection);
                });

                return false;
            } else {
                var collection = self.getThresholdsCollection();

                _.each(collection, function (item) {
                    var modeCondition;

                    switch (app.mode) {
                        case Constants.thresholdAppModes.PATIENT:
                        {
                            modeCondition = { name: item.name, patientId: response.patientId };

                            break;
                        }

                        case Constants.thresholdAppModes.DEFAULT:
                        {
                            modeCondition = { name: item.name, defaultType: Constants.defaultThresholdTypes.DEFAULT };

                            break;
                        }

                        case Constants.thresholdAppModes.CONDITION:
                        {
                            modeCondition = {
                                name: item.name,
                                defaultType: Constants.defaultThresholdTypes.CONDITION,
                                conditionId: app.conditionId
                            };

                            break;
                        }

                        default:
                            modeCondition = { name: item.name, patientId: response.patientId };
                    }

                    var responseItem = _.findWhere(response.thresholds, modeCondition);

                    if (app.mode === Constants.thresholdAppModes.DEFAULT) {
                        item.defaultType = Constants.defaultThresholdTypes.DEFAULT;
                    }

                    if (app.mode === Constants.thresholdAppModes.CONDITION) {
                        item.defaultType = Constants.defaultThresholdTypes.CONDITION;
                        item.conditionId = app.conditionId;
                    }

                    if (responseItem) {
                        item = _.extend(item, responseItem);
                    }

                    if (app.mode !== Constants.thresholdAppModes.DEFAULT) {

                        item.defaultThreshold = self.detectDefaultThreshold(response.thresholds, item.name, response.patientConditions, null);

                    }
                });

                return collection;
            }
        },


        //determining default threshold logic
        detectDefaultThreshold: function (thresholds, vitalName, conditions, severityId) {            

            //0. Select the patient condition ids
            var patientConditionsIds = _.map(conditions, function (condition) {
                return condition.id;
            });

            //1. find condition thresholds using patient's condition ids
            var conditionsThresholds = _.filter(thresholds, function (threshold) {
                return threshold.name === vitalName &&
                    threshold.defaultType === Constants.defaultThresholdTypes.CONDITION &&
                    patientConditionsIds.indexOf(threshold.conditionId) >= 0 &&
                    (!severityId || (threshold.alertSeverity != undefined && threshold.alertSeverity.id === severityId));

            });

            //1.1. If there are condition thresholds then detect minValue, minValueSource, maxValue, maxValueSource
            if (conditionsThresholds && conditionsThresholds.length > 0) {

                var lowThreshold = _.max(conditionsThresholds, function (threshold) { return threshold.minValue; });
                var conditionOfLowThreshold = _.findWhere(conditions, { id: lowThreshold.conditionId });
                var highThreshold = _.min(conditionsThresholds, function (threshold) { return threshold.maxValue; });
                var conditionOfHightThreshold = _.findWhere(conditions, { id: highThreshold.conditionId });

                return {
                    minValue: lowThreshold.minValue,
                    minValueSource: "Source: Condition (" + (conditionOfLowThreshold ? conditionOfLowThreshold.name : "unknown") + ")",
                    maxValue: highThreshold.maxValue,
                    maxValueSource: "Source: Condition (" + (conditionOfHightThreshold ? conditionOfHightThreshold.name : "unknown") + ")"
                }

            }
            //2. If there are no condition thresholds then find customer default thresholds
            else
            {
                var customerThreshold = _.filter(thresholds, function (threshold) {
                    return threshold.name === vitalName &&
                        threshold.defaultType === Constants.defaultThresholdTypes.DEFAULT &&
                        (!severityId || (threshold.alertSeverity != undefined && threshold.alertSeverity.id === severityId));
                })[0];

                //2.1. If there are customer default thresholds then detect minValue, minValueSource, maxValue, maxValueSource
                if (customerThreshold) {

                    return {
                        minValue: customerThreshold.minValue,
                        minValueSource: "Source: Organization Settings",
                        maxValue: customerThreshold.maxValue,
                        maxValueSource: "Source: Organization Settings)"
                    }
                }
            }
        },

        getThresholdsCollection: function () {
            var thresholdCollection = Helpers.getThresholdCollection(),
                result = [];

            for (var key in thresholdCollection) {
                if (thresholdCollection.hasOwnProperty(key) && key !== 'BodyMassIndex' && key !== 'BloodPressure') {
                    result.push($.extend({}, thresholdCollection[key], {
                        patientId: app.patientId,
                        isDisplay: true
                    }));
                }
            }

            return result;
        }
    });
});