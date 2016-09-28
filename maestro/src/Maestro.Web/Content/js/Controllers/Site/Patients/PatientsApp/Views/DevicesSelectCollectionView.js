'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Site/Patients/PatientsApp/AppNamespace',
    'Controllers/Site/Patients/PatientsApp/Views/DevicesSelectCollectionItemView'
], function ($, _, Backbone, app, DevicesSelectCollectionItemView) {
    return Backbone.View.extend({

        tagName: 'select',

        className: 'selectpicker',

        initialize: function () {

        },

        render: function () {
            this.$el.empty();
            this.collection.each(this.renderDevice, this);
            this.$el.on('change', function() {
                $('#measurements-container table').hide();
                $('#measurements-container').find('table#' + this.value).show();
            }).trigger('change');

            return this;
        },

        renderDevice: function (device) {
            if (device.get('status') == 0 || device.get('status') == 1) {
                app.views.devicesSelectCollectionItemView = new DevicesSelectCollectionItemView({ model: device });
                this.$el.append(app.views.devicesSelectCollectionItemView.render().el);
            }

            return this;
        }

    });
});