'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Site/Patients/PatientsApp/AppNamespace',
    'Controllers/Site/Patients/PatientsApp/Views/PatientCalendar/PatientHealthSessionRecurrungMonthCollectionItemView'
], function ($, _, Backbone, app, PatientHealthSessionRecurrungMonthCollectionItemView) {
    return Backbone.View.extend({

        className: 'btn-group btn-group-block-ci recurring-session-month-ci clearfix',

        events: {

        },

        initialize: function () {

        },

        render: function () {

            this.$el.empty();
            this.$el.attr('data-toggle','buttons');
            this.collection.each(this.renderDay, this);

            return this;
        },

        renderDay: function (day) {

            app.views.patientHealthSessionRecurrungMonthCollectionItemView = new PatientHealthSessionRecurrungMonthCollectionItemView({ model: day });
            this.$el.append(app.views.patientHealthSessionRecurrungMonthCollectionItemView.render().el);

            return this;
        }
    });
});