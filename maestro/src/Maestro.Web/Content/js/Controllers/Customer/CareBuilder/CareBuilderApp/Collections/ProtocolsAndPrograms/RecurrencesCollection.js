'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Models/ProtocolsAndPrograms/RecurrenceModel'
], function ($, _, Backbone, RecurrenceModel) {
    return Backbone.Collection.extend({
        model: RecurrenceModel
    });
});