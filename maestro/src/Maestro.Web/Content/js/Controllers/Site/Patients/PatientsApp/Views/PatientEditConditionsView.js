'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Site/Patients/PatientsApp/AppNamespace',
    'Controllers/Helpers',
    'Controllers/Constants',
    'BackboneBootstrapAlert',
    'BackboneBootstrapModal',
    'Controllers/Site/Patients/PatientsApp/Views/ConditionsCollectionView',
    'Controllers/Site/Patients/PatientsApp/Views/ConditionsAssignedCollectionView'
], function (
    $,
    _,
    Backbone,
    app,
    Helpers,
    Constants,
    BackboneBootstrapAlert,
    BackboneBootstrapModal,
    ConditionsCollectionView,
    ConditionsAssignedCollectionView
) {
    return Backbone.View.extend({
        el: '#patients-container',

        availableConditionsContainerSelector: '#available-conditions-container',

        assignedConditionsContainerSelector: '#assigned-conditions-container',

        availableConditionsOverlaySelector: '.available-conditions-container .box-overlay',

        $popover: $(),

        events: {
            'click .js-patient-conditions-save': 'savePatientEditConditions',
            'click .js-patient-conditions-cancel': 'cancelPatientEditConditions',
            'click .js-assign-all-conditions': 'assignAllConditions',
            'click .js-remove-all-conditions': 'removeAllConditions'
        },

        initialize: function () {
            this.customerConditions = app.collections.customerConditionsCollection;
            this.assignedConditions = app.collections.assignedConditionsCollection;
            this.availableConditions = app.collections.availableConditionsCollection;

            _.bindAll(this, "patientEditConditionsChanged");
            app.vent.bind("patientEditConditionsChanged", this.patientEditConditionsChanged);

            this.listenTo(this.customerConditions, 'fetched', function () { this.render(); });
            this.listenTo(this.assignedConditions, 'fetched', function () { this.render(); });
            this.listenTo(this.availableConditions, 'fetched', function () { this.render(); });

            app.vent.on('hidePopover', this.hidePopover.bind(this));
            app.vent.on('condition:assign', this.assignCondition.bind(this));
            app.vent.on('condition:remove', this.removeCondition.bind(this));
        },

        render: function () {
            var self = this;

            Helpers.renderSpinner(this.$(this.availableConditionsContainerSelector));
            Helpers.renderSpinner(this.$(this.assignedConditionsContainerSelector));

            this.initPopovers();
            this.renderConditionsLists();
            this.renderOverlay();

            this.isChanged = false;

            return this;
        },

        renderConditionsLists: function () {
            if (this.assignedConditions.isFetched && this.availableConditions.isFetched) {

                app.views.conditionsAssignedCollectionView = new ConditionsAssignedCollectionView({ collection: this.assignedConditions });
                this.$(this.assignedConditionsContainerSelector).html(app.views.conditionsAssignedCollectionView.render().$el);

                app.views.conditionsCollectionView = new ConditionsCollectionView({ collection: this.availableConditions });
                this.$(this.availableConditionsContainerSelector).html(app.views.conditionsCollectionView.render().$el);
            }
        },

        initPopovers: function () {
            //TODO: refactor this in all tabs
            this.$popover = $('[data-toggle="popover"]').popover({
                template: '<div class="popover">\
                    <div class="arrow"></div>\
                        <div class="popover-header">\
                            <button type="button" class="close" data-dismiss="popover" aria-hidden="true">&times;</button>\
                            <h3 class="popover-title"></h3>\
                        </div>\
                        <div class="popover-content"></div>\
                    </div>',
                html: true,
                container: 'body',
                placement: 'bottom'
            }).on('shown.bs.popover', function (e) {
                $('[data-dismiss="popover"]')
                    .off('click')
                    .on('click', function () {
                        $(e.currentTarget).click();
                    });
            });
        },

        hidePopover: function () {
            this.$popover.popover('hide');
        },

        _showIVRWarning: function () {
            var warningModal = new BackboneBootstrapModal({
                content: _.template($('#devicesIVRErrorTemplate').html())(),
                okText: 'Ok',
                allowCancel: false,
                modalOptions: {
                    backdrop: 'static',
                    keyboard: false
                }
            });

            warningModal.open();
        },

        savePatientEditConditions: function (e) {
            e.preventDefault();

            var self = this;
            var buttons = this.$('.js-patient-conditions-save');

            console.log(self.model);
            buttons.data('loading-text', 'Updating...').button('loading');
            this.assignedConditions.save({
                success: function (model, response, options) {
                    self.model.set('conditions', self.assignedConditions.models);
                    self.model.store();
                    self.isChanged = false;

                    var alert = new BackboneBootstrapAlert({
                        alert: 'success',
                        message: 'Patient Conditions were successfully updated',
                        autoClose: true
                    }).show();

                    var href = $(e.currentTarget).attr('href');
                    app.router.navigate(href, {
                        trigger: true
                    });
                },
                error: function (model, xhr, options) {
                    buttons.button('reset');

                    var alert = new BackboneBootstrapAlert({
                        alert: 'danger',
                        title: 'Error: ',
                        message: xhr.responseJSON.ErrorMessage
                    }).show();
                }
            });
        },

        patientEditConditionsChanged: function () {
            this.isChanged = true;
        },

        conditionsSavedSuccess: function () {
            var href = $(this.e.currentTarget).attr('href');

            this.isChanged = false;

            app.router.navigate(href, {
                trigger: true
            });
        },

        cancelPatientEditConditions: function (e) {
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

        assignCondition: function (model) {
            app.collections.assignedConditionsCollection.add(model);
            app.collections.availableConditionsCollection.remove(model);
            this.renderOverlay();
        },

        removeCondition: function (model) {
            app.collections.assignedConditionsCollection.remove(model);
            app.collections.availableConditionsCollection.add(model);

            this.renderOverlay();
        },

        assignAllConditions: function (e) {
            e.preventDefault();

            var models = app.collections.availableConditionsCollection.models;

            app.collections.assignedConditionsCollection.add(models);
            app.collections.availableConditionsCollection.remove(models);
        },

        removeAllConditions: function (e) {
            e.preventDefault();

            var models = app.collections.assignedConditionsCollection.models;

            app.collections.availableConditionsCollection.add(models);
            app.collections.assignedConditionsCollection.remove(models);
        },

        renderOverlay: function () {
            //if (app.collections.assignedConditionsCollection.isFetched &&
            //    app.collections.assignedConditionsCollection.length > 0) {
            //    this.$(this.availableConditionsOverlaySelector).show();
            //} else {
            //    this.$(this.availableConditionsOverlaySelector).hide();
            //}
        }
    });
});