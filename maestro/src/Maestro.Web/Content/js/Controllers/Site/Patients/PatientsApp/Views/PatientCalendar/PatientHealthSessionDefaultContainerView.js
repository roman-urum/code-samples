'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Site/Patients/PatientsApp/AppNamespace',
    'Controllers/Helpers',
    'Controllers/Site/Patients/PatientsApp/Models/PatientHealthSessionDefaultModel',
    'Controllers/Site/Patients/PatientsApp/Views/PatientCalendar/PatientHealthSessionDefaultView'
], function (
    $,
    _,
    Backbone,
    app,
    Helpers,
    PatientHealthSessionDefaultModel,
    PatientHealthSessionDefaultView
) {
    return Backbone.View.extend({

        className: 'default-health-session',

        template: _.template('<div class="default-health-session__title">\
                                <h5>\
                                    Default Health Sessions\
                                    <span class="glyphicon glyphicon-question-sign" data-toggle="popover" data-content="\
                                        <p>A default Health Session is always available to the patient and they can take it unlimited times.</p>\
                                        <p>The patient can only have one (1) Default Health Session assigned at a time..</p>"\
                                    data-original-title="Default Health Sessions"\
                                    title="Default Health Sessions"></span>\
                                </h5>\
                            </div>\
                            <div id="default-health-session"></div>\
        '),

        events: {

        },

        initialize: function () {

        },

        render: function () {

            this.$el.html( this.template() );

            this.initPopover();

            this.renderDefaultHealthSession();

            return this;
        },

        renderDefaultHealthSession: function(){

            if ( !app.models.patientHealthSessionDefaultModel ){

                app.models.patientHealthSessionDefaultModel = new PatientHealthSessionDefaultModel();
                app.models.patientHealthSessionDefaultModel.isFetched = false;

            }

            if( app.views.patientHealthSessionDefaultView )
                app.views.patientHealthSessionDefaultView.remove();

            app.views.patientHealthSessionDefaultView = new PatientHealthSessionDefaultView({ model: app.models.patientHealthSessionDefaultModel });
            this.$el.find('#default-health-session').removeClass('default-health-session-loading').html( app.views.patientHealthSessionDefaultView.render().el );

        },

        initPopover: function(){

            this.$el.find('[data-toggle="popover"]').popover({
                template: '  <div class="popover">\
                                <div class="arrow"></div>\
                                <div class="popover-header">\
                                    <button type="button" class="close" data-dismiss="popover" aria-hidden="true">&times;</button>\
                                    <h3 class="popover-title"></h3>\
                                </div>\
                                <div class="popover-content"></div>\
                            </div>',
                html: true,
                placement: 'bottom',
            }).on('shown.bs.popover', function (e) {
                $('[data-dismiss="popover"]')
                    .off('click')
                    .on('click', function () {
                        $(e.currentTarget).click();
                    })
                ;

            });

        }


    });
});