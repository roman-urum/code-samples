'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Constants',
    'Controllers/Helpers',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Collections/ProtocolsAndPrograms/ProgramDaysCollection',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Models/ProtocolsAndPrograms/ProgramDayModel',
    'backbone-nested'
], function (
    $,
    _,
    Backbone,
    Constants,
    Helpers,
    ProgramDaysCollection,
    ProgramDayModel
) {
    return Backbone.NestedModel.extend({
        initialize: function () {
            var days = this.get('days');

            if (days == undefined) {
                days = new ProgramDaysCollection();
            }

            while (days.length < 7) {
                days.add(new ProgramDayModel({
                    number: (this.get('number') - 1) * Constants.daysInWeek + days.length + 1
                }));
            }

            this.set('days', days);

            // Memento initializing
            _.extend(this, new Backbone.Memento(this));
        },

        defaults: {

        }
    });
});