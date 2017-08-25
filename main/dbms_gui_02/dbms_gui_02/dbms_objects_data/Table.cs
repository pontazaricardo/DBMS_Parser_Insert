using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;

namespace dbms_objects_data
{
    class Table
    {
        public DataTable table = new DataTable();

        public Table()
        {

        }

        public bool CreateTable(string[] listOfNames, Type[] listOfTypes)
        {
            if((listOfNames == null) || (listOfTypes == null))
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
            }
            return true;
        }

    }
}
