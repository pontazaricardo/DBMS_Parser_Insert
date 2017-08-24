using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exceptions
{
    public class InvalidTableCreationException : Exception
    {
        public InvalidTableCreationException()
        {

        }

        public InvalidTableCreationException(string message) : base(message)
        {
        }

        public InvalidTableCreationException(string message, Exception inner) : base(message, inner)
        {

        }


    }
}
