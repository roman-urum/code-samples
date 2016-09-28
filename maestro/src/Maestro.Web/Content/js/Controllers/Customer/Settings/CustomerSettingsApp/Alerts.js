'use strict';

define([
    'jquery',
    'underscore',
    'BackboneBootstrapAlert'
], function (
    $,
    _,
    BackboneBootstrapAlert
) {

    function show(options) {
        var alertMessage = new BackboneBootstrapAlert({
            alert: options.level,
            message: options.message,
            autoClose: (typeof options.autoClose === 'undefined') ? true : options.autoClose
        }).show();
    }

    function success(message) {
        show({level: 'success', message: message});
    }

    function info(message) {
        show({level: 'info', message: message});
    }


    function warning(message) {
        show({level: 'warning', message: message});
    }


    function danger(message) {
        show({level: 'danger', message: message, autoClose: false});
    }

    return {
        show: show,
        success: success,
        info: info,
        warning: warning,
        danger: danger
    };
});