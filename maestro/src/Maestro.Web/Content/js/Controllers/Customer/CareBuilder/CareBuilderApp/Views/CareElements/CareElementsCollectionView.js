'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Customer/CareBuilder/CareBuilderApp/AppGlobalVariables',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Views/CareElements/CareElementsCollectionItemView',
], function ( $, _, Backbone, app, CareElementsCollectionItemView ) {
    return Backbone.View.extend({

    	className: 'panel-group panel-group-ci',

        events: {

        },

        initialize: function(){

        },

        render: function(){
            this.$el.empty();
            this.collection.each(this.renderCareElement, this);
            return this;
        },

        renderCareElement: function(element){
            app.views.careElementsCollectionItemView = new CareElementsCollectionItemView({ model: element });
            this.$el.append( app.views.careElementsCollectionItemView.render().el );
            return this;

        },
        
    });
});