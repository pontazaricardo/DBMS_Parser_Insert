using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dbms_gui_02
{
    public static class Program
    {
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
            
            CREATE TABLE Student(studentId int, name varchar, gender varchar, age int);
            CREATE TABLE Item itemID int, description varchar,);
            CREATE TABLE Vehicle (licenseNumber varchar,brand varchar,model varchar,type varchar,engineSize int);
            CREATE TABLE Course (courseName varchar,startingDate date,teacherName varchar);
            CREATE TABLE Book (isbn varchar,title varchar,author varchar,pages int,editorial varchar);
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
