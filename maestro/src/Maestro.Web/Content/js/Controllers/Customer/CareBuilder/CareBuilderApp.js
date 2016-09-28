'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Customer/CareBuilder/CareBuilderApp/AppGlobalVariables',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Router',
    'Controllers/SessionExpiration',
    'Controllers/Validation',
    'backbone.memento',
    'jquery-ui',
    'jquery-ui-touch',
    'session-timeout'
], function ($, _, Backbone, app, Router, SessionExpiration) {

    var initialize = function () {
        app.vent = _.extend({}, Backbone.Events);
        app.router = new Router();
        SessionExpiration.initialize();
    };

    return {
        initialize: initialize
    };
});