'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Constants',
    'Controllers/Site/Dashboard/DashboardApp/AppNamespace',
    'Controllers/Site/Dashboard/DashboardApp/Collections/PatientCardCollection',
    'Controllers/Site/Dashboard/DashboardApp/Views/PatientCardsView',
    'Controllers/Site/Dashboard/DashboardApp/Collections/CareManagersCollection'
], function ($, _, Backbone, Constants, app, PatientCardCollection, PatientCardsView, CareManagersCollection) {
    return Backbone.View.extend({
        tagName: 'div',
        className: 'patient-list',
        spinner: app.OPTIONS.TEMPLATE('waitTemplate'),
        noCards: app.OPTIONS.TEMPLATE('patientCardViewTemplateNoCards'),
        $window: $(window),
        isLoading: false,
        careManagersCollection: null,
        careManagersFilterTemplate: _.template($("#careManagersFilterTemplate").html()),
        $careMangersFilterDropdownElement: $('#care-managers-filter-dropdown'),

        events: {
            
            'change #care-managers-filter-dropdown': 'onChangeCareManagerFilter'

        },

        initialize: function () {
            this.careManagersCollection = new CareManagersCollection();

            this.bind();
        },

        bind: function () {
            var self = this;
            self.careManagersCollection.fetch({
                data: {
                    onlyCareManagersWithAssignedPatients: true
                },
                success: function () {
                    self.$careMangersFilterDropdownElement.removeAttr('disabled');

                    var sortedCareManagers = _.sortBy(self.careManagersCollection.toJSON(), function (careManager) {
                        return careManager.firstName.toLowerCase() + " " + careManager.lastName.toLowerCase();
                    });

                    self.$careMangersFilterDropdownElement.html(self.careManagersFilterTemplate({
                        currentCareMenagerId: Constants.currentCareManager.userId,
                        careManagers: sortedCareManagers
                    }));
                }
            });
            self.$careMangersFilterDropdownElement.change(self.onChangeCareManagerFilter.bind(self));

            self.renderCards = self.renderCards.bind(self);
            self.renderSpinner = self.renderSpinner.bind(self);
            self.patientsLoaded = self.patientsLoaded.bind(self);
        },

        patientsLoaded: function() {
            this.trigger('patients-loaded');
        },

        renderCards: function (patients) {
            var patientCardCollection = new PatientCardCollection(patients);

            if (patientCardCollection.length) {

                this.$el.html((new PatientCardsView({
                    collection: patientCardCollection,
                    careManagerId: this.$careMangersFilterDropdownElement.val()
                })).render().el);
            } else {
                this.$el.html(this.noCards());
            }
        },

        renderSpinner: function() {
            if (app.patientStorage.hasMore()) {
                this.$el.append(this.spinner());
            } else {
                this.isLoading = false;
            }
        },

        reloadPatientCards: function() {
            this.$el.empty();
            this.$window.off('scroll.loadMoreCards');
            app.patientStorage.clearStorage();

            if (app.app.views.alertSeverities.hasActives() && app.app.views.alertTypes.hasActives()) {
                this.render( this.$careMangersFilterDropdownElement.val() );
            } else {
                this.$el.html(this.noCards());
            }
        },

        render: function(careManagerId) {
            this.$el.append(this.spinner());

            this.isLoading = true;

            app.patientStorage
                .getPatients(app.OPTIONS.ALERT.DEFAULT_COUNT(), app.app.dashboardFilter.types, app.app.dashboardFilter.severities, careManagerId)
                .then(this.renderCards)
                .then(this.patientsLoaded)
                .fail(app.errorHandler)
                .always(this.renderSpinner);

            return this;
        },

        onChangeCareManagerFilter: function() {
            this.reloadPatientCards(app.app.views.alertTypes.getUnchecked(), app.app.views.alertSeverities.getUnchecked());
        }
    });
});
