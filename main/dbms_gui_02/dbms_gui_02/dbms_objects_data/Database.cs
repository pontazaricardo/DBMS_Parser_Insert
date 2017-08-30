using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Text.RegularExpressions;

namespace dbms_objects_data
{
    public sealed class Database
    {
        private static string pattern_create = @"(?i)CREATE\s*TABLE\s*(\S+)\s*\((.*)\)";
        private static string pattern_insert = @"(?i)INSERT\s*INTO\s*(\S+)\s*(\S+)?\s*VALUES\s*\((\S+)\)";

        private static Dictionary<string, Table> dictionary; //Dictionary that contains the table names and tables.
        public static Dictionary<string, Type> typesDictionary;

        private static readonly object obj = new object();
        private static Database instance = null;


        /// <summary>
        /// Instance of the Database. We use the Singleton design pattern in case the GUI runs in multiple threads.
        /// </summary>
        public static Database GetInstance
        {
            get
            {
                if (instance == null)
                {
                    lock (obj)
                    {
                        if (instance == null)
                        {
                            instance = new Database();
                        }
                    }
                }
                return instance;
            }
        }

        private Database()
        {
            dictionary = new Dictionary<string, Table>();

            typesDictionary = new Dictionary<string, Type>();
            PopulateTypeDictionary();

        }

        public bool ContainsTable(string tablename)
        {
            try
            {
                return dictionary.ContainsKey(tablename);
            }
            catch (Exception e)
            {
                return false;
            }
        }


        private void PopulateTypeDictionary()
        {
            typesDictionary.Add("VARCHAR", typeof(string));
            typesDictionary.Add("INT", typeof(int));
            typesDictionary.Add("DATETIME", typeof(DateTime));
            typesDictionary.Add("BOOL", typeof(bool));
        }



        public bool Create(string name, string[] columns, Type[] types)
        {
            if (string.IsNullOrEmpty(name))
            {
                return false;
            }
            if (dictionary.ContainsKey(name))
            {
                return false;
            }


            Table table = new Table();
            if (!table.CreateTable(columns, types))
            {
                return false;
            }

            try
            {
                dictionary.Add(name, table);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }

            return true;
        }

        public bool Insert(string name, List<string> values, List<string> columns = null)
        {
            if (string.IsNullOrEmpty(name))
            {
                return false;
            }
            if (!dictionary.ContainsKey(name))
            {
                return false;
            }

            bool success = dictionary[name].Insert(values, columns);
            return success;
        }

        public bool Parse(string query)
        {
            if (instance == null)
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(query))
            {
                return false;
            }


            if (Regex.Matches(query, pattern_create).Count > 0)
            {
                //We have a create query
                string[] matches = Regex.Split(query, pattern_create);
                return ParseCreateStatement(matches);

            }
            else if (Regex.Matches(query, pattern_insert).Count > 0)
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
        private bool ParseCreateStatement(string[] matches)
        {
            if (matches.Length != 4)
            {
                //A standard creation query returns 4 matches (the first two empty)
                return false;
            }
            if ((string.IsNullOrWhiteSpace(matches[1])) || (string.IsNullOrWhiteSpace(matches[2])))
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
                for (int i = 0; i < columnsDataSplit.Length; i++)
                {
                    string columnDataIndividual = columnsDataSplit[i].TrimStart().TrimEnd();
                    string[] data = columnDataIndividual.Split(' ');

                    if ((data == null) || (data.Length != 2))
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

                return instance.Create(tableName, columns_names.ToArray(), columns_types.ToArray());

            }
            catch (Exception e)
            {
                return false;
            }
        }

        private bool ParseInsertStatement(string[] matches)
        {
            if ((matches == null) || (matches.Length < 4) || (matches.Length > 5))
            {
                return false;
            }

            string tableName = matches[1];

            if (string.IsNullOrWhiteSpace(tableName))
            {
                return false;
            }
            else
            {
                if (!instance.ContainsTable(tableName)) //The database does not contain the table.
                {
                    return false;
                }
            }

            //At this point the database contains the tabel and the query is in the correct form. We proceed to parse.
            try
            {
                if (matches.Length == 4)
                {
                    string values = matches[2];
                    List<string> valuesList = values.Split(',').ToList();

                    if (valuesList.Count == 0)
                    {
                        return false;
                    }

                    valuesList = TrimmedList(valuesList);

                    return instance.Insert(tableName, valuesList);

                }
                else if (matches.Length == 5)
                {
                    string columns = matches[2];
                    string values = matches[3];

                    List<string> columnsList = columns.Split(',').ToList();
                    List<string> valuesList = values.Split(',').ToList();

                    if ((columnsList.Count == 0) || (valuesList.Count == 0))
                    {
                        return false;
                    }

                    columnsList = TrimmedList(columnsList);
                    valuesList = RemoveApostrophes(TrimmedList(valuesList));

                    return instance.Insert(tableName, valuesList, columnsList);
                }
            }
            catch (Exception e)
            {
                return false;
            }

        }

        /// <summary>
        /// Returns a copy list of the original one where all the leading and trailing white spaces were removed for each entry.
        /// </summary>
        /// <param name="originalList"></param>
        /// <returns></returns>
        static List<string> TrimmedList(List<string> originalList)
        {
            List<string> result = new List<string>();

            for (int i = 0; i < originalList.Count; i++)
            {
                string pivot = originalList[i].TrimStart().TrimEnd();
                result.Add(pivot);
            }

            return result;
        }

    }
}
