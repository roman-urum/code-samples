Maestro.namespace("Maestro.controls");

// Control to verify if panel to save changes should be displayed.
Maestro.controls.SaveFormWrap = (function($) {

    var saveFormWrap = function(form) {
        var $confirmMessage;

        this.$form = $(form);
        this.$btnSave = this.$form.find('.submit-form');
        this.$btnCancel = this.$form.find('.cancel-form');
        this.redirectUrl = this.$btnCancel.attr('redirect');

        $confirmMessage = this.$form.find('.saved-success');

        if ($confirmMessage.length) {
            $confirmMessage.delay(2000).fadeOut('slow');
        }
    };

    saveFormWrap.prototype.DisableSaving = function () {
        this.$btnSave.addClass('disabled');
    };

    saveFormWrap.prototype.EnableSaving = function () {
        this.$btnSave.removeClass('disabled');
        this.EnableCancel();
    };

    saveFormWrap.prototype.EnableCancel = function () {
        this.$btnCancel.show();
    };

    saveFormWrap.prototype.CancelChanges = function () {
        location.href = this.redirectUrl;
    };

    return {
        Initialize: function(form) {
            return new saveFormWrap(form);
        }
    };
}(jQuery));