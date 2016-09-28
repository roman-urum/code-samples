'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Site/Patients/PatientsApp/AppNamespace',
    'Controllers/Customer/Settings/CustomerSettingsApp/Models/ConditionsCollection',
    'Controllers/Customer/Settings/CustomerSettingsApp/Models/ConditionModel',
    'Controllers/Helpers'
], function ($, _, Backbone, app, CustomerConditionsCollection, ConditionModel, Helpers) {
    return Backbone.Collection.extend({

        model: ConditionModel,

        initialize: function (options) {
            options = options || {};

            this.patientId = options.patientId;
            _.extend(this, new Backbone.Memento(this));
        },

        url: function () {
            return '/' + app.siteId + '/Patients/PatientConditions';
        },

        sync: function (method, model, options) {
            if (method == 'read') {
                var patientId = app.models.patientModel.get('id');
                options.url = this.url() + '?patientId=' + patientId;
            }
            return Backbone.sync(method, model, options);
        },

        parse: function (response, options) {
            return Helpers.convertKeysToCamelCase(response);
        },

        save: function (options) {
            options = options || {};

            var self = this;
            var data = {
                patientId: this.patientId,
                patientConditionsIds: this.pluck('id')
            };

            $.ajax({
                url: this.url(),
                method: 'POST',
                dataType: 'json',
                data: data,
                success: function () {
                    //Alerts.success('User was saved successfully');
                    if (_.isFunction(options.success)) options.success.apply(this, arguments);
                },
                error: function (err) {
                    //Alerts.danger(err.responseJSON.Message + '. ' + err.responseJSON.Details);
                    //TODO: fix server endpoint to response with json
                    if (_.isFunction(options.error)) options.success.apply(this, arguments);
                }
            });
        },

        comparator: function (model) {
            return model.get('name').toLowerCase();
        }

    });
});