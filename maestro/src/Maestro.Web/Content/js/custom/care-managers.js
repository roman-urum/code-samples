Maestro.namespace('Maestro.pages');

// Model for any manager
Maestro.pages.ManagerModel = Backbone.Model.extend({
    Id: "",
    Name: "",
    PatientsCount: ""
});

// View for assigned manager.
Maestro.pages.AssignedManagerRow = Backbone.View.extend({
    template: _.template($("#assigned-manager-template").html()),

    initialize: function(options) {
        if (options.rendered) {
            this.model = new Maestro.pages.ManagerModel();
            this.model.set("Id", this.$el.find('[data-container="manager-id"]').val());
            this.model.set("Name", this.$el.find('[data-container="manager-name"]').html());
            this.model.set("PatientsCount", this.$el.find('[data-container="manager-patients"]').val());
        }
    },

    render: function () {
        var rowHtml = this.template(this.model.toJSON());

        this.$el.html(rowHtml);

        return this;
    }
});

// View for not assigned manager.
Maestro.pages.ManagerRow = Backbone.View.extend({
    template: _.template($("#manager-template").html()),

    initialize: function (options) {
        if (options.rendered) {
            this.model = new Maestro.pages.ManagerModel();
            this.model.set("Id", this.$el.find('[data-container="manager-id"]').val());
            this.model.set("Name", this.$el.find('[data-container="manager-name"]').html());
            this.model.set("PatientsCount", this.$el.find('[data-container="manager-patients"]').html());
        }
    },

    render: function () {
        var rowHtml = this.template(this.model.toJSON());

        this.$el.removeAttr('class');
        this.$el.html(rowHtml);

        return this;
    }
});

// View for page
Maestro.pages.CareManagers = Backbone.View.extend({
    el: "#ci-care-managers",

    isChanged: false,

    initialize: function() {
        var self = this,
            managersRows = this.$el.find('.not-assigned-managers .row'),
            assignedManagersRows = this.$el.find('.assigned-managers li');

        this.managersContainer = $('.not-assigned-managers');
        this.assignedManagersContainer = $('.assigned-managers');
        this.patientId = this.$el.find("#PatientId").val();
        this.managers = [];
        this.assignedManagers = [];

        $.each(managersRows, function(index, el) {
            var managerRow = new Maestro.pages.ManagerRow({
                rendered: true,
                el: el
            });

            self.managers.push(managerRow);
        });

        $.each(assignedManagersRows, function(index, el) {
            var managerRow = new Maestro.pages.AssignedManagerRow({
                rendered: true,
                el: el,
            });

            self.assignedManagers.push(managerRow);
        });
    },

    sortByName: function(view1, view2) {
        var name1 = view1.model.get('Name').toLowerCase(),
            name2 = view2.model.get('Name').toLowerCase();

        return ((name1 < name2) ? -1 : ((name1 > name2) ? 1 : 0));
    },

    render: function() {
        var self = this;

        this.isChanged = true;

        this.managersContainer.html('');
        this.assignedManagersContainer.html('');

        this.managers.sort(this.sortByName);
        this.assignedManagers.sort(this.sortByName);

        $.each(this.managers, function(index, el) {
            self.managersContainer.append(el.render().$el);
        });

        $.each(this.assignedManagers, function(index, el) {
            self.assignedManagersContainer.append(el.render().$el);
        });
    },

    events: {
        'click .remove-manager': 'removeSingleCareManager',
        'click .assign-manager': 'assignSingleCareManager',
        'click .save-buttons button': 'saveManagers',
        'click #assign-all': 'assignAll',
        'click #remove-all': 'removeAll',
    },

    assignAll: function() {
        var self = this;

        $.each(this.managers, function(index, el) {
            self.assignCareManager(el);
        });
    },

    removeAll: function() {
        var self = this;

        $.each(this.assignedManagers, function(index, el) {
            self.removeCareManager(el);
        });
    },

    removeSingleCareManager: function(evt) {
        var self = this,
            managerId = $(evt.target).parents('li').find('[data-container="manager-id"]').val();

        $.each(this.assignedManagers, function(index, el) {
            if (el.model.get('Id') == managerId) {
                self.removeCareManager(el);
            }
        });
    },

    assignSingleCareManager: function(evt) {
        var self = this,
            managerId = $(evt.target).parents('.manager-row').find('[data-container="manager-id"]').val();

        $.each(this.managers, function (index, el) {
            if (el != undefined && el.model.get('Id') == managerId) {
                self.assignCareManager(el);
            }
        });
    },

    assignCareManager: function(el) {
        this.managers = $.grep(this.managers, function (item) {
            return item.model.get("Id") != el.model.get("Id");
        });

        var managerRow = new Maestro.pages.AssignedManagerRow({
            model: el.model
        });

        this.assignedManagers.push(managerRow);
        this.render();
    },

    removeCareManager: function(el) {
        this.assignedManagers = $.grep(this.assignedManagers, function(item) {
            return item.model.get("Id") != el.model.get("Id");
        });

        var managerRow = new Maestro.pages.ManagerRow({
            model: el.model
        });

        this.managers.push(managerRow);
        this.render();
    },

    saveManagers: function(evt) {
        var $sender = $(evt.target),
            managersIds = [],
            redirectUrl = $sender.attr('redirect-url');

        $sender.addClass('disabled');

        $.each(this.assignedManagers, function(index, el) {
            managersIds.push(el.model.get('Id'));
        });

        $.post("/CareManagers/AssignCareManagers", {
            patientId: this.patientId,
            assignedManagers: managersIds
        }, function(data) {
            if (data.success && redirectUrl != undefined) {
                window.location.href = redirectUrl;
            } else {
                $sender.removeClass('disabled');
            }
        });
    }
});

(function () {
    var newSiteView = new Maestro.pages.CareManagers();

    jQuery(window).bind('beforeunload', function (e) {

        if (newSiteView.isChanged &&
            $(e.target.activeElement).attr("id") !== "save-and-go-next-tab" &&
            $(e.target.activeElement).attr("id") !== "save-and-exit") {

            var message = "You have made changes to the current tab. Are you sure you want to cancel and discard your changes?";
            e.returnValue = message;

            return message;
        }


    });
})();