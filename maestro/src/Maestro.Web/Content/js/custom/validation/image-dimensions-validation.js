$(function () {
    // Methods to check file extensions
    jQuery.validator.addMethod("imagedimensions", function (value, element, param) {
        var $element = $(element),
            height = $element.attr('imageheight'),
            width = $element.attr('imagewidth');

        if (value == '') {
            return true;
        }

        if (!element.files || !element.files[0]) {
            return true;
        }
        
        return parseInt(height, 10) <= param.maxheight && parseInt(width, 10) <= param.maxwidth;
    });

    jQuery.validator.unobtrusive.adapters.add('imagedimensions', ['maxheight', 'maxwidth'], function (options) {
        options.rules['imagedimensions'] = {
            maxheight: options.params.maxheight,
            maxwidth: options.params.maxwidth
        };

        options.messages["imagedimensions"] = options.message;
    });

}(jQuery));