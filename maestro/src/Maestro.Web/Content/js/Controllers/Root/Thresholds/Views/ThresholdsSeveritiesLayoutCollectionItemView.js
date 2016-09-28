'use strict';

define([
    'jquery',
    'underscore',
    'backbone'
], function ($, _, Backbone) {
    return Backbone.View.extend({

        tagName: 'table',

        className: 'table',

        template: _.template('<% if ( isDisplay ) { %>\
                                <tbody>\
                                    <tr>\
                                        <td class="th-name-of-measure td-<%=name%> td-threshold">\
                                            <% if (overlap) { %>\
                                                <span class="glyphicon glyphicon-warning-sign overlap-alert" title="This measurement has or is inheriting thresholds that overlap. This could cause excessive alerts."></span>\
                                            <% } %>\
                                            <span><%=nameLabel%></span>\
                                        </td>\
                                        <td class="th-unit-of-measure td-<%=name%> td-threshold"><%=unitLabel%></td>\
                                    </tr>\
                                </tbody>\
                            <% } %>'),

        events: {},

        initialize: function () {
            this.listenTo(this.model, 'change:isDisplay', this.render);
        },

        render: function () {
            this.$el.html(this.template(this.model.attributes));

            return this;
        }
    });
});