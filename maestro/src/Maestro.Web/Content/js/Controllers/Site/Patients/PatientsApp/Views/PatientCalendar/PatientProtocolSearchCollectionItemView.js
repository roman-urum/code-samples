'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Site/Patients/PatientsApp/AppNamespace'
], function ($, _, Backbone, app) {
    return Backbone.View.extend({

        tagName: 'li',

        className: 'session-protocols-list-item-ci clearfix',

        template: _.template('<% if( isAdded ){%>\
                                    <a class="btn-add js-remove-protocol">\
                                        <i class="fa  fa-times-circle "></i>\
                                    </a>\
                                <%}else{%>\
                                    <a class="btn-add js-add-protocol">\
                                        <i class="fa fa-plus-circle"></i>\
                                    </a>\
                                <%}%>\
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
            'click .js-add-protocol': 'addProtocol',
            'click .js-remove-protocol': 'removeProtocol'
        },

        initialize: function () {
            
            this.listenTo(this.model, 'change:isAdded', this.render);
        },

        render: function () {
            
            if (!this.model.get('isDisplay')) {
                return false;
            }

            this.$el.html(this.template(this.model.attributes));


            if( this.model.get('isAdded') )
                this.$el.addClass('session-protocols-list-item-selected-ci');
            else
                this.$el.removeClass('session-protocols-list-item-selected-ci');

            var highlight = this.model.get('highlight');


            if( highlight ){
                var protocolTitleElement = this.$el.find('.session-protocol-text'),
                    protocolTitle = protocolTitleElement.text().trim(),
                    pattern = new RegExp(highlight, "gi");

                protocolTitleElement.html( protocolTitle.replace(pattern,'<span class="protocol-text-highlight">' + highlight + '</span>') );
            }

            return this;
        },

        addProtocol: function() {

            var protocolCollection = app.models.patientHealthSessionModel.get('protocols');
            this.model.set({
                'order'     : protocolCollection.length,
                'isAdded'   : true
            });

            protocolCollection.add( this.model );

        },

        removeProtocol: function() {

            var protocolCollection = app.models.patientHealthSessionModel.get('protocols'),
                id = this.model.get('id'),
                model = protocolCollection.find(function(model) { return model.get('id') === id; });

            this.model.set('isAdded',false);

            protocolCollection.remove( model );

        }
    });
});