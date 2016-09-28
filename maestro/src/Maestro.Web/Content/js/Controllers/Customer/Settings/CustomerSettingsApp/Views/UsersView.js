'use strict';

define([
    'jquery',
    'underscore',
    'backbone.marionette',
    'Controllers/Customer/Settings/CustomerSettingsApp/Application',
    '../Models/UserModel',
    '../Models/UsersFiltersModel',
    './BaseLayoutView',
    './UsersFiltersView',
    './UsersCollectionView',
    './UserDetailsView',
    './UserEditView'
], function (
    $,
    _,
    Marionette,
    App,
    UserModel,
    UsersFiltersModel,
    BaseLayoutView,
    UsersFiltersView,
    UsersCollectionView,
    UserDetailsView,
    UserEditView
) {
    return BaseLayoutView.extend({
        template: '#usersView',

        className: 'users-view',

        regions: {
            filters: '.users-filters',
            list: '.users-list',
            edit: '.user-edit'
        },

        onRender: function() {
            this.showChildView('filters', new UsersFiltersView({model: App.models.usersFiltersModel}));

            //Leaved this code because of using 'replaceElement' option,
            //which should be implemented in Marionette 3.0 release,
            //then UsersCollectionView can be refactored
            //from deprecated CompositeView to CollectionView.
            this.getRegion('list').show(new UsersCollectionView({ model: this.options.model }), { replaceElement: true });
        },

        showUserPage: function (userId, action) {
            this.getRegion('filters').empty();
            this.getRegion('list').empty();

            var userModel = App.models.usersCollection.findWhere({id: userId});
            if (!userModel) {
                return App.navigate('Settings/Users');
            }

            if (action === 'Edit') {
                this.showChildView('edit', new UserEditView({model: userModel, action: action}));
            } else {
                this.showChildView('edit', new UserDetailsView({model: userModel, action: action}));
            }
        },

        showCreateUserForm: function () {
            this.getRegion('filters').empty();
            this.getRegion('list').empty();

            var userModel = new UserModel({isEnabled: true, doNotSendInvitation: false});
            userModel.isFetched = true;          //prevent displaying spinner
            this.showChildView('edit', new UserEditView({model: userModel, action: 'create'}));
        }

    });
});