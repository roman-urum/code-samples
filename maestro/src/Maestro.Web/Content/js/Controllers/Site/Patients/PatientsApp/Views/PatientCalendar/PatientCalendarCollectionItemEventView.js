'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Site/Patients/PatientsApp/AppNamespace',
    'Controllers/Site/Patients/PatientsApp/Views/PatientCalendar/PatientCalendarProgramContextView',
    'Controllers/Site/Patients/PatientsApp/Views/PatientCalendar/PatientCalendarSessionContextView'
], function ($, _, Backbone, app, PatientCalendarProgramContextView, PatientCalendarSessionContextView) {
    return Backbone.View.extend({

        className: 'event',

        template: _.template('<div class="program-day"><%=programDay%></div><div class="program-name"><%=name%></div>'),

        events: {
            'contextmenu': 'contextMenu'
        },

        initialize: function () {

        },

        render: function () {

            this.$el.html(this.template(this.model.attributes));
            this.$el.addClass('event-status-' + this.model.get('status'));

            if( this.model.get('isOneTime') ){
                this.$el.addClass('event-one-time-session');
            }

            return this;
        },

        contextMenu: function (e) {

            if( this.model.get('isOneTime') ){
                this.contextMenuSessionShow(e);
            }else{
                this.contextMenuProgramShow(e);
            }

            return false;
        },

        contextMenuSessionShow: function (e) {

            if (app.views.patientCalendarSessionContextView)
                app.views.patientCalendarSessionContextView.remove();

            app.views.patientCalendarSessionContextView = new PatientCalendarSessionContextView({ model: this.model });
            this.$el.prepend(app.views.patientCalendarSessionContextView.render({ contextEvent: e }).el);

            return false;
        },

        contextMenuProgramShow: function (e) {

            if (app.views.patientCalendarProgramContextView)
                app.views.patientCalendarProgramContextView.remove();

            app.views.patientCalendarProgramContextView = new PatientCalendarProgramContextView({ model: this.model });
            this.$el.prepend(app.views.patientCalendarProgramContextView.render({ contextEvent: e }).el);

            return false;
        }
    });
});