'use strict';

define([
    'jquery',
    'underscore',
    'moment-timezone-with-data'
], function ($, _, moment) {
    var initTagsCreationCounter = 0;

    return {
        convertKeysToCamelCase: function (target) {
            var $this = this;

            if (!target || typeof target !== "object") return target;

            if (target instanceof Array) {
                return $.map(target, function (value) {
                    return $this.convertKeysToCamelCase(value);
                });
            }

            var newObj = {};

            $.each(target, function (key, value) {
                key = key.charAt(0).toLowerCase() + key.slice(1);
                newObj[key] = $this.convertKeysToCamelCase(value);
            });

            return newObj;
        },

        renderSpinner: function ($el, small) {
            if (small == 'small') {
                $el.html('<img src="/Content/img/spinner.gif" class="spinner spinner-small" />');
            } else {
                $el.html('<img src="/Content/img/spinner.gif" class="spinner" />');
            }
        },

        initTags: function ($creationEl, $searchingEl, callback) {
            var self = this;

            if ($creationEl instanceof jQuery) {
                $creationEl
                    .attr('placeholder', $creationEl.data('loading-placeholder'))
                    .prop('disabled', true);
            }

            if ($searchingEl instanceof jQuery) {
                $searchingEl
                    .html('<option value="0">' + $searchingEl.data('loading-placeholder') + '</option>')
                    .prop('disabled', true);
            }

            if (window.careBuilderApp && window.careBuilderApp.tags) {
                var tags = window.careBuilderApp.tags;
                this.tagsFetched($creationEl, $searchingEl, tags, this);

                if (callback && typeof callback === "function") {
                    callback();
                }

            } else {
                this.getTags()
                    .success(function (data) {
                        window.careBuilderApp = { 'tags': data };
                        self.tagsFetched($creationEl, $searchingEl, data, self);

                        if (callback && typeof callback === "function") {
                            callback();
                        }
                    })
                    .error(function (data) {

                    });
            }
        },

        resetTags: function ($searchingEl) {
            if (window.careBuilderApp) {
                window.careBuilderApp.tags = null;
            }

            $searchingEl
                .prop('multiple', false)
                .chosen('destroy');

            this.initTags(null, $searchingEl);

        },

        tagsFetched: function ($creationEl, $searchingEl, data, self) {
            if ($creationEl instanceof jQuery) {
                self.initTagsCreation($creationEl, data);
            }

            if ($searchingEl instanceof jQuery) {
                self.initTagsSearching($searchingEl, data);
            }
        },

        getTags: function () {
            return $.ajax({
                url: '/CareBuilder/Tags',
                type: 'GET',
                dataType: 'json',
                contentType: 'application/json; charset=utf-8'
            });
        },

        initTagsCreation: function ($el, data) {
            var placeholder = $el.data('placeholder');

            $el
                .attr('placeholder', placeholder)
                .prop('disabled', false)
                .on('tokenfield:createdtoken', function (e) { })
                .on('tokenfield:createtoken', function (e) {
                    var list = $el.tokenfield('getTokensList');

                    if ($.isArray(list)) {
                        _.each(list.split(', '), function (token) {
                            if (token === e.attrs.value) {
                                e.preventDefault();
                            }
                        });
                    }
                })
                .on('tokenfield:removedtoken', function (e) { })
                .tokenfield({
                    autocomplete: {
                        source: _.map(_.without(data, ''), function (item) { return item.toLowerCase(); }),
                        delay: 100
                    },
                    showAutocompleteOnFocus: false,
                    disableEdit: true,
                    delimiter: [',', ' ']
                });

            var tokenField = $el.data('bs.tokenfield');

            tokenField.$input.on('click', function () {
                tokenField.search();
            });

            tokenField.$input.data('ui-autocomplete').menu.element.wrap('<div class="ui-tokenfield-custom-wrapper-' + (++initTagsCreationCounter) + '"></div>');
            $('body > div.ui-tokenfield-custom-wrapper-' + initTagsCreationCounter).appendTo(tokenField.$wrapper);
        },

        initTagsSearching: function ($el, data) {
            $el.html('<option value=""></option>');

            _.each(data, function (option) {

                var $option = $('<option></option>');

                $option.text(option);
                $el.append($option);
            });

            $el.prop('disabled', false)
                .prop('multiple', true)
                .chosen()
                .val(0)
                .trigger("chosen:updated");

        },

        isTagsValid: function(tags) {
            var result = true,
                pattern = /^[a-zA-Z0-9.\-_]+$/;

            _.each(tags, function(tag) {
                if (tag !== '' && !pattern.test(tag)) {
                    result = false;
                }
            });

            return result;
        },

        getGUID: function () {
            return this.getS4() + this.getS4() + '-' +
                this.getS4() + '-' +
                this.getS4() + '-' +
                this.getS4() + '-' +
                this.getS4() + this.getS4() + this.getS4();
        },

        getS4: function () {
            return Math.floor((1 + Math.random()) * 0x10000)
                .toString(16)
                .substring(1);
        },

        mimeTypes: function () {
            var result = [];

            result["video/mp4"] = "Video";
            result["audio/mpeg"] = "Audio";
            result["audio/mpa"] = "Audio";
            result["audio/mpa-robust"] = "Audio";
            result["audio/mp4"] = "Audio";
            result["audio/vnd.wave"] = "Audio";
            result["audio/wav"] = "Audio";
            result["audio/wave"] = "Audio";
            result["audio/x-wav"] = "Audio";
            result["video/webm"] = "Audio";
            result["audio/webm"] = "Audio";
            result["application/pdf"] = "Document";
            result["application/x-pdf"] = "Document";
            result["application/x-bzpdf"] = "Document";
            result["application/x-gzpdf"] = "Document";
            result["image/jpeg"] = "Image";
            result["image/jpeg"] = "Image";
            result["image/png"] = "Image";

            return result;
        },

        getUrlParameterByName: function (name) {
            name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
            var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
                results = regex.exec(location.search);

            return results === null ? null : decodeURIComponent(results[1].replace(/\+/g, " "));
        },

        getDeviceStatuses: function () {
            return ["Not Activated", "Activated", "Decommission Requested", "Decommission Acknowledged", "Decommission Started", "Decommission Completed"];
        },

        getAgeByBirthday: function (date) {
            var bdate = date.split("-");
            var born = new Date(bdate[0], bdate[1] - 1, bdate[2]);
            var now = new Date();
            var birthday = new Date(now.getFullYear(), born.getMonth(), born.getDate());
            if (now >= birthday)
                return now.getFullYear() - born.getFullYear();
            else
                return now.getFullYear() - born.getFullYear() - 1;

        },

        getGenderName: function (gender) {
            var GenderNames = ['Male', 'Female'];

            return GenderNames[gender - 1];
        },

        getTimeZoneId: function () {
            return moment.tz.guess();
        },

        timeTo12Convert: function (time) {
            var time = time.toString().match(/^([01]\d|2[0-3])(:)([0-5]\d)(:[0-5]\d)?$/) || [time];

            if (time.length > 1) {
                time = time.slice(1);
                time[5] = +time[0] < 12 ? ' AM' : ' PM';
                time[0] = +time[0] % 12 || 12;
            }
            return time.join('');
        },

        timeTo24Convert: function (time) {
            var hours = Number(time.match(/^(\d+)/)[1]),
                minutes = Number(time.match(/:(\d+)/)[1]),
                AMPM = time.match(/\s(.*)$/)[1];

            if (AMPM == "PM" && hours < 12) hours = hours + 12;
            if (AMPM == "AM" && hours == 12) hours = hours - 12;

            var sHours = hours.toString(),
                sMinutes = minutes.toString();

            if (hours < 10) sHours = "0" + sHours;
            if (minutes < 10) sMinutes = "0" + sMinutes;

            return sHours + ':' + sMinutes

        },

        getThreshold: function (type) {
            var thresholds = {
                isWeightAutomated: {
                    name: 'Weight',
                    unit: 'Lbs'
                },
                isWeightManual: {
                    name: 'Weight',
                    unit: 'Lbs'
                },
                isBloodPressureAutomated: [{
                    name: 'DiastolicBloodPressure',
                    unit: 'mmHg'
                }, {
                    name: 'SystolicBloodPressure',
                    unit: 'mmHg'
                }, {
                    name: 'HeartRate',
                    unit: 'BpM'
                }],
                isBloodPressureManual: [{
                    name: 'DiastolicBloodPressure',
                    unit: 'mmHg'
                }, {
                    name: 'SystolicBloodPressure',
                    unit: 'mmHg'
                }, {
                    name: 'HeartRate',
                    unit: 'BpM'
                }],
                isPulseOxAutomated: [{
                    name: 'OxygenSaturation',
                    unit: 'Percent'
                }, {
                    name: 'HeartRate',
                    unit: 'BpM'
                }],
                isPulseOxManual: [{
                    name: 'OxygenSaturation',
                    unit: 'Percent'
                }, {
                    name: 'HeartRate',
                    unit: 'BpM'
                }],
                isBloodGlucoseAutomated: {
                    name: 'BloodGlucose',
                    unit: 'mgdl'
                },
                isBloodGlucoseManual: {
                    name: 'BloodGlucose',
                    unit: 'mgdl'
                },
                isPeakFlowAutomated: [{
                    name: 'PeakExpiratoryFlow',
                    unit: 'L_min'
                }, {
                    name: 'ForcedExpiratoryVolume',
                    unit: 'L'
                }, {
                    name: 'ForcedVitalCapacity',
                    unit: 'L'
                }, {
                    name: 'FEV1_FVC',
                    unit: 'Percent'
                }],
                isPeakFlowManual: [{
                    name: 'PeakExpiratoryFlow',
                    unit: 'L_min'
                }, {
                    name: 'ForcedExpiratoryVolume',
                    unit: 'L'
                }, {
                    name: 'ForcedVitalCapacity',
                    unit: 'L'
                }, {
                    name: 'FEV1_FVC',
                    unit: 'Percent'
                }],
                isTemperatureAutomated: {
                    name: 'Temperature',
                    unit: 'F'
                },
                isTemperatureManual: {
                    name: 'Temperature',
                    unit: 'F'
                },
                isPedometerAutomated: [{
                    name: 'WalkingSteps',
                    unit: 'steps_day'
                }, {
                    name: 'RunningSteps',
                    unit: 'steps_day'
                }],
                isPedometerManual: [{
                    name: 'WalkingSteps',
                    unit: 'steps_day'
                }, {
                    name: 'RunningSteps',
                    unit: 'steps_day'
                }]
            };

            return thresholds[type];

        },

        getThresholdByPeripheral: function (type) {

            var thresholds = {
                weight: {
                    name: 'Weight',
                    unit: 'Lbs'
                },
                bloodPressure: [{
                    name: 'DiastolicBloodPressure',
                    unit: 'mmHg'
                }, {
                    name: 'SystolicBloodPressure',
                    unit: 'mmHg'
                }, {
                    name: 'HeartRate',
                    unit: 'BpM'
                }],
                pulseOx: [{
                    name: 'OxygenSaturation',
                    unit: 'Percent'
                }, {
                    name: 'HeartRate',
                    unit: 'BpM'
                }],
                bloodGlucose: {
                    name: 'BloodGlucose',
                    unit: 'mgdl'
                },
                peakFlow: [{
                    name: 'PeakExpiratoryFlow',
                    unit: 'L_min'
                }, {
                    name: 'ForcedExpiratoryVolume',
                    unit: 'L'
                }, {
                    name: 'ForcedVitalCapacity',
                    unit: 'L'
                }, {
                    name: 'FEV1_FVC',
                    unit: 'Percent'
                }],
                temperature: {
                    name: 'Temperature',
                    unit: 'F'
                },
                pedometer: [{
                    name: 'WalkingSteps',
                    unit: 'steps_day'
                }, {
                    name: 'RunningSteps',
                    unit: 'steps_day'
                }]
            };

            return thresholds[type];

        },

        getVitalType: function (type) {
            var vitalTypes = [
                'SystolicBloodPressure',
                'DiastolicBloodPressure',
                'HeartRate',
                'Temperature',
                'Weight',
                'BodyMassIndex',
                'OxygenSaturation',
                'BloodGlucose',
                'PeakExpiratoryFlow',
                'ForcedExpiratoryVolume',
                'WalkingSteps',
                'RunningSteps',
                'ForcedVitalCapacity',
                'FEV1_FVC'
            ];

            if (type == 'all')
                return vitalTypes;
            else
                return vitalTypes[type];

        },

        getUnitType: function (type) {
            var unitTypes = [
                'mmHg',
                'kPa',
                'BpM',
                'Percent',
                'Lbs',
                'kg',
                'C',
                'F',
                'mgdl',
                'mmol_L',
                'kg_m2',
                'L_min',
                'L',
                'steps_day'
            ];

            return unitTypes[type];
        },

        getDeviceIdType: function (type) {
            if (type == null) return null;
            var deviceIdTypes = [
                'IMEI',
                'MAC'
            ];

            return deviceIdTypes[type];
        },


        getThresholdsLimits: function (name) {
            var limits = {
                OxygenSaturation: [0, 101],
                HeartRate: [0, 250],
                SystolicBloodPressure: [0, 250],
                // SystolicBloodPressure: [0, 35],
                DiastolicBloodPressure: [0, 200],
                // DiastolicBloodPressure: [ 0, 30],
                // Weight: [0, 499.9],
                Weight: [0, 999.9],
                BloodGlucose: [0, 999],
                // BloodGlucose: [ 0, 50],
                PeakExpiratoryFlow: [0, 999],
                ForcedExpiratoryVolume: [0, 9.99],
                WalkingSteps: [0, 100000],
                RunningSteps: [0, 100000],
                // Temperature: [0, 43],
                Temperature: [0, 110],
                ForcedVitalCapacity: [0, 999],
                FEV1_FVC: [0, 101]
            }

            return limits[name];
        },


        isFloat: function (n) {
            return n === +n && n !== (n | 0);
        },

        isInteger: function (n) {
            return n === +n && n === (n | 0);
        },

        checkDecimalDigits: function (number) {
            var number_str = number.toString(),
                decimal_digits = number_str.length - number_str.lastIndexOf('.') - 1;
            decimal_digits === number_str.length && (decimal_digits = 0); // case no decimal
            return decimal_digits;
        },

        dynamicSort: function (property) {
            var sortOrder = 1;
            if (property[0] === "-") {
                sortOrder = -1;
                property = property.substr(1);
            }
            return function (a, b) {
                var result = (a[property] < b[property]) ? -1 : (a[property] > b[property]) ? 1 : 0;
                return result * sortOrder;
            }
        },

        getDates: function (startDate, stopDate) {
            var dateArray = [],
                currentDate = moment(startDate),
                stopDate = moment(stopDate);

            currentDate = moment(currentDate).add(1, 'days');

            while (currentDate < stopDate) {
                dateArray.push(moment(currentDate).format('YYYY-MM-DD'));
                currentDate = moment(currentDate).add(1, 'days');
            }

            return dateArray;
        },

        detectUserStatus: (function () {
            var statuses = {
                active: {
                    name: 'Active',
                    code: 1
                },
                inactive: {
                    name: 'Inactive',
                    code: 0
                },
                training: {
                    name: 'In Training',
                    code: 2
                },
                undefined: {
                    name: 'Undefined',
                    code: -1
                }
            },
                getUserStatus = function (status) {
                    return {
                        name: status.name,
                        code: status.code,
                        className: $.trim(status.name).toLowerCase().split(' ').join('-')
                    };
                };

            return function (statusCode) {
                var name = _.find(Object.getOwnPropertyNames(statuses), function (status) {
                    return statuses[status].code === statusCode;
                });

                return getUserStatus(statuses[name] || statuses.undefined);
            };
        }()),

        getOffsetStringFromTimezoneName: function (timezoneName) {
            return timezoneName.match(/\((.+?)\)/g)[0];
        },

        getThresholdCollection: (function () {
            var collection = {
                BodyMassIndex: {
                    name: 'BodyMassIndex',
                    nameLabel: 'Body Mass Index',
                    unit: 'kg_m2',
                    unitLabel: 'kg/m²',
                    overlap: false
                },
                Weight: {
                    name: 'Weight',
                    nameLabel: 'Weight',
                    unit: 'Lbs',
                    unitLabel: 'Lbs',
                    overlap: false
                },
                BloodPressure: {
                    name: 'BloodPressure',
                    nameLabel: 'Blood Pressure',
                    unit: 'mmHg',
                    unitLabel: 'mmHg',
                    overlap: false
                },
                SystolicBloodPressure: {
                    name: 'SystolicBloodPressure',
                    nameLabel: 'Systolic Blood Pressure',
                    unit: 'mmHg',
                    unitLabel: 'mmHg',
                    overlap: false
                },
                DiastolicBloodPressure: {
                    name: 'DiastolicBloodPressure',
                    nameLabel: 'Diastolic Blood Pressure',
                    unit: 'mmHg',
                    unitLabel: 'mmHg',
                    overlap: false
                },
                HeartRate: {
                    name: 'HeartRate',
                    nameLabel: 'Heart Rate',
                    unit: 'BpM',
                    unitLabel: 'BpM',
                    overlap: false
                },
                OxygenSaturation: {
                    name: 'OxygenSaturation',
                    nameLabel: 'Oxygen Saturation',
                    unit: 'Percent',
                    unitLabel: '%',
                    overlap: false
                },
                BloodGlucose: {
                    name: 'BloodGlucose',
                    nameLabel: 'Blood Glucose',
                    unit: 'mgdl',
                    unitLabel: 'mg/dl',
                    overlap: false
                },
                Temperature: {
                    name: 'Temperature',
                    nameLabel: 'Temperature',
                    unit: 'F',
                    unitLabel: 'F',
                    overlap: false
                },
                ForcedExpiratoryVolume: {
                    name: 'ForcedExpiratoryVolume',
                    nameLabel: 'Forced Expiratory Volume',
                    unit: 'L',
                    unitLabel: 'L',
                    overlap: false
                },
                PeakExpiratoryFlow: {
                    name: 'PeakExpiratoryFlow',
                    nameLabel: 'Peak Expiratory Flow',
                    unit: 'L_min',
                    unitLabel: 'L/min',
                    overlap: false
                },
                WalkingSteps: {
                    name: 'WalkingSteps',
                    nameLabel: 'Walking Steps',
                    unit: 'steps_day',
                    unitLabel: 'steps/day',
                    overlap: false
                },
                RunningSteps: {
                    name: 'RunningSteps',
                    nameLabel: 'Running Steps',
                    unit: 'steps_day',
                    unitLabel: 'steps/day',
                    overlap: false
                },
                ForcedVitalCapacity: {
                    name: 'ForcedVitalCapacity',
                    nameLabel: 'Forced Vital Capacity',
                    unit: 'L',
                    unitLabel: 'L',
                    overlap: false
                },
                FEV1_FVC: {
                    name: 'FEV1_FVC',
                    nameLabel: 'FEV1/FVC Ratio',
                    unit: 'Percent',
                    unitLabel: '%',
                    overlap: false
                }
            };

            return function () {
                return $.extend({}, collection);
            };
        }()),

        isArraysMatch: function (arr1, arr2) {
            var matches = [];

            for (var i = 0; i < arr1.length; i++) {
                for (var e = 0; e < arr2.length; e++) {
                    if (arr1[i] === arr2[e]) matches.push(arr1[i]);
                }
            }

            return matches.length == arr2.length ? true : false;
        },

        hasDuplicates: function(array) {
            var uniqueValues = {},
                result = false;

            _.each(array, function(item) {
                if (!uniqueValues[item]) {
                    uniqueValues[item] = true;
                } else {
                    result = true;
                }
            });

            return result;
        },

        toUcFirstLetter: function( string ){

            return string.substr(0, 1).toUpperCase() + string.substr(1);
        }


    };
});