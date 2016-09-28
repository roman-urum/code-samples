$(function () {
    // Methods to check that control value == true
    jQuery.validator.addMethod("istrue", function (value, element, param) {
        return value == 'true';
    });

    jQuery.validator.unobtrusive.adapters.add('istrue', [], function (options) {
        options.rules['istrue'] = {
            terms: "required"
        };

        options.messages["istrue"] = options.message;
    });

}(jQuery));