'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Helpers',
    'Controllers/Root/Thresholds/AppNamespace',
    'Controllers/Root/Thresholds/Collections/ThresholdsCollection',
    'Controllers/Root/Thresholds/Views/ThresholdsCollectionView',
    'Controllers/Root/Thresholds/Views/ThresholdsSeveritiesView'
], function ($, _, Backbone, Helpers, app, ThresholdsCollection, ThresholdsCollectionView, ThresholdsSeveritiesView) {
    return Backbone.View.extend({
        isSaving: false,

        initialize: function () {

            app.collections.thresholdsCollection = app.initCollections.thresholdsCollection || new ThresholdsCollection();

            _.bindAll(this, "afterSave", "beforeSave", "thresholdCollectionSync");

            app.vent.on('modelChanged', this.onModelChanged);
            app.vent.on('saveMeasurementCollection', this.afterSave);

            this.listenTo(app.collections.thresholdsCollection, 'sync', this.thresholdCollectionSync);

            if (!app.initCollections.thresholdsCollection) {
                app.collections.thresholdsCollection.fetch();
            }

        },

        render: function () {

            Helpers.renderSpinner(this.$el);

            if (app.initCollections.thresholdsCollection) {

                this.thresholdCollectionSync();
                app.vent.trigger('thresholdsTdHeight');

            }

            return this;
        },

        events: {
            'keyup .td-threshold input': 'onThresholdChange',
            'paste .td-threshold input': 'onThresholdChange'
        },

        thresholdCollectionSync: function () {

            if (app.collections.thresholdsCollection.length) {
                this._renderThresholdsCollectionView();
            } else {
                this._renderThresholdsSeverityCollectionsView();
            }

            if (typeof app.events.afterLoad === 'function') {
                setTimeout(function () {
                    app.events.afterLoad();
                }, 0);
            }

            setTimeout(function () {
                app.vent.trigger('thresholdsTdHeight');
            }, 0);
        },

        _renderThresholdsCollectionView: function () {

            app.collections.thresholdsCollection.store();
            if (app.views.thresholdsCollectionView)
                app.views.thresholdsCollectionView.remove();
            app.views.thresholdsCollectionView = new ThresholdsCollectionView({ collection: app.collections.thresholdsCollection });
            this.$el.html(app.views.thresholdsCollectionView.render().el);
            app.views.thresholdsCollectionView.delegateEvents();
        },

        renderThresholdsSeverityCollectionsView: function() {

            this._renderThresholdsSeverityCollectionsView();
            app.vent.trigger('thresholdsTdHeight');
        },

        _renderThresholdsSeverityCollectionsView: function () {

            if (app.views.thresholdsSeveritiesView)
                app.views.thresholdsSeveritiesView.remove();

            app.views.thresholdsSeveritiesView = new ThresholdsSeveritiesView({ collection: app.collections.alertSeveritiesCollection });
            this.$el.html(app.views.thresholdsSeveritiesView.render().el);
            app.views.thresholdsSeveritiesView.delegateEvents();

        },

        afterSave: function () {

            if (this.isSaving) {
                this.isSaving = false;

                if (typeof app.events.afterSave === 'function') {
                    app.events.afterSave();
                }
            }

        },

        onModelChanged: function () {

            if (typeof app.events.onModelChanged === 'function') {
                app.events.onModelChanged();
            }

        },

        beforeSave: function () {

            if (!this.isSaving) {
                this.isSaving = true;

                if (typeof app.events.beforeSave === 'function') {
                    app.events.beforeSave();
                }
            }

        },

        save: function () {

            if (!this.isSaving) {
                app.vent.trigger('checkThresholdsValidation');

                if (app.isValid) {
                    this.beforeSave();
                    app.vent.trigger('savePatientThresholdsCollection');
                }


            }

        },

        onThresholdChange: function (e) {
            e.preventDefault();

            var target = $(e.target),
                defaultThresholdElement =
                    target
                    .parent()
                    .siblings('.help-block');

            if (defaultThresholdElement) {
                if (target.val() !== '') {
                    defaultThresholdElement.removeClass('bold');
                } else {
                    defaultThresholdElement.addClass('bold');
                }
            }
        }
    });
});