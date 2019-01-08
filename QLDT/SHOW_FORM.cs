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
        public SHOW_FORM(string query)
        {
            InitializeComponent();
            ShowResult(query);
        }

        private void ShowResult(string query)
        {
            SqlConnection conn = new SqlConnection("Data Source=TUANDOAN-PC\\SQLEXPRESS;Initial Catalog=QLDT_V1;Integrated Security=True");
            DataTable dt = new DataTable();
            //string query = "Select * From [QLDT].[dbo].[cosodaotao]";
            SqlDataAdapter SDA = new SqlDataAdapter(query, conn);
            //DialogResult result = MessageBox.Show(query, "Print");
            SDA.Fill(dt);
            dataGridView1.DataSource = dt;
        }
    }
}
