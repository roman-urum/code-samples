$.validator.setDefaults({
    ignore: []
});

$(function () {
    // Methods to check file extensions
    jQuery.validator.addMethod("fileextensions", function (value, element, param) {
        if (value == '') {
            return true;
        }

        var extension = getFileExtension(value);
        var validExtension = $.inArray(extension, param.fileextensions) !== -1;

        return validExtension;
    });

    jQuery.validator.unobtrusive.adapters.add('fileextensions', ['fileextensions'], function (options) {
        options.rules['fileextensions'] = {
            fileextensions: options.params.fileextensions.split(',')
        };;

        options.messages["fileextensions"] = options.message;
    });

    function getFileExtension(fileName) {
        var extension = (/[.]/.exec(fileName)) ? /[^.]+$/.exec(fileName) : undefined;

        if (extension != undefined) {
            return extension[0];
        }

        return extension;
    };

}(jQuery));