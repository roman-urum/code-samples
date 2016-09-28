'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Models/ProtocolsAndPrograms/ProgramModel'
], function ($, _, Backbone, ProgramModel) {
    return Backbone.Collection.extend({
        model: ProgramModel,

        url: function () {
            return '/CareBuilder/Programs';
        }
    });
});