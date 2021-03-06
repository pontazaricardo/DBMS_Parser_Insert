﻿using System;
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

        /// <summary>
        /// After inserting the data in the tables, it updates the left window to display the actual tables and data.
        /// </summary>
        public void LoadTablesFromFileSystem()
        {
            DatagridListOfTables = new DataTable("Tables");
            DatagridListOfTables_Column_Name = new DataColumn("Name");
            DatagridListOfTables.Columns.Add(DatagridListOfTables_Column_Name);

            foreach (KeyValuePair<string, Table> pair in Database.dictionary)
            {
                DatagridListOfTables.Rows.Add(pair.Key);
            }
            dataGridView_tables.DataSource = DatagridListOfTables;
            dataGridView_tables.Update();
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

        /// <summary>
        /// Parses all the queries in a list of queries and executes them.
        /// </summary>
        /// <param name="str"></param>
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

                for (int i = 0; i < listOfQueries.Count(); i++)
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

            }
            catch (Exception e)
            {

            }

        }
        

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        /// <summary>
        /// Executes the queries in the main window and outputs the results in the lower window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            QueryUnparsed = richTextBox1.Text;
            ParseQueries(QueryUnparsed);

            LoadTablesFromFileSystem();
        }

        /// <summary>
        /// Cleans the main window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton_Clean_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
            richTextBox1.Focus();

            LoadTablesFromFileSystem();
        }


        private void dataGridView_tables_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewCell cell = (DataGridViewCell)dataGridView_tables.Rows[e.RowIndex].Cells[e.ColumnIndex];

            try
            {
                string cellValue = Convert.ToString(cell.Value);
                if (!string.IsNullOrWhiteSpace(cellValue))
                {
                    if (db.ContainsTable(cellValue))
                    {
                        TableForm tableForm = new TableForm(Database.dictionary[cellValue].table, "Table: " + cellValue);
                        tableForm.Show();
                    }
                }
            }catch(Exception e1)
            {
                //Fade silently
            }
        }

        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {
            
            string examplequeries = System.IO.File.ReadAllText(@"QueriesExample.txt");
            richTextBox1.Text = examplequeries;

        }
    }
}
