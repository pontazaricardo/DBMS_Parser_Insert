using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_Exam
{
    class Program
    {
        static void Main(string[] args)
        {
            I1 obj1 = new Class1();
            Class1 obj2 = new Class1();

            Console.WriteLine(obj1.ReturnName());
            Console.WriteLine(obj2.ReturnName());

            string str2;
            string str1 = Hello("Ricardo", out str2);

            Console.ReadLine();
        }

        public static string Hello(string str, out string str2)
        {
            string resultToReturn = "";
            resultToReturn = "Hello " + str;

            str2 = "This is another output";

            return resultToReturn;
        }
    }

    interface I1
    {
        string ReturnName();
        void setName(string str);
        
    }

    class Class1 : I1
    {

        string name;

        //public Class1() { this.name = "default";   }

        /*public Class1(string str)
        {
            this.name = str;
        }
        */

        public string ReturnName()
        {
            return "Name: "+this.name;
        }

        public void setName(string str)
        {
            this.name = str;
        }
    }

    

}
