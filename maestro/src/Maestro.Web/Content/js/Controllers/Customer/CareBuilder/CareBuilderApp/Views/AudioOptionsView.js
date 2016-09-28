'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Constants',
    'Controllers/Helpers',
    'Controllers/Customer/CareBuilder/CareBuilderApp/AppGlobalVariables',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Models/UploadFileModel'
], function ($, _, Backbone, Constants, Helpers, app, UploadFileModel) {
    return Backbone.View.extend({
        template: _.template($("#audioOptionsTemplate").html()),

        className: 'panel panel-default',

        events: {
            'click .js-audio-options-header': 'onOpenAudioOptions',
            'change .js-upload-audio-file': 'onChangeAudioFile',
            'change .js-pronunciation': 'onChangePronunciation',
            'click .js-remove-audio': 'onClickRemoveAudio',
            'click .js-play-audio': 'onPlayAudio'
        },

        initialize: function (options) {
            this.model.index = options.index ? options.index : 1;
        },

        render: function () {
            // js object (not backbone model) used as model
            this.$el.html(this.template(this.model));

            if (this.model.audioFileMedia) {
                this.$el.find('.js-audio-name').val(this.model.audioFileMedia.originalFileName);
            }

            return this;
        },

        onOpenAudioOptions: function (ev) {
            var $target = $(ev.target);

            if ($target.hasClass('collapsed')) {
                $target.html($target.data('collapsetext'));
            } else {
                $target.html($target.data('expandtext'));
            }
        },

        onChangeAudioFile: function (ev) {
            if (!this.isAudioFileValid(ev.target)) {
                return false;
            }

            var $browseButton = this.$el.find('.btn-file-ci'),
                uploadFileModel = new UploadFileModel({ mediaFile: this.$el.find('.js-upload-audio-file')[0].files[0] }),
                self = this;

            $browseButton.button('loading');

            uploadFileModel.save(null, {
                success: function (e, result) {
                    $browseButton.button('reset');

                    result = Helpers.convertKeysToCamelCase(result);
                    self.model.audioFileMedia = result;

                    self.$el.find('.js-audio-name').val(result.fileName);
                    self.$el.find('.js-play-audio').prop('disabled', false);
                    self.$el.find('.panel-heading .js-play-audio').removeClass('hidden');
                    self.$el.find('.js-remove-audio').removeClass('hidden');
                    self.$el.trigger('fileUploaded', result);
                },
                error: function (e, result) {
                    $browseButton.button('reset');
                }
            });
        },

        onClickRemoveAudio: function (ev) {
            ev.preventDefault();

            if (this.isAudioPlaying()) {
                this.stopAudio();
            }

            this.$el.find('.js-remove-audio').addClass('hidden');
            this.$el.find('.js-audio-name').val('');
            this.$el.find('.js-play-audio').prop('disabled', true);
            this.$el.find('.panel-heading .js-play-audio').addClass('hidden');
            this.$el.find('.js-upload-audio-file').val('');
            this.$el.trigger('removeAudioFile');
        },

        onChangePronunciation: function (ev) {
            var $flag = this.$el.find('.js-pronunciation-flag'),
                $input = $(ev.target);

            if ($input.val() !== '') {
                $flag.removeClass('hidden');
            } else {
                $flag.addClass('hidden');
            }
        },

        onPlayAudio: function () {
            if (this.isAudioPlaying()) {
                this.stopAudio();
            } else {
                this.playAudio();
            }
        },

        playAudio: function () {
            var audioUrl = this.model.audioFileMedia.fileName ?
                window.location.origin + '/TEMP/' + this.model.audioFileMedia.fileName :
                this.model.audioFileMedia.mediaUrl;

            this.stopAudio();

            app.audio = new Audio(audioUrl);
            app.audio.play();

            this.$el.find('.js-play-audio span')
                    .removeClass('glyphicon-play')
                    .addClass('glyphicon-stop');
        },

        stopAudio: function () {
            if (app.audio) {
                app.audio.pause();

                $('.js-play-audio .glyphicon-stop')
                    .removeClass('glyphicon-stop')
                    .addClass('glyphicon-play');
            }
        },

        isAudioPlaying: function() {
            return this.$el.find('.js-play-audio span').hasClass('glyphicon-stop');
        },

        isAudioFileValid: function (input) {
            if (window.FileReader && window.File && window.FileList && window.Blob) {
                var fileSize = input.files[0].size / 1024 / 1024,
                    extension = $(input).val().split('.').pop().toLowerCase(),
                    fileNameLength = input.files[0].name.length;

                if ($.inArray(extension, ['mp3', 'wav']) == -1) {
                    $('.help-block-audio-fileName')
                        .text('Please download audio file mp3 or wav')
                        .removeClass('hidden');

                    return false;
                }

                if (fileSize > Constants.maxMediaSize) {
                    $('.help-block-audio-fileName')
                        .text('Size of the file must not exceed ' + Constants.maxMediaSize + ' MB')
                        .removeClass('hidden');

                    return false;
                }

                if (fileNameLength > Constants.maxMediaNameLength) {
                    $('.help-block-audio-fileName')
                        .text('Name of the file must not exceed ' + Constants.maxMediaNameLength + ' characters')
                        .removeClass('hidden');

                    return false;
                }

                $('.help-block-audio-fileName').addClass('hidden');
            }

            return true;
        }
    });
});