Maestro.namespace("Maestro.pages");

// View for page to create/edit customer user
Maestro.pages.EditCustomerUser = Backbone.View.extend({
    el: "#ci-edit-customer-user",

    initialize: function () {
        var self = this;

        this.saveFormWrap = Maestro.controls.SaveFormWrap.Initialize(this.el);
        this.cancelModal = new Maestro.controls.CancelModal();
        this.$form = this.$el.find('form');

        this.cancelModal.on("confirm", function () {
            self.saveFormWrap.CancelChanges();
        });

        this.showPermissionsForRole(this.$el.find('#CustomerUserRoleId').val());
    },

    events: {
        "change select": "allowSaving",
        "click .cancel-form": "cancelForm",
        "change #CustomerUserRoleId": "onCustomerUserRoleChanged",
        'click #resend-invite': 'onClickResendInvite',
        'click #reset-password': 'onClickResetPassword'
    },

    cancelForm: function () {
        console.log("cancel");
    },

    onCustomerUserRoleChanged: function (event) {
        var selectedRoleId = $(event.target).val();

        this.showPermissionsForRole(selectedRoleId);
    },

    showPermissionsForRole: function (roleId) {
        this.$el.find(".role-permissions").addClass("hidden");
        this.$el.find("#role-" + roleId + "-permissions").removeClass("hidden");
    },

    onClickResendInvite: function (e) {
        e.preventDefault();

        $.post("/Settings/ResendInvite", {
            email: $("#Email").val()
        }, function (data) {
            if (data.success) {
                $(".json-message").text(data.message);
                $(".json-message").show(0);
                $(".json-message").delay(4000).fadeOut('slow');
            }
        });
    },
    onClickResetPassword: function (e) {
        e.preventDefault();

        $.post("/Settings/SendResetPasswordEmail", {
            id: $("#Id").val()
        }, function (data) {
            if (data.success) {
                $(".json-message").text(data.message);
                $(".json-message").show(0);
                $(".json-message").delay(4000).fadeOut('slow');
            }
        }).fail(function (data) {
            var errorMessage = JSON.parse(data.responseText).ErrorMessage;
            if (errorMessage) {
                $(".json-message").text(errorMessage);
                $(".json-message").show(0);
                $(".json-message").delay(4000).fadeOut('slow');
            }
        });
    },
});

(function () {
    var pageView = new Maestro.pages.EditCustomerUser();
})();