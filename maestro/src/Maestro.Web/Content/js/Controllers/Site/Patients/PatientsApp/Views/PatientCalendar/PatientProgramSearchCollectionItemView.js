'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Site/Patients/PatientsApp/AppNamespace'
], function ($, _, Backbone, app) {
    return Backbone.View.extend({

        className: 'calendar-search-result-item-ci clearfix',

        template: _.template($('#patientCalendarSearchListItemTemplate').html()),

        events: {

        },

        initialize: function () {

        },

        render: function () {
            if (!this.model.get('isDisplay')) {
                return false;
            }

            this.$el.html(this.template(this.model.attributes));
            this.$el.data('id', this.model.get('id')).attr('data-id', this.model.get('id'));
            this.$el.data('duration', this.model.get('durationDays')).attr('data-duration', this.model.get('durationDays'));
            this.initDraggable();

            return this;
        },

        initDraggable: function () {
            this.$el.draggable({
                appendTo: 'body',
                cursor: "move",
                helper: 'clone',
                cursorAt: { top: 15, left: 116 },
                start: function (event, ui) { },
                stop: function (event, ui) { }
            });
            return this;
        }
    });
});