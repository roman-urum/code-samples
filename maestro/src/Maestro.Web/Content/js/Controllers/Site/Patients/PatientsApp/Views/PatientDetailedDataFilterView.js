'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Site/Patients/PatientsApp/AppNamespace',
    'Controllers/Helpers',
    'moment',
    'Controllers/Site/Patients/PatientsApp/Collections/GroupedHealthSessionsDetailedCollection',
    'Controllers/Site/Patients/PatientsApp/Collections/UngroupedHealthSessionsDetailedCollection',
    'Controllers/Site/Patients/PatientsApp/Collections/AdhocMeasurementsDetailedCollection',
    'Controllers/Site/Patients/PatientsApp/Collections/GroupedHealthSessionCollection',
    'Controllers/Site/Patients/PatientsApp/Views/GroupedHealthSessionCollectionView',
    'Controllers/Site/Patients/PatientsApp/Views/UngroupedHealthSessionCollectionView'
], function (
    $,
    _,
    Backbone,
    app,
    Helpers,
    moment,
    GroupedHealthSessionsDetailedCollection,
    UngroupedHealthSessionsDetailedCollection,
    AdhocMeasurementsDetailedCollection,
    GroupedHealthSessionCollection,
    GroupedHealthSessionCollectionView,
    UngroupedHealthSessionCollectionView
) {
    return Backbone.View.extend({

        template: _.template('<div class="col-sm-5 pull-left">\
                                <div class="form-inline js-simple-range-block">\
                                    <select class="form-control js-simple-range">\
                                        <option value="1 week">Last 1 week</option>\
                                        <option value="2 weeks">Last 2 weeks</option>\
                                        <option value="4 weeks">Last 4 weeks</option>\
                                        <option value="12 weeks">Last 12 weeks</option>\
                                        <option value="6 months">Last 6 months</option>\
                                        <option value="12 months">Last 12 months</option>\
                                        <option value="custom">Custom range</option>\
                                    </select>\
                                    <a class="btn btn-link js-custom-range-show">Range</a>\
                                </div>\
                                <div class="form-inline hidden js-custom-range-block">\
                                    <div class="input-group" style="width: 280px">\
                                        <input type="text" class="form-control js-from-date" style="width: 100px">\
                                        <span class="input-group-addon">to</span>\
                                        <input type="text" class="form-control js-to-date" style="width: 100px">\
                                        <span class="input-group-btn">\
                                            <a class="btn btn-primary js-custom-range-search">\
                                                 <span class="glyphicon glyphicon-search"></span>\
                                            </a>\
                                        </span>\
                                    </div>\
                                    <a class="btn btn-link js-simple-range-show">Cancel</a>\
                                </div>\
                            </div>\
                            <div class="col-sm-3 pull-right">\
                                <select class="form-control js-element-type-filter">\
                                    <option value="All">All</option>\
                                    <option value="Question">Questions</option>\
                                    <option value="TextMedia">Text and Media</option>\
                                    <option value="Measurement">Measurements</option>\
                                    <option value="Assessment">Assessments</option>\
                                </select>\
                            </div>'),


        events: {
            'change .js-simple-range': 'changeSimpleRange',
            'click .js-custom-range-show': 'customRangeShow',
            'click .js-simple-range-show': 'simpleRangeShow',
            'click .js-custom-range-search': 'customRangeSearch',
            'change .js-element-type-filter': 'elementTypeFilter'
        },

        initialize: function () {

        },

        render: function () {

            this.$el.html(this.template());

            app.models.patientDetailedDataFilterModel = new Backbone.Model({ elementType: 'All' });

            this.simpleRangeRun();

            return this;
        },

        changeSimpleRange: function (e) {
            var value = $(e.target).val();

            if (value == 'custom') {

                this.customRangeShow();

            } else {
                var period = value.split(" "),
                    fromDate = moment().subtract(period[0], period[1]).format('YYYY-MM-DD');

                app.models.patientDetailedDataFilterModel.set({ fromDate: fromDate + 'T23:59:59' });
            }

        },

        customRangeShow: function () {
            $('.js-custom-range-block').removeClass('hidden');
            $('.js-simple-range-block').addClass('hidden');
            this.customRangeRun();
        },

        simpleRangeShow: function () {
            $('.js-custom-range-block').addClass('hidden');
            $('.js-simple-range-block').removeClass('hidden');
            this.simpleRangeRun();
        },

        simpleRangeRun: function () {

            this.$('.js-simple-range').val('4 weeks');

            var rangeValue = this.$('.js-simple-range').val(),
                period = rangeValue.split(" "),
                fromDate = moment().subtract(period[0], period[1]).format('YYYY-MM-DD'),
                toDate = moment().format('YYYY-MM-DD');

            app.models.patientDetailedDataFilterModel.set({ fromDate: fromDate, toDate: toDate });

        },

        customRangeRun: function () {

            var fromDate = app.models.patientDetailedDataFilterModel.get('fromDate'),
                toDate = app.models.patientDetailedDataFilterModel.get('toDate'),
                $fromDate = this.$('.js-from-date'),
                $toDate = this.$('.js-to-date');

            if ($fromDate.data("DateTimePicker"))
                $fromDate.data("DateTimePicker").maxDate(toDate);

            $fromDate
                .val(fromDate)
                .datetimepicker({
                    format: "YYYY-MM-DD",
                    maxDate: toDate
                }).on('dp.change', function (e) {
                    $toDate.data("DateTimePicker").minDate(e.date);
                    app.models.patientDetailedDataFilterModel.set({ fromDate: e.date.format('YYYY-MM-DD') }, { silent: true });
                })
            ;

            $toDate
                .val(toDate)
                .datetimepicker({
                    format: "YYYY-MM-DD",
                    minDate: fromDate,
                    maxDate: toDate,
                    useCurrent: false
                }).on('dp.change', function (e) {
                    $fromDate.data("DateTimePicker").maxDate(e.date);
                    app.models.patientDetailedDataFilterModel.set({ toDate: e.date.format('YYYY-MM-DD') }, { silent: true });
                })
            ;

        },

        customRangeSearch: function () {
            app.models.patientDetailedDataFilterModel.trigger('change');
        },

        elementTypeFilter: function (e) {
            var elementType = $(e.currentTarget).val();
            app.models.patientDetailedDataFilterModel.set('elementType', elementType);
        }

    });
});