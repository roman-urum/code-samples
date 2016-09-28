(function () {
  
  'use strict';

  var view;
  var source = $("#form-template").html();

  var createCustomer = new Thorax.Model({
    defaults: {
      customerName: "",
      subdomainName: "",
      firstSiteName: "",
      contactPhone: "",
      serverMessage: ""
    }
  });

  view = new Thorax.View({
    model: createCustomer,
    events: {
      "change #CustomerName": "onChangeCustomerName",
      "blur #CustomerName": "onBlurCustomerName",
      "change #SubdomainName": "onChangeSubdomainName",
      "change #FirstSiteName": "onChangeFirstSiteName",
      "change #ContactPhone": "onChangeContactPhone",
      "click #cancel-button" : "onClickCancel",
      "click #submit-customer": "saveCustomer",
      rendered: function () { }
    },
    onBlurCustomerName: function(event){
        var customerNameFilter = /[\!\@\#\$\%\^\-\_\s]/g;

        if (event.target.value !== '') {
          var tempString = event.target.value,
              newTempString = tempString.replace(customerNameFilter, ''),
              subDomain = this.$el.find("#SubdomainName"),
              firstSite = this.$el.find("#FirstSiteName");
          if(subDomain.val() === ''){
            subDomain.val(newTempString).change();
          }
          if(firstSite.val() === ''){
            firstSite.val(newTempString).change();
          }
        }
    },
    onChangeCustomerName: function (event) {
      this.model.attributes.defaults.customerName = event.target.value;
    },
    onChangeSubdomainName: function (event) {
      this.model.attributes.defaults.subdomainName = event.target.value;
    },
    onChangeFirstSiteName: function (event) {
      this.model.attributes.defaults.firstSiteName = event.target.value; 
    },
    onChangeContactPhone: function (event) {
      this.model.attributes.defaults.contactPhone = event.target.value; 
    },
    saveCustomer: function (event) {
      /*var thisView = this;
      if (this.$el.find("form").valid()) {
        this.model.save(null, {
          success: function (model, result) {
            if (result.success) {
                window.location = "/Customer/" + result.customerId + "/Settings/General";
            } else {
                thisView.displayErrorMessage(result.message);
            }
          },
          error: function(model, result) {
            thisView.displayErrorMessage("An internal server error occured: " + result);
          }
        });
      }*/
      alert('Saved for now.');
    },
    onClickCancel: function () {
      this.$el.find("#CustomerName").val("").change();
      this.$el.find("#SubdomainName").val("").change();
      this.$el.find("#FirstSiteName").val("").change();
      this.$el.find("#ContactPhone").val("").change();

      /*var validationMessageDiv = this.$el.find('.field-validation-error');
      validationMessageDiv.html("");
      validationMessageDiv.removeClass("field-validation-error");
      validationMessageDiv.addClass("field-validation-valid");

      var validator = this.$el.find("form").validate();
      validator.resetForm();

      this.$el.find('#server-error-message').text('');*/
    },
    template: Handlebars.compile(source)
  });

  view.appendTo('#create-customer-modal-form');
}());