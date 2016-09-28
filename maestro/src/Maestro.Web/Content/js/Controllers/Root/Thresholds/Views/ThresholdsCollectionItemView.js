'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Root/Thresholds/AppNamespace',
    'BackboneModelBinder'
], function ($, _, Backbone, app, BackboneModelBinder) {
    return Backbone.View.extend({

        modelBinder: undefined,

        tagName: 'tr',

        template: _.template('<% if (isDisplay) { %>\
            <td><%=nameLabel%></td>\
            <td><%=unitLabel%></td>\
                <td class="low-limit">\
                <div class="form-group">\
                    <% if (isReadOnly) { %>\
                        <strong id="minValue<%= name %>" name="minValue" data-name="minValue"></strong>\
                    <% } else { %>\
                        <input type="text" class="form-control bold" id="minValue<%= name %>" name="minValue" data-name="minValue" maxlength="6">\
                        <span class="help-block hidden"></span>\
                    <% } %>\
                </div>\
                <% if (typeof minValueDefault !== "undefined" && !isReadOnly) { %>\
                    <p class="help-block italic <%= minValue === null ? "bold" : "" %>"><%= minValueDefault %></p>\
                <% } %>\
            </td>\
            <td class="high-limit">\
                <div class="form-group">\
                    <% if (isReadOnly) { %>\
                        <strong id="maxValue<%= name %>" name="maxValue" data-name="maxValue"></strong>\
                    <% } else { %>\
                        <input type="text" class="form-control bold" id="maxValue<%= name %>" name="maxValue" data-name="maxValue" maxlength="6">\
                        <span class="help-block hidden"></span>\
                    <% } %>\
                </div>\
                <% if (typeof maxValueDefault !== "undefined") { %>\
                    <p class="help-block italic <%= maxValue === null ? "bold" : "" %>"><%= maxValueDefault %></p>\
                <% } %>\
            </td>\
        <% } %>'),

        events: {
            'keyup .form-group input.form-control': 'checkDecimalPlaces'
        },

        initialize: function () {
            this.modelBinder = new BackboneModelBinder();
            this.listenTo(this.model, 'change:isDisplay', this.render);
            this.listenTo(this.model, 'change:minValue change:maxValue', this.modelChanged);
            Backbone.Validation.bind(this);
        },

        render: function () {

            this.$el.html( this.template( $.extend( {}, this.model.attributes, app ) ) );
            this.modelBinder.bind(this.model, this.el);

            return this;
        },

        checkDecimalPlaces: function (e) {
            var input = $(e.target);
            var value = input.val() * 1;

            if (!_.isNumber(value)) return;

            var decimalPlacesNum = this.model.decimalPlacesList[this.model.get('name')] || 0;
            var strArr = value.toString().split('.');
            var decimalStr = (strArr.length > 1) ? _.last(strArr) : ''; //get decimal part string
            if (decimalStr.length > decimalPlacesNum) {
                input.val(value.toFixed(decimalPlacesNum));
                input.trigger('change');
            }
        },

        modelChanged: function () {
            app.vent.trigger('modelChanged');
        }
    });
});