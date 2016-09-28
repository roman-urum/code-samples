'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Customer/CareBuilder/CareBuilderApp/AppGlobalVariables',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Views/ProtocolsAndPrograms/ProtocolsProgramsCollectionItemView',
], function ( $, _, Backbone, app, ProtocolsProgramsCollectionItemView ) {
    return Backbone.View.extend({

    	className: 'panel-group panel-group-ci',

        events: {

        },

        initialize: function(){

        },

        render: function(){
            this.$el.empty();
            this.collection.each(this.renderProtocolProgram, this);
            return this;
        },

        renderProtocolProgram: function(element){
            app.views.protocolsProgramsCollectionItemView = new ProtocolsProgramsCollectionItemView({ model: element });
            this.$el.append( app.views.protocolsProgramsCollectionItemView.render().el );
            return this;

        },
        
    });
});