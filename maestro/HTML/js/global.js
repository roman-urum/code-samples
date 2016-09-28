(function($){
  
  'use strict';
  
  /* Accordions, Tooltips, Global Elements */
  $('.list-collapse').click(function() {
    $(this).closest('.list-accordion').find('.accordion-content').fadeToggle();
    $(this).siblings('.list-arrow').toggleClass('list-arrow-active');
    $(this).toggleClass('active-list');
  });

  $('.accordion-expand-all').click(function() {
    $('.list-accordion').find('.accordion-content').fadeIn();
    $('.list-arrow').addClass('list-arrow-active');
  });

  $('.accordion-collapse-all').click(function() {
    $('.list-accordion').find('.accordion-content').fadeOut();
    $('.list-arrow').removeClass('list-arrow-active');
  });

  $('body').tooltip({ selector: '[data-toggle=tooltip]', html: true });

  $('.show-edit-tools').click(function() {
    $(this).closest('.maestro-noedit-wrap').addClass('edit-hide');
    $('.maestro-edit-wrap').removeClass('edit-hide');
  });

  $('.hide-edit-tools').click(function() {
    $(this).closest('.maestro-edit-wrap').addClass('edit-hide');
    $('.maestro-noedit-wrap').removeClass('edit-hide');
  });

  $('.basic-checkbox').bootstrapSwitch();
})(jQuery);