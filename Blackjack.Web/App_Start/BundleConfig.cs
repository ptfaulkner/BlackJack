using System.Web;
using System.Web.Optimization;
using System.Web.Optimization.React;
using React;

namespace Blackjack.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/bundles/style").Include(
                        "~/Content/Blackjack.css"));

            bundles.Add(new JsxBundle("~/bundles/react-blackjack").Include(
                        "~/Scripts/blackjack.jsx",
                        "~/Scripts/newPlayer.jsx"));
        }
    }
}
