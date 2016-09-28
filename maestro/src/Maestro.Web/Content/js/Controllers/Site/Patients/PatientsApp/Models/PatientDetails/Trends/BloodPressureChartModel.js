'use strict';

define([
    'jquery',
    'underscore',
    'async',
    'backbone',
    'backbone-nested',
    'Controllers/Helpers',
    'Controllers/Site/Patients/PatientsApp/AppNamespace',
    'moment',
    './ChartModel'
], function ($, _, async, Backbone, BackboneNested, Helpers, app, moment, ChartModel) {
    return ChartModel.extend({

        initialize: function (data, options) {
            options = options || {};

            this.models = {
                systolic: new ChartModel(null, {
                    patientId: options.patientId,
                    chartType: 'vital',
                    chartName: 'SystolicBloodPressure',
                    startDate: options.startDate,
                    endDate: options.endDate
                }),
                diastolic: new ChartModel(null, {
                    patientId: options.patientId,
                    chartType: 'vital',
                    chartName: 'DiastolicBloodPressure',
                    startDate: options.startDate,
                    endDate: options.endDate
                })
            };

            ChartModel.prototype.initialize.apply(this, arguments);
        },

        setDaterange: function (daterangeModel) {
            this.startDate = moment(daterangeModel.get('startDate'));
            this.endDate = moment(daterangeModel.get('endDate'));
            this.models.systolic.setDaterange(daterangeModel);
            this.models.diastolic.setDaterange(daterangeModel);
        },

        setSystolicSettings: function (chartSettings, options) {
            var settings = this.get('chartSettings') || {};
            settings.systolic = chartSettings;
            this.set({ chartSettings: settings }, options);
        },

        setDiastolicSettings: function (chartSettings, options) {
            var settings = this.get('chartSettings') || {};
            settings.diastolic = chartSettings;
            this.set({ chartSettings: settings }, options);
        },

        setSettings: function (chartsSettings, options) {
            this.setSystolicSettings(chartsSettings.systolic, options);
            this.setDiastolicSettings(chartsSettings.diastolic, options);
        },

        fetch: function (options) {
            var self = this;
            self.isFetching = true;

            async.parallel([function (cb) {

                self.models.systolic.fetch({
                    success: function (data) { cb(null, data); },
                    error: function (err) { cb(err); }
                });

            }, function (cb) {

                self.models.diastolic.fetch({
                    success: function (data) { cb(null, data); },
                    error: function (err) { cb(err); }
                });

            }], function (err, models) {

                if (err) {
                    if (_.isFunction(options.error)) {
                        options.error();
                    }
                    return;
                }

                //self.extendChartData();

                var chartData = {
                    systolicBloodPressure: models[0].get('chartData'),
                    diastolicBloodPressure: models[1].get('chartData')
                };
                var thresholds = {
                    systolic: models[0].get('thresholds'),
                    diastolic: models[1].get('thresholds')
                };
                self.set('chartData', chartData, { silent: true });
                self.set('thresholds', thresholds, { silent: true });
                self.set('chartRange', models[0].get('chartRange'), { silent: true });
                self.set('chartName', 'BloodPressure', { silent: true });

                self.isFetching = false;
                self.isFetched = true;
                self.trigger('change');     //TODO: clarify events

                if (_.isFunction(options.success)) {
                    options.success(self);
                }

            });

        },

        extendChartData: function () {
            var self = this;
            var systolicData = self.models.systolic.get('chartData');
            var diastolicData = self.models.diastolic.get('chartData');

            _.each(systolicData.avg, function (pointData, index) {
                pointData.diastolicPoint = diastolicData.avg[index];
                diastolicData.avg[index].systolicPoint = pointData;
            });
            _.each(systolicData.min, function (pointData, index) {
                pointData.diastolicPoint = diastolicData.min[index];
                diastolicData.min[index].systolicPoint = pointData;
            });
            _.each(systolicData.max, function (pointData, index) {
                pointData.diastolicPoint = diastolicData.max[index];
                diastolicData.max[index].systolicPoint = pointData;
            });
        }
    });
});