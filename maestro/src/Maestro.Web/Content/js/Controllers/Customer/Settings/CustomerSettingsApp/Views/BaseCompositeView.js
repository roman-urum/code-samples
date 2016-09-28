'use strict';

define([
    'jquery',
    'underscore',
    'backbone.marionette',
    'Controllers/Customer/Settings/CustomerSettingsApp/Application'
], function (
    $,
    _,
    Marionette,
    App
) {
    return Marionette.CompositeView.extend({

        useSpinnerClass: 'use-spinner',

        initialize: function (options) {
            options = options || {};
            if (!options.model) throw 'Model is required';

            Marionette.CompositeView.prototype.initialize.apply(this, arguments);

            var self = this;
            if (this.useSpinner) {
                this.$el.addClass(this.useSpinnerClass);
            }

            this.collection = options.model;    //use 'model' option to unify creating views
            this.listenTo(this.collection, 'fetched', function () {
                self.render();
            });
        },

        render: function () {
            //clean previously rendered spinner
            this.$(this.childViewContainer).empty();

            //run default render functionality
            Marionette.CompositeView.prototype.render.apply(this, arguments);

            if (this.useSpinner && !this.collection.isFetched) {
                this.showSpinner();
            }
        },

        showSpinner: function () {
            var tplFunc = _.template($('#spinnerTemplate').html());
            this.$el.append(tplFunc());
        }

    });
});