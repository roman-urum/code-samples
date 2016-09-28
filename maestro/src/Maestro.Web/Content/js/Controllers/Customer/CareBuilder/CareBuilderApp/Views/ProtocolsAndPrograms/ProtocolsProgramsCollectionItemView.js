'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Customer/CareBuilder/CareBuilderApp/AppGlobalVariables',
    'Controllers/Helpers',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Views/ProtocolsAndPrograms/ProtocolListItemView',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Views/ProtocolsAndPrograms/ProgramListItemView'
], function (
        $,
        _,
        Backbone,
        app,
        Helpers,
        ProtocolListItemView,
        ProgramListItemView
 ) {
    return Backbone.View.extend({

        className: 'panel panel-default',

        events: {

        },

        render: function () {

            var elementView;

            if( !this.model.get('isDisplay') )
                return false;

            switch (this.model.get('type')) {
                case 1:
                    {
                        elementView = new ProgramListItemView({ model: this.model });
                        break;
                    }
                case 2:
                    {
                        elementView = new ProtocolListItemView({ model: this.model });
                        break;
                    }
            }

            this.setElement( elementView.render().el );

            return this;

        }


    });
});