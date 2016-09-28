'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'BackboneModelBinder',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Models/MediaElementModel',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Models/SearchModel',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Views/CareElements/MediaFileView',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Views/CareElements/MediaLibraryView'

], function ($, _, Backbone, BackboneModelBinder, MediaElementModel, SearchModel, MediaFileView, MediaLibraryView) {

    return Backbone.View.extend({
        modelBinder: undefined,

        initialize: function () {
            this.modelBinder = new BackboneModelBinder();
        },

        template: _.template($("#mediaElementFormTemplate").html()),

        render: function () {

            this.$el.html(this.template());
            
            this.renderNewMediaView();
            this.$el.find('a[href="#edit-existing-media"]').tab('show');

            return this;
        },

        events: {
            'show.bs.tab a[href="#edit-existing-media"]': 'renderMediaLibraryView'
        },

        renderNewMediaView: function () {
            var self = this;

            this.mediaFileView = new MediaFileView({
                el: self.$el.find("#media-file-container"),
                model: this.model
            });

            self.mediaFileView.on('onRendered', function () {
                self.trigger('onMediaFileViewRendered');
            });

            self.mediaFileView.on('onErrorFileUpload', function (errorText) {
                self.trigger('onErrorFileUpload', errorText);
            });

            self.mediaFileView.on('onSuccessFileUpload', function (mediaElement) {
                self.trigger('onSuccessFileUpload', mediaElement);
            });

            self.mediaFileView.on('onRemoveMedia', function () {
                self.trigger('onRemoveMedia');
            });

            this.mediaFileView.render();
        },

        renderMediaLibraryView: function (e) {
            var self = this;

            var mediaLibraryView = new MediaLibraryView({
                el: self.$el.find(e.target.hash)
            });

            mediaLibraryView.on('onSelectMedia', function (selectedMedia) {
                self.trigger('onSelectExistingMedia', selectedMedia);
            });

            mediaLibraryView.on('onRendered', function () {
                self.trigger('onMediaLibraryViewRendered');
            });

            mediaLibraryView.render();


        }
    });
});