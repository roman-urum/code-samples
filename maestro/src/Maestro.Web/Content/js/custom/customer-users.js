Maestro.namespace("Maestro.pages");

// View for tab with customer general settings
Maestro.pages.CustomerUsers = Backbone.View.extend({
    el: "#customer-users",

    initialize: function() {
        this.$dataTable = $(".ci-admin-list table").DataTable({
            paging: false,
            pageLength: 10,
            bLengthChange: false,
            bInfo: false,
            ordering: true,
            searching: true,
            bFilter: true,
            aoColumnDefs: [
                {
                    bSearchable: false,
                    aTargets: [ 4 ]
                }
            ],
            oLanguage: {
                sEmptyTable: "No users found matching your criteria."
            }
        });

        var table = this.$dataTable;
        table.on("draw", function () {
            var body = $(table.table().body());

            body.unhighlight();
            body.highlight(table.search());
        });

        this.$dataTableApi = $(".ci-admin-list table").dataTable();
    },

    events: {
        "switchChange.bootstrapSwitch .basic-checkbox": "onUserSwitch",
        "change select#sort-users": "onSortUsers",
        "change select#filter-users-by-role": "onFilterUsersByRole",
        "change select#filter-users-by-site": "onFilterUsersBySite",
        "change select#filter-users-by-status": "onFilterUsersByStatus",
        "keyup .user-filter input": "onFilterUsersByTerm",
        "click .user-filter .input-group-addon": "onClearFilterUsersByTerm"
    },

    onUserSwitch: function (event, state) {

        var userId = $(event.target).parents('.js-user-row').attr("user-id");

        $.post("/Settings/SetEnabledState", {
            userId: userId,
            isEnabled: state
        });

        var stringifiedState = state ? "Enabled" : "Disabled";

        this.$dataTableApi.api().cell($(event.target).parents('.js-user-row').find('td.stringified-status')).data(stringifiedState);
    },

    onSortUsers: function (event) {
        var selectedValue = $(event.target).find("option:selected").val();
        
        switch (selectedValue) {
            case "1":
                this.$dataTableApi.fnSort([[5, "asc"]]);
                break;
            case "2":
                this.$dataTableApi.fnSort([[5, "desc"]]);
                break;
            case "3":
                this.$dataTableApi.fnSort([[6, "asc"]]);
                break;
            case "4":
                this.$dataTableApi.fnSort([[6, "desc"]]);
                break;
            default:
                this.$dataTableApi.fnSort([[5, "asc"]]);
        }
    },

    onFilterUsersByRole: function (event) {
        var selectedValue = $(event.target).find("option:selected").val();

        if (selectedValue === "All") {
            selectedValue = "";
        }

        this.$dataTableApi.fnFilter(selectedValue, 2);
    },

    onFilterUsersBySite: function (event) {
        var selectedValue = $(event.target).find("option:selected").val();

        if (selectedValue === "All") {
            selectedValue = "";
        }

        this.$dataTableApi.fnFilter(selectedValue, 7);
    },

    onFilterUsersByStatus: function (event) {
        var selectedValue = $(event.target).find("option:selected").val();

        if (selectedValue === "All") {
            selectedValue = "";
        }

        this.$dataTableApi.fnFilter(selectedValue, 8);
    },

    onFilterUsersByTerm: function (event) {
        var selectedValue = $(event.target).val();
        var searchIcon = $(".user-filter .glyphicon");

        if (selectedValue.length > 0) {
            searchIcon.removeClass("glyphicon-search");
            searchIcon.addClass("glyphicon-remove");

            this.$dataTableApi.fnFilter(selectedValue);
        } else {
            searchIcon.removeClass("glyphicon-remove");
            searchIcon.addClass("glyphicon-search");

            this.$dataTableApi.fnFilter("");
        }
    },

    onClearFilterUsersByTerm: function (event) {
        $(".user-filter input").val("");
        $(".user-filter input").trigger("keyup");
    },
});

(function () {
    var newSiteView = new Maestro.pages.CustomerUsers();
})();