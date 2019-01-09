using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;

namespace QLDT
{
    class Controller
    {
        SqlConnection conn = DBConnection.GetDBConnection();
        public void PushDataToShowForm(String query)
        {
            DataTable dt = getDataTable(query);

            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu thỏa mãn");
            }
            else
            {
                SHOW_FORM sf = new SHOW_FORM(dt);
                sf.ShowDialog();
            }
        }
        private DataTable getDataTable(String query)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter SDA = new SqlDataAdapter(query, conn);
            //DialogResult result = MessageBox.Show(query, "Print");
            dt.Columns.AddRange(new DataColumn[1] { new DataColumn("STT") });
            dt.Columns["STT"].AutoIncrement = true;
            dt.Columns["STT"].AutoIncrementSeed = 1;
            dt.Columns["STT"].AutoIncrementStep = 1;
            SDA.Fill(dt);
            return dt;
        }
    }
}
