using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace QLDT
{
    public partial class SHOW_FORM : Form
    {
        public SHOW_FORM(DataTable dt)
        {
            InitializeComponent();
            ShowResult(dt);
        }

        private void ShowResult(DataTable dt)
        {
            dataGridShowForm.DataSource = dt;
            dataGridShowForm.ColumnHeadersDefaultCellStyle.BackColor = Color.Blue;
            dataGridShowForm.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridShowForm.ColumnHeadersDefaultCellStyle.Font = new Font("SansSerif", 10F, FontStyle.Bold);
            dataGridShowForm.Columns[0].Width = 50;
            dataGridShowForm.EnableHeadersVisualStyles = false;
        }
    }
}
