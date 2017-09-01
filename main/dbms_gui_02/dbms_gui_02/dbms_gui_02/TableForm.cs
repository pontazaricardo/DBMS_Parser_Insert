using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dbms_gui_02
{
    public partial class TableForm : Form
    {
        public TableForm(DataTable dataTable)
        {
            InitializeComponent();
            dataGridView1.DataSource = dataTable;
            dataGridView1.Update();
        }

        private void TableForm_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
