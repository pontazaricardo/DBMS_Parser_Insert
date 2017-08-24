using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exceptions
{
    public class InsertDataException:Exception
    {

        public InsertDataException()
        {

        }

        public InsertDataException(string message) : base(message)
        {

        }

        public InsertDataException(string message, Exception inner):base(message, inner)
        {

        }
    }
}
