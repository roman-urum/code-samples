'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Helpers',
    'Controllers/Customer/CareBuilder/CareBuilderApp/AppGlobalVariables',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Views/CareBuilderView',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Views/ProtocolsAndPrograms/ProtocolsAndProgramsView',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Views/ProtocolsAndPrograms/ProtocolView',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Views/ProtocolsAndPrograms/ProtocolEditingView',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Models/ProtocolsAndPrograms/ProtocolEditingModel',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Views/ProtocolsAndPrograms/ProgramView',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Models/ProtocolsAndPrograms/ProgramDetailsModel'
], function (
        $,
        _,
        Backbone,
        Helpers,
        app,
        CareBuilderView,
        ProtocolsAndProgramsView,
        ProtocolView,
        ProtocolEditingView,
        ProtocolEditingModel,
        ProgramView,
        ProgramDetailsModel
    ) {
    var router = Backbone.Router.extend({
        initialize: function () {
            Backbone.history.start({
                root: '/CareBuilder/',
                pushState: true
            });
        },

        routes: {
            'EditProtocol/:id': 'editProtocol',
            'EditProgram/:id': 'editProgram',
            'CreateProtocol': 'createProtocol',
            'CreateProgram': 'createProgram',
            ':element': 'showElements',
            ':element/': 'showElements',
            ':element/:type': 'showElements',
            ':element/:type/': 'showElements'

        },

        _clearView: function (view) {
            if (view) {
                view.unbind();
                view.undelegateEvents();
            }
        },

        showElements: function(element, type){
            var CareElementTypes = {
                    'All': 0,
                    'MultipleChoice': 1,
                    'Scale': 2,
                    'Question': 3,
                    'TextAndMedia': 4,
                    'OpenEnded': 5
                },
                ProtocolsAndProgramTypes = {
                    'All': 0,
                    'Protocols': 1,
                    'Programs': 2
                },
                elementTypes = ( element == 'CareElements' ) ? CareElementTypes : ProtocolsAndProgramTypes,
                type = type || 'All'
            ;

            this._clearView(app.views.CareBuilderView);
            app.views.CareBuilderView = new CareBuilderView({
                activeTab: element,
                elementType: elementTypes[type]
            });
            app.views.CareBuilderView.render();    
        },

        editProtocol: function (id) {
            if (app.views.ProtocolView) {
                app.views.ProtocolView.close();
                this._clearView(app.views.ProtocolView);
            }
            app.views.ProtocolView = new ProtocolView();
            app.views.ProtocolView.render(id);
        },

        editProgram: function (id) {
            var self = this;
            var programModel = new ProgramDetailsModel({ id: id });

            Helpers.renderSpinner($("#care-builder-container"));

            programModel.fetch({
                success: function () {
                    if (app.views.ProgramView) {
                        self._clearView(app.views.ProgramView);
                    }
                    app.views.ProgramView = new ProgramView({ model: programModel });
                    app.views.ProgramView.render();
                }
            });
        },

        createProgram: function () {
            var programModel = new ProgramDetailsModel();

            app.views.ProgramView = new ProgramView({ model: programModel });
            app.views.ProgramView.render();
        },

        createProtocol: function () {
            var protocolEditingModel = new ProtocolEditingModel();

            app.views.ProtocolEditingView = new ProtocolEditingView({ model: protocolEditingModel });
            app.views.ProtocolEditingView.render();
        }
    });

    return router;
});