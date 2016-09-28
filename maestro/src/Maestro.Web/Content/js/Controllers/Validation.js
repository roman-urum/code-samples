'use strict';

define([
    'Backbone.Validation'
], function () {
    _.extend(Backbone.Validation.callbacks, {
        valid: function (view, attr, selector) {
            var $el = view.$('[data-name="' + attr + '"]'),
                $group = $el.closest('div'),
                $errorBox;

            // for case if input is wrapped in tokenfield
            if ($group.hasClass('tokenfield')) {
                $group = $group.closest('.form-group');
            }

            $group.removeClass('has-error');
            $errorBox = this.getErrorBox($el, $group, attr);
            $errorBox.html('').addClass('hidden');
        },
        invalid: function (view, attr, error, selector) {
            var $el = view.$('[data-name="' + attr + '"]'),
                $group = $el.closest('div'),
                $errorBox;

            // for case if input is wrapped in tokenfield
            if ($group.hasClass('tokenfield')) {
                $group = $group.closest('.form-group');
            }
            
            $group.addClass('has-error');
            $errorBox = this.getErrorBox($el, $group, attr);
            $errorBox.html(error).removeClass('hidden');

            $el.on('click', function () {
                $el.off('click');
                $group.removeClass('has-error');
                $errorBox.html('').addClass('hidden');
            });
        },
        getErrorBox: function($el, $formGroup, attr) {
            if ($el.next('.help-block').length)
                return  $el.next('.help-block');
            else if ($formGroup.next('.help-block-' + attr.replace(".", "-")).length)
                return $formGroup.next('.help-block-' + attr.replace(".", "-"));
            else
                return $('.help-block-' + attr.replace(".", "-"));
        }
    });

    _.extend(Backbone.Validation.validators, {
        validModel: function (value, attr, customValue, model) {
            if (value && !value.isValid(true)) {
                return 'Invalid ' + attr;
            }
        },
        validCollection: function (value, attr, customValue, model) {
            var errors = value.map(function (entry) {
                return entry.isValid(true);
            });
            if (_.indexOf(errors, false) !== -1) {
                return 'Invalid collection of ' + attr;
            }
        },
    });

    return;
});