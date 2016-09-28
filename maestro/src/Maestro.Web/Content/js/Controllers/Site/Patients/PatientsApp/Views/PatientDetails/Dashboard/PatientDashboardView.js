'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'highcharts',
    'async',
    'Controllers/Site/Patients/PatientsApp/AppNamespace',
    'Controllers/Helpers',
    'moment',
    'Controllers/Constants',
    'Controllers/Site/Patients/PatientsApp/Models/PatientDetails/Dashboard/LastHealthSessionModel',
    'Controllers/Site/Patients/PatientsApp/Models/PatientDetails/Dashboard/HealthSessionsModel',
    'Controllers/Site/Patients/PatientsApp/Models/PatientDetails/Dashboard/ThresholdsModel',
    'Controllers/Site/Patients/PatientsApp/Models/PatientDetails/Dashboard/PeripheralsModel',
    './../../BaseMaestroView',
    './HealthSessionsView',
    './ThresholdsView',
    './PeripheralsView',
    'Controllers/Site/Notes/NotesApp',
    //'backbone.bootstrap-modal'
], function ($,
            _,
            Backbone,
            HighchartsLib,
            async,
            app,
            Helpers,
            moment,
            Constants,
            LastHealthSessionModel,
            HealthSessionsModel,
            ThresholdsModel,
            PeripheralsModel,
            BaseMaestroView,
            HealthSessionsView,
            ThresholdsView,
            PeripheralsView,
            notes) {
    return BaseMaestroView.extend({

        template: _.template($('#patientDashboardViewTemplate').html()),

        regions: {},

        events: {
            'click .js-show-health-session': 'showHealthSession',
            'click .js-show-readings': 'showReadings',
            'click.add-note button.add-note': 'addNote'
        },

        addNote: function (e) {
            e.preventDefault();

            var item = _.find(this.lastHealthSessionModel.get('latestReadings'), function (item) {
                return item.id === $(e.currentTarget).attr('data-id');
            }),
                type = notes.types();

            item.timeZone = this.patientModel.get('timeZone');

            notes.add(this.model.get('id'), type.MEASUREMENT, type.VITAL, notes.parse.from.patientDashboard(type.MEASUREMENT, item));
        },

        initialize: function (options) {
            if (!options) {
                throw 'Options required';
            }

            var self = this;
            this.patientId = app.patientId;
            this.currentTabIndex = 0;
            this.isDataFetched = false;

            this.healthSessionsModel = new HealthSessionsModel(null, { patientId: app.patientId });
            this.thresholdsModel = new ThresholdsModel(null, { patientId: app.patientId });
            this.peripheralsModel = new PeripheralsModel(null, { patientId: app.patientId });
            this.lastHealthSessionModel = new LastHealthSessionModel(null, { patientId: app.patientId });

            var workers = [];
            workers.push(function (cb) {
                self.healthSessionsModel.fetch({
                    success: function (model) {
                        cb(null, model);
                    },
                    error: function (err) {
                        cb(err);
                    }
                });
            });
            workers.push(function (cb) {
                self.thresholdsModel.fetch({
                    success: function (model) {
                        cb(null, model);
                    },
                    error: function (err) {
                        cb(err);
                    }
                });
            });
            workers.push(function (cb) {
                self.peripheralsModel.fetch({
                    success: function (model) {
                        cb(null, model);
                    },
                    error: function (err) {
                        cb(err);
                    }
                });
            });
            workers.push(function (cb) {
                self.lastHealthSessionModel.fetch({
                    success: function (model) {
                        cb(null, model);
                    },
                    error: function (err) {
                        cb(err);
                    }
                });
            });

            async.parallel(workers, function (err, results) {
                self.isDataFetched = true;
                self.healthSessionsModel.isFetched = true;
                self.thresholdsModel.isFetched = true;
                self.peripheralsModel.isFetched = true;
                self.lastHealthSessionModel.isFetched = true;
                self.render();
            });

            this.healthSessionsView = new HealthSessionsView({ model: this.healthSessionsModel });
            this.thresholdsView = new ThresholdsView({ model: this.thresholdsModel });
            this.peripheralsView = new PeripheralsView({ model: this.peripheralsModel });

            this.patientModel = options.model;
        },

        render: function () {
            //var data = _.extend({}, this.patientModel.toJSON(), this.lastHealthSessionModel.toJSON());
            this.$el.html(this.template(this.calculateTemplateData()));
            if (this.isDataFetched) {
                this.renderTabsButtons();
                this.renderHealthSessionsView();
                this.renderThresholdsView();
                this.renderPeripheralsView();
            }
            return this;
        },

        renderTabsButtons: function () {
            if (this.currentTabIndex == 0) {
                this.$('.js-show-readings').removeClass('active');
                this.$('.js-show-health-session').addClass('active');
            } else if (this.currentTabIndex == 1) {
                this.$('.js-show-health-session').removeClass('active');
                this.$('.js-show-readings').addClass('active');
            }
        },

        renderHealthSessionsView: function () {
            var view = this.healthSessionsView;
            view.render();
            this.$('.health-sessions-block').html(view.$el);
        },

        renderThresholdsView: function () {
            var view = this.thresholdsView;
            view.render();
            this.$('.thresholds-block').html(view.$el);
        },

        renderPeripheralsView: function () {
            var view = this.peripheralsView;
            view.render();
            this.$('.peripherals-block').html(view.$el);
        },

        calculateTemplateData: function () {
            var currentTabIndex = this.currentTabIndex;

            var lastHealthSessionData = this.lastHealthSessionModel.toJSON();
            
            var latestHealthSessionReadings = _.map(lastHealthSessionData.latestHealthSessionReadings, function (reading) {
                return reading;
            });

            var latestReadings = _.map(lastHealthSessionData.latestReadings, function (reading) {
                var datetime = moment.parseZone(reading.date);

                reading.dateText = datetime.format('M/D/YYYY');
                reading.timeText = datetime.format('hh:mm A (Z)');

                return reading;
            });

            var healthSessionQuestions = _.map(lastHealthSessionData.latestHealthSessionQuestionsAndAnswers, function (reading) {
                return reading;
            });

            return {
                id: app.models.patientModel.get('id'),
                siteId: app.models.patientModel.get('siteId'),
                isModelFetched: this.lastHealthSessionModel.isFetched,
                currentTabIndex: currentTabIndex,
                latestHealthSessionDateText: lastHealthSessionData.latestHealthSessionDate,
                healthSessionReadings: latestHealthSessionReadings,
                readings: latestReadings,
                healthSessionQuestions: healthSessionQuestions
            }
        },

        showHealthSession: function () {
            this.currentTabIndex = 0;
            this.render();
        },

        showReadings: function () {
            this.currentTabIndex = 1;
            this.render();
        }

    });
});