'use strict';

define([
    'jquery',
    'underscore',
    'backbone.marionette',
    'BackboneBootstrapModal',
    'Controllers/Customer/Settings/CustomerSettingsApp/Application',
], function (
    $,
    _,
    Marionette,
    BackboneBootstrapModal,
    App,
    BackboneModelBinder,
    BaseItemView,
    SiteEditView
) {
    return Backbone.View.extend({
        template: '<a href="/<%= id %>/Dashboard" <%= isActive ? "" : "class=disabled disabled" %>><%= name %></a>',

        render: function() {
            var template = _.template(this.template);
            this.$el.html(template(this.model.attributes));
            return this;
        }
    });
});