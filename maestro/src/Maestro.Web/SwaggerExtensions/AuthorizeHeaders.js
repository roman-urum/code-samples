$('input[name=apiKey]').change(function () {
    var key = $('input[name=apiKey]')[0].value;
    if (key && key.trim() != '') {
        key = "Bearer " + key;
        swaggerUi.api.clientAuthorizations.add("key", new SwaggerClient.ApiKeyAuthorization("Authorization", key, "header"));
    }
});
