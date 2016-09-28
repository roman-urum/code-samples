'use strict';

define([
    'jquery',
    'underscore',
    'BackboneModelBinder',
    './BaseItemView',
    './CloudTagView',
    '../Models/ConditionModel',
    '../Models/ConditionTagsCollection',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Collections/SearchCollection',
    'Controllers/Customer/Settings/CustomerSettingsApp/Application',
    'Controllers/Customer/Settings/CustomerSettingsApp/Alerts',
    'Controllers/Helpers',
    'jquery-chosen',
    'bootstrap-tokenfield',
    'jquery-ui',
    'jquery-ui-touch',
    'tagcloud'
], function (
    $,
    _,
    BackboneModelBinder,
    BaseItemView,
    CloudTagView,
    ConditionModel,
    ConditionTagsCollection,
    SearchCollection,
    App,
    Alerts,
    Helpers
) {
    return BaseItemView.extend({
        template: '#conditionContentView',
        noProgramsFoundTemplate: _.template($('#noProgramsFoundView').html()),
        noProtocolsFoundTemplate: _.template($('#noProtocolsFoundView').html()),
        programListItemTemplate: _.template($('#programListItemTemplate').html()),
        protocolListItemTemplate: _.template($('#protocolListItemTemplate').html()),
        className: 'condition-content-view',

        model: ConditionModel,
        fetchXhr: null,

        events: {
            'click .js-save-condition-content-and-exit-button': 'saveConditionContent',
            'click .js-search-clear': 'searchClear',
            'click .tags-cloud-row a': 'onSelectTag',
            'change #protocols-and-programs-search-tags': 'onSearchTagsChanged',
            'click .glyphicon-triangle-bottom': 'doCollapse',
            'click .glyphicon-triangle-right': 'doExpand',
            'click .programs-text': 'onProgramsTextClick',
            'click .protocols-text': 'onProtocolsTextClick'

        },

        initialize: function () {
            this.modelBinder = new BackboneModelBinder();

            //Backbone.Validation.bind(this);

            if (!this.searchContentCollection) {
                this.searchContentCollection = new SearchCollection();
            }

            // tag cloud settings
            $.fn.tagcloud.defaults = {
                size: { start: 12, end: 32, unit: 'px' },
                color: { start: '#3a86c4', end: '#3a86c4' }
            };
        },

        onBeforeRender: function () {
        },

        onRender: function () {

            var self = this;

            self.$searchingTagsEl = self.$el.find('.searching-tags');
            self.$programsListEl = self.$el.find('.programs-list-container');
            self.$protocolsListEl = self.$el.find('.protocols-list-container');

            self.$programsListEl.html(self.noProgramsFoundTemplate());
            self.$protocolsListEl.html(self.noProtocolsFoundTemplate());

            self.$searchingTagsEl
                .prop('disabled', false)
                .prop('multiple', true)
                .chosen();

            this.renderTagsDropDown();

            this.loadTagsCloud();

            self.$el.find('[data-toggle="popover"]')
                .popover({
                    html: 'true'
                })
                .on('shown.bs.popover', function (e) {
                    var event = e;
                    $('[data-dismiss="popover"]')
                        .off('click')
                        .on('click', function () {
                            $(arguments[0].target).closest('.popover').parent().find('[data-toggle="popover"]').click();
                        });
            });
        },

        saveConditionContent: function (e, preventNavigate) {
            e.preventDefault();

            var self = this;

            self.model.set('tags', self.getSelectedTags());

            if (this.model.isValid(true)) {
                var saveButtons = this.$('.js-save-condition-content-and-exit-button');
                var cancelButton = this.$('.js-cancel-button');

                saveButtons.data('loading-text', 'Updating...').button('loading');
                cancelButton.addClass('disabled');

                this.model.save(null, {
                    success: function (response) {
                        Alerts.success('Condition was saved successfully');

                        if (!preventNavigate) {
                            App.navigate('Settings/Conditions');
                        } else {
                            self.triggerMethod('show:thresholds-tab');
                        }
                    },
                    error: function (model, xhr, options) {
                        Alerts.danger(xhr.responseJSON ? xhr.responseJSON.ErrorMessage : 'Error occured');

                        saveButtons.button('reset');
                        cancelButton.removeClass('disabled');
                    }
                });
            }
        },

        searchClear: function () {
            this.$searchingTagsEl.val(null);
            this.$searchingTagsEl.trigger("chosen:updated").change();
            
            this.$programsListEl.html(this.noProgramsFoundTemplate());
            this.$protocolsListEl.html(this.noProtocolsFoundTemplate());
        },

        getSelectedTags: function() {
            var tags = this.$el.find('#protocols-and-programs-search-tags').val();

            if (!Array.isArray(tags)) return [];

            tags = (tags && tags !== '0') ? tags : [];

            tags = tags.filter(function (element, index) {
                return element !== '';
            });

            return tags;
        },

        onSearchTagsChanged: function () {

            var self = this;
            var searchTags = self.getSelectedTags();

            self.$programsListEl.empty();

            if (self.fetchXhr && self.fetchXhr.readyState > 0 && self.fetchXhr.readyState < 4) {
                self.fetchXhr.abort();
            }

            if (searchTags.length > 0) {

                Helpers.renderSpinner(self.$programsListEl);
                Helpers.renderSpinner(self.$protocolsListEl);

                self.fetchXhr = self.searchContentCollection.fetch({
                    traditional: true,
                    data: {
                        categories: ["protocol", "program"],
                        q: [],
                        tags: searchTags
                    },
                    reset: true,
                    success: function(collection) {
                        self.displaySearchContentResult.call(self, collection);
                    }
                });
            } else {

                self.$programsListEl.html(self.noProgramsFoundTemplate());
                self.$protocolsListEl.html(self.noProtocolsFoundTemplate());
            }
            
            this.loadTagsCloud();
        },

        displaySearchContentResult: function (searchResultCollection) {

            var self = this;
            var programs = searchResultCollection.filter(function(model) { return model.get('type') === 1; });
            var protocols = searchResultCollection.filter(function(model) { return model.get('type') === 2; });

            if (!programs || programs.length <= 0) {
                self.$programsListEl.html(self.noProgramsFoundTemplate());
            } else {
                self.$programsListEl.empty();

                _.each(programs, function(programModel) {

                    self.$programsListEl.append(self.programListItemTemplate(programModel.toJSON()));

                });
            }

            if (!protocols || protocols.length <= 0) {
                self.$protocolsListEl.html(self.noProtocolsFoundTemplate());
            } else {
                
                self.$protocolsListEl.empty();

                _.each(protocols, function (protocolModel) {

                    self.$protocolsListEl.append(self.protocolListItemTemplate(protocolModel.toJSON()));

                });

            }
        },

        loadTagsCloud: function() {
            var self = this;

            if (App.collections.conditionTagsCollection) {
                this.renderTagsCloud();
            } else {
                Helpers.renderSpinner(this.$el.find('.tags-cloud-row'));

                App.collections.conditionTagsCollection = new ConditionTagsCollection();
                App.collections.conditionTagsCollection.isFetched = false;
                App.collections.conditionTagsCollection.fetch({
                    success: function () {
                        self.renderTagsDropDown.call(self);
                        self.renderTagsCloud();
                    }
                });
            }
        },

        renderTagsDropDown: function () {
            var self = this;

            if (!App.collections.conditionTagsCollection) return;

            App.collections.conditionTagsCollection.each(function (tag) {

                var tagOption = $('<option></option>');
                tagOption.text(tag.get('name'));
                if (self.model.get('tags').indexOf(tag.get('name')) >= 0) {
                    tagOption.attr('selected', 'selected');
                }

                self.$searchingTagsEl.append(tagOption);


            });

            self.$searchingTagsEl.trigger('chosen:updated');
            self.$searchingTagsEl.trigger('change');
        },

        renderTagsCloud: function() {
            var $container = this.$el.find('.tags-cloud-row'),
                selectedTags = this.getSelectedTags();

            $container.html('');

            App.collections.conditionTagsCollection.each(function(model) {
                var view = new CloudTagView({ model: model }),
                    $tagElement = view.render().$el;

                if (selectedTags.indexOf(model.get('name')) >= 0) {
                    $tagElement.addClass('selected');
                }

                $container.append($tagElement);
            });

            $container.find('a').tagcloud();
        },

        onSelectTag: function (event) {
            var $tagsInput = this.$el.find('#protocols-and-programs-search-tags'),
                value = $(event.target).html();

            $tagsInput.find('option:contains(' + value + ')').attr('selected', 'selected');
            $tagsInput.trigger('chosen:updated');
            $tagsInput.trigger('change');

            this.renderTagsCloud();
        },

        doCollapse: function (event) {
            $(event.target).removeClass('glyphicon-triangle-bottom');
            $(event.target).addClass('glyphicon-triangle-right');

            $(event.target).siblings('.programs-list-container').toggleClass('hidden');
            $(event.target).siblings('.protocols-list-container').toggleClass('hidden');

        },

        doExpand: function (event) {
            $(event.target).removeClass('glyphicon-triangle-right');
            $(event.target).addClass('glyphicon-triangle-bottom');

            $(event.target).siblings('.programs-list-container').toggleClass('hidden');
            $(event.target).siblings('.protocols-list-container').toggleClass('hidden');
        },

        onProgramsTextClick: function(event) {

            if ($(event.target).prev('.glyphicon-triangle-bottom').length) {
                $(event.target).prev('.glyphicon-triangle-bottom').trigger('click');
            } else if ($(event.target).prev('.glyphicon-triangle-right').length) {
                $(event.target).prev('.glyphicon-triangle-right').trigger('click');
            }            
        },

        onProtocolsTextClick: function (event) {

            if ($(event.target).prev('.glyphicon-triangle-bottom').length) {
                $(event.target).prev('.glyphicon-triangle-bottom').trigger('click');
            } else if ($(event.target).prev('.glyphicon-triangle-right').length) {
                $(event.target).prev('.glyphicon-triangle-right').trigger('click');
            }            
        }
    });
});