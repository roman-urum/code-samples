'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Customer/CareBuilder/CareBuilderApp/AppGlobalVariables',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Collections/SearchCollection',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Views/ProtocolsAndPrograms/ProtocolsProgramsCollectionView',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Views/CareBuilderLoadingAlertView',
    'Controllers/Helpers'
], function (
    $,
    _,
    Backbone,
    app,
    SearchCollection,
    ProtocolsProgramsCollectionView,
    CareBuilderLoadingAlertView,
    Helpers
) {
    return Backbone.View.extend({
        el: '#tab-protocolsPrograms',

        initialize: function () {
            this.initContentModal = new CareBuilderLoadingAlertView();
            this.initContentModal.render();

            this.$protocolsAndProgramsType = this.$el.find("#protocols-and-programs-type");
            this.$protocolsAndProgramsSearchKeyword = this.$el.find("#protocols-and-programs-search-keyword");
            this.$resultBox = this.$el.find('#protocols-and-programs-list');

            Helpers.initTags(null, this.$el.find('.searching-tags'));

        },

        events: {
            'change #protocols-and-programs-type': 'filter',
            'keyup #protocols-and-programs-search-keyword': 'filter',
            'change #protocols-and-programs-search-keyword': 'filter',
            'change #protocols-and-programs-search-tags': 'filter',
            'click .js-search-clear': 'searchClear'
        },

        render: function () {

            var self = this;

            Helpers.renderSpinner(this.$resultBox);

            this.$el.find('.js-search-clear').prop('disabled', true);

            if (!app.collections.protocolsProgramsCollection) {

                app.collections.protocolsProgramsCollection = new SearchCollection();
                app.collections.protocolsProgramsCollection.isFeched = false;
                app.collections.protocolsProgramsCollection.fetch({
                    traditional: true,
                    data: {
                        categories: ["protocol", "program"],
                        q: [],
                        tags: []
                    },
                    reset: true,
                    success: function (model, response, options) {
                        app.collections.protocolsProgramsCollection.isFeched = true;
                        self.protocolsProgramsCollectionRender();
                        /*data && */self.$protocolsAndProgramsType.change();
                    }
                });
            } else {
                self.protocolsProgramsCollectionRender();
                /*data && */self.$protocolsAndProgramsType.change();
            }


            return this;

        },

        protocolsProgramsCollectionRender: function () {
            app.views.protocolsProgramsCollectionView = new ProtocolsProgramsCollectionView({ collection: app.collections.protocolsProgramsCollection });
            this.$resultBox.html(app.views.protocolsProgramsCollectionView.render().el);
            this.initContentModal.close();
        },

        filter: function () {

            // clearTimeout(window.filterTextTimeoutID);

            // window.filterTextTimeoutID = setTimeout(function(){

            var searchString = this.$protocolsAndProgramsSearchKeyword.val().trim();

            var searchTags = this.$el.find('#protocols-and-programs-search-tags').val();
            searchTags = (searchTags && searchTags !== '0') ? searchTags : [];

            searchTags = searchTags.filter(function (element, index) {
                return element !== '';
            });

            var protocolsAndProgramsTypes = {
                '0': 0,
                '1': 2,
                '2': 1
            };

            var protocolsAndProgramsType = protocolsAndProgramsTypes[this.$protocolsAndProgramsType.val()];

            if (searchTags.length) {
                this.$el.find('.js-search-clear').prop('disabled', false);
            } else {
                this.$el.find('.js-search-clear').prop('disabled', true);
            }

            app.collections.protocolsProgramsCollection.each(function (model) {
                var lcName = model.get("name").toLowerCase();
                var isDisplayFilterText = lcName.indexOf(searchString.toLowerCase()) > -1 ? true : false;
                var isDisplayFilterTags = this._isArraysMatch(model.get("tags"), searchTags) ? true : false;

                var isDisplayFilterType = protocolsAndProgramsType == model.get("type") ? true : false;
                isDisplayFilterType = protocolsAndProgramsType ? isDisplayFilterType : true;

                var isDisplay = isDisplayFilterText && isDisplayFilterTags && isDisplayFilterType ? true : false;

                model.set('isDisplay', isDisplay);

            }, this);

            this.protocolsProgramsCollectionRender();

            // }.bind(this), 100);

        },

        _isArraysMatch: function (tags, searchTags) {
            var matches = [];

            for (var i = 0; i < tags.length; i++) {
                for (var e = 0; e < searchTags.length; e++) {
                    if (tags[i] === searchTags[e]) matches.push(tags[i]);
                }
            }

            return matches.length == searchTags.length ? true : false;

        },

        reRender: function (data) {
            app.collections.protocolsProgramsCollection = null;
            this.$protocolsAndProgramsType.val(data.protocolsAndProgramsType);
            this.render(data);
        },

        searchClear: function () {
            this.$el.find('.searching-tags')
                .val(0)
                .trigger("chosen:updated")
                .change()
            ;
        }


    });
});