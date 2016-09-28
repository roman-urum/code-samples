'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'backbone-nested',
    'Controllers/Helpers',
    'Controllers/Site/Patients/PatientsApp/AppNamespace'
], function ($, _, Backbone, BackboneNested, Helpers, app) {
    return Backbone.Collection.extend({

        initialize: function () {

        },

        url: function () {
            var fromDate = app.models.patientDetailedDataFilterModel.get('fromDate'),
                toDate = app.models.patientDetailedDataFilterModel.get('toDate');

            return '/' + app.siteId + '/Patients/AdhocMeasurementsDetailedData?patientId=' + app.patientId +
                    '&observedFromUtc=' + fromDate +
                    '&observedToUtc=' + toDate + 'T23:59:59';

        },

        parse: function (response, options) {
            return Helpers.convertKeysToCamelCase(response);
        }

    });
});