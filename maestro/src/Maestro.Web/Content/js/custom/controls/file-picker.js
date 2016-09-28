Maestro.namespace("Maestro.controls");

// Initializes events for file picker control generated on server side.
Maestro.controls.FilePicker = (function ($) {
    var filePicker = function (container) {
        var thiz = this;

        this.$container = $(container);
        this.$fileUploader = this.$container.find('input[type="file"]');
        this.$filePath = this.$container.find('input[type="text"]');
        this.$button = this.$container.find('button');
        this.$preview = $(this.$fileUploader.attr('preview'));
        this.$currValue = $(this.$fileUploader.attr('currentValue'));
        this.$removeLink = $(this.$fileUploader.attr('removelink'));
        this.defaultFilePath = this.$fileUploader.attr('defaultValue');

        if (this.$currValue.val() == '') {
            this.$removeLink.hide();
        }

        // event handlers
        this.$button.click(function() {
            thiz.$fileUploader.click();
        });

        this.$removeLink.click(function () {
            thiz.$currValue.val('');
            thiz.$filePath.val('');
            thiz.$fileUploader.val('');
            thiz.$preview.attr('src', '');
            thiz.$removeLink.hide();

            if (thiz.defaultFilePath !== undefined) {
                thiz.$preview.attr('src', thiz.defaultFilePath);
                thiz.$currValue.val(thiz.defaultFilePath);
            }
        });

        this.$fileUploader.change(function() {
            var fileName = thiz.$fileUploader.val().split('\\').pop();

            thiz.$filePath.val(fileName);
            thiz.$removeLink.show();

            if (this.files && this.files[0]) {

                var reader = new FileReader();

                reader.onload = function (e) {
                    var image = new Image();

                    thiz.$preview.attr('src', e.target.result);
                    image.src = e.target.result;

                    image.onload = function() {
                        thiz.$fileUploader.attr("imageheight", this.height);
                        thiz.$fileUploader.attr("imagewidth", this.width);
                    };
                }

                reader.readAsDataURL(this.files[0]);
            }
        });
    }

    return filePicker;
}(jQuery));

(function ($) {
    jQuery(function () {

        var filePickers = $('.ci-file-picker');

        $.each(filePickers, function(index, filePicker) {
            var control = new Maestro.controls.FilePicker(filePicker);
        });

    });
})(jQuery);