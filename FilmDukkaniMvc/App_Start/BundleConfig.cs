using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace FilmDukkaniMvc.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/jquery.*",
                "~/Scripts/tabs-accordian.js",
                "~/Scripts/cloud-zoom.1.0.2.js",
                "~/Scripts/jquery.js")
                );
            bundles.Add(new ScriptBundle("~/bundles/knockout").Include(
                "~/Scripts/knockout-3.1.0.js",
                "~/Scripts/MyJSDDL.js")
                );


            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                    "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/Content/css").Include(
               "~/Content/jquery.selectBox.css",
               "~/Content/site.css",
               "~/Content/style.css")
                );
            bundles.Add(new ScriptBundle("~/Content/bootstrap").Include(
               "~/Content/bootstrap.css"
            ));
            //BundleTable.EnableOptimizations = true;

        }
    }
}
