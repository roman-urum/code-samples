'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'backbone-nested',
    'Controllers/Helpers',
    'Controllers/Site/Patients/PatientsApp/AppNamespace',
    'moment'
], function ($, _, Backbone, BackboneNested, Helpers, app, moment) {
    return Backbone.NestedModel.extend({
        datesFormat: 'M/D/YYYY h:mm a',

        defaults: {

        },

        initialize: function () {
            this.fromUtcToLocal('changedUtc');
        },

        fromUtcToLocal: function (fieldName) {
            var dateString = this.get(fieldName);

            if (dateString) {
                this.set(fieldName, moment.utc(dateString).local().format(this.datesFormat));
            }
        },

        formatDateField: function (fieldName) {
            var dateString = this.get(fieldName);

            if (dateString) {
                this.set(fieldName, moment(dateString).format(this.datesFormat));
            }
        }
    });
});