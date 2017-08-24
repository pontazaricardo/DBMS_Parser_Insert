using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Text.RegularExpressions;

using Exceptions;

namespace test_dbms_02
{
    class Program
    {

        public static Database database = new Database("dbms");

        static void Main(string[] args)
        {
            //testRegex();
            /*string query = "CREATE TABLE Person (person_id int PRIMARY KEY, name varchar(20),gender varchar(1));";
            query += "CREATE TABLE Student (studentId int PRIMARY KEY,name varchar(20),last_name varchar(20),age int, phone_number int);";
            query += "create table department(  department_name varchar(20),  type varchar(1),  num_employees int,  code varchar(10), city varchar(15) );";
            query += "Create tAbLe pRoduct (Product_id int PRImaRY KEY, product_name varchar(26));";
            query += "INSERT INTO Student (studentId, name, age) VALUES(1, 'John', 13);";
            query += "INSERT INTO Student (studentId, name, age)VALUES(2, 'Mary', 11);";
            query += "INSERT INTO Student(studentId, name, age ) VALUES(3, 'Will', 12);";
            query += "INSERT INTO Student ( studentId, name, age) VALUES(4, 'Sue', 13);";
            query += "INSERT INTO Student(studentId, name , age) VALUES(5, 'James', 14); ";*/

            string query = "CREATE TABLE Student(studentId int PRIMARY KEY, name varchar(15), gender varchar(1), age int);" +
                "CREATE TABLE Item itemID int, description varchar(20),);" +
                "CREATE TABLE Vehicle (licenseNumber varchar(10),brand varchar(15),model varchar(15),type varchar(2),engineSize int);" +
                "CREATE TABLE Course (courseName varchar(20),startingDate date,teacherName varchar(20));"+
                "CREATE TABLE Book (isbn varchar(20) PRIMARY KEY,title varchar(20),author varchar(20),pages int,editorial varchar(15));"+
                "INSERT INTO Student VALUES(10, 'John Smith', 'M', 22);" +
                "INSERT INTO Student VALUES(11, 'Hsu You-Ting', 'F', 23);" +
                "INSERT INTO Student(name, age, studentId, gender) VALUES('Ai Toshiko', 21, 12, 'F');" +
                "INSERT INTO Student(age, studentId, gender, name) VALUES(20, 13, 'M', 'Fernando Sierra');" +
                "INSERT INTO Student VALUES(14, 'Mohammed Ali', 'M', 25);" +
                "INSERT INTO Student VALUES(10, 'Huang Hao-Wei', 'M', 26);" +
                "INSERT INTO Book VALUES(12345, 'Romeo and Juliet', 'Shakespeare','Hello', 'Prentice Hall');" + 
                "SELECT tweet FROM tweets WHERE 123";

            Parser parser = new Parser();
            parser.RunQuery(query);

            /*string query2 = "Insert into Person(person_id, name) values(1,'john');";
            query2 += "Insert into Person(person_id, name) values(1,'john2');";

            parser.RunQuery(query2);*/

            printTables();

            Console.ReadLine();

        }

        /*public static void testRegex()
        {
            string extractFuncRegex = @"\b[^()]+\((.*)\)$";
            string extractArgsRegex = @"([^,]+\(.+?\))|([^,]+)";

            //Your test string
            string test = @"func1(2 * 7, func2(3, 5))";

            var match = Regex.Match(test, extractFuncRegex);
            string innerArgs = match.Groups[1].Value;
//            Assert.AreEqual(innerArgs, @"2 * 7, func2(3, 5)");
            var matches = Regex.Matches(innerArgs, extractArgsRegex);
            //Assert.AreEqual(matches[0].Value, "2 * 7");
            //Assert.AreEqual(matches[1].Value.Trim(), "func2(3, 5)");
        }*/

        public static void printTables()
        {
            for(int i = 0; i < database.listOfTables.Count();i++)
            {
                database.listOfTables[i].PrintTable();
            }
        }

        public void test()
        {
            /*string nameOfTable = "table1";

            List<string> columnNames = new List<string> { "id", "name", "age", "phone"};
            List<string> columnType = new List<string> { "int", "varchar", "int", "varchar" };
            List<bool> columnPrimaryKeyBools = new List<bool> { true, false, false, false };

            Table table1 = new test_dbms_02.Table(nameOfTable, columnNames, columnType, columnPrimaryKeyBools);

            try
            {
                table1.InsertRecord(new List<string> { "1", "Name1", "17", "1234" });
            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }

            try
            {
                table1.InsertRecord(new List<string> { "dbms_null", "Name2", "1747", "1234135" });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            try
            {
                //table1.InsertRecord(new List<string> { "2", "Name2", "1747", "1234135" });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            try
            {
                table1.PrintTable();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }*/




            /*List<string> lstNames = new List<string> { "A", "B", "C" };

            if (lstNames.Distinct().Count() != lstNames.Count())
            {
                Console.WriteLine("List contains duplicate values.");
            }*/
        }
    }
}
