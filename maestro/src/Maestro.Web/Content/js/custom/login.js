Maestro.namespace("Maestro.pages");

Maestro.pages.Login = Backbone.View.extend({
    el: '.ci-content',

    events: {
        "keyup input": "hideCredentialsError"
    },

    hideCredentialsError: function () {
        this.$el.find('[data-valmsg-for="IncorrectCredentials"]').hide();
    }
});

(function () {
    var pageView = new Maestro.pages.Login();
})();