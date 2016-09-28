(function($){
  
  'use strict';
  
  $('.modal-hide-edit').click(function () {
    $('body').find('.maestro-edit-wrap').addClass('edit-hide');
    $('body').find('.maestro-noedit-wrap').removeClass('edit-hide');
  });
  
  $('.ci-form input, .ci-form select').change(function () {
    $('.ci-form .page-save').removeClass('disabled');
  });
  
  $('.expand-patient').click(function () {
    if($(this).html() === 'Collapse'){
      $(this).html('Expand'); 
    } else {
      $(this).html('Collapse'); 
    }
    $(this).closest('.expand-patient-row').siblings('.patient-details').slideToggle();
    $(this).closest('.expand-patient-row').siblings('.patient-header').toggleClass('open');
  });
  
})(jQuery);