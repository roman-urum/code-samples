(function ($) {
    jQuery(function () {
        var notifications = $("[data-val-notification]");

        $.each(notifications, function (index, notification) {
            var $notification = $(notification),
                message = $notification.attr('data-val-notification');

            if ($notification.hasClass('field-validation-valid')) {
                $notification.html(message);
            }
        });
    });
})(jQuery);