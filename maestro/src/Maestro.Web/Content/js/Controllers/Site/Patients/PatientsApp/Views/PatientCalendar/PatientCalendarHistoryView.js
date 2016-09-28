'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Site/Patients/PatientsApp/AppNamespace',
    'Controllers/Helpers',
    'BackboneCollectionBinder',
    'Controllers/Site/Patients/PatientsApp/Collections/PatientCalendarChangesCollection',
    'Controllers/Site/Patients/PatientsApp/Views/PatientCalendar/PatientCalendarChangeView'
], function ($, _, Backbone, app, Helpers, BackboneCollectionBinder, PatientCalendarChangesCollection, PatientCalendarChangeView) {
    return Backbone.View.extend({

        el: "#calendar-history",

        events: {

        },

        initialize: function () {
            var elManagerFactory = new BackboneCollectionBinder.ViewManagerFactory(
                function (model) {
                    return new PatientCalendarChangeView({ model: model });
                }),
                collectionBinder = new BackboneCollectionBinder(elManagerFactory);

            app.collections.PatientCalendarChangesCollection = app.collections.PatientCalendarChangesCollection || new PatientCalendarChangesCollection();
            collectionBinder.bind(app.collections.PatientCalendarChangesCollection, this.$el);

            this.listenTo(app.collections.PatientCalendarChangesCollection, 'reFetch', this.reFetch);

        },

        render: function () {
            var self = this;

            Helpers.renderSpinner(this.$el);

            if (!app.collections.PatientCalendarChangesCollection.isFetched) {
                app.collections.PatientCalendarChangesCollection.isFetched = false;
                app.collections.PatientCalendarChangesCollection.reset();
                app.collections.PatientCalendarChangesCollection.fetch({
                    success: function () {
                        app.collections.PatientCalendarChangesCollection.isFetched = true;
                        self.$el.find('.spinner').remove();
                    }
                });
            } else {
                if (app.collections.PatientCalendarChangesCollection.isFetched) {
                    this.initialize();
                    self.$el.find('.spinner').remove();
                }
            }

            this.$el.scroll(function () {
                self.loadNextPage();
            });

            return this;
        },

        reFetch: function(){
            app.collections.PatientCalendarChangesCollection.isFetched = false;
            this.render();
        },

        loadNextPage: function () {
            var self = this,
                containerHeight = this.$el.height(),
                scrollPosition = this.$el.scrollTop(),
                scrollHeight = this.$el.prop('scrollHeight'),
                $spinnerBox = $('<div></div>');

            if (!this.disableScrolling
                && !app.collections.PatientCalendarChangesCollection.isLastPage
                && scrollHeight - scrollPosition < containerHeight + 30) {

                this.disableScrolling = true;
                this.$el.append($spinnerBox);
                Helpers.renderSpinner($spinnerBox);

                app.collections.PatientCalendarChangesCollection.fetch({
                    remove: false,
                    success: function () {
                        $spinnerBox.remove();
                        self.disableScrolling = false;
                    }
                });
            }
        }

    });
});