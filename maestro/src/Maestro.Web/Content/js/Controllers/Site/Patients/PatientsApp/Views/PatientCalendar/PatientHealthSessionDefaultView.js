'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Site/Patients/PatientsApp/AppNamespace',
    'Controllers/Helpers',
    'BackboneBootstrapModal',
    'BackboneBootstrapAlert'
], function (
    $,
    _,
    Backbone,
    app,
    Helpers,
    BackboneBootstrapModal,
    BackboneBootstrapAlert
) {
    return Backbone.View.extend({

        className: 'default-health-session__body no-assigned',

        templateNoAssigned: _.template('No Assigned'),

        template: _.template(
            '<div class="default-health-session__mgmt">\
                <a class="js-edit-default-session">\
                    <i class="fa fa-pencil-square-o" aria-hidden="true"></i>\
                </a>\
                <a class="js-delete-default-session">\
                    <i class="fa fa-times" aria-hidden="true"></i>\
                </a>\
            </div>\
            <%=name%>\
        '),

        events: {
            'click .js-edit-default-session': 'editDefaultSession',
            'click .js-delete-default-session': 'deleteDefaultSession'
        },

        initialize: function () {
            _.bindAll(this, "renderSession");

            this.listenTo(this.model, 'updated', this.renderSession);
        },

        render: function () {
            Helpers.renderSpinner(this.$el, 'small');

            if (!this.model.isFetched) {
                this.model.fetch({
                    success: this.renderSession
                });
            } else {
                this.renderSession();
            }

            return this;
        },

        renderSession: function () {
            this.model.isFetched = true;

            if (this.model.get('id')) {
                this.$el.html(this.template(this.model.attributes)).removeClass('no-assigned');
            } else {
                this.$el.html(this.templateNoAssigned()).addClass('no-assigned');
            }
        },

        editDefaultSession: function () {
            app.views.patientCalendarView.editHealthSessionCalendar({ isEdit: true, isDefault: true });

            if (app.collections.patientProtocolSearchCollection.isFetched) {
                this.updateSessionModel();
            } else {
                this.listenTo(app.collections.patientProtocolSearchCollection, 'fetched', this.updateSessionModel);
            }
        },

        updateSessionModel: function () {
            this.stopListening(app.collections.patientProtocolSearchCollection);

            var protocols = _.extend({}, this.model.get('protocols')),
                protocolCollection = app.models.patientHealthSessionModel.get('protocols'),
                patientHealthSessionModel = _.extend({}, this.model.attributes),
                protocolSearchModel;

            delete patientHealthSessionModel.protocols;

            app.models.patientHealthSessionModel.set(patientHealthSessionModel);

            _.each(protocols, function (protocol) {
                protocolSearchModel = app.collections.patientProtocolSearchCollection.findWhere({ id: protocol.protocolId });
                protocolSearchModel.set({
                    'order': protocol.order,
                    'isAdded': true
                });

                protocolCollection.add(protocolSearchModel);
            });
        },

        deleteDefaultSession: function () {
            var self = this;

            var confirmModal = new BackboneBootstrapModal({
                title: 'Delete the Default Health Session?',
                content: '<div class="alert alert-danger hidden" role="alert"></div><h3>Are you sure want to delete Default Health Session?</h3>',
                modalOptions: {
                    backdrop: 'static',
                    keyboard: false
                }
            })
            .open()
            .on('ok', function () {
                confirmModal.$el.find('.btn.ok').data('loading-text', 'Deleting...').button('loading');

                self.model.destroy({
                    success: function (model, response, options) {
                        confirmModal.close();

                        self.model.reset();

                        self.renderSession();

                        var alert = new BackboneBootstrapAlert({
                            alert: 'success',
                            message: 'Default Session was successfully deleted',
                            autoClose: true
                        }).show();

                        app.vent.trigger('patient-health-session:resetSession');
                        app.vent.trigger("updateCurrentMonth");
                    },
                    error: function (model, xhr, options) {
                        confirmModal.$el.find('.btn.ok').button('reset');

                        var errorAlert = confirmModal.$el.find('.alert-danger');
                        errorAlert.html(xhr.responseJSON ? xhr.responseJSON.ErrorMessage : "Response status: " + xhr.statusText);
                        errorAlert.removeClass('hidden');
                    }
                });

                confirmModal.preventClose();
            });
        }
    });
});