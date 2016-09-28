'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Site/Patients/PatientsApp/AppNamespace',
    'Controllers/Helpers',
    'Controllers/Constants',
    'BackboneModelBinder',
    'BackboneBootstrapAlert',
    'moment',
    'Controllers/Site/Patients/PatientsApp/Collections/PatientCareElementsSearchCollection',
    'Controllers/Site/Patients/PatientsApp/Collections/PatientHealthSessionRecurrungWeekCollection',
    'Controllers/Site/Patients/PatientsApp/Collections/PatientHealthSessionRecurrungMonthCollection',
    'Controllers/Site/Patients/PatientsApp/Views/PatientCalendar/PatientProtocolSearchCollectionView',
    'Controllers/Site/Patients/PatientsApp/Views/PatientCalendar/PatientHealthSessionProtocolCollectionView',
    'Controllers/Site/Patients/PatientsApp/Views/PatientCalendar/PatientHealthSessionRecurrungWeekCollectionView',
    'Controllers/Site/Patients/PatientsApp/Views/PatientCalendar/PatientHealthSessionRecurrungMonthCollectionView'
], function (
    $,
    _,
    Backbone,
    app,
    Helpers,
    Constants,
    BackboneModelBinder,
    BackboneBootstrapAlert,
    moment,
    PatientCareElementsSearchCollection,
    PatientHealthSessionRecurrungWeekCollection,
    PatientHealthSessionRecurrungMonthCollection,
    PatientProtocolSearchCollectionView,
    PatientHealthSessionProtocolCollectionView,
    PatientHealthSessionRecurrungWeekCollectionView,
    PatientHealthSessionRecurrungMonthCollectionView
) {
    return Backbone.View.extend({
        className: 'recurring-session-rules-container',

        modelBinder: new BackboneModelBinder(),

        template: _.template($('#patientHealthSessionRecurringRulesTemplate').html()),

        templateRecurringSessionMonth: _.template('<label class="btn btn-default btn-sm btn-sm-square-ci"><input type="checkbox" value="<%=day%>" name="monthDays"><%=day%></label>'),

        events: {

        },

        initialize: function () {
            this.listenTo(this.model, 'change:frequency', this.toggleRecurringFrequency);

            Backbone.Validation.bind(this);

        },

        render: function () {

            this.$el.html(this.template(this.model.attributes));

            this.modelBinder.bind(this.model, this.el);

            this.initializeDatetimepicker();

            this.renderRecurringSessionWeek();

            this.renderRecurringSessionMonth();

            return this;
        },

        initializeDatetimepicker: function () {
            this.$el.find('#endDate-datetimepicker').datetimepicker({
                format: "MM/DD/YYYY",
                minDate: moment().add(2, 'days').format('YYYY-MM-DD'),
                widgetPositioning: {
                    horizontal: 'right',
                    vertical: 'top'
                }
            }).on('dp.change', function (e) {
                var endDate = $(this).find('#endDateDp').val();
                var value = endDate ? moment(endDate, ["MM/DD/YYYY"]).format("YYYY-MM-DD") : endDate;

                $(this).siblings('#endDate').val(value).trigger('change');
            }).on('click', function (e) {
                $(this).siblings('.help-block-error').addClass('hidden');
                $(this).closest('.has-error').removeClass('has-error');
            });

            this.$el.find('#endDateDp').inputmask({
                mask: '99/99/9999',
                showMaskOnHover: false,
                clearIncomplete: true
            });

            this.$el.find('#endDate-datetimepicker').datetimepicker('setStartDate', moment().add(10, 'days').format('YYYY-MM-DD'));
        },

        renderRecurringSessionWeek: function () {

            var week = [];
            _.each(Constants.dayLetterNames, function (dayName, i) {
                week.push({ name: dayName, number: i });
            });

            app.collections.patientHealthSessionRecurrungWeekCollection = new PatientHealthSessionRecurrungWeekCollection(week);
            app.views.patientHealthSessionRecurrungWeekView = new PatientHealthSessionRecurrungWeekCollectionView({ collection: app.collections.patientHealthSessionRecurrungWeekCollection });
            this.$el.find('#recurring-session-week').prepend(app.views.patientHealthSessionRecurrungWeekView.render().el);

            this.listenTo(app.collections.patientHealthSessionRecurrungWeekCollection, 'change', this.weekDaysUpdate);

        },

        renderRecurringSessionMonth: function () {

            var month = [];
            for (var i = 1; i < 29; i++) {
                month.push({ number: i });
            }
            app.collections.patientHealthSessionRecurrungMonthCollection = new PatientHealthSessionRecurrungMonthCollection(month);
            app.views.patientHealthSessionRecurrungMonthView = new PatientHealthSessionRecurrungMonthCollectionView({ collection: app.collections.patientHealthSessionRecurrungMonthCollection });
            this.$el.find('#recurring-session-month').prepend(app.views.patientHealthSessionRecurrungMonthView.render().el);

            this.listenTo(app.collections.patientHealthSessionRecurrungMonthCollection, 'change', this.monthDaysUpdate);

        },

        toggleRecurringFrequency: function () {
            var frequency = this.model.get('frequency');

            this.$el.find('#frequency-controls .btn').removeClass('active');
            this.$el.find('#frequency-controls #' + frequency + '-control').addClass('active');

            this.$el.find('.recurring-session-panel').removeClass('active');
            this.$el.find('.recurring-session-panel#' + frequency).addClass('active');

        },

        weekDaysUpdate: function () {

            var weekDays = [];

            app.collections.patientHealthSessionRecurrungWeekCollection.each(function (model) {
                if (model.get('isSelected'))
                    weekDays.push(model.get('number'));
            });

            this.model.set('weekDays', weekDays);
            this.$el.find('.help-block-weekDays').addClass('hidden');

        },

        monthDaysUpdate: function () {

            var monthDays = [];

            app.collections.patientHealthSessionRecurrungMonthCollection.each(function (model) {
                if (model.get('isSelected'))
                    monthDays.push(model.get('number'));
            });

            this.model.set('monthDays', monthDays);
            this.$el.find('.help-block-monthDays').addClass('hidden');

        }


    });
});