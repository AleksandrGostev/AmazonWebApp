using System.Web;
using System.Web.Optimization;

namespace AmazonWebApp
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.IgnoreList.Clear();
            bundles.Add(new ScriptBundle("~/bundle/jquery").Include("~/Scripts/jquery-{version}.min.js"));
            bundles.Add(new ScriptBundle("~/bundle/bootstrap").Include("~/Scripts/bootstrap.min.js"));
            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/*.css"));

        }
    }
}