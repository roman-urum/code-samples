'use strict';

define([
    'jquery',
    'moment',
    'Controllers/Site/Notes/AppNamespace',
    'Controllers/Constants',
    'underscore'
], function ($, Moment, app, constants, _) {
    var defaultColor = '#000';

    function patientDashboardMeasurement(data) {
        var datetime = Moment.parseZone(data.date);

        return {
            id: data.id,
            measurementId: data.measurement.id,
            date: datetime.format('MM/DD/YYYY'),
            time: datetime.format('hh:mm A'),
            name: data.name,
            isAutomated: data.isAutomated,
            value: data.value,
            unit: data.unit,
            vitals: _.map(data.measurement.vitals, function (vital) {
                var vitalObj = vital;
                vitalObj.color = (vital.alert && vital.alert.alertSeverity && vital.alert.alertSeverity.colorCode) ?
                    vital.alert.alertSeverity.colorCode :
                    (vital.alerts && vital.alerts.systolic && vital.alerts.systolic.alertSeverity && vital.alerts.systolic.alertSeverity.colorCode) ?
                        vital.alerts.systolic.alertSeverity.colorCode :
                        defaultColor;
                return vitalObj;
            })
        };
    }

    function detailedDataMeasurement(data) {
        return {
            id: data.vital.id,
            measurementId: data.measurementId,
            date: data.answeredDate,
            time: data.answeredTime,
            name: data.vital.name,
            isAutomated: data.isAutomated || true,
            value: data.vital.value || data.vital.values.systolic + '/' + data.vital.values.diastolic,
            unit: data.vital.unit,
            vitals: _.map(data.vitals, function (vital) {
                var vitalObj = vital;
                vitalObj.color = (vital.alert && vital.alert.alertSeverity && vital.alert.alertSeverity.colorCode) ?
                    vital.alert.alertSeverity.colorCode :
                    (vital.alerts && vital.alerts.systolic && vital.alerts.systolic.alertSeverity && vital.alerts.systolic.alertSeverity.colorCode) ?
                        vital.alerts.systolic.alertSeverity.colorCode :
                        defaultColor;
                return vitalObj;
            })
        };
    }

    function detailedDataBehavior(data) {
        return {
            id: data.id,
            date: data.answeredDate,
            time: data.answeredTime,
            question: data.text,
            answers: _.map(data.values, function (value) {return value.text || value.value}),
            answer: data.values[0].text || data.values[0].value,
            color: (function () {
                return (data.healthSessionElementAlert && data.healthSessionElementAlert.alertSeverity && data.healthSessionElementAlert.alertSeverity.colorCode) ?
                    data.healthSessionElementAlert.alertSeverity.colorCode :
                    defaultColor;
            }())
        };
    }

    return {
        from: {
            patientDashboard: function (type, data) {
                switch (type) {
                    case app.types.MEASUREMENT:
                        {
                            return patientDashboardMeasurement(data);
                        }
                }
            },
            detailedData: function (type, data) {
                switch (type) {
                    case app.types.MEASUREMENT:
                        {
                            return detailedDataMeasurement(data);
                        }
                    case app.types.BEHAVIOUR:
                        {
                            return detailedDataBehavior(data);
                        }
                }
            }
        }
    };
});