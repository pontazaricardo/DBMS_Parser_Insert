using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Exceptions;

namespace test_dbms_02
{
    class Table
    {

        public string Name;

        public List<string> ColumnNames;
        public List<string> ColumnTypes;
        public List<bool> PrimaryKeyBools;

        public List<Column> Columns;

        /// <summary>
        /// Creates a new Table in the Database. Need to add Name, a list of the column names and a list of the column types. 
        /// THE columnNames and columnTypes need to be of the same size!!!
        /// </summary>
        /// <param name="name"></param>
        /// <param name="columnNames"></param>
        /// <param name="columnTypes"></param>
        public Table(string name, List<string> columnNames, List<string> columnTypes, List<bool> primaryKeyBools)
        {
            if ((columnNames.Count() != columnTypes.Count()) || (columnNames.Count() != primaryKeyBools.Count()) || (columnTypes.Count() != primaryKeyBools.Count()))
            {
                throw new InvalidTableCreationException("The size of the columnNames, columnTypes and primaryKeyBools lists must match.");
            }

            foreach(string type in columnTypes)
            {
                if((type.Contains("PRIMARY")) || (type.Contains("KEY")))
                {
                    continue;
                }

                if ((!type.Equals("string")) && (!type.Equals("int")))
                {
                    throw new InvalidTableCreationException("Data type not recognized.");
                }
            }

            this.Name = name;
            this.ColumnNames = columnNames;
            this.ColumnTypes = columnTypes;
            this.PrimaryKeyBools = primaryKeyBools;

            Columns = new List<Column>();

            int i = 0;
            for (i = 0; i < columnNames.Count(); i++)
            {
                Column column = new Column(columnNames[i], columnTypes[i], primaryKeyBools[i]);
                Columns.Add(column);
            }

            Console.WriteLine("Query ran successfully.");
        }

        /// <summary>
        /// Inserts a new record into the table. The List of values NEED to have the same amount of records as columns! (this is inserting a row).
        /// </summary>
        /// <param name="values"></param>
        public void InsertRecord(List<string> values)
        {
            if (Columns.Count() != values.Count())
            {
                throw new InsertDataException("The number of columns in the table and number of values to insert must match");
            }

            for (int i = 0; i < Columns.Count(); i++)
            {
                Columns[i].AddRecord(values[i]);
            }
            Console.WriteLine("Query ran successfully.");
        }

        /// <summary>
        /// Inserts a new record into the table in the specified columns. The rest of the values are considered 'dbms_null' values.
        /// </summary>
        /// <param name="columns"></param>
        /// <param name="values"></param>
        public void InsertRecord(List<string> columnsToInsert, List<string> values)
        {
            if (columnsToInsert.Count() != values.Count())
            {
                throw new InsertDataException("The number of columns in the table and number of values to insert must match");
            }

            for (int i = 0; i < Columns.Count(); i++)
            {
                if (columnsToInsert.Contains(Columns[i].Name))
                {
                    int indexToUse = columnsToInsert.IndexOf(Columns[i].Name);
                    Columns[i].AddRecord(values[indexToUse]);
                }else
                {
                    Columns[i].AddRecord("dbms_null");
                }
            }
            Console.WriteLine("Query ran successfully.");


            /*for (int i = 0; i < Columns.Count(); i++)
            {
                if (columnsToInsert.Contains(Columns[i].Name))
                {
                    Columns[i].AddRecord(values[i]);
                }
                else
                {
                    Columns[i].AddRecord("dbms_null");
                }
            }*/
        }

        public void PrintTable()
        {
            Console.WriteLine(" ");
            //First print names of columns, then type and finally values.
            Console.WriteLine("============== " + Name + " ============== ");
            for(int i = 0; i < Columns.Count(); i++)
            {
                Console.Write("[" + Columns[i].Name + "]");
            }
            Console.WriteLine("");
            Console.WriteLine("------------------------------------------");
            for (int i = 0; i < Columns.Count(); i++)
            {
                Console.Write("[" + Columns[i].Type + "]");
            }
            Console.WriteLine("");
            Console.WriteLine("------------------------------------------");
            for (int i = 0; i < Columns.Count(); i++)
            {
                Console.Write("[" + Columns[i].IsPrimaryKey + "]");
            }
            Console.WriteLine("");
            Console.WriteLine("************ BEGIN DATA ******************");

            if (Columns[0].Records.Count() > 0)
            {
                for(int j=0;j< Columns[0].Records.Count(); j++)
                {
                    for (int i = 0; i < Columns.Count(); i++)
                    {
                        Console.Write("[" + Columns[i].Records[j] + "]");
                    }
                    Console.WriteLine("");
                }
            }
            Console.WriteLine("************ END DATA  ********************");
        }

    }
}
