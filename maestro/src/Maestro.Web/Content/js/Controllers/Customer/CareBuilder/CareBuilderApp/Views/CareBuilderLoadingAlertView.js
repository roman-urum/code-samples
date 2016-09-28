'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'BackboneBootstrapModal'
], function (
    $,
    _,
    Backbone,
    BackboneBootstrapModal
) {
    return Backbone.View.extend({
        el: 'div',

        render: function () {
            var self = this;

            this.initContentModal = new BackboneBootstrapModal({
                content: '<div class="alert alert-info remove-bottom-magin" role="alert">Care Builder is loading, please be patient</div>',
                showFooter: false,
                showHeader: false,
                allowCancel: false,
                modalOptions: {
                    backdrop: 'static',
                    keyboard: false
                }
            });
            this.initModalInterval = setInterval(function () {
                self.initContentModal.open();
                clearInterval(self.initModalInterval);
            }, 3000);

            return this;
        },

        close: function () {
            clearInterval(this.initModalInterval);
            this.initContentModal.close();
        }
    });
});