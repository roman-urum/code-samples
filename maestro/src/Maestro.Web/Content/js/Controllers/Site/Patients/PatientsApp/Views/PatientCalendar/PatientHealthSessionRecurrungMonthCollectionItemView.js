'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Site/Patients/PatientsApp/AppNamespace',
    'BackboneModelBinder'
], function ($, _, Backbone, app, BackboneModelBinder ) {
    return Backbone.View.extend({

    	tagName: 'label',

        className: 'btn btn-default btn-sm btn-sm-square-ci',

        template: _.template('<input type="checkbox" name="isSelected" id="isSelected"><%=number%>'),

        initialize: function () {

            this.modelBinder = new BackboneModelBinder();
            this.listenTo( this.model , 'change:isSelected', this.collectionChange);
        },

        render: function () {
           
			this.$el.html( this.template( this.model.attributes ) );
            this.modelBinder.bind(this.model, this.el);

            if( this.model.get('isSelected') )
                this.$el.addClass('active');
            else
                this.$el.removeClass('active');

			return this;
        },

        collectionChange: function() {

            this.render();
            app.collections.patientHealthSessionRecurrungMonthCollection.trigger('change');
        }



    });
});