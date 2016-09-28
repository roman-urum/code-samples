'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Site/Dashboard/DashboardApp/AppNamespace',
    'Controllers/Site/Dashboard/DashboardApp/Collections/SeverityCollection',
    'Controllers/Site/Dashboard/DashboardApp/Views/AlertSeverityView'
], function($, _, Backbone, app, SeverityCollection, AlertSeverityView) {
    return Backbone.View.extend({

        template: app.OPTIONS.TEMPLATE('alertSeverityWidgetView'),

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

            this.$el.html( this.template() );
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
                severities = this.extendSeverities( this.flattenSeverities( counts ) ),
                totalCount = this.detectTotalCount( severities );


            if (!totalCount) {

                this.$el.hide();
                return;
            }

            app.app.collections.severityCollection = new SeverityCollection( severities );
            app.app.collections.severityCollection.each( this.renderSeverity, this );

            this.$el.parent('.panel').addClass(this.visibleClass);
        },

        renderSeverity: function(severity){

            var $selectorBody = this.$el.find('.dashboard-selector-body');
            var severityView = new AlertSeverityView( {model: severity });
            
            $selectorBody.append( severityView.render().el );
        },

        selectAll: function(e) {

            e.preventDefault();
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

            if ( /*app.app.views.patientDashboard.isLoading ||*/ !app.app.views.alertTypes.hasActives() ||
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
                types: app.app.views.alertTypes.getUnchecked(),
                severities: this.getUnchecked()
            }


            app.app.views.patientDashboard.reloadPatientCards();
        },


        flattenSeverities: function(counts) {
            var result = [];

            counts = _.map(counts, function(count) {
                return count.alertSeverityCounts;
            });

            counts = _.flatten(counts);

            _.each(counts, function(count) {
                var existing = _.findWhere(result, { id: count.id });

                if (existing) {
                    existing.count += count.count;
                } else {
                    result.push(count);
                }
            });

            return result;
        },

        detectTotalCount: function(severities) {

            var total = 0;

            _.each(severities, function(count) {
                total += count.count;
            });

            return total;
        },

        extendSeverities: function(severities) {

            var totalCount = this.detectTotalCount(severities);

            _.each(severities, function(severity) {
                severity.totalCount = totalCount;
                severity.percentage = Math.round((severity.count * 100) / totalCount);
            });

            return severities;
        },

        getActives: function() {

            return $('.range-item:not(.disabled)', this.$el);
        },

        hasActives: function() {

            return !!this.getActives().size();
        },

        getUnchecked: function() {

            var $disabled = this.getActives();

            return $disabled.map(function(index) {
                return $disabled.eq(index).attr('data-id');
            }).get();
        },

        trimAlertTypes: function(types) {

            types = $.extend({}, types);
            delete types.UNDEFINED;

            return types;
        }


    });
});


