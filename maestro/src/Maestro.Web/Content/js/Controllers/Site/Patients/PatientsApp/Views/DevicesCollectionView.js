'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Site/Patients/PatientsApp/AppNamespace',
    'Controllers/Site/Patients/PatientsApp/Views/DevicesCollectionItemView'
], function ($, _, Backbone, app, DevicesCollectionItemView) {
    return Backbone.View.extend({
        tagName: 'table',

        className: 'table table-striped',

        initialize: function () {
            this.listenTo(this.collection, 'remove', this.render);
            this.listenTo(this.collection, 'change', this.render);
            this.listenTo(this.collection, 'add', this.render);
        },

        render: function () {
            this.$el.empty();
            this.collection.each(this.renderDevice, this);
            return this;
        },

        renderDevice: function (device) {
            app.views.devicesCollectionItemView = new DevicesCollectionItemView({ model: device });
            this.$el.append(app.views.devicesCollectionItemView.render().el);

            return this;
        }
    });
});