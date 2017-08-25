using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dbms_objects_data
{
    public sealed class Database
    {
        public List<Table> tables;

        private static readonly object obj = new object();
        private static Database instance = null;

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
            tables = new List<Table>();
        }


        public bool AddTable(Table table)
        {
            if(table == null)
            {
                return false;
            }
            try
            {
                tables.Add(table);
            }
            catch(Exception e)
            {
                return false;
            }
            return true;
        }
        
    }
}
