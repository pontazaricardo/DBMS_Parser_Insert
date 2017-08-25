using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using dbms_objects_data;

namespace Test_Functions
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] table01_columns = new string[] { "id", "name", "last name", "address" };
            Type[] table01_types = new Type[] { typeof(int), typeof(string), typeof(string), typeof(string) };

            Table table_test01 = new Table();
            bool isTableCreated_01 = table_test01.CreateTable(table01_columns, table01_types);
        



        }
    }
}
