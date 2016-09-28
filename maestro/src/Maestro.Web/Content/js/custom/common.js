/* namespaces */

// Maestro root namespace
var Maestro = Maestro || {};

// Method to declare new namespaces
Maestro.namespace = function(namespace) {
    var parts = namespace.split('.'),
        parent = Maestro,
        i;

    if (parts[0] === "Maestro") {
        parts = parts.slice(1);
    }

    for (i = 0; i < parts.length; i += 1) {
        if (typeof parent[parts[i]] === "undefined") {
            parent[parts[i]] = {};
        }
        parent = parent[parts[i]];
    }

    return parent;
};

/* / namespaces */

/* common controls */

(function($){
    jQuery(function() {
        $('.list-collapse').on('click', function (e) {
            e.preventDefault();

            $(this).closest('.list-accordion').find('.accordion-content').fadeToggle();
            $(this).find('.list-arrow').toggleClass('list-arrow-active');
            $(this).toggleClass('active-list');
        });
    
        $('.accordion-expand-all').on('click', function (e) {
            e.preventDefault();

            $('.list-accordion').find('.accordion-content').fadeIn();
            $('.list-arrow').addClass('list-arrow-active');
        });
    
        $('.accordion-collapse-all').on('click', function (e) {
            e.preventDefault();

            $('.list-accordion').find('.accordion-content').fadeOut();
            $('.list-arrow').removeClass('list-arrow-active');
        });        

        // Site Switches
        $('.basic-checkbox').bootstrapSwitch();

        // Tooltops
        $('body').tooltip({ selector: '[data-toggle=tooltip]', html: true });

        // Password expiration modal
        $('#password-expiration-modal').modal({
            backdrop: 'static',
            keyboard: false
        });
    });
})(jQuery);

