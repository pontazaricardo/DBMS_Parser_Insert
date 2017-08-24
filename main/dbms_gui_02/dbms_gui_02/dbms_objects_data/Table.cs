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

        public bool insertRows(string[] listOfNames, Type[] listOfTypes)
        {
            if((listOfNames == null) || (listOfTypes == null))
            {
                return false;
            }


            return true;
        }

    }
}
