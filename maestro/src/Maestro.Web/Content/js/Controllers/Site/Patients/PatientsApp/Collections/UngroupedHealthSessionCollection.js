'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Site/Patients/PatientsApp/AppNamespace',
    'Controllers/Helpers'
], function ($, _, Backbone, app, Helpers) {
    return Backbone.Collection.extend({

        sort_key: 'answered',

        comparator: function (item) {
            return -Date.parse( item.get(this.sort_key) );
        },

        sortByField: function (fieldName) {
            this.sort_key = fieldName;
            this.sort();
        }

    });
});