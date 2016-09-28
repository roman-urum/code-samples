'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Site/Patients/PatientsApp/AppNamespace',
    'Controllers/Helpers',
    'moment',
    'Controllers/Site/Patients/PatientsApp/Models/PatientHealthSessionModel',
    'Controllers/Site/Patients/PatientsApp/Views/PatientCalendar/PatientHealthSessionContainerView'
], function (
    $,
    _,
    Backbone,
    app,
    Helpers,
    moment,
    PatientHealthSessionModel,
    PatientHealthSessionContainerView
) {
    return Backbone.View.extend({

        className: 'col-sm-12 js-left-panel-content session-container-ci',

        template: _.template('<div class="create-health-session-label-ci">\
                                    Select a session to edit<br>\
                                    or<br>\
                                    Click Create Health Session to create<br>\
                                    a session\
                                </div>\
                                <div class="text-center">\
                                    <a class="btn btn-primary js-create-health-session">Create Health Session</a>\
                                </div>\
        '),

        events: {
            'click .js-create-health-session' : 'createHealthSession' 
        },

        initialize: function () {

        },

        render: function () {

            this.$el.html( this.template() );
            return this;
        },

        editHealthSession: function(options) {

            this.$el.find('[href="#tab-session"]').tab('show');
            this.createHealthSession( {} , options );

            if( app.collections.patientProtocolSearchCollection.isFetched ){

                this.gotToScheduleTab();  
            }else{

                this.listenTo(app.collections.patientProtocolSearchCollection, 'fetched', this.gotToScheduleTab);

            }

        },

        createHealthSession: function ( e, options ) {
            app.models.patientHealthSessionModel = new PatientHealthSessionModel();

            app.models.patientHealthSessionModel.set({
                preferredSessionTime: moment( app.models.patientModel.get('preferredSessionTime') , ["HH:mm"]).format("h:mm A"),
            });

            if( options )
                app.models.patientHealthSessionModel.set( options );
            else{

                app.models.patientHealthSessionModel.set({
                    sessionTime: moment( app.models.patientModel.get('preferredSessionTime') , ["HH:mm"]).format("HH:mm"),
                    sessionTimeTp: moment( app.models.patientModel.get('preferredSessionTime') , ["HH:mm"]).format("h:mm A")
                });

            }

            if( app.views.patientHealthSessionContainerView )
                app.views.patientHealthSessionContainerView.remove();

            app.views.patientHealthSessionContainerView = new PatientHealthSessionContainerView({ model: app.models.patientHealthSessionModel });
            this.$el.html( app.views.patientHealthSessionContainerView.render().el );

        },

        gotToScheduleTab: function() {

            this.$el.find('[href="#tab-session-schedule"]').tab('show');
            this.stopListening( app.collections.patientProtocolSearchCollection );
        }

    });
});