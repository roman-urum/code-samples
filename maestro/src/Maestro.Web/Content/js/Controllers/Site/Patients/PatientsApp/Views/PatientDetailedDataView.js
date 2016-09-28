'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Site/Patients/PatientsApp/AppNamespace',
    'Controllers/Helpers',
    'Controllers/Site/Patients/PatientsApp/Collections/GroupedHealthSessionsDetailedCollection',
    'Controllers/Site/Patients/PatientsApp/Collections/UngroupedHealthSessionsDetailedCollection',
    'Controllers/Site/Patients/PatientsApp/Collections/AdhocMeasurementsDetailedCollection',
    'Controllers/Site/Patients/PatientsApp/Collections/GroupedHealthSessionCollection',
    'Controllers/Site/Patients/PatientsApp/Collections/UngroupedHealthSessionCollection',
    'Controllers/Site/Patients/PatientsApp/Views/GroupedHealthSessionCollectionView',
    'Controllers/Site/Patients/PatientsApp/Views/UngroupedHealthSessionCollectionView',
    'Controllers/Site/Patients/PatientsApp/Views/PatientDetailedDataFilterView'
], function (
    $,
    _,
    Backbone,
    app,
    Helpers,
    GroupedHealthSessionsDetailedCollection,
    UngroupedHealthSessionsDetailedCollection,
    AdhocMeasurementsDetailedCollection,
    GroupedHealthSessionCollection,
    UngroupedHealthSessionCollection,
    GroupedHealthSessionCollectionView,
    UngroupedHealthSessionCollectionView,
    PatientDetailedDataFilterView
) {
    return Backbone.View.extend({

        template: _.template($('#patientDetailedDataViewTemplate').html()),

        events: {
            'change .js-grouped': 'getData',
            'click .js-expand-all': 'expandAll',
            'click .js-collapse-all': 'collapseAll',
            'click .js-patient-export-detailed-data': 'exportPatientDetailedData'
        },

        initialize: function () {
            this.patientId = app.patientId;

            _.bindAll(this, "fetchGroupedDataSuccess", "fetchUngroupedDataSuccess");
        },

        render: function () {

            this.$el.html(this.template(this.model.attributes));

            app.views.patientDetailedDataFilterView = new PatientDetailedDataFilterView();
            this.$el.find('.js-detailed-data-date-filter').html(app.views.patientDetailedDataFilterView.render().el);

            this.listenTo(app.models.patientDetailedDataFilterModel, 'change', this.updateData);

            this.getData();

            return this;
        },

        getData: function () {

            this.isGrouped = this.$el.find(".js-grouped").is(':checked');

            Helpers.renderSpinner(this.$el.find('.js-detailed-data'));

            this.elementType = app.models.patientDetailedDataFilterModel.get('elementType');

            if (this.isGrouped) {
                this.getGroupedData();
                this.$el.find('.js-accordion-collapse-trigger').show();
            }
            else {
                this.getUngroupedData();
                this.$el.find('.js-accordion-collapse-trigger').hide();
            }

        },

        updateData: function () {

            Helpers.renderSpinner(this.$el.find('.js-detailed-data'));

            this.elementType = app.models.patientDetailedDataFilterModel.get('elementType');

            app.collections.groupedHealthSessionCollection = 0;
            app.collections.ungroupedHealthSessionCollection = 0;

            if (this.isGrouped) {
                this.sourceGroupedDataCount = 2;
                this.fetchGroupedData();
            }
            else {
                this.sourceUngroupedDataCount = 2;
                this.fetchUngroupedData();
            }
        },

        getGroupedData: function () {
            if (!app.collections.groupedHealthSessionCollection) {
                this.sourceGroupedDataCount = 2;
                this.fetchGroupedData();
            }
            else
                this.fetchGroupedDataCallback();

        },

        fetchGroupedData: function () {
            this.changeExportButtonState(false);

            app.collections.groupedHealthSessionCollection = {};
            app.collections.groupedHealthSessionCollection.isFetched = false;

            app.collections.groupedHealthSessionsDetailedCollection = new GroupedHealthSessionsDetailedCollection();
            app.collections.groupedHealthSessionsDetailedCollection.fetch({ success: this.fetchGroupedDataSuccess });

            if (this.elementType == 'All' || this.elementType == 'Measurement') {
                app.collections.adhocMeasurementsDetailedCollection = new AdhocMeasurementsDetailedCollection();
                app.collections.adhocMeasurementsDetailedCollection.fetch({ success: this.fetchGroupedDataSuccess });
            } else {
                app.collections.adhocMeasurementsDetailedCollection = new Backbone.Collection();
                this.fetchGroupedDataSuccess();
            }

        },

        fetchGroupedDataSuccess: function () {

            this.sourceGroupedDataCount -= 1;

            if (!this.sourceGroupedDataCount) {
                app.collections.groupedHealthSessionCollection.isFetched = true;
                this.fetchGroupedDataCallback();
            }
        },

        getUngroupedData: function () {

            if (!app.collections.ungroupedHealthSessionCollection) {
                this.sourceUngroupedDataCount = 2;
                this.fetchUngroupedData();
            }
            else
                this.fetchUngroupedDataCallback();

        },

        fetchUngroupedData: function () {
            this.changeExportButtonState(false);

            app.collections.ungroupedHealthSessionCollection = {};
            app.collections.ungroupedHealthSessionCollection.isFetched = false;

            app.collections.ungroupedHealthSessionsDetailedCollection = new UngroupedHealthSessionsDetailedCollection();
            app.collections.ungroupedHealthSessionsDetailedCollection.fetch({ success: this.fetchUngroupedDataSuccess });

            if (this.elementType == 'All' || this.elementType == 'Measurement') {
                app.collections.adhocMeasurementsDetailedCollection = new AdhocMeasurementsDetailedCollection();
                app.collections.adhocMeasurementsDetailedCollection.fetch({ success: this.fetchUngroupedDataSuccess });
            } else {
                app.collections.adhocMeasurementsDetailedCollection = new Backbone.Collection();
                this.fetchUngroupedDataSuccess();
            }

        },

        fetchUngroupedDataSuccess: function () {

            this.sourceUngroupedDataCount -= 1;

            if (!this.sourceUngroupedDataCount) {
                app.collections.ungroupedHealthSessionCollection.isFetched = true;
                this.fetchUngroupedDataCallback();
            }
        },

        fetchGroupedDataCallback: function () {

            if (app.collections.groupedHealthSessionCollection.isFetched) {

                app.collections.groupedHealthSessionsDetailedCollection.each(function (model) {
                    // var calendarItemId = model.get('calendarItemId');
                    // model.set('id', calendarItemId);
                    model.set('id', Helpers.getGUID() );
                    model.set('healthSessionType', 0);
                });

                app.collections.adhocMeasurementsDetailedCollection.each(function (model) {
                    var observed = model.get('observed');
                    model.set('completed', observed);
                    model.set('healthSessionType', 1);
                });

                var groupedHealthSessionArray = app.collections.groupedHealthSessionsDetailedCollection.toJSON()
                                        .concat(app.collections.adhocMeasurementsDetailedCollection.toJSON());

                app.collections.groupedHealthSessionCollection = new GroupedHealthSessionCollection(groupedHealthSessionArray);
                app.collections.groupedHealthSessionCollection.isFetched = true;

                if (app.views.groupedHealthSessionCollectionView)
                    app.views.groupedHealthSessionCollectionView.remove();

                app.views.groupedHealthSessionCollectionView = new GroupedHealthSessionCollectionView({ collection: app.collections.groupedHealthSessionCollection });

                if (this.isGrouped)
                    this.$el.find('.js-detailed-data').html(app.views.groupedHealthSessionCollectionView.render().el);

                this.changeExportButtonState(true);
            }

        },

        fetchUngroupedDataCallback: function () {
            var that = this;

            if (app.collections.ungroupedHealthSessionCollection.isFetched) {

                app.collections.ungroupedHealthSessionsDetailedCollection.each(function (model) {
                    model.set('healthSessionType', 0);
                });

                app.collections.adhocMeasurementsDetailedCollection.each(function (model) {
                    var observed = model.get('observed');
                    model.set('answered', observed);
                    model.set('healthSessionType', 1);
                });

                var ungroupedHealthSessionArray = app.collections.ungroupedHealthSessionsDetailedCollection.toJSON()
                                        .concat(app.collections.adhocMeasurementsDetailedCollection.toJSON());

                app.collections.ungroupedHealthSessionCollection = new UngroupedHealthSessionCollection(ungroupedHealthSessionArray);
                app.collections.ungroupedHealthSessionCollection.isFetched = true;

                if (app.views.ungroupedHealthSessionCollectionView)
                    app.views.ungroupedHealthSessionCollectionView.remove();

                app.collections.ungroupedHealthSessionCollection.each(function (model) {
                    model.set('patientId', that.model.get('id'));
                    model.set('timeZone', that.model.get('timeZone'));
                });
                app.views.ungroupedHealthSessionCollectionView = new UngroupedHealthSessionCollectionView({ collection: app.collections.ungroupedHealthSessionCollection });

                if (!this.isGrouped)
                    this.$el.find('.js-detailed-data').html(app.views.ungroupedHealthSessionCollectionView.render().el);

                this.changeExportButtonState(true);
            }
        },

        expandAll: function () {
            this.$el.find('.panel-collapse')
                .addClass('in')
                .height('auto');

            this.$el.find('.panel-heading').removeClass('collapsed');
        },

        collapseAll: function () {
            this.$el.find('.panel-collapse').removeClass('in');
            this.$el.find('.panel-heading').addClass('collapsed');
        },

        changeExportButtonState: function (state) {
            var exportButton = this.$el.find('.js-patient-export-detailed-data');

            if (state) {
                if ((this.$el.find('.js-detailed-data>table').length > 0 && this.$el.find('.js-detailed-data>table').html() !== "") ||
                    (this.$el.find('.js-detailed-data>div').length > 0 && this.$el.find('.js-detailed-data>div').html() !== "")
                ) {
                    exportButton.removeClass('disabled');
                }
            } else {
                if (!exportButton.hasClass('disabled')) {
                    exportButton.addClass('disabled');
                }
            }
        },

        exportPatientDetailedData: function (e) {
            e.preventDefault();

            var toDate = app.models.patientDetailedDataFilterModel.get('toDate');

            if (toDate != undefined) {
                toDate = toDate + 'T23:59:59';
            }

            // Downloading report
            window.location = '/Patients/ExportDetailedDataToExcel?patientId=' + this.model.get('id') +
                '&groupByHealthSession=' + this.isGrouped +
                '&observedFromUtc=' + app.models.patientDetailedDataFilterModel.get('fromDate') +
                '&observedToUtc=' + toDate +
                '&elementType=' + app.models.patientDetailedDataFilterModel.get('elementType');
        }
    });
});