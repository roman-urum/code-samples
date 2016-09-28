'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'backbone-model-file-upload',
    'Controllers/Helpers',
    'Controllers/Constants',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Models/MediaElementModel'
], function ($, _, Backbone, MackboneModelFileUpload, Helpers, Constants, MediaElementModel) {

    return Backbone.View.extend({

        initialize: function () {

        },

        events: {
            'change #upload-media-file': 'onChangeMediaFile',
            'click #uplaod-media-from-link': 'onClickUploadMediaFromLink',
            'click #remove-media-file-btn': 'onClickRemoveMediaFileBtn'
        },

        getFileElement: function () {
            return this.$el.find('#upload-media-file');
        },

        setMediaFileName: function (mediaFileName) {
            this.$el.find('#media-file-name').val(mediaFileName);
            this.$el.find('#media-file-name').trigger("change");
        },

        setContentType: function (contentType) {
            this.$el.find('#media-content-type').val(contentType);
            this.$el.find('#media-content-type').trigger("change");
        },

        onChangeMediaFile: function (ev) {
            var self = this,
                $input = $(ev.target),
                numFiles = $input.get(0).files ? $input.get(0).files.length : 1,
                label = $input.val().replace(/\\/g, '/').replace(/.*\//, '');

            if (window.FileReader && window.File && window.FileList && window.Blob) {
                var fileSize = ev.target.files[0].size / 1024 / 1024,
                    extension = $input.val().split('.').pop().toLowerCase(),
                    fileNameLength = ev.target.files[0].name.length;

                if ($.inArray(extension, ['mp4', 'm4a', 'webm', 'jpg', 'jpeg', 'png', 'pdf']) == -1) {
                    $('.help-block-media-fileName')
                        .text('Please download file in correct format: mp4, m4a, webm, jpg, jpeg, png, pdf')
                        .removeClass('hidden');

                    return false;
                } else if (fileSize > Constants.maxMediaSize) {
                    $('.help-block-media-fileName')
                        .text('Size of the file must not exceed ' + Constants.maxMediaSize + ' MB')
                        .removeClass('hidden');

                    return false;
                } else if (fileNameLength > Constants.maxMediaNameLength) {
                    $('.help-block-media-fileName')
                        .text('Name of the file must not exceed ' + Constants.maxMediaNameLength + ' characters')
                        .removeClass('hidden');

                    return false;
                } else {
                    $('.help-block-media-fileName').addClass('hidden');
                }
            }

            var browseButton = self.$el.find('.btn-file-ci');
            browseButton.button('loading');
            $('#input-upload-media-name')
                .closest('.has-error').removeClass('has-error')
                .find('.help-block').addClass('hidden');

            if (self.getFileElement()[0].files.length <= 0) {
                return;
            }

            var MediaFileModel = Backbone.Model.extend({ url: '/CareBuilder/UploadFile', fileAttribute: 'mediaFile' });
            var mediaFileModel = new MediaFileModel();

            mediaFileModel.set('mediaFile', self.getFileElement()[0].files[0]);
            mediaFileModel.save(null, {
                success: function (e, result) {
                    browseButton.button('reset');
                    $input.trigger('fileselect', [numFiles, label]);
                    self.onMediaFileUploadSuccessfully(result, self);
                },
                error: function (e, result) {
                    browseButton.button('reset');
                    self.onMediaFileUploadFailed(result, self);
                }
            });
        },

        onMediaFileUploadSuccessfully: function (result) {
            this.setMediaFileName(result.FileName);
            this.setContentType(result.ContentType);

            this.showFilePreview({ contentType: result.ContentType, mediaUrl: "/CareBuilder/DownloadFile?fileName=" + result.FileName });

            this.$el.find('#preview-media-file-name').text(result.FileName);

            this.trigger('onSuccessFileUpload', new MediaElementModel({
                fileName: result.FileName,
                contentType: result.ContentType
            }));
        },

        showFilePreview: function (previewData) {
            this.$el.find('#preview-media-container').removeClass('hidden');

            if (previewData.contentType.match("image")) {
                this.$el.find('#preview-media-file').html('<img src="' + previewData.mediaUrl + '" alt="" width="100" heigh="100" />');
            } else {
                this.$el.find('#preview-media-file').html('<span class="glyphicon glyphicon-file" />');
            }
        },

        getMediaUrl: function () {
            return this.$el.find('#link-to-media-file').val();
        },

        onMediaFileUploadFailed: function (result) {
            var $errorMessageBox = this.$el.find('.help-block-media-fileName');
            
            $errorMessageBox.html(result.responseJSON.ErrorMessage);
            $errorMessageBox.removeClass('hidden');
        },

        onClickUploadMediaFromLink: function (ev) {
            var self = this;

            self.$el.find(ev.target).button('loading');
            self.$el.find('#input-upload-media-name')
                .closest('.has-error').removeClass('has-error')
                .find('.help-block').addClass('hidden');

            var DownloadFileFromUrlModel = Backbone.Model.extend({
                url: '/CareBuilder/DownloadFileFromUrl'
            });

            var model = new DownloadFileFromUrlModel({ fileUrl: self.getMediaUrl() });

            model.save(null, {
                success: function (e, result) {
                    $(ev.target).button('reset');
                    $('#preview-media-container').removeClass('hidden');
                    self.onMediaFileUploadSuccessfully(result, self);
                },
                error: function (e, result) {
                    $(ev.target).button('reset');
                    self.onMediaFileUploadFailed(result, self);
                }
            });
        },

        onClickRemoveMediaFileBtn: function () {
            this.getFileElement().val('');
            this.$el.find('#link-to-media-file').val('');
            this.$el.find('#input-upload-media-name').val('');
            this.$el.find('#preview-media-image').attr('src', '');

            this.setMediaFileName('dummy file name');
            this.setMediaFileName('');

            $('#preview-media-container').addClass('hidden');

            this.trigger('onRemoveMedia');

            return false;
        },

        template: _.template($("#mediaFileTemplate").html()),

        render: function () {
            this.$el.html(this.template());

            Helpers.initTags(this.$el.find('.creation-tags'), null);

            this.$el.find('.input-group-file-ci :file').on('fileselect', function (event, numFiles, label) {
                var input = $(this).closest('.input-group-file-ci').find(':text'),
                    log = numFiles > 1 ? numFiles + ' files selected' : label;

                if (input.length) {
                    input.val(log);
                } else {
                    if (log) alert(log);
                }

            });

            if (this.model.get('mediaUrl') && this.model.get('contentType')) {
                this.showFilePreview(this.model.toJSON());
            }

            this.trigger('onRendered');

            return this;
        }
    });
});