'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'highcharts',
    'Controllers/Site/Patients/PatientsApp/AppNamespace',
    'Controllers/Helpers',
    'moment',
    './../../BaseMaestroView',
    './ChartView',
    './VitalChartView'
], function (
    $,
    _,
    Backbone,
    HighchartsLib,
    app,
    Helpers,
    moment,
    BaseMaestroView,
    ChartView,
    VitalChartView
) {
    return VitalChartView.extend({

        template: _.template($('#patientTrendsBloodPressureChartViewTemplate').html()),

        render: function () {
            var self = this;

            var chartSettings = this.model.get('chartSettings');
            if (_.isEmpty(chartSettings) || _.isEmpty(chartSettings.systolic) || _.isEmpty(chartSettings.diastolic)) {
                this.model.set({
                    chartSettings: {
                        systolic: { showAverage: true },
                        diastolic: { showAverage: true }
                    }
                }, { silent: true });
            }

            var data = _.extend({
                chartLabel: this.model.chartLabel,
                chartData: null,
                thresholds: {}
            }, this.model.toJSON());
            this.$el.html(this.template(data));

            //call chart init during next tick of event loop
            //to let browser render just inserted html first
            //after that highcharts can use elements dimensions
            setTimeout(function () {
                self.renderSpinner();
                self.initChart();
                self.renderSettings();
            }, 0);
        },

        renderSettings: function () {
            var chartSettings = this.model.get('chartSettings') || {};
            var systolicSettings = chartSettings.systolic || {};
            var showSystolicThresholdIds = systolicSettings.showThresholdIds || [];
            var diastolicSettings = chartSettings.diastolic || {};
            var showDiastolicThresholdIds = diastolicSettings.showThresholdIds || [];

            this.$('.js-toggle-avg').filter('[data-level="systolic"]').prop('checked', !!systolicSettings.showAverage);
            this.$('.js-toggle-min').filter('[data-level="systolic"]').prop('checked', !!systolicSettings.showMin);
            this.$('.js-toggle-max').filter('[data-level="systolic"]').prop('checked', !!systolicSettings.showMax);
            this.$('.js-toggle-threshold').filter('[data-level="systolic"]').each(function (index, el) {
                var thresholdId = $(el).data('threshold-id');
                $(el).prop('checked', _.contains(showSystolicThresholdIds, thresholdId));
            });

            this.$('.js-toggle-avg').filter('[data-level="diastolic"]').prop('checked', !!diastolicSettings.showAverage);
            this.$('.js-toggle-min').filter('[data-level="diastolic"]').prop('checked', !!diastolicSettings.showMin);
            this.$('.js-toggle-max').filter('[data-level="diastolic"]').prop('checked', !!diastolicSettings.showMax);
            this.$('.js-toggle-threshold').filter('[data-level="diastolic"]').each(function (index, el) {
                var thresholdId = $(el).data('threshold-id');
                $(el).prop('checked', _.contains(showDiastolicThresholdIds, thresholdId));
            });
        },

        initChart: function () {
            var self = this;
            var colors = this.colors;

            var chartData = this.model.get('chartData') || {};
            var systolicData = chartData.systolicBloodPressure || {};
            var diastolicData = chartData.diastolicBloodPressure || {};

            var chartSettings = this.model.get('chartSettings') || {};
            var systolicSettings = chartSettings.systolic || {};
            var diastolicSettings = chartSettings.diastolic || {};

            var daysPerTick = this.calculateDaysPerTick();
            var unit = _.isEmpty(systolicData.avg) ? '' : systolicData.avg[0].unit;

            var minX = this.startDate.clone().subtract(3, 'hours').valueOf();
            var maxX = this.endDate.clone().valueOf();

            var series = [];
            if (systolicSettings.showAverage) {
                var avgSystolicSeries = this.createLineMetadata({ data: systolicData.avg, zIndex: 3 });
                series.push(avgSystolicSeries);
            }
            if (systolicSettings.showMin) {
                var minSystolicSeries = this.createLineMetadata({ data: systolicData.min, color: colors.DATA_EXTREME, dashStyle: 'shortdash', zIndex: 2 });
                series.push(minSystolicSeries);
            }
            if (systolicSettings.showMax) {
                var maxSystolicSeries = this.createLineMetadata({ data: systolicData.max, color: colors.DATA_EXTREME, dashStyle: 'shortdash', zIndex: 2 });
                series.push(maxSystolicSeries);
            }

            var shownSystolicThresholds = _.filter(systolicData.thresholds, function (threshold, id) { return _.contains(systolicSettings.showThresholdIds, id); });
            _.each(shownSystolicThresholds, function (threshold) {
                var systolicThresholdMinSeries = self.createThresholdMetadata({ data: threshold.min, color: threshold.min[0].color });
                series.push(systolicThresholdMinSeries);

                var systolicThresholdMaxSeries = self.createThresholdMetadata({ data: threshold.max, color: threshold.max[0].color });
                series.push(systolicThresholdMaxSeries);
            });

            if (diastolicSettings.showAverage) {
                var avgDiastolicSeries = this.createLineMetadata({ data: diastolicData.avg, color: colors.DATA_SECONDARY, zIndex: 3 });
                series.push(avgDiastolicSeries);
            }
            if (diastolicSettings.showMin) {
                var minDiastolicSeries = this.createLineMetadata({ data: diastolicData.min, color: colors.DATA_SECONDARY_EXTREME, dashStyle: 'shortdash', zIndex: 2 });
                series.push(minDiastolicSeries);
            }
            if (diastolicSettings.showMax) {
                var maxDiastolicSeries = this.createLineMetadata({ data: diastolicData.max, color: colors.DATA_SECONDARY_EXTREME, dashStyle: 'shortdash', zIndex: 2 });
                series.push(maxDiastolicSeries);
            }

            var shownDiastolicThresholds = _.filter(diastolicData.thresholds, function (threshold, id) { return _.contains(diastolicSettings.showThresholdIds, id); });
            _.each(shownDiastolicThresholds, function (threshold) {
                var diastolicThresholdMinSeries = self.createThresholdMetadata({ data: threshold.min, color: threshold.min[0].color });
                series.push(diastolicThresholdMinSeries);

                var diastolicThresholdMaxSeries = self.createThresholdMetadata({ data: threshold.max, color: threshold.max[0].color });
                series.push(diastolicThresholdMaxSeries);
            });

            this.$('.chart').highcharts({
                chart: {
                    marginLeft: 60,
                    marginRight: 25,
                    spacingTop: 30,
                    spacingBottom: 5
                },
                title: { text: null },
                xAxis: {
                    type: 'datetime',
                    gridLineWidth: 1,
                    min: minX,
                    max: maxX,
                    tickInterval: 1000 * 60 * 60 * 24 * daysPerTick,
                    labels: {
                        formatter: function () {
                            return moment(this.value).format("MM/DD");
                        }
                    }
                },
                yAxis: {
                    title: {
                        text: unit,
                        offset: 40
                    },
                    labels: {
                        distance: 3,
                        x: -5
                    },
                    gridLineWidth: 0,
                    minorGridLineWidth: 0
                },
                credits: {
                    enabled: false
                },
                legend: {
                    enabled: false
                },
                plotOptions: {
                    line: {
                        animation: false,
                        color: colors.DATA,
                        marker: {
                            symbol: 'circle'
                        }
                    }
                },
                tooltip: {
                    enabled: false,
                    animation: false,
                    useHTML: true,
                    style: {
                        'color': '#333333',
                        'cursor': 'default',
                        'fontSize': '12px',
                        'padding': '8px',
                        'pointerEvents': 'none',
                        'width': '300px'
                    }
                },
                series: series
            });
        },

        calculateAvgText: function (pointData) {
            var chartData = this.model.get('chartData') || {};
            var systolicData = chartData.systolicBloodPressure || {};
            var systolicPointData = _.find(systolicData.avg, function (point) { return moment(point.x).isSame(pointData.x); });
            var systolicReadings = systolicPointData.readings || [];

            var diastolicData = chartData.diastolicBloodPressure || {};
            var diastolicPointData = _.find(diastolicData.avg, function (point) { return moment(point.x).isSame(pointData.x); });
            var diastolicReadings = diastolicPointData.readings || [];

            var avgText = systolicPointData.avg + '/' + diastolicPointData.avg + ' ' + pointData.unit;
            if (systolicReadings.length > 1) {
                avgText = avgText + ' (' + systolicPointData.min + '/' + diastolicPointData.min + ' ' + pointData.unit + ' - ' +
                    systolicPointData.max + '/' + diastolicPointData.max + ' ' + pointData.unit + ')';
            }
            return avgText;
        },

        calculateReadingItems: function (pointData) {
            var chartData = this.model.get('chartData') || {};
            var systolicData = chartData.systolicBloodPressure || {};
            var systolicPointData = _.find(systolicData.avg, function (point) { return moment(point.x).isSame(pointData.x); });
            var systolicReadings = systolicPointData.readings || [];

            var diastolicData = chartData.diastolicBloodPressure || {};
            var diastolicPointData = _.find(diastolicData.avg, function (point) { return moment(point.x).isSame(pointData.x); });
            var diastolicReadings = diastolicPointData.readings || [];

            if (systolicReadings.length > 5) {
                systolicReadings.length = 5;
                diastolicReadings.length = 5;
            }

            var readingItems = _.map(systolicReadings, function (systolicReading, index) {
                var diastolicReading = diastolicReadings[index];
                var alertSeverity, textColor;

                if (diastolicReading.alert) {
                    alertSeverity = diastolicReading.alert.alertSeverity;
                    textColor = alertSeverity.colorCode || defaultAlertColor;
                }

                if (systolicReading.alert) {
                    alertSeverity = systolicReading.alert.alertSeverity;
                    textColor = alertSeverity.colorCode || defaultAlertColor;
                }

                return {
                    date: moment(systolicReading.date).format('MM/DD/YYYY'),
                    time: moment(systolicReading.date).format('hh:mm A (Z)'),
                    value: systolicReading.value + '/' + diastolicReading.value + ' ' + systolicReading.unit,
                    textColor: textColor
                };
            });

            return readingItems;
        },

        onToggleMin: function (e) {
            var chartSettings = this.model.get('chartSettings') || {};
            var level = $(e.target).data('level');          //'systolic'|'diastolic'

            chartSettings[level].showMin = !chartSettings[level].showMin;
            this.model.set('chartSettings', chartSettings);
            this.model.trigger('change');
            app.vent.trigger('chart:settings:changed');
        },

        onToggleMax: function (e) {
            var chartSettings = this.model.get('chartSettings') || {};
            var level = $(e.target).data('level');          //'systolic'|'diastolic'

            chartSettings[level].showMax = !chartSettings[level].showMax;
            this.model.set('chartSettings', chartSettings);
            this.model.trigger('change');
            app.vent.trigger('chart:settings:changed');
        },

        onToggleAvg: function (e) {
            var chartSettings = this.model.get('chartSettings') || {};
            var level = $(e.target).data('level');          //'systolic'|'diastolic'

            chartSettings[level].showAverage = !chartSettings[level].showAverage;
            this.model.set('chartSettings', chartSettings);
            this.model.trigger('change');
            app.vent.trigger('chart:settings:changed');
        },

        onToggleThreshold: function (e) {
            var thresholdId = $(e.target).data('threshold-id');
            var level = $(e.target).data('level');          //'systolic'|'diastolic'
            var chartSettings = this.model.get('chartSettings') || {};
            var levelSettings = chartSettings[level] || {};
            levelSettings.showThresholdIds = levelSettings.showThresholdIds || [];

            var isThresholdShown = _.contains(levelSettings.showThresholdIds, thresholdId);
            if (isThresholdShown) {
                levelSettings.showThresholdIds = _.without(levelSettings.showThresholdIds, thresholdId)
            } else {
                levelSettings.showThresholdIds.push(thresholdId);
            }
            chartSettings[level] = levelSettings;
            this.model.set('chartSettings', chartSettings);
            this.model.trigger('change');
            app.vent.trigger('chart:settings:changed');
        }
    });

});