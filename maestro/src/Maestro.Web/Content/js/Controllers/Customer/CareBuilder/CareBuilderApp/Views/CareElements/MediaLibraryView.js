'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Models/MediaElementModel',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Collections/SearchMediaCollection',
    'Controllers/Helpers'
], function ($, _, Backbone, MediaElementModel, SearchMediaCollection, Helpers) {

    return Backbone.View.extend({
        searchCollection: undefined,

        initialize: function () {
            this.searchCollection = new SearchMediaCollection();
        },

        template: _.template($("#mediaLibraryTemplate").html()),
        mediaItemTemplate: _.template($("#mediaListItemTemplate").html()),

        render: function () {

            this.$el.html(this.template());

            this.trigger('onRendered');

            Helpers.initTags(null, this.$el.find('.searching-tags'));

            this.searchMedia();

            return this;
        },

        events: {
            "change #media-search-type": "searchMedia",
            "change #media-search-tags": "searchMedia",
            "change #media-search-keyword": "searchMedia",
            'click .js-search-clear': 'searchClear',
            "click .media-item": "onClickMediaItem"
        },

        searchMedia: function () {
            var self = this;
            Helpers.renderSpinner(self.$el.find('.media-search-result'));

            var newSearchCollection = new SearchMediaCollection(),
                searchTags = self.$el.find('#media-search-tags').val();

            searchTags = (searchTags && searchTags !== "0") ? searchTags : [];

            if (searchTags.length) {
                this.$el.find('.js-search-clear').prop('disabled', false);
            } else {
                this.$el.find('.js-search-clear').prop('disabled', true);
            }

            newSearchCollection.fetch({
                traditional: true,
                data: {
                    types: self.$el.find('#media-search-type').val(),
                    q: self.$el.find('#media-search-keyword').val(),
                    tags: searchTags
                },
                success: function () {
                    self.searchCollection = newSearchCollection;
                    self.displaySearchCollection();
                }
            });
        },

        displaySearchCollection: function () {

            var self = this;
            self.$el.find('.media-search-result').html('');
            _.each(self.searchCollection.models, function (model) {

                var clonedModel = model.clone();
                _.extend(clonedModel.attributes, { mimeTypes: Helpers.mimeTypes() });

                self.$el.find('.media-search-result').append(self.mediaItemTemplate(clonedModel.attributes));

            });

        },

        searchClear: function () {
            this.$el.find('.searching-tags')
                .val(0)
                .trigger("chosen:updated")
                .change();
        },

        onClickMediaItem: function (e) {

            var target = $(e.target);
            if (!target.hasClass('media-item')) {
                target = target.closest('.media-item');
            }

            var foundMedia = this.searchCollection.find(function (model) {
                return model.get('id') == target.attr('media-id');
            });

            if (foundMedia) {
                this.trigger('onSelectMedia', foundMedia);
            }
        }

    });
});