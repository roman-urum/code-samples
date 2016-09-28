'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Constants',
    'Controllers/Helpers',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Models/ProtocolsAndPrograms/ProtocolModel',
    'backbone-nested'
], function (
    $,
    _,
    Backbone,
    Constants,
    Helpers,
    ProtocolModel
) {
    return ProtocolModel.extend({
        defaults: {
            firstProtocolElementId: '',
            protocolElements: [],
            isPrivate: false,
            tags: []
        },

        initialize: function () {
        },

        url: function () {
            return '/CareBuilder/Protocol';
        },

        validation: {
            name: [
                {
                    required: true,
                    msg: '"Name of Protocol" is required'
                }, {
                    maxLength: 50,
                    msg: '"Name of Protocol" shouldn\'t exceed 50 symbols'
                }
            ],
            tags: function(tags) {
                if (!Helpers.isTagsValid(tags)) {
                    return 'Incorrect tag value. Tags can consist of alphanumeric symbols, dot, dash, underscore.';
                }

                if (Helpers.hasDuplicates(tags)) {
                    return globalStrings.DuplicatedTags_ErrorMessage;
                }
            }
        }
    });
});