'use strict';

define([
        'jquery',
        'underscore',
        'Controllers/Site/Dashboard/Helpers/SessionStorage',
        'Controllers/Site/Dashboard/Helpers/DashboardOptions',
        'Controllers/Helpers'
], function ($, _, storage, OPTIONS, Helpers) {
    var _storage = storage,
        patientsKey = 'patients',
        detailsKey = 'details',
        patientsTotalKey = 'total',
        countsKey = 'counts',
        ajaxPatients = {
            type: 'GET',
            dataType: 'json',
            traditional: true,
            url: OPTIONS.URL.PATIENTS_CARDS
        },
        ajaxPatientDetails = {
            type: 'GET',
            dataType: 'json',
            traditional: true,
            url: OPTIONS.URL.PATIENT_DETAILS
        },
        ajaxAcknowledgeAlert = {
            type: 'POST',
            traditional: true,
            url: OPTIONS.URL.ACKNOWLEDGE_ALERT
        },
        ajaxIgnoreAlert = {
            type: 'POST',
            traditional: true,
            url: OPTIONS.URL.IGNORE_ALERT
        };

    function hasMore() {
        var patients = _storage.getItem(patientsKey);

        if (!patients) {
            return true;
        }

        return !!(patients.length + 1 <= _storage.getItem(patientsTotalKey) || 0);
    }

    function parseBloodPressure(data) {
        var names = [
            'SystolicBloodPressure',
            'DiastolicBloodPressure'
        ];

        function parseVitalName(name) {
            return typeof (name) === 'string' ? name.split(' ').join('') : '';
        }

        function hasPartialBloodPressure(vitals) {
            var hasSystolicBloodPressure = false,
                hasDiastolicBloodPressure = false;

            _.each(vitals, function loop(vital) {
                if (loop.hasMatches) {
                    return;
                }

                if (parseVitalName(vital.name) === names[0]) {
                    hasSystolicBloodPressure = true;
                }

                if (parseVitalName(vital.name) === names[1]) {
                    hasDiastolicBloodPressure = true;
                }
            });

            return (hasSystolicBloodPressure || hasDiastolicBloodPressure) && (hasSystolicBloodPressure !== hasDiastolicBloodPressure);
        }

        function hasBloodPressure(vitals) {
            var hasSystolicBloodPressure = false,
                hasDiastolicBloodPressure = false;

            _.each(vitals, function loop(vital) {
                if (parseVitalName(vital.name) === names[0]) {
                    hasSystolicBloodPressure = true;
                }

                if (parseVitalName(vital.name) === names[1]) {
                    hasDiastolicBloodPressure = true;
                }
            });

            return hasSystolicBloodPressure && hasDiastolicBloodPressure;
        }

        function getCurrentVitals(vitals) {
            return _.filter(vitals, function (vital) {
                return !!_.filter(names, function (name) {
                    return parseVitalName(vital.name) === name;
                }).length;
            });
        }

        function getGeneratedVitals(vitals) {
            return {
                systolicBloodPressure: _.find(vitals, function (vital) {
                    return parseVitalName(vital.name) === names[0];
                }),
                diastolicBloodPressure: _.find(vitals, function (vital) {
                    return parseVitalName(vital.name) === names[1];
                })
            };
        }

        function cleanData(data) {
            var length = data.length,
                newData = [],
                count = 0;

            for (; count < length; count += 1) {
                if (data[count]) {
                    newData.push(data[count]);
                }
            }
            return newData;
        }

        (function () {
            var length = data.length,
                count = 0;

            for (; count < length; count += 1) {
                var item = data[count],
                    hasVitals = !!(item.reading && item.reading.measurement && item.reading.measurement.vitals);

                if (hasVitals && hasPartialBloodPressure(item.reading.measurement.vitals)) {

                    item.reading.measurement.vitals.push({
                        id: null,
                        name: parseVitalName(item.reading.measurement.vitals[0].name) === names[1] ? "Systolic Blood Pressure" : "Diastolic Blood Pressure",
                        unit: "mmHg",
                        value: "-",
                        vitalAlert: null
                    });

                    //delete data[count];

                }
                if (hasVitals && (data[count].reading.name === names[0] || data[count].reading.name === names[1])) {
                    var vitals = item.reading.measurement.vitals,
                        hasBP = hasBloodPressure(vitals);

                    if (hasBP) {
                        vitals = getCurrentVitals(vitals);

                        if (vitals.length === names.length) {
                            data[count].name = 'BloodPressure';
                            data[count].reading.measurement.vitals = getGeneratedVitals(vitals);

                            (function () {
                                var hasRecentReadings = !!(data[count].recentReadings && data[count].recentReadings.length),
                                    hasBP = false,
                                    localCount = 0;

                                if (hasRecentReadings) {
                                    var tempReadings = [];
                                    for (; localCount < data[count].recentReadings.length; localCount += 1) {
                                        var vitals = data[count].recentReadings[localCount].measurement.vitals;
                                        hasBP = hasBloodPressure(vitals);

                                        if (hasBP) {
                                            tempReadings.push($.extend({}, data[count].recentReadings[localCount], {
                                                measurement: $.extend({}, data[count].recentReadings[localCount].measurement, {
                                                    vitals: getGeneratedVitals(vitals)
                                                })
                                            }));
                                        }
                                    }

                                    if (tempReadings.length) {
                                        data[count].recentReadings = tempReadings;
                                    }
                                }
                            }());
                        }
                    }
                }
            }
        }());

        return cleanData(data);
    }

    function getCounts() {
        return _storage.getItem(countsKey);
    }

    function getPatients(take, types, severities, careManagerId) {
        var patients = _storage.getItem(patientsKey),
            skip = patients ? patients.length : 0,
            totalCount = skip + take,
            deferred = $.Deferred();

        patients = patients || [];

        var resolve = function (response, counts) {
            _storage.setItem(countsKey, counts);
            _storage.setItem(patientsKey, $.extend(true, [], patients));
            _storage.setItem(patientsTotalKey, response.total);

            deferred.resolve(patients.slice(skip, totalCount));
        };

        if (patients && patients.length >= totalCount) {
            deferred.resolve(patients.slice(skip, totalCount));
        }

        if (patients && patients.length < totalCount) {
            $.ajax($.extend({}, ajaxPatients, {
                data: {
                    skip: patients.length,
                    take: totalCount - patients.length,
                    types: types || [],
                    severityIds: severities || [],
                    careManagerId: careManagerId
                },
                success: function (response) {
                    response = Helpers.convertKeysToCamelCase(response);

                    patients = patients.concat(response.patientCards.results);

                    resolve(response.patientCards, response.counts);
                },
                error: function (e) {
                    deferred.reject(e);
                }
            }));
        }

        return deferred.promise();
    }

    function getPatientDetails(patientId, types, severities, careManagerId) {

        if (!_storage.getItem(detailsKey)) {
            _storage.setItem(detailsKey, {});
        }
        var details = _storage.getItem(detailsKey),
            deferred = $.Deferred();

        if (!details[patientId]) {
            $.ajax($.extend({}, ajaxPatientDetails, {
                data: {
                    patientId: patientId,
                    types: types || [],
                    severityIds: severities || [],
                    careManagerId: careManagerId
                },
                success: function (response) {
                    response = Helpers.convertKeysToCamelCase(response);

                    details[patientId] = parseBloodPressure(response);
                    _storage.setItem(detailsKey, details);

                    deferred.resolve($.extend(true, [], details[patientId]));
                },
                error: function (e) {
                    deferred.reject(e);
                }
            }));
        } else {
            deferred.resolve(details[patientId]);
        }

        return deferred.promise();
    }

    function _mapDeletedAlerts(patientId, alerts) {
        var result = [];

        _.each(alerts, function (alert) {
            if (alert.name !== 'BloodPressure') {
                result.push({
                    alertType: alert.reading.alert.type,
                    alertId: alert.reading.alert.id,
                    patientId: patientId,
                    severityId: alert.reading.alert.alertSeverity ?
                        alert.reading.alert.alertSeverity.id :
                        OPTIONS.ALERT.NOT_EXISTING_ID
                });
            } else {
                (function () {
                    var vitals = alert.reading.measurement.vitals;

                    if (vitals.systolicBloodPressure && vitals.systolicBloodPressure.vitalAlert) {
                        result.push({
                            alertType: vitals.systolicBloodPressure.vitalAlert.type,
                            alertId: vitals.systolicBloodPressure.vitalAlert.id,
                            patientId: patientId,
                            severityId: vitals.systolicBloodPressure.vitalAlert.alertSeverity ?
                                vitals.systolicBloodPressure.vitalAlert.alertSeverity.id :
                                OPTIONS.ALERT.NOT_EXISTING_ID
                        });
                    }

                    if (vitals.diastolicBloodPressure && vitals.diastolicBloodPressure.vitalAlert) {
                        result.push({
                            alertType: vitals.diastolicBloodPressure.vitalAlert.type,
                            alertId: vitals.diastolicBloodPressure.vitalAlert.id,
                            patientId: patientId,
                            severityId: vitals.diastolicBloodPressure.vitalAlert.alertSeverity ?
                                vitals.diastolicBloodPressure.vitalAlert.alertSeverity.id :
                                OPTIONS.ALERT.NOT_EXISTING_ID
                        });
                    }
                }());
            }
        });

        return result;
    }

    function _isDeletedAlertBP(item) {
        return item.reading.name !== 'DiastolicBloodPressure' && item.reading.name !== 'SystolicBloodPressure';
    }

    function _hasBPVital(item) {
        return item.reading.measurement.vitals.diastolicBloodPressure.vitalAlert ?
            item.reading.measurement.vitals.diastolicBloodPressure.vitalAlert.id :
            (item.reading.measurement.vitals.systolicBloodPressure.vitalAlert ?
                item.reading.measurement.vitals.systolicBloodPressure.vitalAlert.id :
                false);
    }

    function _deleteAlerts(patientId, alertIds, deferred) {
        return function () {
            var details = _storage.getItem(detailsKey),
                detail = details[patientId],
                deletedAlerts = _.filter(detail, function (item) {
                    return _isDeletedAlertBP(item) ?
                        _.contains(alertIds, item.reading.alert.id) :
                        (_.contains(alertIds, _hasBPVital(item)));
                }),
                existingAlerts = _.filter(detail, function (item) {
                    return _isDeletedAlertBP(item) ?
                        !_.contains(alertIds, item.reading.alert.id) :
                        !(_.contains(alertIds, _hasBPVital(item)));
                });

            if (existingAlerts.length) {
                details[patientId] = existingAlerts;
            } else {
                delete details[patientId];
            }

            _storage.setItem(detailsKey, $.extend(true, {}, details));

            deferred.resolve(_mapDeletedAlerts(patientId, deletedAlerts));
        }
    }

    function acknowledgeAlerts(patientId, alertIds) {
        var deferred = $.Deferred();

        $.ajax($.extend({}, ajaxAcknowledgeAlert, {
            data: {
                alertIds: alertIds
            },
            success: _deleteAlerts.call(null, patientId, alertIds, deferred),
            error: function (e) {
                deferred.reject(e);
            }
        }));

        return deferred.promise();
    }

    function ignoreReading(measurementId, patientId, alertIds) {
        var deferred = $.Deferred();

        $.ajax($.extend({}, ajaxIgnoreAlert, {
            data: {
                measurementId: measurementId,
                patientId: patientId,
                alertIds: alertIds
            },
            success: _deleteAlerts.call(null, patientId, alertIds, deferred),
            error: function (e) {
                deferred.reject(e);
            }
        }));

        return deferred.promise();
    }

    function clearStorage() {
        _storage.clear();
    }

    return {
        getCounts: getCounts,
        clearStorage: clearStorage,
        ignoreReading: ignoreReading,
        acknowledgeAlerts: acknowledgeAlerts,
        getPatientDetails: getPatientDetails,
        getPatients: getPatients,
        hasMore: hasMore
    };
});