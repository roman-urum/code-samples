$(function () {
    // Methods to check file extensions
    jQuery.validator.addMethod("filesize", function (value, element, param) {
        if (value == '') {
            return true;
        }

        if (element.files && element.files[0]) {
            return element.files[0].size <= param.maxsize;
        }

        return true;
    });

    jQuery.validator.unobtrusive.adapters.add('filesize', ['maxsize'], function (options) {
        options.rules['filesize'] = {
            maxsize: options.params.maxsize
        };

        options.messages["filesize"] = options.message;
    });

}(jQuery));