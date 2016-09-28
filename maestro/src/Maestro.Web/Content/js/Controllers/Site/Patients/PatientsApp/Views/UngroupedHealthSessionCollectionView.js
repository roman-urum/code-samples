'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Site/Patients/PatientsApp/AppNamespace',
    'Controllers/Site/Patients/PatientsApp/Views/UngroupedHealthSessionCollectionItemView'
], function ($, _, Backbone, app, UngroupedHealthSessionCollectionItemView) {
    return Backbone.View.extend({

        tagName: 'table',

        className: 'table table-striped table-border-ci',

        initialize: function () {
            this.listenTo(this.collection, 'remove', this.render);
        },

        render: function () {
            this.$el.empty();
            this.collection.each(this.renderHealthSessionItem, this);
            return this;
        },

        renderHealthSessionItem: function (item) {
            app.views.ungroupedHealthSessionCollectionItemView = new UngroupedHealthSessionCollectionItemView({ model: item });
            this.$el.append(app.views.ungroupedHealthSessionCollectionItemView.render().el);

            return this;
        }
    });
});