'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Site/Patients/PatientsApp/AppNamespace',
    'Controllers/Site/Patients/PatientsApp/Views/PatientCalendar/PatientProgramSearchCollectionItemView'
], function ($, _, Backbone, app, PatientProgramSearchCollectionItemView) {
    return Backbone.View.extend({
        // className: 'calendar-month',

        events: {

        },

        initialize: function () {

        },

        render: function () {
            this.$el.empty();
            this.collection.each(this.renderProgram, this);
            return this;
        },

        renderProgram: function (program) {
            app.views.patientProgramSearchCollectionItemView = new PatientProgramSearchCollectionItemView({ model: program });
            this.$el.append(app.views.patientProgramSearchCollectionItemView.render().el);

            return this;
        }
    });
});