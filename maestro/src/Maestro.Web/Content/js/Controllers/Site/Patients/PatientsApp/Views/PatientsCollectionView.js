'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Site/Patients/PatientsApp/AppNamespace',
    'Controllers/Constants',
    'Controllers/Site/Patients/PatientsApp/Views/PatientsCollectionItemView'
], function ($, _, Backbone, app, CONSTANTS, PatientsCollectionItemView) {
    return Backbone.View.extend({
        tagName: 'div',
        className: 'panel-group panel-group-primary-ci',
        defaultHeight: 60,
        currentCount: 0,
        loadMore: false,
        spinnerTemplate: _.template($('#waitTemplate').html()),
        $window: $(window),
        $document: $(document),
        data: {
            q: '',
            includeInactivePatients: false,
            careManagerId: CONSTANTS.currentCareManager == null ? "" : CONSTANTS.currentCareManager.userId
        },

        loadPatients: function (take) {
            this.collection.sync({
                data: $.extend({}, this.data, {
                    take: take
                })
            });
        },

        initialize: function () {
            this.bind();

            this.collection.reset();
            this.loadPatients((this.currentCount = Math.round((this.$window.height() / this.defaultHeight))) * 2);

            this.listen();
        },

        bind: function () {
            this.windowScroll = this.windowScroll.bind(this);
        },

        listen: function () {
            this.$window
                .off('scroll.scrollPatients')
                .on('scroll.scrollPatients', this.windowScroll);

            this.listenTo(this, 'show-patients', this.showPatients);
            this.listenTo(this.collection, 'add', this.addPatients);
            this.listenTo(this.collection, 'patients-loaded', this.patientsLoaded);
            this.listenTo(this.collection, 'patients-loading', this.patientsLoading);
        },

        showPatients: function (data) {
            data = {
                q: data.q,
                includeInactivePatients: data.includeInactivePatients,
                careManagerId: data.careManagerId,
                identifiers: data.identifiers
            };

            if (_.isMatch(this.data, data)) return;

            this.collection.reset();
            this.$el.empty();

            this.data = data;
            this.loadPatients(this.currentCount);
        },

        patientsLoaded: function () {
            this.removeSpinner();

            if (this.loadMore) {
                this.loadMore = false;
                this.loadPatients(this.currentCount);
            } else if (this.shouldLoad() && this.collection.hasMore() && !this.collection.isLoading) {
                this.loadPatients(this.currentCount);
            }

            /*if (!this.collection.hasMore()) {
                this.$window.off('.scrollPatients');
            }*/
        },

        patientsLoading: function () {
            this.renderSpinner();
        },

        addPatients: function (patient) {
            this.renderPatient(patient);
        },

        shouldLoad: function () {
            return this.$window.height() + this.$window.scrollTop() >= this.$document.innerHeight() / 2;
        },

        windowScroll: function () {
            if (this.shouldLoad() && this.collection.hasMore() && !this.collection.isLoading) {
                this.loadPatients(this.currentCount);
            }
        },

        renderSpinner: function () {
            setTimeout(function () {
                this.$el.append(this.spinnerTemplate());
            }.bind(this), 0);
        },

        removeSpinner: function () {
            this.$el.children('.spinner').remove();
        },

        render: function () {

            // this.collection.each(this.renderCareManager, this);
            this.renderSpinner();
            return this;
        },

        renderPatient: function (patient) {
            var patientsCollectionItemView = new PatientsCollectionItemView({ model: patient });
            this.$el.append(patientsCollectionItemView.render().el);

            return this;
        }
    });
});