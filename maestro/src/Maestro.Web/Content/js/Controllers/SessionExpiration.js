'use strict';

define([
    'jquery'
], function ($) {
    var $sessionTimeoutContainer = $("#session-timeout"),
        timeout = parseInt($('#session-timeout').val()),
        $modal = $("#session-timeout-modal"),
        minuteTimeout = 60000,
        resumed = false,
        initialized = false,
        isAnotherModalOpened = false,

        resumeSession = function () {
            $.post(
                '/Account/ResumeSession',
                null,
                function (response) {
                    if (!response.success) {
                        window.location.reload();
                    }
                });
        },

        stopSession = function () {
            $.ajax({
                type: 'POST',
                async: false,
                url: '/Account/StopSession',
                success: function () {
                    window.location.reload();
                }
            });
        },

        keepSessionAliveOnActivity = function () {
            var lastUpdate = 0;

            var checkInterval = setInterval(function () {
                if (new Date().getTime() - lastUpdate > timeout - minuteTimeout) {
                    isAnotherModalOpened = $('body').hasClass('modal-open');

                    $modal.modal({
                        keyboard: false,
                        backdrop: 'static'
                    });

                    setTimeout(function () {
                        if (!resumed) {
                            clearInterval(checkInterval);

                            stopSession();
                        } else {
                            resumed = false;
                            resumeSession();
                        }
                    }, minuteTimeout);
                } else {
                    resumeSession();
                }
            }, timeout - minuteTimeout);

            $(document).on('keydown mousedown', function () {
                lastUpdate = new Date().getTime();
            });

            $modal.find("#btn-resume-session").click(function () {
                resumed = true;
            });
        }

    // Handling cases when timeout modal opened above another modal
    $modal.on('hidden.bs.modal', function (e) {
        if (isAnotherModalOpened) {
            $('body').addClass('modal-open');
        }
    });

    return {
        initialize: function () {
            if (initialized || !$sessionTimeoutContainer.length) {
                return;
            }

            initialized = true;
            keepSessionAliveOnActivity();
        }
    };
});