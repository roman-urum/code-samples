'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Site/Patients/PatientsApp/AppNamespace',
    'BackboneModelBinder'
], function ($, _, Backbone, app, BackboneModelBinder) {
    return Backbone.View.extend({

        modelBinder: undefined,

        className: 'col-sm-4',

        template: _.template('<div class="form-group">\
                                <label><%if(isRequired){%>*<%}%><%=name%></label>\
                                <input type="text" class="form-control" name="value" data-name="value" value="" data-inputmask-mask="<%=inputMask%>">\
                                <span class="help-block hidden"></span>\
                            </div>'),

        events: {

        },

        initialize: function () {
            this.modelBinder = new BackboneModelBinder();
            Backbone.Validation.bind(this);
        },

        render: function () {
            this.$el.html(this.template(this.model.attributes));
            this.modelBinder.bind(this.model, this.el);

            return this;
        }
    });

});