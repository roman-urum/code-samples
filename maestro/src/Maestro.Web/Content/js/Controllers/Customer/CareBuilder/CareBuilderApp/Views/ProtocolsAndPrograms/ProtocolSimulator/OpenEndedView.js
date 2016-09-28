'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Customer/CareBuilder/CareBuilderApp/AppGlobalVariables',
], function (
    $,
    _,
    Backbone,
    app
) {
    return Backbone.View.extend({

        template: _.template('<div class="simulator-content-question simulator-content-question-full pull-left">\
                                <div class="question-text">\
                                    <div class="text">\
                                        <%=questionElementString.value%><br>\
                                        <textarea style="width: 900px; height: 250px; margin-top: 20px"></textarea>\
                                    </div>\
                                </div>\
                                <div class="question-nav">\
                                    <a class="nav-next-question js-next-question">Next Step</a>\
                                </div>\
                            </div>'),


        render: function () {
            this.$el.html(this.template(this.model.attributes));

            return this;
        }
    });
});