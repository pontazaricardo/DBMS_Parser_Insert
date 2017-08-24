using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exceptions
{
    public class InvalidSelectQueryException : Exception
    {
        public InvalidSelectQueryException()
        {

        }

        public InvalidSelectQueryException(string message) : base(message)
        {

        }

        public InvalidSelectQueryException(string message, Exception inner) : base(message, inner)
        {

        }


    }
}
