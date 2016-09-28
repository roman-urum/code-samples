'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Customer/CareBuilder/CareBuilderApp/AppGlobalVariables',
    'Controllers/Helpers',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Models/SelectionAnswerSetModel',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Models/ScaleAnswerSetModel',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Models/TextMediaElementModel',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Models/QuestionElementModel',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Views/CareElements/SelectionAnswerSetListItemView',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Views/CareElements/ScaleAnswerSetListItemView',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Views/CareElements/QuestionElementListItemView',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Views/CareElements/TextMediaElementListItemView',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Views/CareElements/OpenEndedAnswerSetListItemView'
], function (
        $,
        _,
        Backbone,
        app,
        Helpers,
		SelectionAnswerSetModel,
		ScaleAnswerSetModel,
		TextMediaElementModel,
		QuestionElementModel,
		SelectionAnswerSetListItemView,
		ScaleAnswerSetListItemView,
		QuestionElementListItemView,
		TextMediaElementListItemView,
		OpenEndedAnswerSetListItemView
 ) {
    return Backbone.View.extend({

        render: function () {

        	var elementView;

            if( !this.model.get('isDisplay') )
            	return false;

            switch (this.model.get('type')) {
                case 4:
                    {
                        var questionElementModel = new QuestionElementModel({
                            id: this.model.get('id'),
                            questionElementString: {
                                value: this.model.get('name')
                            },
                            tags: this.model.get('tags')
                        });
                        elementView = new QuestionElementListItemView({ model: questionElementModel });
                        break;
                    }
                case 5:
                    {
                        var textMediaElementModel = new TextMediaElementModel({
                            id: this.model.get('id'),
                            name: this.model.get('name'),
                            tags: this.model.get('tags')
                        });

                        textMediaElementModel.set('id', this.model.get('id'));

                        elementView = new TextMediaElementListItemView({ model: textMediaElementModel });
                        break;
                    }
                case 6:
                    {
                        var scaleAnswerSetModel = new ScaleAnswerSetModel({
                            id: this.model.get('id'),
                            name: this.model.get('name'),
                            tags: this.model.get('tags')
                        });
                        elementView = new ScaleAnswerSetListItemView({ model: scaleAnswerSetModel });
                        break;
                    }
                case 7:
                    {
                        var selectionAnswerSetModel = new SelectionAnswerSetModel({
                            id: this.model.get('id'),
                            name: this.model.get('name'),
                            tags: this.model.get('tags')
                        });
                        elementView = new SelectionAnswerSetListItemView({ model: selectionAnswerSetModel });
                        break;
                    }
                case 8:
                    {
                        var OpenEndedAnswerSetModel = Backbone.Model.extend({});
                        var openEndedAnswerSetModel = new OpenEndedAnswerSetModel({
                            id: this.model.get('id'),
                            name: this.model.get('name'),
                            tags: this.model.get('tags')
                        });
                        elementView = new OpenEndedAnswerSetListItemView({ model: openEndedAnswerSetModel });
                        break;
                    }
            }

            this.setElement( elementView.render().el );

            return this;

        }


    });
});