'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Site/Patients/PatientsApp/AppNamespace',
    'Controllers/Site/Patients/PatientsApp/Views/PatientCalendar/PatientProtocolSearchCollectionItemView'
], function ($, _, Backbone, app, PatientProtocolSearchCollectionItemView) {
    return Backbone.View.extend({

        tagName: 'ul',

        className: 'session-protocols-list reset-list-ci',

        events: {

        },

        initialize: function () {

        },

        render: function () {
            this.$el.empty();
            this.collection.each(this.renderProtocol, this);
            return this;
        },

        renderProtocol: function (protocol) {

            app.views.patientProtocolSearchCollectionItemView = new PatientProtocolSearchCollectionItemView({ model: protocol });
            this.$el.append(app.views.patientProtocolSearchCollectionItemView.render().el);

            return this;
        }
    });
});