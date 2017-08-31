using System;
using System.Collections.Generic;
using System.Configuration;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Text.RegularExpressions;
using Exceptions;
using System.IO;

using dbms_objects_data;

namespace dbms_gui_02
{
    public partial class MainForm : Form
    {

        public string QueryUnparsed;
        public DataTable DatagridListOfTables = new DataTable("Tables");
        public DataColumn DatagridListOfTables_Column_Name = new DataColumn("Name");
        public List<string> ListOfTables = new List<string>();

        private static Database db;

        public MainForm()
        {
            db = Database.GetInstance;

            InitializeComponent();

            LoadTablesFromFileSystem();
        }

        public void LoadTablesFromFileSystem()
        {
            DatagridListOfTables.Columns.Add(DatagridListOfTables_Column_Name);
            dataGridView_tables.DataSource = DatagridListOfTables;
            dataGridView_tables.Update();



            string folderPath = ConfigurationManager.AppSettings["FolderDatabase"];
            foreach(string file in Directory.EnumerateFiles(folderPath, "*.csv"))
            {
                //string contents = File.ReadAllText(file);
                string FileName = file.Replace(folderPath, "");
                FileName = FileName.Replace(".csv", "");
                ListOfTables.Add(FileName);
            }


        }


        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void buttonClean_Click(object sender, EventArgs e)
        {
            
        }

        private void buttonRun_Click(object sender, EventArgs e)
        {
            
        }

        public void ParseQueries(string str)
        {
            while (str.Contains("  "))
            {
                str = str.Replace("  ", " ");
            }
            str = Regex.Replace(str, @"(select|insert|into|values|create table|primary key|int|varchar)", m => m.Value.ToUpper(), RegexOptions.IgnoreCase);

            List<string> listOfQueries = new List<string>();

            DataTable queryresultstable = new DataTable("Results");
            DataColumn column1 = new DataColumn("No.", typeof(int));
            DataColumn column2 = new DataColumn("Query", typeof(string));
            DataColumn column3 = new DataColumn("Status", typeof(string));

            queryresultstable.Columns.Add(column1);
            queryresultstable.Columns.Add(column2);
            queryresultstable.Columns.Add(column3);

            dataGridView1.DataSource = queryresultstable;
            dataGridView1.Columns[0].Width = 35;
            dataGridView1.Columns[1].Width = 450;
            dataGridView1.Columns[2].Width = 100;
            dataGridView1.Update();

            try
            {
                listOfQueries = str.Split(';').ToList();

                for(int i = 0; i < listOfQueries.Count(); i++)
                {
                    if (string.IsNullOrWhiteSpace(listOfQueries[i]))
                    {
                        continue;
                    }

                    listOfQueries[i] = listOfQueries[i].Trim();

                    int counter = i + 1;
                    bool result = db.Parse(listOfQueries[i]);

                    string resultDisplay = "Failed";
                    if (result)
                    {
                        resultDisplay = "Success";
                    }

                    queryresultstable.Rows.Add(counter, listOfQueries[i], resultDisplay);
                    
                    dataGridView1.Update();
                }

            }catch(Exception e)
            {

            }

        }
        

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            QueryUnparsed = richTextBox1.Text;
            ParseQueries(QueryUnparsed);
        }

        private void toolStripButton_Clean_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
            richTextBox1.Focus();
        }
    }
}
