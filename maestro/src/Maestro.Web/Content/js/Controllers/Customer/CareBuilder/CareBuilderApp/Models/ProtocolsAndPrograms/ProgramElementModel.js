'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Constants',
    'Controllers/Helpers',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Models/ProtocolsAndPrograms/ProtocolModel',
    'backbone-nested'
], function (
    $,
    _,
    Backbone,
    Constants,
    Helpers,
    ProtocolModel
) {
    return ProtocolModel.extend({
        initialize: function () {
            // Memento initializing
            _.extend(this, new Backbone.Memento(this));
        },

        defaults: {
            id: null,
            name: {
                value: ''
            },
            sort: 1,
            daySort: null,
            recurrenceId: null,
            tags: {}
        }
    });
});