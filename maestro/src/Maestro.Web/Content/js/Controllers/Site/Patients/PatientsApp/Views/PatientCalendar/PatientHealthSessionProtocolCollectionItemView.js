'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Site/Patients/PatientsApp/AppNamespace'
], function ($, _, Backbone, app) {
    return Backbone.View.extend({

        tagName: 'li',

        className: 'session-protocols-list-item-ci session-protocols-list-item-selected-ci clearfix',

        template: _.template('<a class="btn-add js-remove-protocol">\
                                    <i class="fa fa-times-circle"></i>\
                                </a>\
                                <a class="session-protocol-handle js-session-protocol-handle">\
                                    <i class="fa fa-arrows-v"></i>\
                                </a>\
                                <p class="session-protocol-text" title="<%=name%>"><%=name%></p>\
                                <% if( tags.length ) { %>\
                                    <div class="session-protocol-tags">\
                                        <% _.each(tags, function(tag) { %>\
                                        <span class="label label-info"><%= tag %></span>\
                                        <% }); %>\
                                    </div>\
                                <% } %>\
        '),

        events: {
            'click .js-remove-protocol': 'removeProtocol'
        },

        initialize: function () {

        },

        render: function () {

            this.$el.html(this.template(this.model.attributes));
            return this;
        },

        removeProtocol: function() {

            var protocolCollection = app.models.patientHealthSessionModel.get('protocols'),
                id = this.model.get('id'),
                model = app.collections.patientProtocolSearchCollection.find(function(model) { return model.get('id') === id; });
            
            model.set('isAdded',false);

            protocolCollection.remove( this.model );

        }

    });
});