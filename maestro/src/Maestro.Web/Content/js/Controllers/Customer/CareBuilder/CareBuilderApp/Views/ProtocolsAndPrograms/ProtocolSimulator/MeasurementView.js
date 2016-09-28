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
                                        <%=name%><br>\
                                        <input type="text" style="width: 150px;"><br>\
                                    </div>\
                                </div>\
                                <div class="question-nav">\
                                    <a class="nav-cancel-question js-next-question">Cancel</a>\
                                    <a class="nav-next-question js-next-question">Next Step</a>\
                                </div>\
                            </div>'),

        templateWeightScale: _.template('<div class="simulator-content-question simulator-content-question-full pull-left">\
                                <div class="question-text">\
                                    <div class="text">\
                                        <%=name%><br>\
                                        <div class="measurement-item">\
                                            <input type="text"><br>\
                                            <div class="label">Pounds (Lbs)</div>\
                                        </div>\
                                    </div>\
                                </div>\
                                <div class="question-nav">\
                                    <a class="nav-cancel-question js-next-question">Cancel</a>\
                                    <a class="nav-next-question js-next-question">Next Step</a>\
                                </div>\
                            </div>'),

        templateBloodPressure: _.template('<div class="simulator-content-question simulator-content-question-full pull-left">\
                                <div class="question-text">\
                                    <div class="text">\
                                        <%=name%><br>\
                                        <div class="measurement-item">\
                                            <input type="text"><br>\
                                            <div class="label">Systolic (mmHg)</div>\
                                        </div>\
                                        <div class="measurement-item">\
                                            <input type="text"><br>\
                                            <div class="label">Diastolic (mmHg)</div>\
                                        </div>\
                                        <div class="measurement-item">\
                                            <input type="text"><br>\
                                            <div class="label">Pulse (BPM)</div>\
                                        </div>\
                                    </div>\
                                </div>\
                                <div class="question-nav">\
                                    <a class="nav-cancel-question js-next-question">Cancel</a>\
                                    <a class="nav-next-question js-next-question">Next Step</a>\
                                </div>\
                            </div>'),

        templatePeakFlow: _.template('<div class="simulator-content-question simulator-content-question-full pull-left">\
                                <div class="question-text">\
                                    <div class="text">\
                                        <%=name%><br>\
                                        <div class="measurement-item">\
                                            <input type="text"><br>\
                                            <div class="label">Peak Flow (L/min)</div>\
                                        </div>\
                                        <div class="measurement-item">\
                                            <input type="text"><br>\
                                            <div class="label">FEV1 (L)</div>\
                                        </div>\
                                    </div>\
                                </div>\
                                <div class="question-nav">\
                                    <a class="nav-cancel-question js-next-question">Cancel</a>\
                                    <a class="nav-next-question js-next-question">Next Step</a>\
                                </div>\
                            </div>'),

        templateGlucose: _.template('<div class="simulator-content-question simulator-content-question-full pull-left">\
                                <div class="question-text">\
                                    <div class="text">\
                                        <%=name%><br>\
                                        <div class="measurement-item">\
                                            <input type="text"><br>\
                                            <div class="label">Glucose (mg/dL)</div>\
                                        </div>\
                                    </div>\
                                </div>\
                                <div class="question-nav">\
                                    <a class="nav-cancel-question js-next-question">Cancel</a>\
                                    <a class="nav-next-question js-next-question">Next Step</a>\
                                </div>\
                            </div>'),

        templateThermometer: _.template('<div class="simulator-content-question simulator-content-question-full pull-left">\
                                <div class="question-text">\
                                    <div class="text">\
                                        <%=name%><br>\
                                        <div class="measurement-item">\
                                            <input type="text"><br>\
                                            <div class="label">Temperature (&#8457;)</div>\
                                        </div>\
                                    </div>\
                                </div>\
                                <div class="question-nav">\
                                    <a class="nav-cancel-question js-next-question">Cancel</a>\
                                    <a class="nav-next-question js-next-question">Next Step</a>\
                                </div>\
                            </div>'),

        templatePulseOximeter: _.template('<div class="simulator-content-question simulator-content-question-full pull-left">\
                                <div class="question-text">\
                                    <div class="text">\
                                        <%=name%><br>\
                                        <div class="measurement-item">\
                                            <input type="text"><br>\
                                            <div class="label">Oxygen (%)</div>\
                                        </div>\
                                        <div class="measurement-item">\
                                            <input type="text"><br>\
                                            <div class="label">Pulse (BPM)</div>\
                                        </div>\
                                    </div>\
                                </div>\
                                <div class="question-nav">\
                                    <a class="nav-cancel-question js-next-question">Cancel</a>\
                                    <a class="nav-next-question js-next-question">Next Step</a>\
                                </div>\
                            </div>'),

        templatePedometer: _.template('<div class="simulator-content-question simulator-content-question-full pull-left">\
                                <div class="question-text">\
                                    <div class="text">\
                                        <%=name%><br>\
                                        <div class="measurement-item">\
                                            <input type="text"><br>\
                                            <div class="label">Walking (steps/day)</div>\
                                        </div>\
                                        <div class="measurement-item">\
                                            <input type="text"><br>\
                                            <div class="label">Running (steps/day)</div>\
                                        </div>\
                                    </div>\
                                </div>\
                                <div class="question-nav">\
                                    <a class="nav-cancel-question js-next-question">Cancel</a>\
                                    <a class="nav-next-question js-next-question">Next Step</a>\
                                </div>\
                            </div>'),


        render: function () {
            var measurementType = this.model.get('measurementType');

            switch (measurementType) {
                case 1: //"Glucose Meter"
                    this.$el.html(this.templateGlucose(this.model.attributes));
                    break;
                case 2: // "Pulse Oximeter"
                    this.$el.html(this.templatePulseOximeter(this.model.attributes));
                    break;
                case 3: // "Blood Pressure Monitor"
                    this.$el.html(this.templateBloodPressure(this.model.attributes));
                    break;
                case 4: // "Thermometer"
                    this.$el.html(this.templateThermometer(this.model.attributes));
                    break;
                case 5: // "Peak Flow Meter"
                    this.$el.html(this.templatePeakFlow(this.model.attributes));
                    break;
                case 6: // "Weight Scale"
                    this.$el.html(this.templateWeightScale(this.model.attributes));
                    break;
                case 7: // "Pedometer"
                    this.$el.html(this.templatePedometer(this.model.attributes));
                    break;
                default:
                    this.$el.html(this.template(this.model.attributes));
                    break;
            }

            return this;
        }
    });
});