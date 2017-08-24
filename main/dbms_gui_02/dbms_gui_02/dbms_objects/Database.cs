using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Exceptions;

namespace dbms_objects
{
    class Database
    {
        public string Name;

        public List<Table> listOfTables;
        public List<string> NamesOfTables;

        public Database(string Name)
        {
            this.Name = Name;
            listOfTables = new List<Table>();
            NamesOfTables = new List<string>();
        }

        public void AddTable(Table table)
        {
            if (NamesOfTables.Contains(table.Name))
            {
                throw new InvalidTableCreationException("Table already exist");
            }
            NamesOfTables.Add(table.Name);
            listOfTables.Add(table);
        }

    }
}
