using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dbms_objects_data
{
    public sealed class Database
    {
        public static Dictionary<string, Table> dictionary;


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

            return true;
        }
        
    }
}
