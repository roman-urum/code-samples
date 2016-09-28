'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Site/Patients/PatientsApp/AppNamespace',
    'Controllers/Helpers',
    'BackboneBootstrapAlert',
    'BackboneBootstrapModal',
    'Controllers/Site/Patients/PatientsApp/Collections/CareManagersCollection',
    'Controllers/Site/Patients/PatientsApp/Views/CareManagersCollectionView',
    'Controllers/Site/Patients/PatientsApp/Collections/CareManagersAssignedCollection',
    'Controllers/Site/Patients/PatientsApp/Views/CareManagersAssignedCollectionView',
], function (
        $,
        _,
        Backbone,
        app,
        Helpers,
        BackboneBootstrapAlert,
        BackboneBootstrapModal,
        CareManagersCollection,
        CareManagersCollectionView,
        CareManagersAssignedCollection,
        CareManagersAssignedCollectionView
    ) {
    return Backbone.View.extend({


        el: '#patients-container',

        events: {

            'click .js-patient-care-managers-save': 'savePatientEditCareManagers',
            'click .js-patient-care-managers-cancel': 'cancelPatientEditCareManagers',
            'click .js-assign-all-care-manager': 'assignAllCareManagers',
            'click .js-remove-all-care-manager': 'removeAllCareManagers'

        },

        initialize: function () {
            _.bindAll(this, "patientEditCareManagersChanged");
            app.vent.bind("patientEditCareManagersChanged", this.patientEditCareManagersChanged);

            this.listenTo(app.collections.careManagersCollection, 'fetched', this.render);

        },

        render: function () {
            var self = this;

            Helpers.renderSpinner(this.$el.find('#available-care-managers-container'));
            Helpers.renderSpinner(this.$el.find('#assigned-care-managers-container'));

            // if( app.collections.careManagersCollection ){

            if (app.collections.careManagersCollection.isFetched)
                self.renderCareManagers();

            // }else{
            //     app.collections.careManagersCollection = new CareManagersCollection();
            //     app.collections.careManagersCollection.isFetched = false;
            //     app.collections.careManagersCollection.fetch({
            //         success: function () {

            //             app.collections.careManagersCollection.isFetched = true;
            //             self.renderCareManagers();

            //         }
            //     });
            // }

            return this;

        },

        renderCareManagers: function () {

            var assignedCareManagers = this.model.get('careManagers'),
                assignedCareManagersCollection = [];

            app.collections.careManagersCollection.restore();
            app.collections.careManagersCollection.store();

            app.views.careManagersCollectionView = new CareManagersCollectionView({ collection: app.collections.careManagersCollection });
            this.$el.find('#available-care-managers-container').html(app.views.careManagersCollectionView.render().el);

            app.collections.careManagersCollection.each(function (model) {
                if ($.inArray(model.get('userId'), assignedCareManagers) + 1) {
                    assignedCareManagersCollection.push(model);
                }
            })

            app.collections.careManagersCollection.remove(assignedCareManagersCollection);

            app.collections.careManagerAssignedsCollection = new CareManagersAssignedCollection(assignedCareManagersCollection);
            app.views.careManagersAssignedCollectionView = new CareManagersAssignedCollectionView({ collection: app.collections.careManagerAssignedsCollection });
            this.$el.find('#assigned-care-managers-container').html(app.views.careManagersAssignedCollectionView.render().el);

            this.isChanged = false;
            return this;

        },

        savePatientEditCareManagers: function (e) {

            e.preventDefault();

            var self = this,
                managers = [];

            app.collections.careManagerAssignedsCollection.each(function (model) {
                managers.push(model.get('userId'));
            })

            this.model.set('careManagers', managers)

            if (this.model.isValid(true)) {

                $('.js-patient-care-managers-save').data('loading-text', 'Updating...').button('loading');

                this.model.save({ siteId: app.siteId }, {
                    success: function (model, response, options) {

                        self.model.store();
                        self.isChanged = false;

                        var alert = new BackboneBootstrapAlert({
                            alert: 'success',
                            message: 'Patient Care Managers were successfully updated',
                            autoClose: true
                        })
                        .show();

                        var href = $(e.currentTarget).attr('href');

                        app.router.navigate(href, {
                            trigger: true
                        });

                    },
                    error: function (model, xhr, options) {

                        $('.js-patient-care-managers-save').button('reset');

                        var alert = new BackboneBootstrapAlert({
                            alert: 'danger',
                            title: 'Error: ',
                            message: xhr.responseJSON.ErrorMessage,
                        })
                        .show();
                    }
                });
            }

        },

        cancelPatientEditCareManagers: function (e) {
            e.preventDefault();

            var self = this;

            if (this.isChanged) {

                var modalView = new BackboneBootstrapModal({
                    content: 'You have unsaved changes. If you leave the page, these changes will be lost.',
                    title: 'Confirm Navigation',
                    okText: 'Leave this page',
                    cancelText: 'Stay on this page',
                    modalOptions: {
                        backdrop: 'static',
                        keyboard: false
                    }
                })
                .open()
                .on('ok', function () {
                    self.cancelTriggerRouter(e);
                });

            } else {
                self.cancelTriggerRouter(e);
            }

        },

        cancelTriggerRouter: function (e) {
            e.preventDefault();

            if (!e.altKey && !e.ctrlKey && !e.metaKey && !e.shiftKey) {
                var href = $(e.currentTarget).attr('href');

                app.router.navigate(href, {
                    trigger: true
                });
            }
        },

        patientEditCareManagersChanged: function () {
            this.isChanged = true;
        },

        assignAllCareManagers: function () {

            app.collections.careManagersCollection.each(function (model) {
                app.collections.careManagerAssignedsCollection.add(model);
            })
            app.collections.careManagersCollection.reset();

        },

        removeAllCareManagers: function () {

            app.collections.careManagerAssignedsCollection.each(function (model) {
                app.collections.careManagersCollection.add(model);
            })
            app.collections.careManagerAssignedsCollection.reset();

        }

    });
});