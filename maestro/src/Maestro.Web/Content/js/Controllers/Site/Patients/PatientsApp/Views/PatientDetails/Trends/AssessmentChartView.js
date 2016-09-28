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

        template: _.template($('#patientTrendsAssessmentChartViewTemplate').html()),
        answerFlyoutTemplate: _.template($('#patientTrendsAssessmentAnswerFlyoutTemplate').html()),

        events: function () {
            var parentEvents = ChartView.prototype.events;
            if (_.isFunction(parentEvents)) {
                parentEvents = parentEvents();
            }
            return _.extend({}, parentEvents, {
                'click .js-close-tooltip': 'closeChartTooltip'
            });
        },

        closeChartTooltip: function (e) {
            e.preventDefault();

            if (_.isFunction(this.tooltipCloser)) {
                this.tooltipCloser();
            }
        },

        initChart: function () {
            var self = this;
            var ASSESSMENTS_LINE_WIDTH = 3;
            var colors = this.colors;
            var chartData = this.model.get('chartData');

            var categories = _.pluck(this.model.get('answers'), 'answerText');
            var daysPerTick = this.calculateDaysPerTick();

            var minY = 0;
            var maxY = categories.length - 1;
            var minX = this.startDate.clone().subtract(3, 'hours').valueOf();
            var maxX = this.endDate.clone().valueOf();

            this.$('.chart').highcharts({
                chart: {
                    marginLeft: 60,
                    marginRight: 5,
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
                    title: { text: null },
                    labels: {
                        distance: 10,
                        x: -10
                    },
                    gridLineWidth: 0,
                    minorGridLineWidth: 0,
                    min: minY,
                    max: maxY,
                    categories: categories,
                    plotLines: [
                        {
                            color: colors.ASSESSMENT[0],
                            width: ASSESSMENTS_LINE_WIDTH,
                            value: 0
                        },
                        {
                            color: colors.ASSESSMENT[1],
                            width: ASSESSMENTS_LINE_WIDTH,
                            value: 1
                        },
                        {
                            color: colors.ASSESSMENT[2],
                            width: ASSESSMENTS_LINE_WIDTH,
                            value: 2
                        },
                        {
                            color: colors.ASSESSMENT[3],
                            width: ASSESSMENTS_LINE_WIDTH,
                            value: 3
                        },
                        {
                            color: colors.ASSESSMENT[4],
                            width: ASSESSMENTS_LINE_WIDTH,
                            value: 4
                        },
                        {
                            color: colors.ASSESSMENT[5],
                            width: ASSESSMENTS_LINE_WIDTH,
                            value: 5
                        },
                        {
                            color: colors.ASSESSMENT[6],
                            width: ASSESSMENTS_LINE_WIDTH,
                            value: 6
                        },
                        {
                            color: colors.ASSESSMENT[0],
                            width: ASSESSMENTS_LINE_WIDTH,
                            value: 7
                        },
                        {
                            color: colors.ASSESSMENT[1],
                            width: ASSESSMENTS_LINE_WIDTH,
                            value: 8
                        },
                        {
                            color: colors.ASSESSMENT[2],
                            width: ASSESSMENTS_LINE_WIDTH,
                            value: 9
                        },
                        {
                            color: colors.ASSESSMENT[3],
                            width: ASSESSMENTS_LINE_WIDTH,
                            value: 10
                        },
                        {
                            color: colors.ASSESSMENT[4],
                            width: ASSESSMENTS_LINE_WIDTH,
                            value: 11
                        },
                        {
                            color: colors.ASSESSMENT[5],
                            width: ASSESSMENTS_LINE_WIDTH,
                            value: 12
                        },
                        {
                            color: colors.ASSESSMENT[6],
                            width: ASSESSMENTS_LINE_WIDTH,
                            value: 13
                        }
                    ]
                },
                credits: {
                    enabled: false
                },
                legend: {
                    enabled: false
                },
                plotOptions: {
                    scatter: {
                        animation: false,
                        color: colors.DATA,
                        dataLabels: {
                            enabled: true,
                            color: '#fff',
                            y: 12,
                            inside: true,
                            padding: 0,
                            style: {
                                cursor: 'default',
                                fontSize: '14px',
                                textShadow: 'none'
                            },
                            formatter: function () {
                                return this.point.count;
                            }
                        },
                        marker: {
                            symbol: 'circle',
                            radius: 10
                        },
                        tooltip: {
                            headerFormat: '',
                            hideDelay: 0,
                            pointFormatter: function () {
                                var data = self.calculateFlyoutData(this.options);
                                return self.answerFlyoutTemplate(data);
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
                series: [
                    {
                        //name: chartName,
                        step: 'left',
                        type: 'scatter',
                        data: chartData,
                        zones: [
                            {
                                value: 1,
                                color: colors.ASSESSMENT[0]
                            },
                            {
                                value: 2,
                                color: colors.ASSESSMENT[1]
                            },
                            {
                                value: 3,
                                color: colors.ASSESSMENT[2]
                            },
                            {
                                value: 4,
                                color: colors.ASSESSMENT[3]
                            },
                            {
                                value: 5,
                                color: colors.ASSESSMENT[4]
                            },
                            {
                                value: 6,
                                color: colors.ASSESSMENT[5]
                            },
                            {
                                value: 7,
                                color: colors.ASSESSMENT[6]
                            },
                            {
                                value: 8,
                                color: colors.ASSESSMENT[0]
                            },
                            {
                                value: 9,
                                color: colors.ASSESSMENT[1]
                            },
                            {
                                value: 10,
                                color: colors.ASSESSMENT[2]
                            },
                            {
                                value: 11,
                                color: colors.ASSESSMENT[3]
                            },
                            {
                                value: 12,
                                color: colors.ASSESSMENT[4]
                            },
                            {
                                value: 13,
                                color: colors.ASSESSMENT[5]
                            },
                            {
                                value: 14,
                                color: colors.ASSESSMENT[6]
                            }

                        ]
                    }
                ]
            });
        },

        calculateFlyoutData: function (pointData) {
            var defaultAlertColor = 'red';
            var readings = pointData.readings || [];
            var dates = _.pluck(readings, 'date') || [];
            dates = _.sortBy(dates, function (date) { return date.valueOf(); });

            //header
            var headerText = '';
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

            //answer
            var answers = this.model.get('answers');
            var answerText = answers[pointData.y * 1].answerText;
            var answerTextColor = 'inherit';
            if (readings.length === 1) {
                var reading = readings[0];
                if (_.isObject(reading.alert)) {
                    var alertSeverity = reading.alert.alertSeverity;
                    answerTextColor = alertSeverity.colorCode || defaultAlertColor;
                }
            }

            //readings
            var readingItems = _.map(readings, function (reading) {
                var textColor = 'inherit';
                if (_.isObject(reading.alert)) {
                    var alertSeverity = reading.alert.alertSeverity;
                    textColor = alertSeverity.colorCode || defaultAlertColor;
                }
                return {
                    text: reading.date.format('MM/DD/YYYY hh:mm A (Z)'),
                    textColor: textColor
                }
            });

            return {
                headerText: headerText,
                answerText: answerText,
                answerTextColor: answerTextColor,
                readingItems: readingItems
            };
        }

    });
});