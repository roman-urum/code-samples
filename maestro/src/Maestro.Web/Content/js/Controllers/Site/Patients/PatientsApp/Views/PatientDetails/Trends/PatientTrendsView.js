'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'BackboneBootstrapModal',
    'highcharts',
    'Controllers/Site/Patients/PatientsApp/AppNamespace',
    'Controllers/Helpers',
    'Controllers/Site/Patients/PatientsApp/Models/PatientDetails/Trends/ChartsCollection',
    'Controllers/Site/Patients/PatientsApp/Models/PatientDetails/Trends/ChartModel',
    'Controllers/Site/Patients/PatientsApp/Models/PatientDetails/Trends/QuestionsCollection',
    'Controllers/Site/Patients/PatientsApp/Models/PatientDetails/Trends/DaterangeModel',
    './../../BaseMaestroView',
    './DaterangeView',
    './VitalChartView',
    './AssessmentChartView',
    './BloodPressureVitalChartView',
    './PatientTrendsAddChartsView'
], function (
    $,
    _,
    Backbone,
    BackboneBootstrapModal,
    HighchartsLib,
    app,
    Helpers,
    ChartsCollection,
    ChartModel,
    QuestionsCollection,
    DaterangeModel,
    BaseMaestroView,
    DaterangeView,
    VitalChartView,
    AssessmentChartView,
    BloodPressureVitalChartView,
    PatientTrendsAddChartsView
) {
    return BaseMaestroView.extend({

        template: _.template($('#patientTrendsViewTemplate').html()),

        regions: {
            daterange: '.daterange-container'
        },

        events: {
            'click .patient-trends-add-charts': 'showAddChartsView',
            'click .chart-container .remove': 'removeChart',
            'click .chart-container .move-up': 'moveChartUp',
            'click .chart-container .move-down': 'moveChartDown',
            'click .chart-container .js-reorder': 'showAddChartsView',
            'click .js-patient-export-trends': 'exportPatientTrends'
        },

        initialize: function () {
            var self = this;

            //TODO: move it to some global settings section
            Highcharts.setOptions({
                global: { useUTC: false }
            });

            this.patientId = app.patientId;
            this.addChartsModal = null;

            this.selectedCharts = [];
            this.charts = new ChartsCollection(null, {
                patientId: app.patientId
            });
            this.daterangeView = new DaterangeView({
                model: this.charts.daterangeModel
            });

            this.vitalsCollection = this.charts.vitalsCollection;
            this.questionsCollection = this.charts.questionsCollection;

            this.debouncedSaveSettings = _.debounce(function () {
                self.charts.save();
            }, 2000);

            this.charts.fetchSettings({
                success: function () {
                    self.charts.fetch();
                    self.render();
                }
            });

            this.vent = _.extend({}, Backbone.Events);

            this.listenTo(app.vent, 'charts:selected', function (selectedCharts) {
                self.charts.setCharts(selectedCharts);
                self.charts.fetch({ onlyNew: true });
                self.saveSettings();
                self.render();
            });

            this.listenTo(app.vent, 'chart:settings:changed', function () {
                self.saveSettings();
            });

            this.listenTo(this.charts.daterangeModel, 'change', function () {
                self.saveSettings();
                self.render();
            });

            this.listenTo(this.vent, 'chart:remove', this.removeChart);
            this.listenTo(this.vent, 'chart:up', this.moveChartUp);
            this.listenTo(this.vent, 'chart:down', this.moveChartDown);

            this.listenTo(app.vent, 'patient:trends:loading', this.deactivateExportButton);
            this.listenTo(app.vent, 'patient:trends:loaded', this.activateExportButton);
        },

        saveSettings: function () {
            this.debouncedSaveSettings();
        },

        debouncedSaveSettings: function () {
            //defined in initialize
        },

        activateExportButton: function () {
            if (this.charts.models.length > 0) {
                this.$el.find('.js-patient-export-trends').removeClass('disabled');
            }
        },

        deactivateExportButton: function () {
            var target = this.$el.find('.js-patient-export-trends');

            if (!target.hasClass('disabled')) {
                target.addClass('disabled');
            }
        },

        render: function () {
            var self = this;

            this.$el.html(this.template({
                isSettingsFetched: this.charts.isSettingsFetched
            }));

            this.charts.each(function (chartModel) {
                self.renderChart(chartModel);
            });

            this.renderDaterangeSection();

            this.activateExportButton();

            return this;
        },

        renderChart: function (chartModel) {
            var chartsContainer = this.$('.chart-containers-list');

            var chartViewClass;
            if (chartModel.chartType === 'assessment') {
                chartViewClass = AssessmentChartView;
            } else {
                if (chartModel.chartName === 'BloodPressure') {
                    chartViewClass = BloodPressureVitalChartView;
                } else {
                    chartViewClass = VitalChartView;
                }
            }

            var chartView = new chartViewClass({
                model: chartModel,
                daterangeModel: this.charts.daterangeModel,
                vent: this.vent
            });
            chartView.render();
            chartsContainer.append(chartView.el);
        },

        renderDaterangeSection: function () {
            this.$('.daterange-container').html(this.daterangeView.$el);
            this.daterangeView.render();
        },

        showAddChartsView: function (e) {
            e.preventDefault();

            this.addChartsModal = new BackboneBootstrapModal({
                content: new PatientTrendsAddChartsView({
                    vitals: this.vitalsCollection,
                    questions: this.questionsCollection,
                    selectedCharts: this.charts
                }),
                title: 'Add Charts',
                okText: 'Save',
                cancelText: 'Cancel',
                template: _.template($('#modalLgTemplate').html()),
                modalOptions: {
                    backdrop: 'static',
                    keyboard: false
                }
            });

            this.addChartsModal.open();
        },

        removeChart: function (chartModel) {
            this.deactivateExportButton();
            this.charts.remove(chartModel);
            this.selectedCharts = _.reject(this.selectedCharts, function (chartMeta) { return chartMeta.name === chartModel.chartName; });
            this.saveSettings();
            this.activateExportButton();
        },

        moveChartUp: function (chartModel) {
            var index = this.charts.indexOf(chartModel);
            var charts = this.charts.models;
            var chartContainers = this.$('.charts');

            if (index > 0) {
                //swap charts models
                var temp = charts[index - 1];
                charts[index - 1] = charts[index];
                charts[index] = temp;

                //swap html containers
                var chartContainer = chartContainers.eq(index);
                var prev = chartContainers.eq(index - 1);
                chartContainer.detach();
                prev.before(chartContainer);
            }

            this.saveSettings();
        },

        moveChartDown: function (chartModel) {
            var index = this.charts.indexOf(chartModel);
            var charts = this.charts.models;
            var chartContainers = this.$('.charts');

            if (index < charts.length - 1) {
                //swap charts models
                var temp = charts[index + 1];
                charts[index + 1] = charts[index];
                charts[index] = temp;

                //swap html containers
                var chartContainer = chartContainers.eq(index);
                var next = chartContainers.eq(index + 1);
                chartContainer.detach();
                next.after(chartContainer);
            }

            this.saveSettings();
        },

        onBeforeRemove: function () {
            //TODO: implement saving selected vitals
        },

        exportPatientTrends: function (e) {
            e.preventDefault();

            // Downloading report
            window.location = '/Patients/ExportTrendsToExcel?patientId=' + this.model.get('id');
        }
    });
});