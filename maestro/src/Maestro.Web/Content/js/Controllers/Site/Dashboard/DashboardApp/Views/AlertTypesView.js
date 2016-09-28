'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Site/Dashboard/DashboardApp/AppNamespace',
    'Controllers/Site/Dashboard/DashboardApp/Collections/AlertTypeCollection',
    'Controllers/Site/Dashboard/DashboardApp/Views/AlertTypeView'
], function($, _, Backbone, app, AlertTypeCollection, AlertTypeView) {
    return Backbone.View.extend({

        template: app.OPTIONS.TEMPLATE('alertTypeWidgetView'),

        visibleClass: 'visible',

        disabledClass: 'disabled',

        isFirstLoad: true,

        events: {
            'click .range-item': 'selectFilter',
            'click a.select-all': 'selectAll'
        },

        initialize: function() {

            this.listenTo(app.app.views.patientDashboard, 'patients-loaded', this.patientsLoaded);
        },

        render: function() {

            this.$el.html(this.template());
            return this;
        },

        patientsLoaded: function() {

            if (this.isFirstLoad) {
                this.renderContent();
                this.isFirstLoad = false;
            }
        },

        renderContent: function() {

            var counts = app.patientStorage.getCounts(),
                totalCount = this.detectTotalCount(counts);

            if (!totalCount) {

                return;
            }

            var alertTypes = this.extendAlertTypes(
                this.trimAlertTypes(app.OPTIONS.ALERT.TYPES),
                app.OPTIONS.ALERT.GET_ALL_TYPES(),
                app.patientStorage.getCounts()
            );

            app.app.collections.alertTypeCollection = new AlertTypeCollection(alertTypes);
            app.app.collections.alertTypeCollection.each( this.renderAlertType, this );

            this.$el.parent('.panel').addClass(this.visibleClass);
        },

        renderAlertType: function( alertType ){

            var $selectorBody = this.$el.find('.dashboard-selector-body');

            var alertTypeView = new AlertTypeView({ model: alertType });

            $selectorBody.append( alertTypeView.render().el );

        },

        selectAll: function(e) {

            e.stopImmediatePropagation();

            // if (app.app.views.patientDashboard.isLoading) {
            //     return;
            // }

            $('.range-item', this.$el)
                .removeClass(this.disabledClass);

            $('a.select-all', this.$el)
                .addClass('not-active');

            this.applyFilter();
        },

        selectFilter: function(e) {

            var $target = $(e.currentTarget);

            if ( /*app.app.views.patientDashboard.isLoading ||*/ !app.app.views.alertSeverities.hasActives() ||
                (!$target.hasClass('disabled') && $target.siblings().hasClass(this.disabledClass))
            ) {
                return;
            }

            $target
                .removeClass(this.disabledClass)
                .siblings()
                .addClass(this.disabledClass);

            $('a.select-all', this.$el)
                .removeClass('not-active');

            this.applyFilter();
        },

        applyFilter: function() {

            app.app.dashboardFilter = {
                types: this.getUnchecked(),
                severities: app.app.views.alertSeverities.getUnchecked()
            }

            app.app.views.patientDashboard.reloadPatientCards();
        },

        detectTotalCount: function(counts) {

            var total = 0;

            _.each(counts, function(count) {
                total += count.alertTypeCount;
            });

            return total;
        },

        extendAlertTypes: function(types, alertTypes, counts) {

            var totalCount = this.detectTotalCount(counts);

            _.each(counts, function(count) {
                var alert = app.OPTIONS.ALERT.GET_NAME_BY_CODE(count.alertType);

                types[alert.NAME].PERCENTAGE = Math.round((count.alertTypeCount * 100) / totalCount);
                types[alert.NAME].COUNT = count.alertTypeCount;
                types[alert.NAME].TOTALCOUNT = totalCount;
            });

            return alertTypes;
        },

        getActives: function() {

            return this.$el.find('.range-item:not(.disabled)');
        },

        hasActives: function() {

            return !!this.getActives().size();
        },

        getUnchecked: function() {

            var $disabled = this.getActives();

            return $disabled.map(function(index) {
                return $disabled.eq(index).attr('data-code');
            }).get();
        },

        trimAlertTypes: function(types) {

            types = $.extend({}, types);
            delete types.UNDEFINED;
            
            return types;

        }

    });
});