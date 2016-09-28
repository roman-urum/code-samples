Maestro.namespace("Maestro.pages.Customers");

Maestro.pages.Customers.Filter = (function ($) {
    var $customersTable = $('.ci-user-list .customer-item'),
        // Verifies that row with customer data matches to filter
        verifyCustomerRow = function($customerRow, filter) {
            var $fieldsToSearch = $customerRow.find('.ci-customer-name, .ci-site-name'),
                isMatches = false,
                sitesRowsNotFound = new Array();
            
            $.each($fieldsToSearch, function (index, field) {


                if (verifyField($(field), filter)) {
                    isMatches = true;
                } else if ($(field).hasClass('ci-site-name')) {
                    sitesRowsNotFound.push($(field).closest('.row'));
                }


            });

            if (isMatches) {

                $customerRow.show();

                $.each(sitesRowsNotFound, function (index, item) {
                    if ($(item)[0] !== $customerRow[0] ) $(item).hide();
                });

            } else {
                $customerRow.hide();
            }
        },

        // Verifies that field value matches to request. Returns true if matches.
        verifyField = function ($field, filter) {
            
            var fieldValue = $field.attr('search-value'),
                matchIndex = fieldValue.toLowerCase().indexOf(filter);

            //console.log($field.attr('class') + ' : ' + $field.attr('search-value') + ' : ' + matchIndex);

            if (matchIndex >= 0) {

                $field.html(highlightText(fieldValue, matchIndex, filter.length));

                //expand customers sites list
                if ($field.closest(".accordion-content").length) {
                    $field.closest(".accordion-content").fadeIn();
                    $field.closest(".list-accordion").find('.list-arrow').addClass('list-arrow-active');
                    $field.closest(".list-accordion").find('.list-collapse').addClass('active-list');
                    
                }

                //console.log('highlight ' + fieldValue);

                return true;
            } else {
                
                $field.html(fieldValue);

                //console.log('do not highligh ' + fieldValue);

                return false;
            }
        },

        // highlights text placed in specified range. Returns html.
        highlightText = function (text, start, end) {
            return text.substring(0, start) + '<span class="search-result">' +
                text.substring(start, start + end) + '</span>' +
                text.substring(start + end, text.length);
        };

    return {
        // Initiates search by customers table with specified filter.
        SearchSites: function(filter) {
            $.each($customersTable, function (index, row) {
                verifyCustomerRow($(row), filter.toLowerCase());
            });
        },
        ResetSearch: function() {
            $.each($customersTable, function (index, row) {

                var fieldsToSearch = $(row).find('.ci-customer-name, .ci-site-name');

                $.each(fieldsToSearch, function(cellIndex, cell) {
                    $(cell).text($(cell).attr('search-value'));
                });

                $(row).show();

                $.each($(row).find('.row'), function(index, siteRow) {
                    $(siteRow).show();
                });

                //collapse customers sites list
                $(row).find(".accordion-content").fadeOut();
                $(row).find('.list-arrow').removeClass('list-arrow-active');
                $(row).find('.list-collapse').removeClass('active-list');

            });
        },
        GetSearchResultCount: function () {
            return $(".ci-user-list div.user-item:visible").length;
        }
    };
}(jQuery));

(function ($) {
    jQuery(function () {
        var filter = Maestro.pages.Customers.Filter,
            $searchForm = $('.site-filter'),
            $searchButton = $searchForm.find('.submit-site-filter'),
            $searchIcon = $searchForm.find('.submit-site-filter .glyphicon'),
            $filter = $searchForm.find('.filter-input'),
            $searchResultCount = $('#search-result-sites-count'),
            startSearchHandler = function() {
                //console.log($filter.val());
                filter.SearchSites($filter.val());

                displaySearchResultCount(filter.GetSearchResultCount());

                $searchIcon.removeClass('glyphicon-search');
                $searchIcon.addClass('glyphicon-remove');
            },
            resetSearch = function() {

                filter.ResetSearch();

                displaySearchResultCount(filter.GetSearchResultCount());

                $filter.val("");
                $searchIcon.removeClass('glyphicon-remove');
                $searchIcon.addClass('glyphicon-search');
            },
            displaySearchResultCount = function(cnt) {
                if (cnt > 0) {
                    $searchResultCount.text(cnt + " Sites Shown");
                } else {
                    $searchResultCount.text("No sites found matching your keyword.");
                }
            },
            createNewCustomerButton = $(".add-cust");
            
        $searchButton.click(function () {
            console.log('click search');
            if ($searchIcon.hasClass('glyphicon-search')) {
                console.log('start search from click');
                startSearchHandler();
            } else if ($searchIcon.hasClass('glyphicon-remove')) {
                console.log('reset from click');
                resetSearch();
            }
        });

        $filter.keydown(function(e) {
            if (e.keyCode == 9 && $filter.val().length > 0) {
                startSearchHandler();
            }
        });

        $filter.keyup(function (e) {
            if ((e.keyCode == 13) && $filter.val().length > 0) {
                console.log('start search from keyup');
                startSearchHandler();
            } else if (e.keyCode == 8 && $filter.val().length <= 0) {
                console.log('reset from keyup');
                resetSearch();
            }
        });

        $searchForm.submit(function() {
            event.preventDefault();
        });

        createNewCustomerButton.click(function() {
            $("#create-customer").modal({
                keyboard: false,
                backdrop: 'static'
            });
        });
    });
})(jQuery);