'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'BackboneBootstrapAlert',
    'Controllers/Site/Patients/PatientsApp/AppNamespace',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Collections/SearchCollection',
    'Controllers/Helpers'
], function ($, _, Backbone, BackboneBootstrapAlert, app, SearchCollection, Helpers) {
    return Backbone.View.extend({
        template:                   _.template($('#conditionDetailsContentTemplate').html()),
        noProgramsFoundTemplate:    _.template($('#noProgramsFoundView').html()),
        noProtocolsFoundTemplate:   _.template($('#noProtocolsFoundView').html()),
        programListItemTemplate:    _.template($('#programListItemTemplate').html()),
        protocolListItemTemplate:   _.template($('#protocolListItemTemplate').html()),

        selectors: {
            programsContainer: '.programs-list-container',
            protocolsContainer: '.protocols-list-container'
        },

        initialize: function () {
        },

        render: function () {
            var self = this;
            this.$el.html(this.template(this.model.attributes));

            this.$programsListEl = this.$(this.selectors.programsContainer);
            this.$protocolsListEl = this.$(this.selectors.protocolsContainer);

            this.$programsListEl.html(this.noProgramsFoundTemplate());
            this.$protocolsListEl.html(this.noProtocolsFoundTemplate());

            Helpers.renderSpinner(this.$programsListEl);
            Helpers.renderSpinner(this.$protocolsListEl);

            this.getContent({
                success: function (searchResultCollection) {
                    self.renderContent(searchResultCollection);
                }
            });

            return this;
        },

        renderContent: function (searchResultCollection) {
            var self = this;
            var programs = searchResultCollection.filter(function(model) { return model.get('type') === 1; });
            var protocols = searchResultCollection.filter(function(model) { return model.get('type') === 2; });

            if (!programs || programs.length <= 0) {
                self.$programsListEl.html(self.noProgramsFoundTemplate());
            } else {
                self.$programsListEl.empty();

                _.each(programs, function(programModel) {
                    self.$programsListEl.append(self.programListItemTemplate(programModel.toJSON()));
                });
            }

            if (!protocols || protocols.length <= 0) {
                self.$protocolsListEl.html(self.noProtocolsFoundTemplate());
            } else {
                self.$protocolsListEl.empty();

                _.each(protocols, function (protocolModel) {
                    self.$protocolsListEl.append(self.protocolListItemTemplate(protocolModel.toJSON()));
                });
            }
        },

        getContent: function (options) {
            options = options || {};
            var self = this;
            this.searchContentCollection = new SearchCollection();

            this.fetchXhr = this.searchContentCollection.fetch({
                traditional: true,
                data: {
                    categories: ["protocol", "program"],
                    q: [],
                    tags: self.model.get('tags')
                },
                reset: true,
                success: function(collection) {
                    if (_.isFunction(options.success)) options.success.apply(self, arguments);
                },
                error: function () {
                    var alertMessage = new BackboneBootstrapAlert({
                        alert: 'danger',
                        message: 'Error occured during fetching content',
                        autoClose: false
                    }).show();
                    if (_.isFunction(options.error)) options.success.apply(self, arguments);
                }
            });
        }
    });
});