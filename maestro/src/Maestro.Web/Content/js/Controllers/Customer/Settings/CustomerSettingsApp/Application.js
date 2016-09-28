'use strict';

define([
    'backbone',
    'backbone.marionette'
],
function (
    Backbone,
    Marionette
) {
    var App = new Marionette.Application();

    App.views = {};
    App.models = {};
    App.collections = {};
    App.config = {
        name: 'Veteran\'s Administration'
    };

    App.vent = _.extend({}, Backbone.Events);

    /* Add application regions here */
    App.addRegions({
        main: '.customer-settings-container'
    });

    /* Add initializers here */
    App.addInitializer(function () {
        $(document).on("click", "a.js-link", function (event) {
            var el = $(event.currentTarget);
            var href = el.hasClass('js-tab-link') ? el.data('href') : el.attr('href');

            if (!event.altKey && !event.ctrlKey && !event.metaKey && !event.shiftKey) {
                event.preventDefault();
                //var url = href.replace(/^\//,'').replace('\#\!\/','');
                App.navigate(href);

                return false;
            }
        });

        // Site Switches


        // Tooltips
        $('body').tooltip({ selector: '[data-toggle=tooltip]', html: true });
    });

    App.navigate = function (path) {
        App.router.navigate(path, {trigger: true});
    };

    return App;
});
