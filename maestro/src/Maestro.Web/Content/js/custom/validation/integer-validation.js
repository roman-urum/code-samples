$(function () {
    // Methods to check that control value is integer
    jQuery.validator.addMethod("integer", function (value, element, param) {
        return Math.floor(value) == value && $.isNumeric(value);
    });

    jQuery.validator.unobtrusive.adapters.add('integer', [], function (options) {
        options.rules['integer'] = {
            terms: "required"
        };

        options.messages["integer"] = options.message;
        options.messages["number"] = options.message;
    });

}(jQuery));