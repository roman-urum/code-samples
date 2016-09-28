using System.Web.Mvc;

namespace Maestro.Web.Extensions
{
    /// <summary>
    /// HtmlHelperExtensions.
    /// </summary>
    public static class HtmlHelperExtensions
    {
        /// <summary>
        /// Renders the application version.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <returns></returns>
        public static string RenderAppVersion(this HtmlHelper target)
        {
            return "v=" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
        }
    }
}