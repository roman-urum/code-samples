'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Models/MediaElementModel',
    'Controllers/Helpers',
    'Controllers/Constants',
    'backbone-nested'
], function ($, _, Backbone, MediaElementModel, Helpers, Constants) {
    return Backbone.NestedModel.extend({
        initialize: function () {
            // Memento initializing
            _.extend(this, new Backbone.Memento(this));
        },

        defaults: function() {
            return {
                name: null,
                text: {
                    value: "",
                    pronunciation: "",
                    audioFileMedia: null
                },
                media: null,
                imageName: null,
                mediaUrl: null,
                thumbnailUrl: null,
                tags: []
            }
        },

        validation: {
            'name': [{
                required: true,
                msg: 'Please enter name'
            }, {
                maxLength: 50,
                msg: 'TextMedia element name shouldn\'t exceed 50 letters'
            }],
            'text.value': function (value) {
                var isMedia = this.get('media') != undefined && (this.get('media').get('name') !== "" ||
                                                                 this.get('media').get('fileName') !== "" ||
                                                                 this.get('media').get('contentType') !== "");
                if (!isMedia && !value) {
                    return 'Please enter text';
                }

                if (value != null && value.length > 500) {
                    return 'TextMedia element text shouldn\'t exceed 500 letters';
                }
            },

            'text.audioFileMedia.fileName': function(value) {

                if (value != null && value.length > Constants.maxMediaNameLength) {
                    return 'The lenght of the file name must be less or equal than 100';
                }

            },

            'media.name': function () {

                if (!this.get('media')) {
                    return;
                }

                if (this.get('media').get('id')) {
                    return;
                }

                if (!this.get('media').get('name')) {
                    return 'Please enter media name';
                }
            },
            'media.fileName': function () {

                if (!this.get('media')) {
                    return;
                }

                if (this.get('media').get('id')) {
                    return;
                }

                if (!this.get('media').get('fileName')) {
                    return 'Please enter media file';
                }

                if (this.get('media').get('fileName').length > Constants.maxMediaNameLength) {
                    return 'The lenght of the file name must be less or equal than 100';
                }
            },
            'tags': function (tags) {
                if (!Helpers.isTagsValid(tags)) {
                    return 'Incorrect tag value. Tags can consist of alphanumeric symbols, dot, dash, underscore.';
                }

                if (Helpers.hasDuplicates(tags)) {
                    return globalStrings.DuplicatedTags_ErrorMessage;
                }
            }
        },

        url: function () {
            return '/CareBuilder/TextMediaElement';
        },


        parse: function(response, options) {
            var result = Helpers.convertKeysToCamelCase(response);

            if (result != null && typeof response != "string") {
                if (result.media != null) {
                    result.media = new MediaElementModel(result.media);
                }

                if (!result.text) {
                    result.text = {
                        value: "",
                        pronunciation: "",
                        audioFileMedia: null
                    }
                }
            }

            return result;
        }
    });
});