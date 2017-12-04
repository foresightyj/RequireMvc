using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Optimization;

namespace HelloMvcBundle
{
    public static class MvcBundle
    {
        private static string ScriptBundleKey = "script-bundle";

        public static IHtmlString RenderScript(string path)
        {
            var bundleId = ScriptBundleKey + new Regex(@"\W+").Replace(path, "-").ToLower();
            var count = HttpContext.Current.Items[bundleId] as int?;
            count = (count ?? 0) + 1;
            HttpContext.Current.Items[bundleId] = count;

            if (count == 1)
            {
                return Scripts.RenderFormat("<script type='text/javascript' class='" + bundleId + "'>{0}</script>", path);
            }
            return new HtmlString(string.Format("<!-- {0} -->", bundleId + " is rendered the " + count + "-th time"));
        }
    }
}