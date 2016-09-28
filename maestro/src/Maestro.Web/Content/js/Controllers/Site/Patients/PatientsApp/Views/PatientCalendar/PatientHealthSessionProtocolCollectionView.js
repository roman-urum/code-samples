'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Site/Patients/PatientsApp/AppNamespace',
    'Controllers/Site/Patients/PatientsApp/Views/PatientCalendar/PatientHealthSessionProtocolCollectionItemView'
], function ($, _, Backbone, app, PatientHealthSessionProtocolCollectionItemView) {
    return Backbone.View.extend({

        tagName: 'ul',

        className: 'session-protocols-list reset-list-ci',

        itemView: PatientHealthSessionProtocolCollectionItemView,

        _listItems: null,

        _listIsSyncing: false,

        orderAttr: 'order',

        events: {
            'sortupdate': 'sortCompleted'
        },

        initialize: function () {

        },

        render: function () {
            this._listItems = {};
            
            this.listSync();
            return this;
        },

        sortCompleted: function () {
            var oatr = this.orderAttr;
            _.each( this._listItems, function ( v ) {
                v.model.set(oatr, v.$el.index());
            });
            this.collection.sort({silent: true});
            this.listSetup();
        },

        listSetup: function () {

            var $ods = this.$('li');

            if ( $ods.length == 1 ) {
                if ( this.$el.data('ui-sortable') )
                    this.$el.sortable('destroy');
            }else {
                this.$el.sortable({ placeholder: "session-protocols-sortable-placeholder", handle: '.js-session-protocol-handle' });

            }
        },

        listSync: function () {

            var list = this.collection.models
            
            this._listIsSyncing = true;
            
            _.invoke( this._listItems, 'remove' );
            
            this._listItems = {};
            
            for ( var model in list )
                this.listAdd( list[ model ] );

            this._listIsSyncing = false;

            this.listSetup();
        },

        listAdd: function ( model ) {
            var view;
            if ( !this._listItems[model.cid] ) {
                view = this._listItems[model.cid] = new this.itemView({ model: model });
                this.$el.append( view.render().$el );
            }
            if ( !this._listIsSyncing )
                this.listSetup();
        },

        remove: function () {
            _.invoke( this._listItems, 'remove' );
        }


    });
});