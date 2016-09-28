'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Site/Dashboard/DashboardApp/AppNamespace',
    'BackboneBootstrapModal',
    'Controllers/Site/Dashboard/DashboardApp/Views/PatientCardIgnoreReadingModal',
    'Controllers/Site/OneWayChat/OneWayChatModule',
    'BackboneZoomCalls',
    'BackboneBootstrapAlert'
], function($, _, Backbone, app, BackboneBootstrapModal, PatientCardIgnoreReadingModal, OneWayChat, BackboneZoomCalls, BackboneBootstrapAlert) {
    return Backbone.View.extend({
        tagName: 'div',
        className: 'patient-alert-detail',
        isDeleting: false,
        isDeleted: false,
        templates: {
            MEASUREMENT: app.OPTIONS.TEMPLATE('patientCardViewTemplateDetailTHRESHOLD'),
            BEHAVIOR: app.OPTIONS.TEMPLATE('patientCardViewTemplateDetailBEHAVIOR'),
            ADHERENCE: app.OPTIONS.TEMPLATE('patientCardViewTemplateDetailADHERENCE')
        },
        alerts: {
            MEASUREMENT: app.OPTIONS.TEMPLATE('patientCardViewTemplateHeaderTHRESHOLD'),
            BEHAVIOR: app.OPTIONS.TEMPLATE('patientCardViewTemplateHeaderBEHAVIOR'),
            ADHERENCE: app.OPTIONS.TEMPLATE('patientCardViewTemplateHeaderADHERENCE')
        },
        watchButton: app.OPTIONS.TEMPLATE('patientCardViewWatchPatient'),
        'events': {
            'click.acknowledge-alert .acknowledge-alert': 'acknowledgeAlert',
            'click.ignore-reading .ignore-reading': 'showIgnoreModal',
            'click.start-chat a.js-chat': 'openChat',
            'click.start-video a.js-video': 'videoCall'
        },

        openChat: function(e) {
            e.preventDefault();

            var patientName = this.model.get('patientInfo').name.split(' ');

            OneWayChat.showModal({
                siteId: app.OPTIONS.URL.SITE,
                patientId: this.model.get('patientInfo').id,
                patientFirstName: patientName[0],
                patientLastName: patientName[1]
            });
        },

        videoCall: function(e) {
            e.preventDefault();

            this.videoCallBtn = $(e.currentTarget);
            this.videoCallBtn.attr('data-loading-text', 'Calling...').button('loading');

            var zoomCalls = new BackboneZoomCalls({
                siteId: app.OPTIONS.URL.SITE,
                patientId: this.model.get('patientInfo').id,
                started: this.videoCallStarted,
                timeout: this.videoCallTimeout
            });

            zoomCalls.createMeeting();

        },

        videoCallStarted: function() {
            var alertMessage = new BackboneBootstrapAlert({
                alert: 'success',
                message: 'Meeting started successfully',
                autoClose: true
            })
                .show();

            this.videoCallBtn.button('reset');

        },

        videoCallTimeout: function() {
            var alertMessage = new BackboneBootstrapAlert({
                alert: 'danger',
                title: 'Timeout Error: ',
                message: 'Please confirm that Zoom is installed and try again.',
            })
                .show();
            this.videoCallBtn.button('reset');
        },

        initialize: function() {
            this.bind();
            this.setId();
            this.listen();

            this.model.attributes.url = app.OPTIONS.URL.LINK;
        },

        bind: function() {
            this.ignoreAlert = this.ignoreAlert.bind(this);
            this.afterDelete = this.afterDelete.bind(this);
        },

        setId: function() {
            this.$el.attr('id', this.model.attributes.id);
        },

        listen: function() {
            //this.listenTo(this.model, 'watch-patient', this.renderPatientLink);
        },

        afterDelete: function() {
            if (this.ignoreModal) {
                this.ignoreModal.close();
            }

            this.isDeleting = false;
            this.isDeleted = true;
        },

        acknowledgeAlert: function(e) {
            e.preventDefault();

            if (this.isDeleting || this.isDeleted) {
                return;
            }

            this.isDeleting = true;

            this.model.collection.trigger('remove-alert-up', app.OPTIONS.ALERT.ACTIONS.ACKNOWLEDGE, this.model, this.afterDelete);
        },

        ignoreAlert: function() {
            this.ignoreModal.preventClose();

            if (this.isDeleting || this.isDeleted) {
                return;
            }

            this.isDeleting = true;

            this.model.collection.trigger('remove-alert-up', app.OPTIONS.ALERT.ACTIONS.IGNORE, this.model, this.afterDelete);
        },

        showIgnoreModal: function(e) {
            e.preventDefault();

            if (this.isDeleting || this.isDeleted) {
                return;
            }

            this.ignoreModal = new BackboneBootstrapModal({
                content: new PatientCardIgnoreReadingModal(),
                title: 'Ignore this reading',
                okText: 'Ignore',
                cancelText: 'Cancel',
                animate: true,
                modalOptions: {
                    backdrop: 'static',
                    keyboard: false
                }
            });

            this.ignoreModal
                .on('ok', this.ignoreAlert)
                .open();
        },

        renderHeader: function(model) {
            $('<div class="row alert-wrapper" data-toggle="collapse" data-target="#details-' + model.id + '" aria-expanded="false" aria-controls="details-' + model.id + '"><div class="col-sm-3"></div><div class="col-sm-9 alert"><div class="row">' + this.alerts[model.reading.alert.type.NAME](model) + '</div></div></div>')
                .find('.collapse')
                .collapse()
                .end()
                .prependTo(this.$el);
        },

        renderPatientLink: function() {
            var $wrapper = this.$el.find('.watch-patient-wrapper');

            if (!this.model.attributes.siblings) {
                $wrapper
                    .html(this.watchButton({
                        link: app.OPTIONS.URL.LINK,
                        patientId: this.model.attributes.patientInfo.id
                    }));
            } else {
                $wrapper.empty();
            }
        },

        render: function () {
            var template = this.templates[this.model.attributes.reading.alert.type.NAME];

            if (template) {
                this.$el
                    .append(this.templates[this.model.attributes.reading.alert.type.NAME](this.model.attributes));

                this.renderPatientLink();
                this.renderHeader(this.model.attributes);
            }

            return this;
        }
    });
});