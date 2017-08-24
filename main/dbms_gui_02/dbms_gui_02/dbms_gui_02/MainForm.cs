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

namespace dbms_gui_02
{
    public partial class MainForm : Form
    {

        public string QueryUnparsed;
        public DataTable DatagridListOfTables = new DataTable("Tables");
        public DataColumn DatagridListOfTables_Column_Name = new DataColumn("Name");
        public List<string> ListOfTables = new List<string>();

        public MainForm()
        {
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
                    listOfQueries[i] = listOfQueries[i].Trim();
                }

                for(int i = 0; i < listOfQueries.Count(); i++)
                {
                    int counter = i + 1;
                    queryresultstable.Rows.Add(counter, listOfQueries[i], "Pending...");
                    
                    dataGridView1.Update();
                }

            }catch(Exception e)
            {

            }
        }

        public string ExecuteQuery(string query)
        {
            //string folderPath = ConfigurationManager.AppSettings["FolderDatabase"];
            string result = "";

            if (query.Contains("CREATE TABLE"))
            {
                try
                {
                    string NameOfTable = query.Substring(12, query.IndexOf('(') - 12);
                    NameOfTable = NameOfTable.Trim();

                    List<string> parameters = CommonObject.GetParameters(query);

                    List<string> parameters_name = new List<string>();
                    List<string> parameters_type = new List<string>();
                    List<bool> parameters_primaryKeyBool = new List<bool>();

                    for (int i = 0; i < parameters.Count(); i++)
                    {
                        //We only use the non-empty parameters
                        if (!string.IsNullOrWhiteSpace(parameters[i]))
                        {
                            parameters[i] = parameters[i].Trim();

                            bool isPrimaryKey = parameters[i].Contains("PRIMARY KEY");
                            List<string> parameters_entries = parameters[i].Split(' ').ToList();

                            string parameter_name = parameters_entries[0];
                            string parameter_type = parameters_entries[1]; //We need to verify if it is a int or string.

                            if (parameter_type.Contains("INT"))
                            {
                                parameter_type = "int";
                            }
                            if (parameter_type.Contains("VARCHAR"))
                            {
                                //TODO: get size
                                parameter_type = "string";
                            }

                            parameters_name.Add(parameter_name);
                            parameters_type.Add(parameter_type);
                            parameters_primaryKeyBool.Add(isPrimaryKey);
                        }
                    }

                    //At this point, we already have the table name, and parameters. We proceed to create
                    /*Table table = new Table(NameOfTable, parameters_name, parameters_type, parameters_primaryKeyBool);
                    Program.database.AddTable(table);*/
                }
                catch (InvalidTableCreationException e)
                {
                    Console.WriteLine(e.Message);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Invalid sintax in table creation.");
                }
            }

            if (query.Contains("INSERT INTO"))
            {
                /*try
                {

                    string NameOfTable = query.Substring(11, query.IndexOf('(') - 11);

                    if (NameOfTable.Contains("VALUES"))
                    {
                        NameOfTable = query.Substring(11, query.IndexOf("VALUES") - 12);
                    }

                    NameOfTable = NameOfTable.Trim();

                    string columns = query.Substring(query.IndexOf(NameOfTable), query.IndexOf("VALUES") - 12);
                    string values = query.Substring(query.IndexOf("VALUES") + 5);

                    List<string> list_columns = CommonObject.GetParameters(columns.Trim());
                    List<string> list_values = CommonObject.GetParameters(values.Trim());

                    //We eliminate spaces and white entries
                    List<string> list_columns_formated = new List<string>();
                    List<string> list_values_formated = new List<string>();

                    foreach (string column in list_columns)
                    {
                        if (!string.IsNullOrWhiteSpace(column))
                        {
                            list_columns_formated.Add(column.Trim());
                        }
                    }

                    foreach (string value in list_values)
                    {
                        if (!string.IsNullOrWhiteSpace(value))
                        {
                            list_values_formated.Add(value.Trim());
                        }
                    }

                    if ((list_columns_formated.Count() != list_values_formated.Count()) && (list_columns_formated.Count() != 0))
                    {
                        throw new InsertDataException("Error in INSERT sintax.");
                    }

                    //We need to bring up the table with the same name
                    Table table = null;
                    for (int j = 0; j < Program.database.listOfTables.Count(); j++)
                    {
                        if (Program.database.listOfTables[j].Name.Equals(NameOfTable))
                        {
                            table = Program.database.listOfTables[j];
                            break;
                        }
                    }
                    try
                    {
                        if (table.Equals(null))
                        {
                            //Try to access the table. If it is null, it will raise an exception. We will override this one to use a InsertDataException.
                        }
                    }
                    catch (Exception e)
                    {
                        throw new InsertDataException("Error in INSERT sintax: Table does not exists in database."); //TODO: verify
                    }

                    //We now proceed to verify which columns are the ones we need to insert into
                    List<string> table_columns = new List<string>();
                    List<string> table_types = new List<string>();

                    if (list_columns_formated.Count() > 0)
                    {
                        //We are using INSERT INTO table (column1, ... columnN) VALUES (val1, ... valN)
                        for (int i = 0; i < list_columns_formated.Count(); i++)
                        {
                            for (int j = 0; j < table.Columns.Count(); j++)
                            {
                                if (table.Columns[j].Name.Equals(list_columns_formated[i]))
                                {
                                    table_columns.Add(table.Columns[j].Name);
                                    table_types.Add(table.Columns[j].Type);
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        //We are using INSERT INTO table VALUES (val1, ... valN) (val1, ... valN match with number of columns)
                        for (int j = 0; j < table.Columns.Count(); j++)
                        {
                            table_columns.Add(table.Columns[j].Name);
                            table_types.Add(table.Columns[j].Type);
                        }
                    }



                    if ((table_columns.Count() != list_columns_formated.Count()) && (list_columns_formated.Count() > 0))
                    {
                        throw new InsertDataException("Error in INSERT sintax: Column does not exists in table."); //TODO: verify
                    }

                    //We now proceed to verify if the types match with the data inserted //TODO
                    for (int i = 0; i < list_values_formated.Count(); i++)
                    {
                        if (list_values_formated[i].Contains("'"))//We have a varchar
                        {
                            if (!table_types[i].Equals("string"))
                            {
                                throw new InsertDataException("Error in INSERT sintax: Type in column and type in data does not match."); //TODO: verify
                            }
                            while (list_values_formated[i].Contains("'"))
                            {
                                list_values_formated[i] = list_values_formated[i].Replace("'", "");
                            }
                        }
                        else//We have an int
                        {
                            if (!table_types[i].Equals("int"))
                            {
                                throw new InsertDataException("Error in INSERT sintax: Type in column and type in data does not match."); //TODO: verify
                            }
                        }
                    }

                    //Finally we proceed to insert the data
                    table.InsertRecord(table_columns, list_values_formated);


                }
                catch (InsertDataException e)
                {
                    Console.WriteLine(e.Message);
                }
                catch (PrimaryKeyException e)
                {
                    Console.WriteLine(e.Message);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }*/
            }

            if (query.Contains("SELECT"))
            {
                //We first need to split into column names, tables and conditions
                //QueryDefragmenterString queryDefragmenter = new QueryDefragmenterString(query);
            }

            return result;
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
