(function($){
  
  'use strict';
  
  var stringFilter = /[\!\@\#\$\%\^\-\_\s]/g;

  /* Site Search Filter for Site Management Screen */
  $('.ci-content .submit-site-filter').click('click', function() {
    $('.ci-content .site-filter').submit();
  });

  $('.ci-content .site-filter').submit(function(e) {
    e.preventDefault();
    $('#page-loader').modal({
      keyboard: false,
      backdrop: 'static'
    });
    $('#page-loader').modal('show');
    window.setTimeout(function() { $('#page-loader').modal('hide'); }, 1500);
  });

  $('.ci-content .clear-filter').click(function () {
    $('.ci-content .filter-input').val('');
    $(this).find('.glyphicon-remove').removeClass('glyphicon-remove').addClass('glyphicon-search');
    $(this).removeClass('clear-filter').addClass('submit-site-filter');
  });

  $('input#new-cust-name').blur(function () {
    if($(this).val() !== ''){
      var tempString = $(this).val();
      var newTempString = tempString.replace(stringFilter, '');
      if($('input#new-subdomain').val() === ''){
        $('input#new-subdomain').val(newTempString);
      }
      if($('input#new-site-name').val() === ''){
        $('input#new-site-name').val(newTempString);
      }
    }
  });
})(jQuery);