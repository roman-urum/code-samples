'use strict';

define([
    'backbone',
    'moment',
    'Controllers/Site/Patients/PatientsApp/AppNamespace',
    'Controllers/Constants'
], function (Backbone, Moment, patient, CONSTANTS) {
    var defaultColor = '#000';

    function parseMeasurementReading(data) {
        var time = Moment(data.updated);
        var name = data.vitals[0].name === 'Systolic Blood Pressure' || data.vitals[0].name === 'Diastolic Blood Pressure' ?
            'Blood Pressure' : data.vitals[0].name;

        return {
            date: time.format('MM/DD/YYYY'),
            time: time.format('hh:mm A'),
            name: name,
            isAutomated: data.isAutomated,
            vitals: _.map(data.vitals, function (vital) {
                return {
                    value: vital.value,
                    unit: vital.unit,
                    name: vital.name,
                    color: (function () {
                        return (vital.vitalAlert && vital.vitalAlert.alertSeverity && vital.vitalAlert.alertSeverity.colorCode) ?
                            vital.vitalAlert.alertSeverity.colorCode :
                            defaultColor;
                    }())
                };
            })
        };
    }

    function parseBehavior(data) {
        var time = Moment(data.answeredUtc);

        return {
            date: time.format('MM/DD/YYYY'),
            time: time.format('hh:mm A'),
            question: data.text,
            answers: _.map(data.values, function (value) { return value.text || value.value }),
            answer: data.values[0].text || data.values[0].value,
            color: (function () {
                return (data.healthSessionElementAlert && data.healthSessionElementAlert.alertSeverity && data.healthSessionElementAlert.alertSeverity.colorCode) ?
                    data.healthSessionElementAlert.alertSeverity.colorCode :
                    defaultColor;
            }())
        };
    }

    return Backbone.Model.extend({
        initialize: function () {
            this
                .detectUtc()
                .detectVital()
                .detectHealthSession();
        },
        detectUtc: function () {
            this.attributes.createdUtcFormated = Moment(this.attributes.createdUtc).format('MM/DD/YYYY');

            return this;
        },
        detectVital: function () {
            var vital = this.attributes.vitalReading;

            if (vital) {
                this.attributes.measurement = parseMeasurement(vital);
            }

            return this;
        },
        detectHealthSession: function () {
            var healthSessionElement = this.attributes.healthSessionElementReading;

            if (this.attributes.measurementReading) {
                this.attributes.measurementReading = parseMeasurementReading(this.attributes.measurementReading);
            }

            if (healthSessionElement && healthSessionElement.type === 1) {
                this.attributes.behavior = parseBehavior(healthSessionElement);
            }

            return this;
        }
    });
});