'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'highcharts',
    'Controllers/Site/Patients/PatientsApp/AppNamespace',
    'Controllers/Helpers',
    'moment',
    './../../BaseMaestroView'
], function (
    $,
    _,
    Backbone,
    HighchartsLib,
    app,
    Helpers,
    moment,
    BaseMaestroView
) {
    return BaseMaestroView.extend({

        colors: {
            DATA: '#5ea6b3',
            DATA_EXTREME: '#a7dcdb',
            DATA_SECONDARY: '#938cc8',
            DATA_SECONDARY_EXTREME: '#c3bde9',
            DATA_W_ALERT: '#ff0000',
            DATA_EXTREME_W_ALERT: '#dca69f',
            THRESHOLD_DEFAULT: '#999',
            ASSESSMENT: ['#7a62d6', '#5775d0', '#59aed4', '#66c85f', '#c4ca61', '#c78c61', '#c77ca5']
        },

        events: {
            'click .js-remove': 'removeChart',
            'click .js-move-up': 'moveChartUp',
            'click .js-move-down': 'moveChartDown'
        },

        initialize: function (options) {
            options = options || {};

            this.model = options.model || new Backbone.Model();
            this.vent = options.vent;
            this.startDate = moment(options.daterangeModel.get('startDate'));
            this.endDate = moment(options.daterangeModel.get('endDate'));

            this.bindModelEvents();
        },

        bindModelEvents: function () {
            var self = this;
            this.listenTo(this.model, 'change', this.render);
            this.listenTo(this.model, 'fetch:error', function () {
                self.renderSpinner();
            });
        },

        render: function () {
            var self = this;


            var data = _.extend({
                chartLabel: this.model.chartLabel
            }, this.model.toJSON());
            this.$el.html(this.template(data));

            //call chart init during next tick of event loop
            //to let browser render just inserted html first
            //after that highcharts can use elements dimensions
            setTimeout(function () {
                self.renderSpinner();
                self.initChart();
            }, 0);
        },

        renderSpinner: function () {
            var spinner = this.$('.spinner-container');
            if (this.model.isFetching) {
                spinner.show();
            } else {
                spinner.hide();
            }
        },

        //override this method
        initChart: function () { },

        calculateDaysPerTick: function () {
            var X_POINTS_MAX_NUM = 30;

            var startDatetime = this.startDate.valueOf();
            var endDatetime = this.endDate.valueOf();
            var daysNum = moment.duration(endDatetime - startDatetime).asDays();

            var daysPerTick = Math.ceil(daysNum / X_POINTS_MAX_NUM);
            //console.log('days per tick =', daysPerTick);

            return daysPerTick;
        },

        destroyChart: function () {
            var chart = this.$('.chart').highcharts();
            if (chart && _.isFunction(chart.destroy)) {
                chart.destroy();
            }
        },

        removeChart: function (e) {
            e.preventDefault();
            this.vent.trigger('chart:remove', this.model);

            //self remove
            this.remove();
        },

        moveChartUp: function (e) {
            e.preventDefault();
            this.vent.trigger('chart:up', this.model);
        },

        moveChartDown: function (e) {
            e.preventDefault();
            this.vent.trigger('chart:up', this.model);
        },

        onBeforeRemove: function () {
            this.destroyChart();
        }
    });
});