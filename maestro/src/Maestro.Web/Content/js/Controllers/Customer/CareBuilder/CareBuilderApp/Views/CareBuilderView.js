'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'BackboneBootstrapModal',
    'Controllers/Helpers',
    'Controllers/Customer/CareBuilder/CareBuilderApp/AppGlobalVariables',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Views/CareElements/CareElementsView',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Views/ProtocolsAndPrograms/ProtocolsAndProgramsView',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Views/ContentTypeView'
], function (
    $,
    _,
    Backbone,
    BackboneBootstrapModal,
    Helpers,
    app,
    CareElementsView,
    ProtocolsAndProgramsView,
    ContenTypeView
) {
    return Backbone.View.extend({
        el: '#care-builder-container',

        template: _.template($("#CareBuilderTemplate").html()),

        initialize: function (options) {

            this.activeTab = options.activeTab;
            this.elementType = options.elementType;

        },

        events: {
            'click .js-add-content': 'addContent',
            'click #care-elements-tab-link': 'navigateToCareElements',
            'click #protocols-and-programs-tab-link': 'navigateToProtocolsAndPrograms'
        },

        render: function () {
            // var elementType = Helpers.getUrlParameterByName('elementType');

            this.$el.html(this.template());

            this.$careElementType = this.$el.find("#care-element-type");
            this.$protocolAndProgramType = this.$el.find("#protocols-and-programs-type");

            if (this.activeTab == 'ProtocolsAndPrograms') {
                this.$el.find('#protocols-and-programs-tab-link').tab('show');

                // if (elementType != null) {
                //     this.$el.find('#protocols-and-programs-type option:contains(' + elementType + ')').attr('selected', 'selected');
                // }

                this.initProtocolsAndProgramsTab();
                this.$protocolAndProgramType.val( this.elementType );
            } else {

                this.initCareElementsTab();
                this.$careElementType.val( this.elementType );
            }

            return this;
        },

        navigateToCareElements: function () {
            app.router.navigate('CareElements', {
                trigger: false
            });
            this.$careElementType.val(0);
            // if (app.views.CareElementsView == undefined) {
                this.initCareElementsTab();
            // }
        },

        navigateToProtocolsAndPrograms: function () {
            app.router.navigate('ProtocolsAndPrograms', {
                trigger: false
            });
            this.$protocolAndProgramType.val(0);
            // if (app.views.ProtocolsAndProgramsView == undefined) {
                this.initProtocolsAndProgramsTab();
            // }
        },

        addContent: function (e) {
            e.preventDefault();

            app.views.contentTypeView = new ContenTypeView();

            var modalView = new BackboneBootstrapModal({
                content: app.views.contentTypeView,
                title: 'Add Content',
                okText: 'Cancel',
                cancelText: false,
                modalOptions: {
                    backdrop: 'static',
                    keyboard: false
                }
            });

            modalView.open();

            app.views.contentTypeModalView = modalView;
        },

        initProtocolsAndProgramsTab: function () {
            this._clearView( app.views.ProtocolsAndProgramsView );
            app.views.ProtocolsAndProgramsView = new ProtocolsAndProgramsView();
            app.views.ProtocolsAndProgramsView.render();
        },

        initCareElementsTab: function () {
            this._clearView( app.views.CareElementsView );
            app.views.CareElementsView = new CareElementsView();
            app.views.CareElementsView.render();
        },

        _clearView: function (view) {
            if(app.views.contentTypeModalView) {
                app.views.contentTypeModalView.close();
            }
            if (view) {
                view.unbind();
                view.undelegateEvents();
            }
        },
    });
});