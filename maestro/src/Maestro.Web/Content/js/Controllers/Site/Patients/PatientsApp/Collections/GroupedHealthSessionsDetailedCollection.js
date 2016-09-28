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
                toDate = app.models.patientDetailedDataFilterModel.get('toDate'),
                elementType = app.models.patientDetailedDataFilterModel.get('elementType');

            return '/' + app.siteId + '/Patients/GroupedHealthSessionsDetailedData?patientId=' + app.patientId +
                    '&observedFromUtc=' + fromDate +
                    '&observedToUtc=' + toDate + 'T23:59:59' +
                    '&elementType=' + elementType;
        },

        parse: function (response, options) {
            return Helpers.convertKeysToCamelCase(response);
        }

    });
});