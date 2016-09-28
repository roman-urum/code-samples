'use strict';

define([
    'jquery',
    'underscore',
    'backbone.marionette',
    'Controllers/Customer/Settings/CustomerSettingsApp/Application',
    './BaseCompositeView',
    './UserItemView'
], function (
    $,
    _,
    Marionette,
    App,
    BaseCompositeView,
    UserItemView
) {
    return BaseCompositeView.extend({
        className: 'users-collection-view',

        template: '#usersCollectionView',

        childView: UserItemView,

        childViewContainer: 'tbody',

        useSpinner: true,

        initialize: function () {
            BaseCompositeView.prototype.initialize.apply(this, arguments);

            var self = this;
            this.listenTo(App.models.usersFiltersModel, 'change', function() {
                self.sortCollection();
                self.render();
            });
        },

        onRender: function () {
            //console.log('onRender, App.models.usersFiltersModel: ', App.models.usersFiltersModel);
            this.$('.basic-checkbox').bootstrapSwitch({
                size: 'mini',
                onText: 'ENABLED',
                offText: 'DISABLED'
            });
        },

        filter: function (userModel, index, collection) {
            var filtersModel = App.models.usersFiltersModel,
                filterByRole = filtersModel.get('filterByRole'),
                filterByStatus = filtersModel.get('filterByStatus'),
                filterBySite = filtersModel.get('filterBySite'),
                searchStr = filtersModel.get('searchStr'),
                isDisplayed = true;

            if (filterByRole && userModel.get('customerUserRoleId') !== filterByRole) {
                isDisplayed = false;
            }
            if (filterBySite && !_.contains(userModel.get('siteIds'), filterBySite)) {
                isDisplayed = false;
            }
            if (filterByStatus) {
                if (filterByStatus === '1' && !userModel.get('isEnabled')) {
                    isDisplayed = false;
                }
                if (filterByStatus === '2' && userModel.get('isEnabled')) {
                    isDisplayed = false;
                }
            }            

            if (searchStr) {
                searchStr = searchStr.toLocaleLowerCase();
                var firstName = userModel.get('firstName').toLocaleLowerCase();
                var lastName = userModel.get('lastName').toLocaleLowerCase();
                var email = userModel.get('email').toLocaleLowerCase();

                if (!firstName.includes(searchStr) &&
                    !lastName.includes(searchStr) &&
                    !email.includes(searchStr)) {
                    isDisplayed = false;
                }
            }

            //The puprose of this is to provide the substring to be highlited when reneder filter result
            userModel.set('searchStr', searchStr ? searchStr: "");

            return isDisplayed;
        },

        sortCollection: function () {
            var sortingCase = App.models.usersFiltersModel.get('sortingCase'),
                sortKey,
                sortOrder;

            switch (sortingCase) {
                case '4':   sortKey = 'lastName'; sortOrder = -1; break;
                case '3':   sortKey = 'lastName'; sortOrder = 1; break;
                case '2':   sortKey = 'firstName'; sortOrder = -1; break;
                default:    sortKey = 'firstName'; sortOrder = 1; break;
            }

            this.collection.comparator = function (item1, item2) {
                var val1 = item1.get(sortKey);
                var val2 = item2.get(sortKey);

                if (typeof (val1) === "string") {
                    val1 = val1.toString().toLowerCase();
                    val2 = val2.toString().toLowerCase();
                }

                var sortValue = val1 > val2 ? 1 : -1;
                return sortValue * sortOrder;
            };
            this.collection.sort();
        },

        /*onBeforeDestroy: function () {
            debugger;
        }*/

    });
});