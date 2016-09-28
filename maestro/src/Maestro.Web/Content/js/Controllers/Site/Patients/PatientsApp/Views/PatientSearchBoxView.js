'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Site/Patients/PatientsApp/AppNamespace',
    'Controllers/Constants',
    'Controllers/Site/Patients/PatientsApp/Views/PatientSearchBoxAdvancedView',
    'Controllers/Site/Patients/PatientsApp/Collections/PatientIdentifiersCollection',
    'Controllers/Site/Patients/PatientsApp/Collections/CareManagersCollection'
], function ($, _, Backbone, app, CONSTANTS, PatientSearchBoxAdvancedView, PatientIdentifiersCollection, CareManagersCollection) {
    return Backbone.View.extend({
        tagName: 'div',
        template: _.template($('#PatientSearchBoxTemplate').html()),
        itemTemplate: _.template($('#PatientSearchBoxItemTemplate').html()),
        emptyTemplate: _.template('<li><div class="no-matches"><%- obj.noContent %></div></li>'),
        q: '',
        xhr: new XMLHttpRequest(),
        includeInactivePatients: '',
        data: {
            q: '',
            includeInactivePatients: false,
            careManagerId: CONSTANTS.currentCareManager == null ? "" : CONSTANTS.currentCareManager.userId
        },
        typeahead: {},
        ajaxTimeout: 0,
        statusClasses: {
            content: 'content',
            loading: 'loading'
        },
        $typeahead: $(),
        $activity: $(),
        $permissions: $(),
        $typeaheadActions: $(),
        $dropdown: $(),
        $searchButton: $(),
        // $advancedSearchIcon: $(),
        delay: 300,
        careManagersCollection: null,
        events: {
            'click .js-patient-add': 'addPatient',
            'change #suggestion-box-inactive': 'changeActivity',
            'change #suggestion-box-manage-permission': 'changeCareManager',
            'input input.typeahead': 'changeQuery',
            'focus input.typeahead': 'showDropdown',
            'blur input.typeahead': 'hideDropdown',
            'click .glyphicon-remove': 'clearTypeaheadInput',
            'click #suggestion-box-search': 'showSearchResults',
            'keypress input.form-control': 'triggerSearchByIdentifiersField',
            // 'show.bs.collapse #collapseAdvancedSearch': 'toggleAdvancedSearchIcon',
            // 'hide.bs.collapse #collapseAdvancedSearch': 'toggleAdvancedSearchIcon'
        },

        triggerSearchByIdentifiersField: function (e) {
            if (e.keyCode === 13) {
                this.triggerChanges();
            }
        },

        // toggleAdvancedSearchIcon: function(e) {
        //     if (e.type === 'show') {
        //         app.views.patientSearchBoxAdvancedView.clearIdentifiers();
        //     }
        //     this.$advancedSearchIcon.toggleClass('fa-angle-double-down fa-angle-double-up');
        // },

        abortSearch: function () {
            clearTimeout(this.ajaxTimeout);
            this.xhr.abort();
            this.$typeaheadActions.removeClass(this.statusClasses.content + ' ' + this.statusClasses.loading);
            this.$typeahead.val('');
            this.$typeahead.trigger('keyup');
            this.$dropdown.empty();
        },

        showSearchResults: function (e) {
            e.preventDefault();

            this.triggerChanges();
        },

        clearTypeaheadInput: function () {
            this.data.q = '';

            this.abortSearch();

            this.triggerChanges();
        },

        changeActivity: function () {
            this.data.q = '';
            this.data.includeInactivePatients = this.$activity.is(':checked');
             
            this.abortSearch();

            this.triggerChanges();

            this.fillCareManagersDropdown();
        },

        fillCareManagersDropdown: function () {
            var self = this;
            var $careManagersFilterDropdown = self.$('#suggestion-box-manage-permission');
            $careManagersFilterDropdown.removeAttr('disabled');
            var oldSelectedValue = $careManagersFilterDropdown.val();
            $careManagersFilterDropdown.html("");
            $careManagersFilterDropdown.append($('<option selected>').val(CONSTANTS.currentCareManager == null ? null : CONSTANTS.currentCareManager.userId).text('My Patients'));
            $careManagersFilterDropdown.append($('<option>').val(null).text('All Patients'));
            var careManagers;

            if (this.$activity.is(':checked')) {
                careManagers = self.careManagersCollection.select(function(model) {
                    return (model.get('assignedPatientStatuses').indexOf(CONSTANTS.patientStatuses.INACTIVE) >= 0
                            || model.get('assignedPatientStatuses').indexOf(CONSTANTS.patientStatuses.ACTIVE) >= 0
                            || model.get('assignedPatientStatuses').indexOf(CONSTANTS.patientStatuses.INTRAINING) >= 0)
                            && (CONSTANTS.currentCareManager == null || model.get('userId') !== CONSTANTS.currentCareManager.userId);
                });
            } else {
                careManagers = self.careManagersCollection.select(function (model) {
                    return (model.get('assignedPatientStatuses').indexOf(CONSTANTS.patientStatuses.ACTIVE) >= 0
                            || model.get('assignedPatientStatuses').indexOf(CONSTANTS.patientStatuses.INTRAINING) >= 0)
                            && (CONSTANTS.currentCareManager == null || model.get('userId') !== CONSTANTS.currentCareManager.userId);
                });
            }

            var sortedCareManagers = _.sortBy(careManagers, function(careManagerModel) {
                return careManagerModel.get('firstName') + " " + careManagerModel.get('lastName');
            });

            _.each(sortedCareManagers, function (careManagerModel) {
                var optionElement = $('<option>').val(careManagerModel.get('userId')).text(careManagerModel.get('firstName') + " " + careManagerModel.get('lastName'));
                $careManagersFilterDropdown.append(optionElement);
            });

            if (oldSelectedValue && $careManagersFilterDropdown.find('option[value="' + oldSelectedValue + '"]').length > 0) {
                $careManagersFilterDropdown.val(oldSelectedValue);
            }
            
            
        },

        changeCareManager: function () {
            this.data.q = '';
            this.data.careManagerId = this.$permissions.find(":selected").val();

            this.abortSearch();

            this.triggerChanges();
        },

        changeQuery: function () {
            if (!this.$typeahead.val().length) {
                this.$typeaheadActions.removeClass(this.statusClasses.content);
                this.abortSearch();
            }
        },

        showDropdown: function () {
            if (this.hasMatches()) {
                this.$dropdown.show();
            }
        },

        hideDropdown: function () {
            if (!this.isClickable) {
                this.$dropdown.hide();
            }
        },

        bindDropdownEvents: function () {
            this.$dropdown.on({
                mouseenter: function () {
                    this.isClickable = true;
                }.bind(this),
                mouseleave: function () {
                    this.isClickable = false;
                }.bind(this)
            });
        },

        hasMatches: function () {
            return !!this.$dropdown.find('li').size();
        },

        initialize: function () {
            this.bind();
            this.initializeTypeahead();

            this.loadCareManagers();

            app.views.patientSearchBoxAdvancedView = new PatientSearchBoxAdvancedView({
                collection: new PatientIdentifiersCollection(CONSTANTS.customer.patientIdentifiers)
            });
        },

        loadCareManagers: function() {
            var self = this;
            self.careManagersCollection = new CareManagersCollection();

            self.careManagersCollection.fetch({
                data: {
                    onlyCareManagersWithAssignedPatients: true
                },
                success: function() {

                    self.fillCareManagersDropdown();

                }
            });
        },

        bind: function () {
            this.source = this.source.bind(this);
            this.select = this.select.bind(this);
            this.highlighter = this.highlighter.bind(this);
        },

        initializeTypeahead: function () {
            this.typeahead = {
                items: 5,
                autoSelect: false,
                source: this.source,
                sorter: this.sorter,
                select: this.select,
                matcher: this.matcher,
                highlighter: this.highlighter
            };
        },

        addPatient: function (e) {
            e.preventDefault();

            if (!e.altKey && !e.ctrlKey && !e.metaKey && !e.shiftKey) {
                var href = $(e.currentTarget).attr('href');
                
                app.router.navigate(href, {
                    trigger: true
                });
            }
        },

        source: function (q, process) {
            this.data.q = q;

            if (this.data.q !== this.q) {
                this.xhr.abort();
                clearTimeout(this.ajaxTimeout);
            }

            this.ajaxTimeout = setTimeout(function () {
                this.q = q;
                this.includeInactivePatients = this.data.includeInactivePatients;
                this.$typeaheadActions.addClass(this.statusClasses.loading);

                var tempData = $.extend({}, this.data);
                if (tempData.identifiers) {
                    delete tempData.identifiers;
                }
                this.xhr.abort();
                this.xhr = $.get(window.location.href + '/SuggestionSearch', tempData);

                this.xhr.then(function (data) {
                    this.$typeaheadActions
                        .removeClass(this.statusClasses.loading)
                        .addClass(this.statusClasses.content);
                    this.$typeahead.trigger('focus');

                    return process(data);
                }.bind(this));
            }.bind(this), this.delay);
        },

        matcher: function () {
            return true;
        },

        sorter: function (data) {
            _.each(data, function (item) {
                item.Identifiers = _.sortBy(item.Identifiers, function (item) {
                    return item.Name;
                });
            });

            return _.sortBy(data, function (item) {
                return !item.FirstName;
            });
        },

        highlighter: function (item) {
            if (item.noContent) {
                return this.emptyTemplate(item);
            }

            return this.itemTemplate(item);
        },

        select: function () {
            app.router.navigate(this.$dropdown.find('.active .suggestion-box-item').attr('data-href'), {
                trigger: true
            });
        },

        triggerChanges: function () {
            if (app.views.patientSearchBoxAdvancedView) {
                this.data.identifiers = app.views.patientSearchBoxAdvancedView.serializeIdentifiers();
            }

            app.views.patientsCollectionView.trigger('show-patients', this.data);
        },

        hasIdentifiers: function () {
            return !!CONSTANTS.customer.patientIdentifiers && CONSTANTS.customer.patientIdentifiers.length;
        },

        render: function () {
            this.$el
                .html(this.template({
                    managePermission: app.managePermission,
                    hasIdentifiers: this.hasIdentifiers()
                }));

            if (this.hasIdentifiers()) {
                this.$el
                    .find('#collapseAdvancedSearch')
                    .html(app.views.patientSearchBoxAdvancedView.render().el);
            }

            this.afterRender();
            this.delegateEvents();

            return this;
        },

        typeaheadKeyupEvent: function () {
            var triggerChanges = this.triggerChanges.bind(this),
                that = this;

            return function (e) {
                switch (e.keyCode) {
                    case 9: // tab
                        //case 13: // enter
                    case 40: // down arrow
                    case 38: // up arrow
                    case 16: // shift
                    case 17: // ctrl
                    case 18: // alt
                        break;
                    case 13:
                        {
                            if (!this.query) {
                                that.data.q = '';
                            }
                            triggerChanges();
                            break;
                        }
                    default:
                        this.lookup();
                }

                e.stopPropagation();
                e.preventDefault();
            }
        },

        modifyTypeaheadProto: function () {
            var that = this;

            if ($.fn.typeahead.Constructor.prototype.process_original && $.fn.typeahead.Constructor.prototype.render_original) {
                return;
            }

            $.fn.typeahead.Constructor.prototype.process_original = $.fn.typeahead.Constructor.prototype.process;
            $.fn.typeahead.Constructor.prototype.render_original = $.fn.typeahead.Constructor.prototype.render;

            $.fn.typeahead.Constructor.prototype.process = function (items) {
                if (!items.length) {
                    return this.process_original.call(this, [{
                        noContent: 'No Matches'
                    }]);
                } else {
                    return this.process_original.call(this, items);
                }
            };
            $.fn.typeahead.Constructor.prototype.render = function (items) {
                if (items.length === 1 && items[0].noContent) {
                    this.$menu.html(that.emptyTemplate(items[0]));
                } else {
                    this.render_original.call(this, items);
                }

                return this;
            };
        },

        modifyTypeaheadEvents: function () {
            var _typeahead = this.$typeahead.off('keyup blur').data('typeahead');

            this.$typeahead.on('keyup', this.typeaheadKeyupEvent().bind(_typeahead));

            this.$dropdown = _typeahead.$menu.off('mouseleave');

            this.bindDropdownEvents();
        },

        afterRender: function () {
            if (app.managePermission) {
                this.$el.find('.selectpicker').selectpicker();
            }

            this.modifyTypeaheadProto();
            this.$typeahead = this.$el.find('.typeahead').typeahead(this.typeahead);
            this.modifyTypeaheadEvents();

            this.$activity = this.$el.find('input#suggestion-box-inactive');
            this.$permissions = this.$el.find('select#suggestion-box-manage-permission');
            this.$typeaheadActions = this.$el.find('.typeahead-actions');
            this.$advancedSearchIcon = this.$el.find('#advanced-search-button .fa');
        }
    });
});