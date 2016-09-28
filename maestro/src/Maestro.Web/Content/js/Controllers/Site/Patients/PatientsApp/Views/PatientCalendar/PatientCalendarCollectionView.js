'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Site/Patients/PatientsApp/AppNamespace',
    'Controllers/Site/Patients/PatientsApp/Views/PatientCalendar/PatientCalendarCollectionItemView'
], function ($, _, Backbone, app, PatientCalendarCollectionItemView) {
    return Backbone.View.extend({
        className: 'calendar-month',

        events: {

        },

        initialize: function () {

        },

        render: function () {
            this.$el.empty();
            this.collection.each(this.renderCalendarDay, this);

            return this;
        },

        renderCalendarDay: function (day) {
            app.views.patientCalendarCollectionItemView = new PatientCalendarCollectionItemView({ model: day });
            this.$el.append(app.views.patientCalendarCollectionItemView.render().el);

            return this;
        }
    });
});