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
        public static Database db = Database.GetInstance;

        static void Main(string[] args)
        {
            //TestDB();\
            //TestRegex();
            //TestRegex2();


            bool value1 = ParseQuery("Create Table tabla1 (column1 int, column2 varchar)");
            bool value2 = ParseQuery("INSErT INTO table_name (1,2,3) VALUES (1)");


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
            string pattern = @"(?i)CREATE\s*TABLE\s*(\S+)\s*\((.*)\)";
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

        public static bool ParseQuery(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return false;
            }

            query = query.TrimStart().TrimEnd();

            string pattern_create = @"(?i)CREATE\s*TABLE\s*(\S+)\s*\((.*)\)";
            string pattern_insert = @"(?i)INSERT\s*INTO\s*(\S+)\s*(\S+)?\s*VALUES\s*\((\S+)\)";

            if (Regex.Matches(query, pattern_create).Count > 0)
            {
                //We have a create query
                string[] matches = Regex.Split(query, pattern_create);
                return ParseCreateStatement(matches);

            }else if(Regex.Matches(query, pattern_insert).Count > 0)
            {
                //We have an insert query
                string[] matches = Regex.Split(query, pattern_insert);
                return ParseCreateStatement(matches);

            }

            return false; //There should be no other at the moment.

        }

        /// <summary>
        /// Creates a table in the database based on the matches of the Regex.
        /// </summary>
        /// <param name="matches"></param>
        /// <returns></returns>
        static bool ParseCreateStatement(string[] matches)
        {
            if(matches.Length != 4)
            {
                //A standard creation query returns 4 matches (the first two empty)
                return false;
            }
            if((string.IsNullOrWhiteSpace(matches[1])) || (string.IsNullOrWhiteSpace(matches[2])))
            {
                //Cannot contain empty name or columns
                return false;
            }

            string tableName = matches[1];
            string columnsData = matches[2];

            List<string> columns_names = new List<string>();
            List<Type> columns_types = new List<Type>();

            try
            {
                string[] columnsDataSplit = columnsData.Split(',');
                for(int i = 0; i < columnsDataSplit.Length; i++)
                {
                    string columnDataIndividual = columnsDataSplit[i].TrimStart().TrimEnd();
                    string[] data = columnDataIndividual.Split(' ');

                    if((data == null) || (data.Length != 2))
                    {
                        return false;
                    }

                    data[1] = data[1].ToUpper();

                    //We now check if the type of column exist in the database dictionary
                    if (!Database.typesDictionary.ContainsKey(data[1]))
                    {
                        return false;
                    }

                    columns_names.Add(data[0]);
                    columns_types.Add(Database.typesDictionary[data[1]]);
                }

                return db.Create(tableName, columns_names.ToArray(), columns_types.ToArray());
                
            }catch(Exception e)
            {
                return false;
            }
            
            return false;
        }

        static bool ParseInsertStatement(string[] matches)
        {
            if ((matches == null) || (matches.Length<4) || (matches.Length > 5))
            {
                return false;
            }




            return false;
        }

    }
}
