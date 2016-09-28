'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'highcharts',
    'Controllers/Site/Patients/PatientsApp/AppNamespace',
    'Controllers/Helpers',
    'Controllers/Site/Patients/PatientsApp/Models/PatientDetails/Trends/ChartsCollection',
    'Controllers/Site/Patients/PatientsApp/Models/PatientDetails/Trends/ChartModel',
    'Controllers/Site/Patients/PatientsApp/Models/PatientDetails/Trends/QuestionsCollection',
    './../../BaseMaestroView'
], function (
    $,
    _,
    Backbone,
    HighchartsLib,
    app,
    Helpers,
    ChartsCollection,
    ChartModel,
    QuestionsCollection,
    BaseMaestroView
) {
    return BaseMaestroView.extend({

        template: _.template($('#patientTrendsDaterangeViewTemplate').html()),

        events: {
            'change select.js-daterange-predefined': 'selectDaterange',
            'click .js-custom-daterange-enable': 'enableCustomDaterange',
            'click .js-custom-daterange-save': 'saveCustomDaterange'
        },

        initialize: function (options) {
            options = options || {};

            var self = this;
            //this.vent = options.vent;

            //this.daterangeCase = options.daterangeCase;
            //this.startDate = options.startDate;
            //this.endDate = options.endDate;
            //this.setDaterangeCase(this.daterangeCase);

            this.model = options.model || new Backbone.Model();
        },

        render: function () {
            var self = this;
            this.$el.html(this.template(this.model.toJSON()));

            this.renderDaterangeSection();
            this.delegateEvents();
        },

        renderDaterangeSection: function () {
            var caseNumber = this.model.get('daterangeCase');
            var startDate = this.model.get('startDate');
            var endDate = this.model.get('endDate');

            //render selected select's option
            this.$('.js-daterange-predefined')
                .children().prop('selected', false)
                .filter('[value="' + caseNumber + '"]').prop('selected', true);

            //render custom daterange section
            if (caseNumber == '0') {   //if 'custom range' option selected
                this.$('.custom-daterange-quick-access').hide();
                this.$('.custom-daterange').show();

                //init linked datepickers http://eonasdan.github.io/bootstrap-datetimepicker/#linked-pickers
                var startEl = this.$('.js-daterange-start');
                var endEl = this.$('.js-daterange-end');

                startEl.datetimepicker({
                    format: "YYYY-MM-DD"
                });
                endEl.datetimepicker({
                    format: "YYYY-MM-DD",
                    useCurrent: false
                });
                startEl.on("dp.change", function (e) {
                    endEl.data("DateTimePicker").minDate(e.date);
                });
                endEl.on("dp.change", function (e) {
                    startEl.data("DateTimePicker").maxDate(e.date);
                });
                startEl.data("DateTimePicker").date(startDate);
                endEl.data("DateTimePicker").date(endDate);

            } else {
                this.$('.custom-daterange').hide();
                this.$('.custom-daterange-quick-access').show();
            }
        },

        selectDaterange: function (e) {
            e.preventDefault();
            var selectedCase = $(e.target).children(':selected').val();

            if (selectedCase === '0') {
                this.enableCustomDaterange();
            } else {
                this.model.setDaterangeCase(selectedCase);
            }
        },

        enableCustomDaterange: function (e) {
            if (e) e.preventDefault();
            this.model.setDaterangeCase('0', this.model.get('startDate'), this.model.get('endDate'), { silent: true });
            this.renderDaterangeSection();
        },

        saveCustomDaterange: function (e) {
            e.preventDefault();

            var startEl = this.$('.js-daterange-start');
            var endEl = this.$('.js-daterange-end');

            var startDate = startEl.data("DateTimePicker").date();
            var endDate = endEl.data("DateTimePicker").date();
            this.model.setDaterangeCase('0', startDate, endDate);
        }

    });
});