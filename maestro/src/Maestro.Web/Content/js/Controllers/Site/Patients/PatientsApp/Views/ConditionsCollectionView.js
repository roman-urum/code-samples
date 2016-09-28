'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Site/Patients/PatientsApp/AppNamespace',
    'Controllers/Site/Patients/PatientsApp/Views/ConditionsCollectionItemView'
], function ($, _, Backbone, app, ConditionsCollectionItemView) {
    return Backbone.View.extend({
        className: 'list-group',

        initialize: function () {
            this.listenTo(this.collection, 'add', this.render);
            this.listenTo(this.collection, 'remove', this.render);
            this.listenTo(this.collection, 'reset', this.render);
        },

        render: function () {
            this.$el.empty();
            this.collection.each(this.renderCareManager, this);
            app.vent.trigger("patientEditConditionsChanged");

            return this;
        },

        renderCareManager: function (patient) {
            var view = new ConditionsCollectionItemView({ model: patient });
            this.$el.append(view.render().el);

            return this;
        }

    });
});

