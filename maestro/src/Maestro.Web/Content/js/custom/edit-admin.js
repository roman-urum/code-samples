Maestro.namespace("Maestro.pages.editadmin");

Maestro.pages.editadmin = Backbone.View.extend({
    el: '#edit-admin-user',

    events: {
        'click #cancel-edit-admin': 'onCancelEditAdmin',
        'click #resend-invite': 'onClickResendInvite',
        'click #reset-password': 'onClickResetPassword'
    },

    onCancelEditAdmin: function (event) {
        location.href = $(event.target).attr('href');
    },
    onClickResendInvite: function (event) {
        $.post("/Users/ResendInvite", {
            email: $("#Email").val()
        }, function (data) {
            if (data.success) {
                $(".json-message").text(data.message);
                $(".json-message").show(0);
                $(".json-message").delay(4000).fadeOut('slow');
            }
        });
    },
    onClickResetPassword: function (event) {
        $.post("/Users/SendResetPasswordEmail", {
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
    }
});
(function () {
    var editAdminView = new Maestro.pages.editadmin();
})()