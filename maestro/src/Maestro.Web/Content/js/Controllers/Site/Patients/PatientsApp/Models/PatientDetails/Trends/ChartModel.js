'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'backbone-nested',
    'Controllers/Helpers',
    'Controllers/Site/Patients/PatientsApp/AppNamespace',
    'moment'
], function ($, _, Backbone, BackboneNested, Helpers, app, moment) {
    return Backbone.NestedModel.extend({

        requestDateFormat: 'YYYY-MM-DD',

        url: function () {
            var url;

            if (this.chartType === 'vital') {
                url = '/' + app.siteId + '/Patients/VitalsChart' +
                    '?patientId=' + this.patientId +
                    '&measurement=' + this.chartName +
                    '&startDate=' + this.startDate.format(this.requestDateFormat) +
                    '&endDate=' + this.endDate.format(this.requestDateFormat) +
                    '&pointsPerChart=' + this.pointsPerChart;
            } else {
                url = '/' + app.siteId + '/Patients/AssessmentChart' +
                    '?patientId=' + this.patientId +
                    '&questionId=' + this.chartName +
                    '&startDate=' + this.startDate.format(this.requestDateFormat) +
                    '&endDate=' + this.endDate.format(this.requestDateFormat);
            }

            return url;
        },

        initialize: function (attrs, options) {
            if (!options) throw 'Options required';

            this.patientId = options.patientId;
            this.chartType = options.chartType;
            this.chartName = options.chartName;
            this.chartLabel = options.chartLabel;
            this.startDate = moment(options.startDate);
            this.endDate = moment(options.endDate);
            this.pointsPerChart = 24;

            _.extend(this, new Backbone.Memento(this));
        },

        setDaterange: function (daterangeModel) {
            this.startDate = moment(daterangeModel.get('startDate'));
            this.endDate = moment(daterangeModel.get('endDate'));
        },

        setSettings: function (chartSettings, options) {
            this.set({ chartSettings: chartSettings }, options);
        },

        fetch: function (options) {
            var self = this;
            self.isFetching = true;
            var tempSuccessFunc = options.success;
            if (tempSuccessFunc) {
                options.success = function () {
                    self.isFetching = false;
                    self.isFetched = true;
                    self.trigger('fetch:success');
                    if (_.isFunction(tempSuccessFunc)) tempSuccessFunc.apply(this, arguments);
                };
            }
            var tempErrorFunc = options.error;
            if (tempErrorFunc) {
                options.error = function () {
                    self.isFetching = false;
                    self.trigger('fetch:error');
                    if (_.isFunction(tempErrorFunc)) tempErrorFunc.apply(this, arguments);
                };
            }

            Backbone.NestedModel.prototype.fetch.apply(this, arguments);
        },

        parse: function (response) {
            var self = this;
            response = Helpers.convertKeysToCamelCase(response);

            if (this.chartType === 'vital') {
                this.parseVitalsData(response);
            } else {
                this.parseAssessmentsData(response);
            }

            response.chartRange.startDate = moment.parseZone(response.chartRange.startDate);
            response.chartRange.endDate = moment.parseZone(response.chartRange.endDate);

            return response;
        },

        parseVitalsData: function (response) {
            _.each(response.chartData, function (item) {
                item.x = moment.parseZone(item.date).startOf('day').toDate();
            });

            response.chartData = _.sortBy(response.chartData, function (item) { return item.x.getTime(); });
            response.chartData = _.uniq(response.chartData, function (item) { return item.x.getTime(); });

            var chartDataTmp = {
                avg: [],
                min: [],
                max: [],
                thresholds: {}
            };
            _.each(response.chartData, function (item) {

                _.each(item.readings, function (reading) {
                    reading.date = moment.parseZone(reading.date).valueOf();
                });

                chartDataTmp.avg.push({
                    x: item.x,
                    y: item.avgReading,
                    avg: item.avgReading,
                    min: item.minReading,
                    max: item.maxReading,
                    unit: item.unit,
                    count: item.count,
                    readings: item.readings
                });
                chartDataTmp.min.push({
                    x: item.x,
                    y: item.minReading,
                    avg: item.avgReading,
                    min: item.minReading,
                    max: item.maxReading,
                    unit: item.unit,
                    count: item.count,
                    readings: item.readings
                });
                chartDataTmp.max.push({
                    x: item.x,
                    y: item.maxReading,
                    avg: item.avgReading,
                    min: item.minReading,
                    max: item.maxReading,
                    unit: item.unit,
                    count: item.count,
                    readings: item.readings
                });
            });

            //generate thresholds series
            var tempDate = this.startDate.clone();
            var delta = (this.endDate.valueOf() - this.startDate.valueOf()) / this.pointsPerChart;

            if (this.startDate.isBefore(this.endDate)) {
                while (!tempDate.isAfter(this.endDate)) {
                    _.each(response.thresholds, function (threshold) {
                        var severityData = threshold.alertSeverity || { name: 'default' };

                        if (!chartDataTmp.thresholds[threshold.id]) {
                            chartDataTmp.thresholds[threshold.id] = { min: [], max: [] };
                        }

                        chartDataTmp.thresholds[threshold.id].min.push({
                            x: tempDate.clone(),
                            y: threshold.minValue,
                            data: threshold,
                            name: severityData.name,
                            severity: severityData.severity,
                            color: severityData.colorCode || '#f00'        //nocolor threshold as red
                        });
                        chartDataTmp.thresholds[threshold.id].max.push({
                            x: tempDate.clone(),
                            y: threshold.maxValue,
                            data: threshold,
                            name: severityData.name,
                            severity: severityData.severity,
                            color: severityData.colorCode || '#f00'        //nocolor threshold as red
                        });
                    });

                    tempDate.add(delta, 'ms');
                }
            }

            response.chartData = chartDataTmp;
            response.chartLabel = response.chartName;
            this.chartLabel = response.chartName;

            if (!(response.chartSettings || this.get('chartSettings'))) {
                response.chartSettings = {};
            }
        },

        parseAssessmentsData: function (response) {
            var self = this;

            var answers = _.pluck(response.answers, 'answerId');
            var chartDataTmp = [];

            _.each(response.chartData, function (item) {
                item.x = moment.parseZone(item.date).startOf('day').toDate();

                _.each(item.values, function (valueObj) {

                    if (valueObj.count > 0) {
                        var dataItem = {
                            x: item.x,
                            y: _.indexOf(answers, valueObj.answerId),
                            count: valueObj.count,
                            readingsDaterange: {},
                            readings: []
                        };

                        if (valueObj.readingsDateRange) {
                            dataItem.readingsDaterange.startDate = moment.parseZone(valueObj.readingsDateRange.startDate);
                            dataItem.readingsDaterange.endDate = moment.parseZone(valueObj.readingsDateRange.endDate);
                        }

                        if (_.isArray(valueObj.readings)) {
                            _.each(valueObj.readings, function (reading) {
                                reading.date = moment.parseZone(reading.date);
                            });
                            dataItem.readings = valueObj.readings;
                        }
                        chartDataTmp.push(dataItem);
                    }

                });
            });

            response.chartData = chartDataTmp;
            response.chartLabel = response.chartName;
            this.chartLabel = response.chartName;
        }

    });
});