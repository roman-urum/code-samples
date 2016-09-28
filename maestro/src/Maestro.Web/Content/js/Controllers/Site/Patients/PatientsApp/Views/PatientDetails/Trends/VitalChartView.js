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
    './ChartView'
], function (
    $,
    _,
    Backbone,
    HighchartsLib,
    app,
    Helpers,
    moment,
    BaseMaestroView,
    ChartView
) {
    return ChartView.extend({

        template: _.template($('#patientTrendsVitalChartViewTemplate').html()),
        readingFlyoutTemplate: _.template($('#patientTrendsVitalReadingFlyoutTemplate').html()),
        thresholdFlyoutTemplate: _.template($('#patientTrendsVitalThresholdFlyoutTemplate').html()),

        events: function () {
            var parentEvents = ChartView.prototype.events;
            if (_.isFunction(parentEvents)) {
                parentEvents = parentEvents();
            }
            return _.extend({}, parentEvents, {
                'change .js-toggle-min': 'onToggleMin',
                'change .js-toggle-max': 'onToggleMax',
                'change .js-toggle-avg': 'onToggleAvg',
                'change .js-toggle-threshold': 'onToggleThreshold',
                'click .js-close-tooltip': 'closeChartTooltip'
            });
        },

        initialize: function (options) {
            options = options || {};

            //call parent constructor
            ChartView.prototype.initialize.call(this, options);
        },

        bindModelEvents: function () {
            var self = this;
            this.listenTo(this.model, 'change', function () {
                self.render();
                self.destroyChart();
                self.initChart();
                self.renderSettings();
            });
            this.listenTo(this.model, 'fetch:success', function () {
                self.renderSpinner();
            });
            this.listenTo(this.model, 'fetch:error', function () {
                self.renderSpinner();
            });
        },

        render: function () {
            var self = this;

            if (_.isEmpty(this.model.get('chartSettings'))) {
                this.model.set({ chartSettings: { showAverage: true } }, { silent: true });
            }

            var data = _.extend({
                chartLabel: this.model.chartLabel,
                thresholds: []
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
            chartSettings.showThresholdIds = chartSettings.showThresholdIds || [];

            this.$('.js-toggle-avg').prop('checked', !!chartSettings.showAverage);
            this.$('.js-toggle-min').prop('checked', !!chartSettings.showMin);
            this.$('.js-toggle-max').prop('checked', !!chartSettings.showMax);
            this.$('.js-toggle-threshold').each(function (index, el) {
                var thresholdId = $(el).data('threshold-id');
                $(el).prop('checked', _.contains(chartSettings.showThresholdIds, thresholdId));
            });
        },

        closeChartTooltip: function (e) {
            e.preventDefault();

            if (_.isFunction(this.tooltipCloser)) {
                this.tooltipCloser();
            }
        },

        initChart: function (options) {
            var self = this;
            var colors = this.colors;
            var chartData = this.model.get('chartData') || {};
            var chartSettings = this.model.get('chartSettings') || {};
            var daysPerTick = this.calculateDaysPerTick();
            var unit = _.isEmpty(chartData.avg) ? '' : chartData.avg[0].unit;

            var minX = this.startDate.clone().subtract(3, 'hours').valueOf();
            var maxX = this.endDate.clone().valueOf();

            var series = [];
            if (chartSettings.showAverage) {
                var avgSeries = this.createLineMetadata({ data: chartData.avg, zIndex: 3 });
                series.push(avgSeries);
            }
            if (chartSettings.showMin) {
                var minSeries = this.createLineMetadata({ data: chartData.min, color: colors.DATA_EXTREME, dashStyle: 'shortdash', zIndex: 2 });
                series.push(minSeries);
            }
            if (chartSettings.showMax) {
                var maxSeries = this.createLineMetadata({ data: chartData.max, color: colors.DATA_EXTREME, dashStyle: 'shortdash', zIndex: 2 });
                series.push(maxSeries);
            }

            var shownThresholds = _.filter(chartData.thresholds, function (threshold, id) { return _.contains(chartSettings.showThresholdIds, id); });
            _.each(shownThresholds, function (threshold) {
                var dashStyle = threshold.min[0].data.isDefault ? 'shortdot' : 'solid';
                var thresholdMinSeries = self.createThresholdMetadata({ data: threshold.min, color: threshold.min[0].color, dashStyle: dashStyle });
                series.push(thresholdMinSeries);

                var thresholdMaxSeries = self.createThresholdMetadata({ data: threshold.max, color: threshold.max[0].color, dashStyle: dashStyle });
                series.push(thresholdMaxSeries);
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
                            return moment(this.value).format('MM/DD');
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
                        'width': '300px'
                    }
                },
                series: series
            });
        },

        createLineMetadata: function (options) {
            var self = this;
            var defaults = {
                color: this.colors.DATA,
                dashStyle: 'solid',
                zIndex: 1
            };
            options = _.extend({}, defaults, options);
            return {
                type: 'line',
                data: options.data,
                color: options.color,
                dashStyle: options.dashStyle,
                tooltip: {
                    headerFormat: '',
                    hideDelay: 0,
                    pointFormatter: function () {
                        var data = self.calculateFlyoutData(this.options);
                        return self.readingFlyoutTemplate(data);
                    }
                },
                events: {
                    click: function (e) {
                        self.closeChartTooltip(e);
                        var tooltip = new Highcharts.Tooltip(this.chart, this.chart.options.tooltip);
                        tooltip.refresh(e.point, e);
                        self.tooltipCloser = function () {
                            tooltip.destroy();
                        }
                    }
                },
                zIndex: options.zIndex
            }
        },

        calculateFlyoutData: function (pointData) {
            var readings = pointData.readings || [];
            readings = _.sortBy(readings, function (reading) { return reading.date.valueOf(); });

            //header
            var headerText = '';
            var dates = _.pluck(readings, 'date');
            if (dates.length === 1) {
                headerText = moment(dates[0]).format('MM/DD/YYYY hh:mm A (Z)');
            } else {
                var min = moment(_.min(dates));
                var max = moment(_.max(dates));
                if (min.isSame(max, 'day')) {
                    headerText = moment(min).format('MM/DD/YYYY hh:mm A (Z)') + ' - ' + moment(max).format('hh:mm A (Z)');
                } else {
                    headerText = moment(min).format('MM/DD/YYYY') + ' - ' + moment(max).format('MM/DD/YYYY');
                }
            }

            //avg text
            var avgText = this.calculateAvgText(pointData);
            var avgTextColor = 'inherit';
            if (readings.length === 1) {
                var reading = readings[0];
                if (_.isObject(reading.alert)) {
                    var alertSeverity = reading.alert.alertSeverity;
                    avgTextColor = alertSeverity.colorCode || defaultAlertColor;
                }
            }

            //misc
            var readingsNumber = readings.length;

            //readings data
            var readingItems = this.calculateReadingItems(pointData);

            return {
                headerText: headerText,
                avgText: avgText,
                avgTextColor: avgTextColor,
                readingsNumber: readingsNumber,
                readingItems: readingItems
            }
        },

        calculateAvgText: function (pointData) {
            var readings = pointData.readings || [];
            readings = _.sortBy(readings, function (reading) { return reading.date.valueOf(); });

            var avgText = pointData.avg + ' ' + pointData.unit;
            if (readings.length > 1) {
                avgText = avgText + ' (' + pointData.min + ' ' + pointData.unit + ' - ' +
                    pointData.max + ' ' + pointData.unit + ')';
            }
            return avgText;
        },

        calculateReadingItems: function (pointData) {
            var defaultAlertColor = 'red';
            var readings = pointData.readings || [];
            readings = _.sortBy(readings, function (reading) { return reading.date.valueOf(); });

            if (readings.length > 5) {
                readings.length = 5;
            }

            var readingItems = _.map(readings, function (reading) {
                var textColor = 'inherit';
                if (_.isObject(reading.alert)) {
                    var alertSeverity = reading.alert.alertSeverity;
                    textColor = alertSeverity.colorCode || defaultAlertColor;
                }
                return {
                    date: moment(reading.date).format('MM/DD/YYYY'),
                    time: moment(reading.date).format('hh:mm A (Z)'),
                    value: reading.value + ' ' + reading.unit,
                    textColor: textColor
                };
            });
            return readingItems;
        },

        createThresholdMetadata: function (options) {
            var self = this;
            var defaults = {
                color: this.colors.THRESHOLD_DEFAULT,
                dashStyle: 'solid',
                lineWidth: 1,
                zIndex: 1
            };
            options = _.extend({}, defaults, options);
            return {
                type: 'line',
                data: options.data,
                color: options.color,
                dashStyle: options.dashStyle,
                lineWidth: options.lineWidth,
                marker: {
                    enabled: false,
                    states: {
                        hover: {
                            enabled: false
                        }
                    }
                },
                tooltip: {
                    headerFormat: '',
                    hideDelay: 0,
                    pointFormatter: function () {
                        var data = self.calculateThresholdFlyoutData(this.options);
                        return self.thresholdFlyoutTemplate(data);
                    }
                },
                events: {
                    click: function (e) {
                        self.closeChartTooltip(e);
                        var tooltip = new Highcharts.Tooltip(this.chart, this.chart.options.tooltip);
                        tooltip.refresh(e.point, e);
                        self.tooltipCloser = function () {
                            tooltip.destroy();
                        }
                    }
                },
                zIndex: options.zIndex
            }
        },

        calculateThresholdFlyoutData: function (pointData) {
            var alertSeverity = pointData.data.alertSeverity || {};
            var color = alertSeverity.colorCode || 'inherit';

            return _.extend({
                id: app.models.patientModel.get('id'),
                siteId: app.models.patientModel.get('siteId'),
                chartName: this.model.get('chartName'),
                color: color,
                isDefault: pointData.data.isDefault
            }, pointData);
        },

        onToggleMin: function () {
            var chartSettings = this.model.get('chartSettings') || {};
            chartSettings.showMin = !chartSettings.showMin;
            this.model.set('chartSettings', chartSettings);
            this.model.trigger('change');
            app.vent.trigger('chart:settings:changed');
        },

        onToggleMax: function () {
            var chartSettings = this.model.get('chartSettings') || {};
            chartSettings.showMax = !chartSettings.showMax;
            this.model.set('chartSettings', chartSettings);
            this.model.trigger('change');
            app.vent.trigger('chart:settings:changed');
        },

        onToggleAvg: function () {
            var chartSettings = this.model.get('chartSettings') || {};
            chartSettings.showAverage = !chartSettings.showAverage;
            this.model.set('chartSettings', chartSettings);
            this.model.trigger('change');
            app.vent.trigger('chart:settings:changed');
        },

        onToggleThreshold: function (e) {
            var thresholdId = $(e.target).data('threshold-id');
            var chartSettings = this.model.get('chartSettings') || {};
            chartSettings.showThresholdIds = chartSettings.showThresholdIds || [];

            var isThresholdShown = _.contains(chartSettings.showThresholdIds, thresholdId);
            if (isThresholdShown) {
                chartSettings.showThresholdIds = _.without(chartSettings.showThresholdIds, thresholdId)
            } else {
                chartSettings.showThresholdIds.push(thresholdId);
            }
            this.model.set('chartSettings', chartSettings);
            this.model.trigger('change');
            app.vent.trigger('chart:settings:changed');
        }

    });
});