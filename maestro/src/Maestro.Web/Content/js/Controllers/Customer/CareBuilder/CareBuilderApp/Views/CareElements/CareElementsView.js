'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Collections/SearchCollection',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Views/CareElements/CareElementsCollectionView',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Views/CareBuilderLoadingAlertView',
    'Controllers/Helpers',
    'Controllers/Customer/CareBuilder/CareBuilderApp/AppGlobalVariables',
    'BootstrapSlider',
    'jquery-chosen',
    'bootstrap-tokenfield'
], function (
    $,
    _,
    Backbone,
    SearchCollection,
    CareElementsCollectionView,
    CareBuilderLoadingAlertView,
    Helpers,
    app
) {
    return Backbone.View.extend({
        el: '#tab-careElements',

        initialize: function () {
            this.initContentModal = new CareBuilderLoadingAlertView();
            this.initContentModal.render();

            this.$careElementType = this.$el.find("#care-element-type");
            this.$careElementsSearchKeyword = this.$el.find("#care-elements-search-keyword");
            this.$resultBox = this.$el.find('#care-elements-list');

            Helpers.initTags(null, this.$el.find('.searching-tags'));

            _.bindAll(this, "careElementsCollectionRender","reRender");

            app.vent.bind("reRenderCareElements", this.reRender);

        },

        events: {
            'change #care-element-type': 'filter',
            'keyup #care-elements-search-keyword': 'filter',
            'change #care-elements-search-keyword': 'filter',
            'change #care-elements-search-tags': 'filter',
            'click .js-search-clear': 'searchClear'
        },

        render: function (data) {
            var self = this;

            Helpers.renderSpinner(this.$resultBox);

            this.$el.find('.js-search-clear').prop('disabled', true);

            if( !app.collections.careElementsCollection ){

                app.collections.careElementsCollection = new SearchCollection();
                app.collections.careElementsCollection.isFeched = false;
                app.collections.careElementsCollection.fetch({
                    traditional: true,
                    data: {
                        categories: ["selectionAnswerSet", "scaleAnswerSet", "questionElement", "textMediaElement", "openEndedAnswerSet"],
                        q: [],
                        tags: [] 
                    },
                    reset: true,
                    success: function (model, response, options) {
                        app.collections.careElementsCollection.isFeched = true;
                        self.careElementsCollectionRender();
                        // console.log('data', data );
                        /*data && */self.$careElementType.change();
                    }
                });
            }else{
                // console.log('data', data );
                self.careElementsCollectionRender();
                /*data && */self.$careElementType.change();
            }
        

            return this;
        },

        careElementsCollectionRender: function(){
            app.views.careElementsCollectionView = new CareElementsCollectionView({ collection: app.collections.careElementsCollection });
            this.$resultBox.html( app.views.careElementsCollectionView.render().el );
            this.initContentModal.close();
        },

        filter: function() {

            // clearTimeout(window.filterTextTimeoutID);

            // window.filterTextTimeoutID = setTimeout(function(){

            var searchString = this.$careElementsSearchKeyword.val().trim(),
                pattern = new RegExp(searchString, "gi");

            var searchTags = this.$el.find('#care-elements-search-tags').val();
            searchTags = (searchTags && searchTags !== '0') ? searchTags : [];

            searchTags = searchTags.filter(function (element, index) {
                return element !== '';
            });

            var careElementTypes = {
                    '0': 0,
                    '1': 7,
                    '2': 6,
                    '3': 4,
                    '4': 5,
                    '5': 8,
                },
                careElementType = careElementTypes[this.$careElementType.val()];

            if (searchTags.length) {
                this.$el.find('.js-search-clear').prop('disabled', false);
            } else {
                this.$el.find('.js-search-clear').prop('disabled', true);
            }

            app.collections.careElementsCollection.each(function(model) {

                var isDisplayFilterText = pattern.test(model.get("name")) ? true : false;
                var isDisplayFilterTags = Helpers.isArraysMatch(model.get("tags"), searchTags) ? true : false;

                var isDisplayFilterType = careElementType == model.get("type") ? true : false;
                isDisplayFilterType = careElementType ? isDisplayFilterType : true;

                var isDisplay = isDisplayFilterText && isDisplayFilterTags && isDisplayFilterType ? true : false

                model.set('isDisplay', isDisplay);

            }, this);

            this.careElementsCollectionRender();

            // }.bind(this), 100);

        },

        reRender: function(data){
            app.collections.careElementsCollection = null;

            Helpers.resetTags( this.$el.find('.searching-tags') );

            this.$careElementType.val(data.careElementType);
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