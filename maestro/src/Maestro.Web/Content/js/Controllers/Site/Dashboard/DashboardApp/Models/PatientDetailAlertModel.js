'use strict';

define([
    'backbone',
    'moment',
    'underscore',
    'Controllers/Site/Dashboard/DashboardApp/AppNamespace',
    'Controllers/Helpers'
], function(Backbone, Moment, _, app, Helpers) {
    var thresholdCollection = Helpers.getThresholdCollection();
    return Backbone.Model.extend({
        initialize: function() {
            if (this.get('name') !== 'BloodPressure') {
                this
                    .detectDate()
                    .detectColor()
                    .detectThreshold()
                    .hasMoreReadings()
                    .parseReadings()
                    .sortReadings()
                    .detectLabels();
            } else {
                this
                    .detectDateDouble()
                    .detectColorDouble()
                    .detectThresholdDouble()
                    .hasMoreReadingsDouble()
                    .parseReadingsDouble()
                    .sortReadingsDouble()
                    .detectLabelsDouble();
            }
        },

        detectLabels: function() {
            var name = this.attributes.reading.name;

            if (name && thresholdCollection[name]) {
                this.attributes.reading.name = thresholdCollection[name].nameLabel;
                this.attributes.reading.unit = thresholdCollection[name].unitLabel;
            }

            return this;
        },

        detectLabelsDouble: function() {
            var name = this.attributes.name;

            if (name && thresholdCollection[name]) {
                this.attributes.nameLabel = thresholdCollection[name].nameLabel;
                this.attributes.unitLabel = thresholdCollection[name].unitLabel;
            }

            return this;
        },

        detectColor: function() {
            this.attributes.color = (this.attributes.noColor = this.attributes.reading.alert.alertSeverity && this.attributes.reading.alert.alertSeverity.colorCode) ?
                this.attributes.reading.alert.alertSeverity.colorCode :
                app.OPTIONS.ALERT.NOT_EXISTING_COLOR;

            return this;
        },

        detectColorDouble: function() {
            if (!this.attributes.reading.measurement.vitals.systolicBloodPressure.vitalAlert && !this.attributes.reading.measurement.vitals.diastolicBloodPressure.vitalAlert) {
                this.attributes.noColor = false;
                this.attributes.color = app.OPTIONS.ALERT.NOT_EXISTING_COLOR;
            } else if (!this.attributes.reading.measurement.vitals.systolicBloodPressure.vitalAlert) {
                if (this.attributes.reading.measurement.vitals.diastolicBloodPressure.vitalAlert &&
                    this.attributes.reading.measurement.vitals.diastolicBloodPressure.vitalAlert.alertSeverity &&
                    this.attributes.reading.measurement.vitals.diastolicBloodPressure.vitalAlert.alertSeverity.colorCode) {

                    this.attributes.noColor = true;
                    this.attributes.color = this.attributes.reading.measurement.vitals.diastolicBloodPressure.vitalAlert.alertSeverity.colorCode;
                }
            } else if (!this.attributes.reading.measurement.vitals.diastolicBloodPressure.vitalAlert) {
                if (this.attributes.reading.measurement.vitals.systolicBloodPressure.vitalAlert &&
                    this.attributes.reading.measurement.vitals.systolicBloodPressure.vitalAlert.alertSeverity &&
                    this.attributes.reading.measurement.vitals.systolicBloodPressure.vitalAlert.alertSeverity.colorCode) {

                    this.attributes.noColor = true;
                    this.attributes.color = this.attributes.reading.measurement.vitals.systolicBloodPressure.vitalAlert.alertSeverity.colorCode;
                }
            } else {
                this.attributes.noColor = true;

                if (this.attributes.reading.measurement.vitals.diastolicBloodPressure.vitalAlert.alertSeverity &&
                    this.attributes.reading.measurement.vitals.systolicBloodPressure.vitalAlert.alertSeverity) {
                    this.attributes.color = this.attributes.reading.measurement.vitals.diastolicBloodPressure.vitalAlert.alertSeverity.severity >=
                        this.attributes.reading.measurement.vitals.systolicBloodPressure.vitalAlert.alertSeverity.severity ?
                        this.attributes.reading.measurement.vitals.diastolicBloodPressure.vitalAlert.alertSeverity.colorCode :
                        this.attributes.reading.measurement.vitals.systolicBloodPressure.vitalAlert.alertSeverity.colorCode;
                } else {
                    this.attributes.color = app.OPTIONS.ALERT.NOT_EXISTING_COLOR;
                }
            }

            if (this.attributes.reading.measurement.vitals.diastolicBloodPressure.vitalAlert &&
                this.attributes.reading.measurement.vitals.diastolicBloodPressure.vitalAlert.alertSeverity &&
                this.attributes.reading.measurement.vitals.diastolicBloodPressure.vitalAlert.alertSeverity.colorCode) {

                this.attributes.reading.measurement.vitals.diastolicBloodPressure.color =
                    this.attributes.reading.measurement.vitals.diastolicBloodPressure.vitalAlert.alertSeverity.colorCode;
            }

            if (this.attributes.reading.measurement.vitals.systolicBloodPressure.vitalAlert &&
                this.attributes.reading.measurement.vitals.systolicBloodPressure.vitalAlert.alertSeverity &&
                this.attributes.reading.measurement.vitals.systolicBloodPressure.vitalAlert.alertSeverity.colorCode) {

                this.attributes.reading.measurement.vitals.systolicBloodPressure.color =
                    this.attributes.reading.measurement.vitals.systolicBloodPressure.vitalAlert.alertSeverity.colorCode;
            }

            return this;
        },

        detectDate: function() {
            if (this.attributes.reading) {
                this.attributes.reading.formattedDate = Moment(this.attributes.reading.date).format(app.OPTIONS.ALERT.DATE_TYPE);
            }

            return this;
        },

        detectDateDouble: function() {
            if (this.attributes.reading.measurement.vitals.diastolicBloodPressure) {
                this.attributes.reading.measurement.vitals.diastolicBloodPressure.formattedDate = Moment(this.attributes.reading.measurement.vitals.diastolicBloodPressure.observedUtc).format(app.OPTIONS.ALERT.DATE_TYPE);
            }
            if (this.attributes.reading.measurement.vitals.systolicBloodPressure) {
                this.attributes.reading.measurement.vitals.systolicBloodPressure.formattedDate = Moment(this.attributes.reading.measurement.vitals.systolicBloodPressure.observedUtc).format(app.OPTIONS.ALERT.DATE_TYPE);
            }

            return this;
        },

        detectThreshold: function() {
            if (this.attributes.threshold) {
                this.attributes.threshold.isHigher = this.attributes.reading.value > this.attributes.threshold.maxValue;
                this.attributes.threshold.than = this.attributes.reading.value;
            }

            return this;
        },

        detectThresholdDouble: function() {
            if (this.attributes.reading.measurement.vitals.systolicBloodPressure && this.attributes.reading.measurement.vitals.systolicBloodPressure.vitalAlert) {
                this.attributes.reading.measurement.vitals.systolicBloodPressure.isHigher =
                    this.attributes.reading.measurement.vitals.systolicBloodPressure.value >
                    this.attributes.reading.measurement.vitals.systolicBloodPressure.vitalAlert.violatedThreshold.maxValue;
                this.attributes.reading.measurement.vitals.systolicBloodPressure.than =
                    this.attributes.reading.measurement.vitals.systolicBloodPressure.isHigher ?
                        this.attributes.reading.measurement.vitals.systolicBloodPressure.vitalAlert.violatedThreshold.maxValue :
                        this.attributes.reading.measurement.vitals.systolicBloodPressure.vitalAlert.violatedThreshold.minValue;
            }

            if (this.attributes.reading.measurement.vitals.diastolicBloodPressure && this.attributes.reading.measurement.vitals.diastolicBloodPressure.vitalAlert) {
                this.attributes.reading.measurement.vitals.diastolicBloodPressure.isHigher =
                    this.attributes.reading.measurement.vitals.diastolicBloodPressure.value >
                    this.attributes.reading.measurement.vitals.diastolicBloodPressure.vitalAlert.violatedThreshold.maxValue;
                this.attributes.reading.measurement.vitals.diastolicBloodPressure.than =
                    this.attributes.reading.measurement.vitals.diastolicBloodPressure.isHigher ?
                        this.attributes.reading.measurement.vitals.diastolicBloodPressure.vitalAlert.violatedThreshold.maxValue :
                        this.attributes.reading.measurement.vitals.diastolicBloodPressure.vitalAlert.violatedThreshold.minValue;
            }

            return this;
        },

        hasMoreReadings: function() {
            if (this.attributes.recentReadings) {
                this.attributes.hasMore = this.attributes.totalRecentReadingsCount - this.attributes.recentReadings.length;
            }

            return this;
        },

        hasMoreReadingsDouble: function() {
            if (this.attributes.recentReadings) {
                this.attributes.hasMore = this.attributes.totalRecentReadingsCount - this.attributes.recentReadings.length;
            }

            return this;
        },

        parseReadings: function() {
            if (this.attributes.recentReadings) {
                if (this.attributes.reading.alert.type.CODE === app.OPTIONS.ALERT.TYPES.ADHERENCE.CODE) {
                    _.each(this.attributes.recentReadings, function(recentReading) {
                        var moment = Moment.parseZone(recentReading.date);

                        recentReading.first = moment.format(app.OPTIONS.RECENT_READINGS.FULL_DATE);
                        recentReading.second = recentReading.calendarEvent.name + (recentReading.calendarEvent.programDay !== null ? (' - Day ' + recentReading.calendarEvent.programDay) : '');
                        recentReading.third = app.OPTIONS.ADHERENCE.GET_STATUS_BY_CODE(recentReading.status).NAME;
                    });
                } else {
                    _.each(this.attributes.recentReadings, function(recentReading) {
                        var moment = Moment.parseZone(recentReading.date);

                        recentReading.first = moment.format(app.OPTIONS.RECENT_READINGS.DATE);
                        recentReading.second = moment.format(app.OPTIONS.RECENT_READINGS.TIME);
                        recentReading.color = recentReading.alert ?
                            recentReading.alert.alertSeverity ?
                                recentReading.alert.alertSeverity.colorCode :
                                app.OPTIONS.ALERT.DEFAULT_COLOR :
                            app.OPTIONS.ALERT.DEFAULT_COLOR;
                    });
                }
            }

            return this;
        },

        parseReadingsDouble: function() {
            if (this.attributes.recentReadings) {
                _.each(this.attributes.recentReadings, function(recentReading) {
                    var moment;

                    if (recentReading.measurement.vitals) {
                        if (recentReading.measurement.vitals.diastolicBloodPressure) {
                            moment = Moment.parseZone(recentReading.measurement.vitals.diastolicBloodPressure.observedUtc);

                            recentReading.measurement.vitals.diastolicBloodPressure.first = moment.format(app.OPTIONS.RECENT_READINGS.DATE);
                            recentReading.measurement.vitals.diastolicBloodPressure.second = moment.format(app.OPTIONS.RECENT_READINGS.TIME);
                            recentReading.measurement.vitals.diastolicBloodPressure.color =
                                recentReading.measurement.vitals.diastolicBloodPressure.vitalAlert ?
                                    recentReading.measurement.vitals.diastolicBloodPressure.vitalAlert.alertSeverity ?
                                        recentReading.measurement.vitals.diastolicBloodPressure.vitalAlert.alertSeverity.colorCode :
                                        app.OPTIONS.ALERT.DEFAULT_COLOR :
                                    app.OPTIONS.ALERT.DEFAULT_COLOR;
                        }

                        if (recentReading.measurement.vitals.systolicBloodPressure) {
                            moment = Moment.parseZone(recentReading.measurement.vitals.systolicBloodPressure.observedUtc);

                            recentReading.measurement.vitals.systolicBloodPressure.first = moment.format(app.OPTIONS.RECENT_READINGS.DATE);
                            recentReading.measurement.vitals.systolicBloodPressure.second = moment.format(app.OPTIONS.RECENT_READINGS.TIME);
                            recentReading.measurement.vitals.systolicBloodPressure.color =
                                recentReading.measurement.vitals.systolicBloodPressure.vitalAlert ?
                                    recentReading.measurement.vitals.systolicBloodPressure.vitalAlert.alertSeverity ?
                                        recentReading.measurement.vitals.systolicBloodPressure.vitalAlert.alertSeverity.colorCode :
                                        app.OPTIONS.ALERT.DEFAULT_COLOR :
                                    app.OPTIONS.ALERT.DEFAULT_COLOR;
                        }
                    }
                });
            }

            return this;
        },

        sortReadings: function() {
            if (this.attributes.recentReadings) {
                this.attributes.recentReadings = _.sortBy(this.attributes.recentReadings, function(recentReading) {
                    return -(+Moment(recentReading.date));
                });
            }

            return this;
        },

        sortReadingsDouble: function() {
            if (this.attributes.recentReadings) {
                this.attributes.recentReadings = _.sortBy(this.attributes.recentReadings, function(recentReading) {
                    return recentReading.measurement.vitals && recentReading.measurement.vitals.systolicBloodPressure ?
                        -(+Moment(recentReading.measurement.vitals.systolicBloodPressure.observedUtc)) :
                        -Number.MAX_VALUE;
                });
            }

            return this;
        }
    });
});