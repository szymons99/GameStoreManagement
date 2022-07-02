using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Games_EF.Helpers
{
    public class AddNumbersHelper
    {
        public static string add(string s)
        {
            int result = 0;
            int num;
            for (int i = 0; i < s.Length; i++)
            {
                int.TryParse(s[i].ToString(), out num);
                result = +num;
            }
            return result.ToString();
        }
    }
}
