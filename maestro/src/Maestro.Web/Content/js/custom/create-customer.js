(function() {
    var CreateCustomer = Backbone.Model.extend({
        defaults: {
            customerName: "",
            subdomainName: "",
            firstSiteName: "",
            contactPhone: "",
            serverMessage: ""
        }
    });

    var CreateCustomerView = Backbone.View.extend({
        el: "#create-customer",
        type: "CreateCustomerView",
        initialize: function () {
            this.model.on("change", this.onModelChange, this);
            this.model.url = this.$el.find("form").attr("action");
        },
        onModelChange: function (event) {
            console.log("model changed: ", event);
        },
        events: {
            "change #CustomerName": "onChangeCustomerName",
            "blur #CustomerName": "onBlurCustomerName",
            "change #SubdomainName": "onChangeSubdomainName",
            "change #FirstSiteName": "onChangeFirstSiteName",
            "change #ContactPhone": "onChangeContactPhone",
            "click #submit-customer": "saveCustomer",
            "click #cancel-button" : "onClickCancel"
        },
        //Fix for MS-213
        onBlurCustomerName: function(event){
            var customerNameFilter = /[\!\@\#\$\%\^\-\_\s]/g;
            
            if(event.target.value !== ''){
              var tempString = event.target.value;
              var newTempString = tempString.replace(customerNameFilter, '');
              var subDomain = this.$el.find("#SubdomainName");
              var firstSite = this.$el.find("#FirstSiteName");
              if(subDomain.val() === ''){
                subDomain.val(newTempString).change();
              }
              if(firstSite.val() === ''){
                firstSite.val(newTempString).change();
              }
            }
        },
        //End fix for MS-213
        onChangeCustomerName: function (event) {
            this.model.set({ customerName: event.target.value });
        },

        onChangeSubdomainName: function(event) {
            this.model.set({ subdomainName: event.target.value });
        },

        onChangeFirstSiteName: function(event) {
            this.model.set({ firstSiteName: event.target.value });
        },

        onChangeContactPhone: function(event) {
            this.model.set({ contactPhone: event.target.value });
        },

        saveCustomer: function (event) {
            var thisView = this,
                $submitButton = this.$el.find('#submit-customer');

            if (this.$el.find("form").valid()) {
                $submitButton.prop('disabled', true);

                this.model.save(null, {
                    success: function (model, result) {
                        if (result.success) {
                            window.location = result.redirectUrl;
                        } else {
                            thisView.displayErrorMessage(result.message);
                            $submitButton.prop('disabled', false);
                        }
                    },
                    error: function (model, result) {
                        $submitButton.prop('disabled', false);
                        thisView.displayErrorMessage("An internal server error occured: " + result);
                    }
                });
            }
        },

        onClickCancel: function () {
            this.$el.find("#CustomerName").val("");
            this.$el.find("#SubdomainName").val("");
            this.$el.find("#FirstSiteName").val("");
            this.$el.find("#ContactPhone").val("");
            this.model.clear();

            var validationMessageDiv = this.$el.find('.field-validation-error');
            validationMessageDiv.html("");
            validationMessageDiv.removeClass("field-validation-error");
            validationMessageDiv.addClass("field-validation-valid");

            var validator = this.$el.find("form").validate();
            validator.resetForm();

            this.$el.find('#server-error-message').text('');
        },

        displayErrorMessage: function(errorMessgae) {
            this.$el.find('#server-error-message').text(errorMessgae);
        },

        render: function() {
        }
    });

    var newCustomer = new CreateCustomer({
        
    });

    var modelView = new CreateCustomerView({ model: newCustomer });

}());