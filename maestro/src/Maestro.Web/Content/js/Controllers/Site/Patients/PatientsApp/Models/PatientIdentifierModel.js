'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Site/Patients/PatientsApp/AppNamespace',
    'Controllers/Helpers'
], function ($, _, Backbone, app, Helpers) {
    return Backbone.Model.extend({
        defaults: {
            name: null,
            value: null
        },

        initialize: function () {

        },

        validation: {

            value: function (value) {
                if (!value && this.get('isRequired')) {
                    return 'Field is required';
                }

                if (value && this.get('validationRegEx')) {
                    var re = new RegExp(this.get('validationRegEx'), 'g'),
                        result = re.test(value);

                    if (!result) {
                        return this.get('validationErrorMessage'); // validationErrorResourceString
                    }
                }
            }

        }

    });
});