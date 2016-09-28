'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Site/Patients/PatientsApp/AppNamespace',
    'Controllers/Helpers'
], function (
    $,
    _,
    Backbone,
    app,
    Helpers
) {
    return Backbone.View.extend({

        template: _.template($('#patientHistoryViewTemplate').html()),

        events: {

        },

        initialize: function () {
            this.patientId = app.patientId;
        },

        render: function () {
            this.$el.html(this.template(this.model.attributes));
            return this;
        },

        remove: function () { }

    });
});