﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;

namespace dbms_objects_data
{
    public class Table
    {
        public DataTable table = new DataTable();

        public Table()
        {

        }

        /// <summary>
        /// Creates a Table object based on the columns names and types. Returns true if the creation was successful and false if there is an error.
        /// </summary>
        /// <param name="listOfNames"></param>
        /// <param name="listOfTypes"></param>
        /// <returns></returns>
        public bool CreateTable(string[] listOfNames, Type[] listOfTypes)
        {
            table = new DataTable();

            if ((listOfNames == null) || (listOfTypes == null))
            {
                return false;
            }

            if(listOfNames.Length != listOfTypes.Length)
            {
                return false;
            }

            for(int i = 0; i < listOfNames.Length; i++)
            {
                try
                {
                    table.Columns.Add(listOfNames[i], listOfTypes[i]);
                }catch(Exception e)
                {
                    Console.WriteLine(e.Message);

                    table = new DataTable();
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Inserts the data in the given columns. Returns true if the data was inserted successfully and false otherwise.
        /// </summary>
        /// <param name="values"></param>
        /// <param name="columns"></param>
        /// <returns></returns>
        public bool Insert(List<string> values, List<string> columns = null)
        {
            if((values == null) || (values.Count == 0))
            {
                return false;
            }
            if (columns!= null)
            {
                if(values.Count != columns.Count)
                {
                    return false;
                }
            }else
            {
                //columns == null
                if(values.Count != table.Columns.Count)
                {
                    return false;
                }
            }

            //At this point we insert a new row
            try
            {
                DataRow row = table.NewRow();

                if (columns == null)
                {
                    for (int i = 0; i < table.Columns.Count; i++)
                    {
                        row[table.Columns[i].ColumnName] = values[i];
                    }
                }else
                {
                    for(int i = 0; i < columns.Count; i++)
                    {
                        row[columns[i]] = values[i];
                    }
                }

                table.Rows.Add(row);
                table.AcceptChanges();

            }catch(Exception e)
            {
                return false;
            }

            return true;
        }

    }
}
