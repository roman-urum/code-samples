'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Customer/CareBuilder/CareBuilderApp/AppGlobalVariables',
    'BackboneBootstrapModal',
    'BackboneModelBinder',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Models/TextMediaElementModel',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Views/CareElements/MediaElementFormView',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Views/AudioOptionsView',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Models/MediaElementModel',
    'Controllers/Helpers'
], function (
    $,
    _,
    Backbone,
    app,
    BackboneBootstrapModal,
    BackboneModelBinder,
    TextMediaElementModel,
    MediaElementFormView,
    AudioOptionsView,
    MediaElementModel,
    Helpers
) {
    return Backbone.View.extend({
        modelBinder: undefined,
        mediaModelBinder: undefined,

        initialize: function () {
            this.modelBinder = new BackboneModelBinder();
            this.mediaModelBinder = new BackboneModelBinder();

            Backbone.Validation.bind(this);
        },

        template: _.template($("#textMediaElementTemplate").html()),
        mediaButtonsTemplate: _.template($('#textMediaElementMediaButtonsTemplate').html()),
        mediaPreviewTemplate: _.template($('#textMediaElementMediaPreviewTemplate').html()),

        events: {
            'click .js-add-media-btn': 'onClickAddMediaBtn',
            'click .js-replace-media-btn': 'onClickReplaceMediaBtn',
            'click .js-remove-media-btn': 'onClickRemoveMediaBtn',
            'fileUploaded': 'onFileUploaded',
            'removeAudioFile': 'onRemoveAudioFile'
        },

        close: function () {
            this.audioOptionsView.stopAudio();
            this.$el.find('.creation-tags').tokenfield('destroy');
        },

        render: function () {
            var bindings = {
                'name': '[data-name="name"]',
                'text.value': '[data-name="text.value"]',
                'text.pronunciation': '[data-name="pronunciation"]',
                'tags': '[data-name="tags"]'
            };

            this.audioOptionsView = new AudioOptionsView({ model: this.model.get('text') });
            this.$el.html(this.template(this.model.attributes));
            this.$el.find('#audio-options').html(this.audioOptionsView.render().$el);
            this.renderMediaButtons();
            this.renderMediaPreview();
            this.modelBinder.bind(this.model, this.el, bindings);

            return this;
        },

        renderMediaButtons: function () {
            this.$el.find('.media-buttons').html(this.mediaButtonsTemplate(this.model.attributes));
        },

        renderMediaPreview: function () {

            if (this.model.get('media')) {
                this.$el.find('.media-preview').html(this.mediaPreviewTemplate(this.model.get('media').attributes));
                this.$el.find('.media-preview').removeClass('hidden');
            } else {
                this.$el.find('.media-preview').html('');
                this.$el.find('.media-preview').addClass('hidden');
            }
        },

        getModalView: function (options) {
            var self = this,
                modalViewOptions = {
                    content: self,
                    title: 'Add Text And Media',
                    modalOptions: {
                        backdrop: 'static',
                        keyboard: false
                    },
                    okText: 'Save'
                };
            modalViewOptions = $.extend(modalViewOptions, options);

            var modalView = new BackboneBootstrapModal(modalViewOptions);

            modalView.on('ok', function () {
                modalView.preventClose();
                console.log(self.model.toJSON());
                // Updating model with Tags
                var newTextAndMediaElementTags = modalView.$el.find('#text-and-media-element-tags').tokenfield('getTokensList').split(', ');
                newTextAndMediaElementTags = newTextAndMediaElementTags.filter(function (element, index) {
                    return element !== '';
                });

                self.model.set('tags', newTextAndMediaElementTags);

                var newMediaTags = modalView.$el.find('#media-content-tags').length ? modalView.$el.find('#media-content-tags').tokenfield('getTokensList').split(', ') : [];
                newMediaTags = newMediaTags.filter(function (element, index) {
                    return element !== '';
                });

                var mediaModel = self.model.get('media');

                if (mediaModel) {
                    mediaModel.set('tags', newMediaTags);
                    self.model.set('media', mediaModel);
                }

                if (!self.model.isValid(true)) {
                    return;
                }

                var buttonOk = this.$el.find('.btn.ok');
                buttonOk.attr('data-loading-text', 'Saving...');
                buttonOk.button('loading');

                self.model.save(null, {
                    success: function (model, response, options) {

                        var sucessMessageEl = model.isNew() ?
                            modalView.$el.find('.js-alert-create-success') :
                            modalView.$el.find('.js-alert-update-success');

                        modalView.$el.find('.alert-danger').addClass('hidden');
                        sucessMessageEl.removeClass('hidden');

                        setTimeout(function () {
                            buttonOk.button('reset');
                            modalView.close();
                            sucessMessageEl.addClass('hidden');

                            app.vent.trigger("reRenderCareElements", {careElementType: "4"});

                        }, 2000);

                        self.trigger("OnTextMediaElementSaved", model.isNew());
                    },
                    error: function (model, xhr, options) {
                        buttonOk.button('reset');
                        var errorFrame = modalView.$el.find('.alert-danger');
                        errorFrame.html(xhr.responseJSON.ErrorMessage);
                        errorFrame.removeClass('hidden');
                    }
                });

            });

            modalView.on('shown', function () {
                Helpers.initTags($('.creation-tags'), null);
            });

            return modalView;
        },

        getMediaElementFormContainer: function () {
            return this.$el.find('#js-media-form-container');
        },

        onFileUploaded: function (event, audioFileModel) {
            this.model.set('text.audioFileMedia', audioFileModel);
        },

        onRemoveAudioFile: function () {
            this.model.set('text.audioFileMedia', null);
        },

        onClickAddMediaBtn: function (e) {

            this.model.set('media', new MediaElementModel());
            this.renderMediaButtons();
            this.renderMediaForm();
        },

        onClickReplaceMediaBtn: function () {

            this.model.set('media', new MediaElementModel());
            this.renderMediaForm();
        },

        onClickRemoveMediaBtn: function (e) {

            this.model.set('media', null);
            this.getMediaElementFormContainer().html('');
            this.renderMediaButtons();
            this.renderMediaPreview();
        },

        renderMediaForm: function () {
            var self = this;

            //self.model.set('media', mediaModel);
            console.log('renderMediaForm');
            var mediaElementFormView = new MediaElementFormView({
                el: self.getMediaElementFormContainer(),
                model: new MediaElementModel()
            });

            mediaElementFormView.on('onSelectExistingMedia', function (media) {

                if (media) {
                    self.model.set('media', media);

                    self.getMediaElementFormContainer().html('');
                    self.renderMediaButtons();
                    self.renderMediaPreview();

                } else {
                    throw new Error("Selected media is null");
                }
            });

            mediaElementFormView.on('onMediaFileViewRendered', function () {
                self.model.set('media', new MediaElementModel());

                self.applyMediaBinding();
            });

            mediaElementFormView.on('onMediaLibraryViewRendered', function () {
                self.model.set('media', null);
            });

            mediaElementFormView.on('onErrorFileUpload', function (errorText) {

                var errorFrame = self.$el.find('.alert-danger');
                errorFrame.html(errorText);
                errorFrame.removeClass('hidden');
            });

            mediaElementFormView.on('onSuccessFileUpload', function (mediaElement) {

                self.model.set('media', mediaElement);
                self.applyMediaBinding();

                var errorFrame = self.$el.find('.alert-danger');
                errorFrame.html("");
                errorFrame.addClass('hidden');
            });

            mediaElementFormView.on('onRemoveMedia', function () {
                self.model.set('media', null);
            });

            mediaElementFormView.render();

            $('#careElement-textelementText')
                .closest('.has-error').removeClass('has-error')
                .find('.help-block').addClass('hidden');
        },

        applyMediaBinding: function () {
            var mediaBindings = {
                'name': '[name="media.name"]',
                'fileName': '[name="media.fileName"]',
                'contentType': '[name="media.contentType"]'
            };

            this.mediaModelBinder.bind(this.model.get('media'), this.el, mediaBindings);
        }
    });
});