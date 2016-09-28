Maestro.namespace("Maestro.pages");

Maestro.pages.AdminUsers = Backbone.View.extend({
    el: '#admin-users',

    initialize: function () {
        this.$el.find('.basic-checkbox').on('switchChange.bootstrapSwitch', this.onUserSwitch);
    },

    onUserSwitch: function(event, state) {
        var userId = $(this).parents('.row').attr("user-id");

        $.post("SetEnabledState", {
            userId: userId,
            isEnabled: state
        });
    }
});

(function() {
    var adminUsersView = new Maestro.pages.AdminUsers();
})();