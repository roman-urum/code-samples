'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Models/ProtocolsAndPrograms/MeasurementThresholdModel'
], function ($, _, Backbone, MeasurementThresholdModel) {
    return Backbone.Collection.extend({
        model: MeasurementThresholdModel
    });
});