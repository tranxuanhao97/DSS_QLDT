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
        int src_val;
        DataTable dtTemp;
        public SHOW_FORM(DataTable dt)
        {
            InitializeComponent();
            ShowResult(dt);
            src_val = 0;
            
        }

        private void ShowResult(DataTable dt)
        {
            dataGridShowForm.DataSource = dt;
            dataGridShowForm.ColumnHeadersDefaultCellStyle.BackColor = Color.Blue;
            dataGridShowForm.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridShowForm.ColumnHeadersDefaultCellStyle.Font = new Font("SansSerif", 10F, FontStyle.Bold);
            dataGridShowForm.Columns[0].Width = 50;
            dataGridShowForm.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridShowForm.EnableHeadersVisualStyles = false;
        }

    }
}
