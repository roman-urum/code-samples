'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Root/Thresholds/AppNamespace',
    'Controllers/Helpers',
    'Controllers/Constants'
], function ($, _, Backbone, app, Helpers, Constants) {
    return Backbone.Model.extend({
        defaults: {
            type: 'Basic',
            defaultType: null,
            patientId: null,
            name: null,
            minValue: null,
            maxValue: null,
            unit: null,
            isDisplay: true
        },

        decimalPlacesList: {
            Weight: 1,
            OxygenSaturation: 1,
            Temperature: 2,
            ForcedExpiratoryVolume: 2,
            FEV1_FVC: 2
        },

        url: function () {
            return (app.siteId ? ('/' + app.siteId) : '') + (app.crud.save);
        },

        sync: function (method, model, options) {
            var thresholdId = this.get('id');

            if (method == 'delete') {
                options.url = (app.siteId ? ('/' + app.siteId) : '') + (app.crud.save) + (app.mode === Constants.thresholdAppModes.DEFAULT || app.mode === Constants.thresholdAppModes.CONDITION ? '?defaultThresholdId=' : '?thresholdId=') + thresholdId + (app.patientId ? '&patientId=' + app.patientId : '');
            }

            return Backbone.sync(method, model, options);
        },

        parse: function (response, options) {
            return Helpers.convertKeysToCamelCase(response);
        },

        validation: {
            
            minValue: function (value) {
                var name = this.get('name'),
                    limits = Helpers.getThresholdsLimits(name),
                    maxValue = this.get('maxValue');

                value = (value || value == 0) ? value.toString() : value;

                if (!value && maxValue) {
                    if (!this.get('isDisplay')) {
                        this.set('isDisplay', true);
                    }

                    return 'Low Limit is required';
                }

                var numericPattern = new RegExp('^[0-9]*\.?[0-9]+$');
                if (value && value.length > 0 && !numericPattern.test(value)) {

                    return 'Invalid characters in Low limit value';
                }

                value = value * 1;

                if (name == 'Weight' && (!Helpers.isFloat(value) && !Helpers.isInteger(value))) {
                    if (!this.get('isDisplay')) {
                        this.set('isDisplay', true);
                    }

                    return 'Weight can be only positive integer or float number within ' + limits[0] + ' to ' + limits[1];
                }

                if (name == 'ForcedExpiratoryVolume' && (!Helpers.isFloat(value) && !Helpers.isInteger(value))) {
                    if (!this.get('isDisplay')) {
                        this.set('isDisplay', true);
                    }

                    return 'Forced Expiratory Volume can be only positive integer or float number within ' + limits[0] + ' to ' + limits[1];
                }

                if (limits[0] > value || limits[1] < value) {
                    if (!this.get('isDisplay')) {
                        this.set('isDisplay', true);
                    }

                    return 'Low Limit should be within ' + limits[0] + ' to ' + limits[1];
                }
            },

            maxValue: function (value) {
                var name = this.get('name'),
                    limits = Helpers.getThresholdsLimits(name),
                    minValue = this.get('minValue');

                value = (value || value == 0) ? value.toString() : value;

                if (!value && minValue) {
                    if (!this.get('isDisplay')) {
                        this.set('isDisplay', true);
                    }

                    return 'High Limit is required';
                }

                var numericPattern = new RegExp('^[0-9]*\.?[0-9]+$');
                if (value && value.length > 0 && !numericPattern.test(value)) {

                    return 'Invalid characters in High limit value';
                }

                value = value * 1;

                if (name == 'Weight' && (!Helpers.isFloat(value) && !Helpers.isInteger(value))) {
                    if (!this.get('isDisplay')) {
                        this.set('isDisplay', true);
                    }

                    return 'Weight can be only positive integer or float number within ' + limits[0] + ' to ' + limits[1];
                }

                if (name == 'ForcedExpiratoryVolume' && (!Helpers.isFloat(value) && !Helpers.isInteger(value))) {
                    if (!this.get('isDisplay')) {
                        this.set('isDisplay', true);
                    }

                    return 'Forced Expiratory Volume can be only positive integer or float number within ' + limits[0] + ' to ' + limits[1];
                }

                if (limits[0] > value || limits[1] < value) {
                    if (!this.get('isDisplay')) {
                        this.set('isDisplay', true);
                    }

                    return 'High Limit should be within ' + limits[0] + ' to ' + limits[1];
                }
                if (value < minValue || value == minValue && minValue != '') {
                    if (!this.get('isDisplay')) {
                        this.set('isDisplay', true);
                    }

                    return 'High Limit cannot be lower than Low Limit or equal to Low Limit';
                }
            }
        }
    });
});