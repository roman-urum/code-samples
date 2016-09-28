'use strict';

define([
    'backbone',
    'BackboneBootstrapAlert'
], function(Backbone, BackboneBootstrapAlert) {
    return function(xhr) {
        new BackboneBootstrapAlert({
            alert: 'danger',
            title: 'Error: ',
            message: xhr.responseJSON.ErrorMessage,
            fixed: true
        })
            .show();
    }
});