'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Site/Patients/PatientsApp/AppNamespace',
    'Controllers/Site/Patients/PatientsApp/Models/PatientModel',
    'Controllers/Helpers'
], function ($, _, Backbone, app, PatientModel, Helpers) {
    return Backbone.Collection.extend({
        sort_key: 'firstName',

        total: 0,

        xhr: new XMLHttpRequest(),

        model: PatientModel,

        initialize: function () {
            this.success = this.success.bind(this);
        },

        url: function () {
            return '/' + app.siteId + '/Patients/Search';
        },

        encodeData: function (data) {
            var currentData = [];

            for (var name in data) {
                currentData.push({ name: name, value: data[name] });
            }

            return currentData.map(function (item) {
                return encodeURIComponent(item.name) + '=' + encodeURIComponent(item.value);
            }).join('&').replace(/%20/g, '+');
        },

        sync: function (data) {
            if (this.isLoading) {
                this.xhr.abort();
            }

            this.isLoading = true;
            this.trigger('patients-loading');

            data.add = true;
            data.success = this.success;
            data.processData = true;
            data.data.skip = this.models.length;
            data.data = this.encodeData(data.data);

            this.xhr = Backbone.sync('read', this, data);
        },

        success: function (data) {
            var current = _.filter(data.Results, function (item) {
                return !_.find(this.models, function (existingItem) {
                    return existingItem.attributes.Id === item.Id;
                });
            }, this);

            this.add(current);

            this.total = data.Total;
            this.isLoading = false;
            this.trigger('patients-loaded');
        },

        hasMore: function () {
            return this.total > this.models.length;
        }
    });
});