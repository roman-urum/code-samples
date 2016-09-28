using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Maestro.Common.Extensions;
using Maestro.Web.Resources;

namespace Maestro.Web.Controls
{
    /// <summary>
    /// Contains extension methods for HtmlHelper to generate file picker control.
    /// </summary>
    public static class FilePicker
    {
        private const string FilePathInputIdTemplate = "{0}Path";
        private const string BrowseButtonTemplate = "<button type=\"button\" class=\"btn btn-default\">{0}</button>";
        private const string FilePickerContainerTemplate = "<div class=\"ci-file-picker\">{0}{1}{2}</div>";

        private static readonly KeyValuePair<string, object> FileInputPredefinedSettings =
            new KeyValuePair<string, object>("type", "file");

        private static readonly IDictionary<string, object> FilePathPredefinedSettings =
            new Dictionary<string, object> {{"class", "disabled"}};

        /// <summary>
        /// Generates set of controls to upload file.
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="htmlHelper"></param>
        /// <param name="property"></param>
        /// <returns></returns>
        public static MvcHtmlString FilePickerFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> property, object htmlAttributes)
        {
            var htmlAttributesDictionary = htmlAttributes.ToDictionary();
            htmlAttributesDictionary.Add(FileInputPredefinedSettings);

            string filePathId = FilePathInputIdTemplate.FormatWith(property.Name);
            string browseButton = BrowseButtonTemplate.FormatWith(GlobalStrings.Common_BrowseButtonText);
            MvcHtmlString filePickerHtml = htmlHelper.TextBoxFor(property, htmlAttributesDictionary);
            MvcHtmlString filePathHtml = htmlHelper.TextBox(filePathId, string.Empty, FilePathPredefinedSettings);

            string controlHtmlString = FilePickerContainerTemplate.FormatWith(
                filePickerHtml.ToString(),
                filePathHtml.ToString(),
                browseButton);

            return new MvcHtmlString(controlHtmlString);
        }
    }
}