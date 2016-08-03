using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace FilmDukkaniMvc.Helpers
{
    public static class HtmlHelpers
    {
        public static string Kisalt(this HtmlHelper helper, string yazi, int uzunluk)
        {
            if (yazi.Length < uzunluk)
            {
                return yazi;
            }
            else
            {
                return yazi.Substring(0, uzunluk) + "...";
            }
        }
        public static string ParaBirim(this HtmlHelper helper, decimal para, string birim = "")
        {
            if (!string.IsNullOrEmpty(birim))
            {
                return para + " " + birim;
            }
            else
            {
                return para.ToString("C");
            }
        }
    }
}
