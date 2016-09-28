'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Site/Patients/PatientsApp/AppNamespace',
    'Controllers/Site/Patients/PatientsApp/Views/GroupedHealthSessionCollectionItemView'
], function ($, _, Backbone, app, GroupedHealthSessionCollectionItemView) {
    return Backbone.View.extend({

        className: 'panel-group panel-group-detailed-data-ci',

        initialize: function () {
            this.listenTo(this.collection, 'remove', this.render);
        },

        render: function () {
            this.$el.empty();
            app.collections.groupedHealthSessionInnerCollection = {};
            app.views.groupedHealthSessionInnerCollectionView = {};
            this.collection.each(this.renderHealthSessionItem, this);
            this.$el.attr('id', 'accordion');

            return this;
        },

        renderHealthSessionItem: function (item) {
            app.views.groupedHealthSessionCollectionItemView = new GroupedHealthSessionCollectionItemView({ model: item });
            this.$el.append(app.views.groupedHealthSessionCollectionItemView.render().el);

            return this;
        }

    });
});