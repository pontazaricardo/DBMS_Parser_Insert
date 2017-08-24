using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exceptions
{
    public class PrimaryKeyException : Exception
    {

        public PrimaryKeyException()
        {

        }

        public PrimaryKeyException(string message) : base(message)
        {

        }

        public PrimaryKeyException(string message, Exception inner) : base(message, inner)
        {

        }

    }
}
