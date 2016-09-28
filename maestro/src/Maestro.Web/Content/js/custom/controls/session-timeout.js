var sessionTimer = (function ($) {
    var $sessionTimeoutContainer = $("#session-timeout"),
        timeout = parseInt($('#session-timeout').val()),
        $modal = $("#session-timeout-modal"),
        minuteTimeout = 60000,
        backendTimeAccuracy = 15000, // this was added because of difference between the time when session started on back-end and on front-end, usually on backend session starts a bit earlier, it depends of how fast the http request is processed on back-end, the faster the smaller the difference
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

            var interval = timeout - minuteTimeout - backendTimeAccuracy;

            var nextCheck = new Date();
            nextCheck.setMilliseconds(interval);
            console.log('check session timeout every ', (interval / 1000) / 60, ' minutes, next check aprox at ', nextCheck.toString());

            var checkInterval = setInterval(function () {

                var idleMilliseconds = new Date().getTime() - lastUpdate;

                console.log('start checking session timout -----------------');
                console.log('idle time = ', (idleMilliseconds / 1000) / 60, ' minutes');
                console.log('timeout = ', ((timeout - minuteTimeout) / 1000) / 60, ' minutes');

                if (idleMilliseconds > timeout - minuteTimeout) {

                    console.log('session is about to be expired');

                    isAnotherModalOpened = $('body').hasClass('modal-open');

                    $modal.modal({
                        keyboard: false,
                        backdrop: 'static'
                    });

                    setTimeout(function () {
                        if (!resumed) {
                            console.log('clear checking interval');
                            clearInterval(checkInterval);

                            stopSession();
                        } else {
                            resumed = false;
                            resumeSession();
                        }
                    }, minuteTimeout);
                } else {

                    console.log('session is not expired so resume session');

                    resumeSession();
                }

                console.log('end checking session timout -----------------');
            }, interval);

            $(document).on('keydown mousedown', function () {
                lastUpdate = new Date().getTime();
            });

            $modal.find("#btn-resume-session").click(function () {
                resumed = true;
            });
        }

    // Handling cases when timeout modal opened above another modal
    $modal.on('hidden.bs.modal', function(e) {
        if (isAnotherModalOpened) {
            $('body').addClass('modal-open');
        }
    });
        
    return {

        Initialize: function () {
            if (initialized || !$sessionTimeoutContainer.length) {
                return;
            }

            initialized = true;
            keepSessionAliveOnActivity();
        }
    };
}(jQuery));

(function() {
    sessionTimer.Initialize();
})();