using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Text.RegularExpressions;

namespace dbms_objects
{
    class CommonObject
    {
        public static List<string> GetParameters(string str)
        {
            string extractFuncRegex = @"\b[^()]+\((.*)\)$";
            string extractArgsRegex = @"([^,]+\(.+?\))|([^,]+)";

            //Your test string
            //string test = @"func1(2 * 7, func2(3, 5))";

            var match = Regex.Match(str, extractFuncRegex);
            string innerArgs = match.Groups[1].Value;
            var matches = Regex.Matches(innerArgs, extractArgsRegex);

            List<string> result = matches.Cast<Match>().Select(m => m.Value).ToList();
            return result;
        }
    }
}
