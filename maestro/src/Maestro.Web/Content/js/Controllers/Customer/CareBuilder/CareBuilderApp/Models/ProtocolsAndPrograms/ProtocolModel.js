'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Customer/CareBuilder/CareBuilderApp/AppGlobalVariables',
    'Controllers/Constants',
    'Controllers/Helpers',
    'backbone-nested'
], function (
    $,
    _,
    Backbone,
    app,
    Constants,
    Helpers
) {
    return Backbone.NestedModel.extend({
        url: function () {
            return '/CareBuilder/Protocol?protocolId=' + this.get('id') + '&isBrief=false';
        },

        sync: function (method, model, options) {
            if (method == 'update') {

                options.url = '/CareBuilder/Protocol';
            }
            return Backbone.sync(method, model, options);

        },

        initialize: function () {
            // Memento initializing
            _.extend(this, new Backbone.Memento(this));
        },

        defaults: {
            id: null,
            name: {
                value: ''
            },
            isPrivate: false,
            tags: []
        },

        parse: function (response, options) {
            return Helpers.convertKeysToCamelCase(response);
        },

        // Validates that elements' branches haven't references to not existed severities.
        isBranchesValid: function () {
            var result = true,
                alertSeverities = app.collections.alertSeverities;

            _.each(this.get('protocolElements'), function (protocolElement) {
                _.each(protocolElement.branches, function (branch) {

                    if (branch.thresholdAlertSeverityId != null
                        && !alertSeverities.where({ id: branch.thresholdAlertSeverityId }).length) {

                        result = false;
                    }
                });
            });

            return result;
        },

        deleteProtocolElement: function (deletedElementId, nextProtocolElement) {
            var removedProtocolElements = this.getBranchElementsIds(
                    deletedElementId,
                    nextProtocolElement),
                protocolElements = this.get('protocolElements'),
                updatedProtocolElements = _.reject(protocolElements, function (element) {
                    return removedProtocolElements.indexOf(element.id) + 1;
                });

            this.set('protocolElements', updatedProtocolElements);
        },

        deleteMeasurementBranch: function (branchId) {
            var self = this,
                protocolElements = this.get('protocolElements');

            _.each(protocolElements, function (protocolElement) {
                if (protocolElement.element.type === 3) {
                    var branchToDelete = _.findWhere(protocolElement.branches, { id: branchId });

                    if (branchToDelete) {
                        if (branchToDelete.nextProtocolElementId != null &&
                            branchToDelete.nextProtocolElementId !== protocolElement.nextProtocolElementId) {

                            self.deleteProtocolElement(
                                branchToDelete.nextProtocolElementId,
                                protocolElement.nextProtocolElementId);
                        }

                        protocolElement.branches = _.filter(protocolElement.branches, function (branchEl) {
                            return branchEl.id !== branchId;
                        });
                    }
                }
            });
        },

        getBranchElementsIds: function (rootElementId, nextProtocolElementId) {
            var self = this,
                branchElementsIds = [],
                protocolElements = this.get('protocolElements'),
                currentElement = _.findWhere(protocolElements, { id: rootElementId }),
                currentNextProtocolElementId = currentElement.nextProtocolElementId;

            branchElementsIds.push(rootElementId);

            if (currentElement.branches.length) {
                _.each(currentElement.branches, function (branchElement) {
                    var branchNextProtocolElementId = branchElement.nextProtocolElementId;

                    if (branchNextProtocolElementId !== nextProtocolElementId && branchNextProtocolElementId != null) {
                        branchElementsIds = branchElementsIds.concat(
                            self.getBranchElementsIds(branchNextProtocolElementId, nextProtocolElementId));
                    }
                });
            }

            if (currentNextProtocolElementId != null &&
                currentNextProtocolElementId !== nextProtocolElementId) {
                branchElementsIds = branchElementsIds.concat(
                    self.getBranchElementsIds(currentNextProtocolElementId, nextProtocolElementId));
            }

            return branchElementsIds;
        }
    });
});