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
    public partial class QLDT_MAIN : Form
    {
        private SqlConnection conn = DBConnection.GetDBConnection();
        string query = "";
        Controller controller;
        public QLDT_MAIN()
        {
            InitializeComponent();
            controller = new Controller();

        }
        private void btnHien_Click(object sender, EventArgs e)
        {
            query = "Select MaTruong as 'Mã Trường',TenTruong as 'Tên Trường', Website, DiaChi as 'Địa Chỉ', DVChuquan as 'Đơn vị chủ quản' From [QLDT_V1].[dbo].[cosodaotao]";
            controller.PushDataToShowForm(query);
        }

        private void btnAsk_Click(object sender, EventArgs e)
        {
            new ASK_FORM().ShowDialog();
        }
    }
}
