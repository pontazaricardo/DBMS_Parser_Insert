using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Exceptions;

namespace test_dbms_02
{
    class QueryDefragmenterString
    {

        public List<string> StringList_Tables = new List<string>();
        public List<string> StringList_Columns = new List<string>();
        public List<string> StringList_Conditions = new List<string>();

        public QueryDefragmenterString(string str)
        {
            //We need to split the string
            string rawColumns = str.Substring(7, str.IndexOf("FROM") - 8);
            string subquery1 = str.Substring(str.IndexOf("FROM") + 5);

            string rawTables = "";
            string subquery2 = "";

            if (subquery1.Contains("WHERE"))
            {
                //It contains a where clause


            }else
            {
                //It does not contain a where clause
                if (subquery1.Contains(","))
                {
                    //If there are two tables, then we need to raise an exception because we have two columns and no join.
                    throw new InvalidSelectQueryException("SELECT query contains more than one column but no join condition.");
                }



            }
            
            /*string rawTables = subquery1.Substring(0, subquery1.IndexOf("WHERE")-1);
            string subquery2 = subquery1.Substring(subquery1.)*/
        }

    }
}
