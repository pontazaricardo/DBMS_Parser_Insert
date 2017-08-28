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

        private static Dictionary<string, Table> dictionary;
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
                if(instance == null)
                {
                    lock (obj)
                    {
                        if(instance == null)
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
            catch(Exception e)
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
            if(instance == null)
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


        private static bool ParseCreateStatement(string[] matches)
        {

            return false;
        }

        private static bool ParseInsertStatement(string[] matches)
        {
            return false;
        }

    }
}
