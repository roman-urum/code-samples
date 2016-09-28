'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Models/ProtocolsAndPrograms/ProgramDayModel'
], function ($, _, Backbone, ProgramDayModel) {
    return Backbone.Collection.extend({
        model: ProgramDayModel
    });
});