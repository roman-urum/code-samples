'use strict';

define(['jquery'], function ($) {
    return {
        models: {},
        views: {},
        collections: {},
        types: {
            HEALTHSESSIONELEMENT: 'HEALTHSESSIONELEMENT',
            VITAL: 'VITAL',
            MEASUREMENT: 'MEASUREMENT',
            BEHAVIOUR: 'BEHAVIOUR'
        },
        extendedTypes: function () {
            return $.extend({}, this.types);
        },
        link: window.location.protocol + '//' + window.location.host + '/' + window.location.pathname.split('/')[1]
    };
});