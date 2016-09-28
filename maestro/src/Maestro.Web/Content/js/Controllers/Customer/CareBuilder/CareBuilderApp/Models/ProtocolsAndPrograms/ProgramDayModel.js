'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Constants',
    'Controllers/Helpers',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Collections/ProtocolsAndPrograms/ProgramDayElementsCollection',
    'backbone-nested'
], function (
    $,
    _,
    Backbone,
    Constants,
    Helpers,
    ProgramDayElementsCollection
) {
    return Backbone.NestedModel.extend({
        initialize: function () {
            if (this.get('dayElements') == undefined) {
                this.set('dayElements', new ProgramDayElementsCollection());
            }

            // Memento initializing
            _.extend(this, new Backbone.Memento(this));
        },

        defaults: {

        }
    });
});