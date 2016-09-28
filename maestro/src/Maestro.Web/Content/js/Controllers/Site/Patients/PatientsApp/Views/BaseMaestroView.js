'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Site/Patients/PatientsApp/AppNamespace'
], function ($, _, Backbone, app) {
    return Backbone.View.extend({

        //this is commented to force
        //methods who use this property
        //to create it in itself directly
        //not in prototype
        //childViews: {},

        //override it
        //example
        //regions: {
        //    regionName: '.region-selector'
        //}
        regions: {},

        events: {
            'click .js-link': '_changeState'
        },

        //use it to set view example to region
        setChildView: function (regionName, childViewExample) {

            if (!this.childViews) {
                this.childViews = {};
            }

            if (!this.regions) {
                this.regions = {};
            }

            if (this.childViews[regionName]) {
                this.childViews[regionName].remove();           //run child view self-destroying
            }

            this.childViews[regionName] = childViewExample;

            if (!childViewExample) {
                return;
            }

            //childViewExample.render();

            //var regionSelector = this.regions[regionName];
            //this.$(regionSelector).html(childViewExample.$el);  //insert child's html

        },

        _changeState: function (e) {
            e.preventDefault();

            app.models.patientModel = app.models.patientModel || this.model;

            if (!e.altKey && !e.ctrlKey && !e.metaKey && !e.shiftKey) {
                var href = $(e.currentTarget).attr('href');

                app.router.navigate(href, {
                    trigger: true
                });
            }
        },

        //override it if needed
        onBeforeRender: function () { },

        //override it id needed
        onAfterRender: function () { },

        //do not override
        //use onBeforeRender & onAfterRender
        render: function () {
            var self = this;
            this.onBeforeRender();

            var data = self.model ? self.model.toJSON() : {};
            self.$el.html(self.template(data));

            _.each(self.childViews, function (view, regionName) {
                view.render();
                self.$(regionName).html(view.$el);
            });

            self.onAfterRender();
            return this;
        },

        //override it
        //view's method to remove custom events etc
        //called during view.remove()
        onBeforeRemove: function () { },

        //do not override
        //use onBeforeRemove
        remove: function () {

            //call childViews's remove methods
            if (this.childViews) {
                _.invoke(this.childViews, 'remove');
            }

            //view's method to remove custom events etc
            this.onBeforeRemove();

            //call backbone remove method
            Backbone.View.prototype.remove.apply(this, arguments);

        }

    });
});