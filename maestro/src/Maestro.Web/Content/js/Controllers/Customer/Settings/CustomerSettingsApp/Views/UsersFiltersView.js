'use strict';

define([
    'jquery',
    'underscore',
    'backbone.marionette',
    'Controllers/Customer/Settings/CustomerSettingsApp/Application',
    './BaseItemView'
], function (
    $,
    _,
    Marionette,
    App,
    BaseItemView
) {
    return BaseItemView.extend({
        template: '#usersFiltersView',

        className: 'users-filters-view',

        ui: {
            searchStrInput: '#search-users',
            sortingSelect:  '#sort-users',
            roleSelect:     '#filter-users-by-role',
            statusSelect:   '#filter-users-by-status',
            siteSelect:     '#filter-users-by-site'
        },

        events: {
            'keyup @ui.searchStrInput': 'onSearchStrChange',
            'change @ui.sortingSelect': 'onSortingChange',
            'change @ui.roleSelect, @ui.statusSelect, @ui.siteSelect': 'onFiltersChange'
        },

        initialize: function () {
            BaseItemView.prototype.initialize.apply(this, arguments);

            var self = this;
            this.debouncedChangeSearchStr = _.debounce(function () {
                self.changeSearchStr();
            }, 200);

            this.listenTo(App.models.usersFiltersModel, 'fetched', function () {
                self.render();
            });
        },

        onSearchStrChange: function (e) {
            this.debouncedChangeSearchStr();
        },

        onSortingChange: function (e) {
            var sortingCase = $(e.target).val();
            this.model.set('sortingCase', sortingCase);
        },

        onFiltersChange: function (e) {
            this.model.set({
                filterByRole:   this.ui.roleSelect.val(),
                filterByStatus: this.ui.statusSelect.val(),
                filterBySite:   this.ui.siteSelect.val()
            });
        },

        //initialized as debounced function in initialize method
        debouncedChangeSearchStr: function () {},

        changeSearchStr: function () {
            this.model.set('searchStr', this.ui.searchStrInput.val());
        }
    });
});