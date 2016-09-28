(function($){
  
  'use strict';
  
  /* Tabbed Form Save Functionality */
  //The current form.  Changes based on current tab.  Default is the form of the first tab.
  var currForm = $('#ci-default-information .settings-form'),
      currSaveBtn = currForm.find('.save-form-wrap').find('.submit-form'),
      currCancelBtn = currForm.find('.save-form-wrap').find('.cancel-form'),
      currSuccess = currForm.find('.save-form-wrap').find('.confirm-success'),
      currSwitch, currState, requestedForm,
      currTab;

  //Modal confirmation if user clicks save
  //THIS FINALLY SUBMITS THE FORM
  $('.submit-form-confirm').click(function(){
    currForm.submit();
  });

  //Modal confirmation if user clicks cancel
  $('.cancel-form-confirm').click(function(){
    resetForm();
  });

  //Modal confirmation if user switches tabs
  $('.switch-form-confirm').click(function() {
    resetForm();
    currTab.tab('show');
    switchForm(currTab);
  });

  //Modal confirmation if user clicks on the edit role links on Default Access Roles tab
  $('.edit-role-confirm, .add-site-confirm').click(function() {
    showSaveWrap();
  });

  //If the user edits the form, show the Save button
  $('.settings-form input').keyup(function(){
    showSaveWrap();
  });

  //If the user edits the form, show the Save button
  $('.settings-form input[type=radio]').change(function() {
    showSaveWrap();
  });

  //User tried to switch tabs in middle of editing, ask them if they're sure
  $("#ci-site-settings-tabs a").click(function(e){
    e.preventDefault();
    currTab = $(this);
    if(currSaveBtn.hasClass('disabled')){
      currTab.tab('show');
      switchForm(currTab);
    } else {
      $('#page-switch').modal('show');
    }
  });

  //Form handler after a form is submitted
  $('.settings-form').submit(function(e) {
    e.preventDefault();
    resetForm();
    currSuccess.fadeIn('slow', function() {
      $(this).delay(2000).fadeOut('slow');
    });
  });

  //Show a modal if a user clicks a switch box.  Save current state of the clicked button, save the clicked button as an object.
  $('.ci-site-switch .bootstrap-switch span').click(function () {
    currState = $(this).siblings('.basic-checkbox').bootstrapSwitch('state');
    currSwitch = $(this).siblings('.basic-checkbox');
    if(currState == false){
      $('#site-switch').modal({ 
        keyboard: false,
        backdrop: 'static'
      });
      $('#site-switch').find('.site-switch-state').html("Off");
    } else {
      $('#site-switch').modal({ 
        keyboard: false,
        backdrop: 'static'
      });
      $('#site-switch').find('.site-switch-state').html("On");
    }
  });

  //If a user switches a  switchbox, but declines, switch it back, don't submit anything.
  $('.switch-state-revert').click(function() {
    currSwitch.bootstrapSwitch('toggleState');
  });

  //Use this to submit the state of the switch box that was clicked.
  /*$('.switch-state-confirm').click(function() {
    //currState will either equal true or false.  You can grab the ID of the switchbox that was selected, then submit the value.
  });*/

  //When a user changes the tabs, change the current form
  function switchForm(requestedTab) {
    requestedForm = $(requestedTab).attr('href') + ' .settings-form';
    currForm = $(requestedForm);
    currSaveBtn = currForm.find('.save-form-wrap .submit-form');
    currCancelBtn = currForm.find('.save-form-wrap .cancel-form');
    currSuccess = currForm.find('.save-form-wrap .confirm-success');
  }

  //Reset the forms
  function resetForm() {
    currForm.find('input[type=text], textarea').val('');
    currForm.find('input[type=radio]').prop('checked', function() {
      return this.getAttribute("checked") == 'checked';
    });
    currCancelBtn.hide();
    currSaveBtn.addClass('disabled');
  }

  //Show the save and cancel buttons
  function showSaveWrap() {
    currCancelBtn.show();
    currSaveBtn.removeClass('disabled');
  }
  //
  /* END Tabbed Form Save Functionality */
})(jQuery);