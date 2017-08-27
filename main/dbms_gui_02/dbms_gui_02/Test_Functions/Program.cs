using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Text.RegularExpressions;

using dbms_objects_data;

namespace Test_Functions
{
    class Program
    {
        static void Main(string[] args)
        {
            //TestDB();\
            //TestRegex();
            TestRegex2();


        }

        static void TestDB()
        {
            string[] table01_columns = new string[] { "id", "name", "last name", "address" };
            Type[] table01_types = new Type[] { typeof(int), typeof(string), typeof(string), typeof(string) };

            List<string> values = new List<string>() { "1", "test", "lastnametest", "add1" };

            List<string> values02 = new List<string>() { "2", "test02" };
            List<string> values02_columns = new List<string>() { "id", "name" };

            Table table_test01 = new Table();
            bool isTableCreated_01 = table_test01.CreateTable(table01_columns, table01_types);
            bool inserted = table_test01.Insert(values);

            bool inserted02 = table_test01.Insert(values02, values02_columns);

            Database db = Database.GetInstance;
            bool isTableCreated_02 = db.Create("table01", table01_columns, table01_types);
            bool insertTest_02 = db.Insert("table01", values);
            bool insertTest_03 = db.Insert("table01", values02, values02_columns);
            bool insertTest_04 = db.Insert("table02", values02, values02_columns);
        }

        static void TestRegex()
        {
            string pattern = @"(?i)INSERT\s*INTO\s*(\S+)\s*(\S+)?\s*VALUES\s*\((\S+)\)";
            string input = @"INSErT INTO table_name (1,2,3) VALUES (1)";

            bool matchFound = false;

            foreach (Match m in Regex.Matches(input, pattern))
            {
                Console.WriteLine("'{0}' found at index {1}.", m.Value, m.Index);
                matchFound = true;
            }

            if (matchFound)
            {
                string[] matches = Regex.Split(input, pattern);
                foreach (string match in matches)
                {
                    Console.WriteLine("'{0}'", match);
                }

            }
           

        }

        static void TestRegex2()
        {
            string pattern = @"(?i)CREATE\s*TABLE\s*(\S+)\s*\((\S+)\)";
            string input = @"Create Table tabla1 (column1 int, column2 varchar)";

            bool matchFound = false;

            foreach (Match m in Regex.Matches(input, pattern))
            {
                Console.WriteLine("'{0}' found at index {1}.", m.Value, m.Index);
                matchFound = true;
            }

            if (matchFound)
            {
                string[] matches = Regex.Split(input, pattern);
                foreach (string match in matches)
                {
                    Console.WriteLine("'{0}'", match);
                }

            }
        }

    }
}
