using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

using dbms_objects;

namespace dbms_gui_02
{
    public static class Program
    {
        public static Parser parser = new Parser();
        
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }

        /*
         * 
            
            CREATE TABLE Student(studentId int PRIMARY KEY, name varchar(15), gender varchar(1), age int);
            CREATE TABLE Item itemID int, description varchar(20),);
            CREATE TABLE Vehicle (licenseNumber varchar(10),brand varchar(15),model varchar(15),type varchar(2),engineSize int);
            CREATE TABLE Course (courseName varchar(20),startingDate date,teacherName varchar(20));
            CREATE TABLE Book (isbn varchar(20) PRIMARY KEY,title varchar(20),author varchar(20),pages int,editorial varchar(15));
            INSERT INTO Student VALUES(10, 'John Smith', 'M', 22);
            INSERT INTO Student VALUES(11, 'Hsu You-Ting', 'F', 23);
            INSERT INTO Student(name, age, studentId, gender) VALUES('Ai Toshiko', 21, 12, 'F');
            INSERT INTO Student(age, studentId, gender, name) VALUES(20, 13, 'M', 'Fernando Sierra');
            INSERT INTO Student VALUES(14, 'Mohammed Ali', 'M', 25);
            INSERT INTO Student VALUES(10, 'Huang Hao-Wei', 'M', 26);
            INSERT INTO Book VALUES(12345, 'Romeo and Juliet', 'Shakespeare','Hello', 'Prentice Hall');

         */
    }
}
