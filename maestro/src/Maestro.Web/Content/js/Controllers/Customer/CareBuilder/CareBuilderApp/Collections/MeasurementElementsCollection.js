'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Models/MeasurementModel'
], function ($, _, Backbone, MeasurementModel) {
    return Backbone.Collection.extend({
        model: MeasurementModel,

        url: function () {
            return '/CareBuilder/Search';
        }
    });
});