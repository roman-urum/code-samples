'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Site/Patients/PatientsApp/AppNamespace',
    'Controllers/Helpers',
    'Controllers/Site/Patients/PatientsApp/Models/PatientCalendarChangeModel'
], function ($, _, Backbone, app, Helpers, PatientCalendarChangeModel) {
    return Backbone.View.extend({

        model: PatientCalendarChangeModel,

        tagName: "p",

        events: {

        },

        initialize: function () {

        },

        render: function () {

            var elementType = this.model.get('elementType'),
                template,
                content;
            if (elementType === 2) {
                template = this.programMessagesMap[this.model.get('action')];

            } else if (elementType === 3) {
                template = this.defaultSessionMessagesMap[this.model.get('action')];

            } else {
                template = this.getEventMessageTemplate();
            }

            content = '<b>' + this.model.get('changedUtc') + '</b><br/>' + template(this.model.attributes);

            this.$el.html(content);

            return this;
        },

        close: function () {

        },

        getEventMessageTemplate: function () {
            if (this.model.get('recurrenceEndDate') && this.model.get('recurrenceStartDate')) {
                return this.recurrentEventsMessagesMap[this.model.get('action')];
            }

            return this.singleEventsMessagesMap[this.model.get('action')];
        },

        singleEventsMessagesMap: {
            1: _.template($('#oneTimeEventScheduledTemplate').html()),
            2: _.template($('#oneTimeEventRescheduledTemplate').html()),
            3: _.template($('#oneTimeEventDeletedTemplate').html()),
            4: _.template($('#recurrentEventTerminatedTemplate').html())
        },

        recurrentEventsMessagesMap: {
            1: _.template($('#recurrentEventScheduledTemplate').html()),
            2: _.template($('#recurrentEventRescheduledTemplate').html()),
            3: _.template($('#recurrentEventDeletedTemplate').html()),
            4: _.template($('#recurrentEventTerminatedTemplate').html())
        },

        programMessagesMap: {
            1: _.template($('#programScheduledTemplate').html()),
            2: _.template($('#programDeletedTemplate').html()),
            3: _.template($('#programTerminatedTemplate').html())
        },

        defaultSessionMessagesMap: {
            1: _.template($('#defaultSessionCreatedTemplate').html()),
            2: _.template($('#defaultSessionUpdatedTemplate').html()),
            3: _.template($('#defaultSessionDeletedTemplate').html())
        }

    });
});
