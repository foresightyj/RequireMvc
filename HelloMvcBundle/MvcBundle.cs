using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Optimization;

namespace HelloMvcBundle
{
    public static class MvcBundle
    {
        private static IHtmlString Empty = new HtmlString("");
        private static string ScriptBundleKey = "script:bundle";

        public static IHtmlString RenderScript(string path)
        {
            var bundleId = "script-bundle" + new Regex(@"\W+").Replace(path, "-").ToLower();
            var bundle = Scripts.RenderFormat("<script type='text/javascript' class='" + bundleId + "'>{0}</script>", path);
            var bundles = HttpContext.Current.Items[ScriptBundleKey] as List<IHtmlString>;
            if(bundles == null)
            {
                bundles = new List<IHtmlString>();
                HttpContext.Current.Items[ScriptBundleKey] = bundles;
            }
            bundles.Add(bundle);
            return Empty;
        }
    }
}