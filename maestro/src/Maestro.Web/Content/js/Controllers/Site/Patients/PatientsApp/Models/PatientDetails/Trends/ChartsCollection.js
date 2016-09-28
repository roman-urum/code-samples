'use strict';

define([
    'jquery',
    'underscore',
    'async',
    'backbone',
    'Controllers/Site/Patients/PatientsApp/AppNamespace',
    'Controllers/Site/Patients/PatientsApp/Models/PatientDetails/Trends/QuestionsCollection',
    'Controllers/Site/Patients/PatientsApp/Models/PatientDetails/Trends/DaterangeModel',
    'Controllers/Site/Patients/PatientsApp/Models/PatientDetails/Trends/ChartModel',
    'Controllers/Site/Patients/PatientsApp/Models/PatientDetails/Trends/BloodPressureChartModel',
    'Controllers/Helpers',
    'moment'
], function ($, _, async, Backbone, app, QuestionsCollection, DaterangeModel, ChartModel, BloodPressureChartModel, Helpers, moment) {
    return Backbone.Collection.extend({

        types: {
            VITAL: 1,
            ASSESSMENT: 2
        },

        vitals: [
            {
                name: 'Weight',
                label: 'Weight'
            }, {
                name: 'BloodPressure',
                label: 'Blood Pressure'
            }, {
                name: 'HeartRate',
                label: 'Heart Rate'
            }, {
                name: 'BloodGlucose',
                label: 'Blood Glucose'
            }, {
                name: 'Temperature',
                label: 'Temperature'
            }, {
                name: 'PeakExpiratoryFlow',
                label: 'Peak Expiratory Flow'
            }, {
                name: 'ForcedExpiratoryVolume',
                label: 'Forced Expiratory Volume'
            }, {
                name: 'ForcedVitalCapacity',
                label: 'Forced Vital Capacity'
            }, {
                name: 'FEV1_FVC',
                label: 'FEV1/FVC'
            }, {
                name: 'WalkingSteps',
                label: 'Walking Steps'
            }, {
                name: 'RunningSteps',
                label: 'Running Steps'
            }, {
                name: 'OxygenSaturation',
                label: 'Oxygen Saturation'
            }
        ],

        url: function () {
            return '/' + app.siteId + '/patients/trendsSettings' +
                '?patientId=' + this.patientId;
        },

        initialize: function (models, options) {
            if (!options) throw 'Options required';
            var self = this;

            this.isSettingsFetched = false;

            this.patientId = options.patientId;
            this.initMetadata();

            this.listenTo(this.daterangeModel, 'change', function () {
                self.applyDaterange();
                self.fetch();
            });

            this.setCharts(options.charts);
        },

        initMetadata: function () {
            this.daterangeModel = new DaterangeModel();

            var models = _.map(this.vitals, function (vitalMeta) {
                return new Backbone.Model(vitalMeta);
            });
            this.vitalsCollection = new Backbone.Collection(models);

            this.questionsCollection = new QuestionsCollection();
            this.questionsCollection.fetch();
        },

        setCharts: function (chartsMeta) {
            var self = this;
            var models = [];
            var neededChartNames = _.pluck(chartsMeta, 'name');

            //remove charts models
            var modelsToRemove = this.reject(function (model) {
                return _.contains(neededChartNames, model.chartName);
            });
            this.remove(modelsToRemove);

            //add charts models
            var existingChartNames = _.pluck(this.models, 'chartName');
            var newChartsMeta = _.filter(chartsMeta, function (chartMeta) {
                return !_.contains(existingChartNames, chartMeta.name);
            });

            _.each(newChartsMeta, function (chartMeta, index) {
                var vitals = _.pluck(self.vitals, 'name');
                var chartName = chartMeta.name;
                var chartLabel = chartMeta.label;
                var isVital = _.contains(vitals, chartName);
                var modelClass;

                if (chartName === 'BloodPressure') {
                    modelClass = BloodPressureChartModel;
                } else {
                    modelClass = ChartModel;
                }

                //for assessment charts chartName is equal to questionId
                var model = new modelClass(null, {
                    patientId: self.patientId,
                    chartType: isVital ? 'vital' : 'assessment',
                    chartName: chartName,
                    chartLabel: chartLabel,
                    startDate: self.startDate,
                    endDate: self.endDate
                });

                models.push(model);
            });

            this.add(models, { silent: true });
            this.models = _.sortBy(this.models, function (model) {
                var meta = _.findWhere(chartsMeta, { name: model.chartName });
                var index = chartsMeta.indexOf(meta);
                if (index < 0) index = self.length + 1;

                return index;
            });
        },

        applyDaterange: function () {
            this.startDate = moment(this.daterangeModel.get('startDate'));
            this.endDate = moment(this.daterangeModel.get('endDate'));
            this.invoke('setDaterange', this.daterangeModel);
        },

        fetchSettings: function (options) {
            options = options || {};
            var self = this;

            async.waterfall([
                function (cb) {

                    self.fetchSettingsData({
                        success: function (chartsSettings) {
                            cb(null, chartsSettings);
                        },
                        error: function (err) {
                            cb(err);
                        }
                    });

                },
                function (chartsSettings, cb) {

                    //apply daterange
                    var daterangeCase = chartsSettings.dateRange || DaterangeModel.prototype.DEFAULT_DATERANGE_CASE;
                    if (chartsSettings.dateRange) {
                        var startDate = moment(chartsSettings.dateRange.startDate);
                        var endDate = moment(chartsSettings.dateRange.endDate);
                        daterangeCase = DaterangeModel.prototype.CUSTOM_DATERANGE_CASE;
                    } else if (chartsSettings.lastDays) {
                        daterangeCase = chartsSettings.lastDays;
                    }
                    self.daterangeModel.setDaterangeCase(daterangeCase, startDate, endDate, { silent: true });
                    self.applyDaterange();

                    //init charts models
                    var chartsMeta = [];
                    _.each(chartsSettings.charts, function (chartSettings) {
                        if (chartSettings.name === 'SystolicBloodPressure' || chartSettings.name === 'DiastolicBloodPressure') {

                            if (!_.findWhere(chartsMeta, {name: 'BloodPressure'})) { //check if BloodPressure chart was already added
                                chartsMeta.push({
                                    name: 'BloodPressure',
                                    label: 'Blood Pressure'
                                });
                            }

                        } else {

                            //get label from predefined local vitals metadata
                            var label = '';
                            if (chartSettings.type === 1) {     //1 = vital, 2 = assessment
                                var vitalMeta = _.findWhere(self.vitals, { name: chartSettings.name }) || {};
                                label = vitalMeta.label;
                            }

                            chartsMeta.push({
                                name: chartSettings.name || chartSettings.questionId,
                                label: label,
                                settings: chartSettings
                            });

                        }
                    });
                    self.setCharts(chartsMeta);

                    //apply charts settings
                    self.each(function (chartModel) {
                        var chartName = chartModel.chartName;
                        if (chartName === 'BloodPressure') {
                            var systolicSettings = _.findWhere(chartsSettings.charts, { name: 'SystolicBloodPressure' }) || {};
                            var diastolicSettings = _.findWhere(chartsSettings.charts, { name: 'DiastolicBloodPressure' }) || {};
                            chartModel.setSystolicSettings(systolicSettings);
                            chartModel.setDiastolicSettings(diastolicSettings);
                        } else {
                            var chartSettings = _.findWhere(chartsSettings.charts, { name: chartName });
                            chartModel.setSettings(chartSettings);
                        }
                    });

                    cb(null, chartsSettings);

                }
            ], function (err, result) {
                self.isSettingsFetched = true;
                if (err && _.isFunction(options.error)) return options.error();
                if (_.isFunction(options.success)) options.success();
            });
        },

        fetchSettingsData: function (options) {
            $.ajax({
                url: this.url(),
                method: 'GET',
                success: function (result) {
                    if (_.isEmpty(result)) result = {};

                    result = Helpers.convertKeysToCamelCase(result);
                    if (_.isFunction(options.success)) options.success(result);
                },
                error: options.error
            });
        },

        fetch: function (options) {
            options = options || {};

            var self = this;
            var workers = [];
            var models = this.models;

            //filter models which to fetch
            if (options.onlyNew) {
                models = _.filter(models, function (model) { return !model.isFetched; });
            }

            //make requests parallel
            _.each(models, function (model) {
                workers.push(function (cb) {
                    model.fetch({
                        success: function (data) { cb(null, data); },
                        error: function (err) { cb(err); }
                    });
                });
            });

            app.vent.trigger('patient:trends:loading');

            //launch parallel requests
            async.parallel(workers, function (errModel, results) {
                self.isFetched = true;
                self.trigger('change');     //TODO: clarify events

                if (errModel && _.isFunction(options.error)) {
                    options.error(self);
                } else if (_.isFunction(options.success)) {
                    options.success(self);
                }

                app.vent.trigger('patient:trends:loaded');
            });
        },

        save: function (options) {
            options = options || {};
            var settings = {};

            //collect daterange
            var lastDays = this.daterangeModel.get('daterangeCase');
            if (this.daterangeModel &&
                lastDays &&
                lastDays !== '0') {
                settings.lastDays = lastDays;
            } else {
                settings.dateRange = {
                    startDate: this.startDate.format('YYYY-MM-DD'),
                    endDate: this.endDate.format('YYYY-MM-DD')
                };
            }

            //collect charts
            settings.charts = [];
            this.each(function (chartModel) {
                var chartSettings = {};
                if (chartModel.chartName === 'BloodPressure') {

                    chartSettings = chartModel.get('chartSettings');

                    var systolicSettings = _.extend({
                        type: 1,
                        name: 'SystolicBloodPressure'
                    }, chartSettings.systolic);
                    settings.charts.push(systolicSettings);

                    var diastolicSettings = _.extend({
                        type: 1,
                        name: 'DiastolicBloodPressure'
                    }, chartSettings.diastolic);
                    settings.charts.push(diastolicSettings);

                } else {

                    chartSettings = _.extend({
                        type: chartModel.chartType === 'vital' ? 1 : 2
                    }, chartModel.get('chartSettings'));

                    if (chartModel.chartType === 'vital') {
                        chartSettings.name = chartModel.chartName;
                    } else {
                        chartSettings.questionId = chartModel.chartName;
                    }
                    settings.charts.push(chartSettings);

                }
            });

            //save charts
            $.ajax({
                url: this.url(),
                method: 'POST',
                dataType: 'json',
                data: settings,
                success: options.success,
                error: options.error
            });
        }

    });
});