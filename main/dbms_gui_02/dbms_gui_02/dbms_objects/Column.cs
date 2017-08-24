using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Exceptions;

namespace dbms_objects
{
    public class Column
    {
        public string Name;
        public string Type;
        public bool IsPrimaryKey;

        public List<string> Records;

        public Column(string name, string type, bool isPrimaryKey)
        {
            this.Name = name;
            this.Type = type;
            this.IsPrimaryKey = isPrimaryKey;

            Records = new List<string>();

        }

        /// <summary>
        /// Creates a column completely based on the list. If the column is a primary key and (there is a least one 'dbms_null' value in it (customized null) or there are repeated values), then we throw a primary key exception. 
        /// </summary>
        /// <param name="list"></param>
        public void SetRecords(List<string> list)
        {
            if (IsPrimaryKey)
            {
                if (list.Contains("dbms_null"))
                {
                    throw new PrimaryKeyException("Primary key must not be empty.");
                }
                if (list.Distinct().Count() != list.Count())
                {
                    throw new PrimaryKeyException("Primary key constraint violated. There are repeated values inside the primary key.");
                }
            }
            this.Records = list;
        }


        /// <summary>
        /// Adds a new record at the end of the column (int to string).
        /// </summary>
        /// <param name="number"></param>
        public void AddRecord(int number)
        {
            Records.Add(Convert.ToString(number));
        }


        /// <summary>
        /// Adds a new record at the end of the column (string to string). It can throw a Primary Key constraint exception if the primary key is null or already exists.
        /// </summary>
        /// <param name="value"></param>
        public void AddRecord(string value)
        {
            if (IsPrimaryKey)
            {
                if (value.Equals("dbms_null"))
                {
                    throw new PrimaryKeyException("Primary key must not be empty.");
                }
                if (Records.Contains(value))
                {
                    throw new PrimaryKeyException("Primary key constraint violated. Primary key already exists.");
                }
            }

            Records.Add(value);
        }

    }
}
