using System.Web.Optimization;

namespace Maestro.Web
{
    /// <summary>
    /// BundleConfig.
    /// </summary>
    public class BundleConfig
    {
        /// <summary>
        /// Registers the bundles.
        /// </summary>
        /// <param name="bundles">The bundles.</param>
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/js/jqueryval").Include(
                "~/Content/js/libs/jquery.validate*",
                "~/Content/js/custom/validation/file-extension-validation.js",
                "~/Content/js/custom/validation/file-size-validation.js",
                "~/Content/js/custom/validation/image-dimensions-validation.js",
                "~/Content/js/custom/validation/enforce-true-validation.js",
                "~/Content/js/custom/validation/integer-validation.js",
                "~/Content/js/custom/validation/notification-message.js"));

            bundles.Add(new ScriptBundle("~/bundles/dataTables",
                "http://cdn.datatables.net/1.10.7/js/jquery.dataTables.min.js").Include(
                "~/Content/js/libs/jquery.dataTables-1.10.7.js"));
            bundles.Add(new ScriptBundle("~/bundles/jquery-highlight",
                "https://cdn.datatables.net/plug-ins/1.10.7/features/searchHighlight/dataTables.searchHighlight.min.js").Include(
                "~/Content/js/libs/jquery.highlight.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/common").Include(
                "~/Content/js/libs/jquery-{version}.js",
                "~/Content/js/libs/underscore.js",
                "~/Content/js/libs/backbone.js",
                "~/Content/js/libs/backbone.epoxy.min.js",
                "~/Content/js/libs/bootstrap.min.js",
                "~/Content/js/libs/bootstrap-switch.modified.js",
                "~/Content/js/custom/common.js",
                "~/Content/js/custom/controls/session-timeout.js",
                "~/Content/js/custom/controls/file-picker.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/login").Include(
                "~/Content/js/custom/login.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/activate-account")
                .Include("~/Content/js/custom/activate-account.js"));

            #region CustomerArea

            bundles.Add(new ScriptBundle("~/bundles/js/customerslist").Include(
                "~/Content/js/custom/create-customer.js",
                "~/Content/js/custom/search-customers.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/customer-careelements").Include(
                "~/Content/js/custom/customer-careelements.js",
                "~/Content/js/customer/CareBuilder/add-question.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/customer-users").Include(
                "~/Content/js/custom/customer-users.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/customer-edit-user").Include(
                "~/Content/js/custom/customer-edit-user.js"));

            #endregion

            #region SettingsArea

            bundles.Add(new ScriptBundle("~/bundles/js/create-admin").Include(
                "~/Content/js/custom/create-admin.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/edit-admin").Include(
                "~/Content/js/custom/edit-admin.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/admin-users").Include(
                "~/Content/js/custom/admin-users.js"
                ));

            #endregion

            #region Css

            bundles.Add(new StyleBundle("~/bundles/css")
                .Include("~/Content/css/bootstrap-switch.min.css", new CssRewriteUrlTransform())
                .Include("~/Content/css/bootstrap.min.css", new CssRewriteUrlTransform())
                .Include("~/Content/css/bootstrap-datetimepicker.css", new CssRewriteUrlTransform())
                .Include("~/Content/css/bootstrap-chosen.css", new CssRewriteUrlTransform())
                .Include("~/Content/css/tokenfield-typeahead.css", new CssRewriteUrlTransform())
                .Include("~/Content/css/bootstrap-tokenfield.css", new CssRewriteUrlTransform())
                .Include("~/Content/css/bootstrap-select.css", new CssRewriteUrlTransform())
                .Include("~/Content/css/bootstrap-slider.css", new CssRewriteUrlTransform())
                .Include("~/Content/css/jquery-ui.css", new CssRewriteUrlTransform())
                .Include("~/Content/css/main.css", new CssRewriteUrlTransform())
                .Include("~/Content/css/healthharmony.css", new CssRewriteUrlTransform())
                .Include("~/Content/css/font-awesome.min.css", new CssRewriteUrlTransform())
            );

            #endregion
        }
    }
}