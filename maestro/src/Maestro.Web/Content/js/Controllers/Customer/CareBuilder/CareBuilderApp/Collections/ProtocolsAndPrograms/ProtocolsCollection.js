'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Models/ProtocolsAndPrograms/ProtocolModel'
], function ($, _, Backbone, ProtocolModel) {
    return Backbone.Collection.extend({
        model: ProtocolModel,

        url: function () {
            return '/CareBuilder/Protocols';
        }
    });
});