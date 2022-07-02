using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Games_EF.Helpers
{
    public class StripHtmlHelper
    {
        public static string Strip(string html)
        {
            return Regex.Replace(html, "<.*?>", String.Empty);
        }
    }
}
