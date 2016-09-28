'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Site/Patients/PatientsApp/AppNamespace',
    'Controllers/Helpers',
    'Controllers/Site/Patients/PatientsApp/Views/PatientDetailsView',
    'Controllers/Site/Patients/PatientsApp/Models/PatientDetailsModel',

    'Controllers/Site/Notes/NotesApp'
], function ($, _, Backbone, app, helpers, PatientDetailsView, PatientDetailsModel, notesApp) {
    return Backbone.View.extend({
        tagName: 'div',
        className: 'panel panel-default',
        template: _.template($('#patientsListItemTemplate').html()),
        $indicator: $(),
        hasDetails: false,
        events: {
            'click .js-patient-show': 'showPatient',
            'click .indicator': 'toggleCollapse'
        },

        initialize: function () {
            this.bind();
            this.listen();
            this.detectStatus();

            this.$el.addClass(this.model.attributes.Status.className);
        },

        bind: function () {
            this.hideCollapse = this.hideCollapse.bind(this);
            this.showCollapse = this.showCollapse.bind(this);
            this.renderDetails = this.renderDetails.bind(this);
            this.toggleIndicatorState = this.toggleIndicatorState.bind(this);
        },

        listen: function () {
            this.$el.on('hide.bs.collapse', this.hideCollapse);
            this.$el.on('show.bs.collapse', this.showCollapse);
        },

        hideCollapse: function () {
            this.toggleIndicatorState();
        },

        showCollapse: function () {
            this.toggleIndicatorState();

            if (!this.hasDetails) {
                this.hasDetails = true;

                this.$el.find('.panel-body').html((new PatientDetailsView({
                    model: new PatientDetailsModel(this.model.attributes.Id)
                })).render().el);
            }
        },

        detectStatus: function () {
            if (typeof this.model.attributes.Status === 'number') {
                this.model.attributes.Status = helpers.detectUserStatus(this.model.attributes.Status);
            }
        },

        toggleCollapse: function () {
            this.$el.find('.panel-collapse').collapse('toggle');
        },

        toggleIndicatorState: function () {
            this.$indicator.toggleClass('fa-angle-down fa-angle-up');
            this.$indicator.closest('.panel-heading').toggleClass('panel-heading-active');
        },

        showPatient: function (e) {
            app.models.patientModel = this.model;

            e.preventDefault();
            e.stopPropagation();

            if (!e.altKey && !e.ctrlKey && !e.metaKey && !e.shiftKey) {
                var href = $(e.currentTarget).attr('href');

                app.router.navigate(href, {
                    trigger: true
                });
            } else if (e.ctrlKey) {
                var href = $(e.currentTarget).attr('href');
                window.open(href, '_blank');
            }
        },

        renderDetails: function () { },

        render: function () {
            this.$el.html(this.template(this.model.attributes));

            this.$indicator = this.$el.find('.indicator .fa');

            return this;
        }
    });
});