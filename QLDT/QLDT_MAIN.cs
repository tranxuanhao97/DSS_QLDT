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
            query = "Select MaTruong as 'MÃ TRƯỜNG',TenTruong as 'TÊN TRƯỜNG', Website as 'WEBSITE', DiaChi as 'ĐỊA CHỈ', DVChuquan as 'ĐƠN VỊ CHỦ QUẢN' From [QLDT_V1].[dbo].[cosodaotao]";
            controller.PushDataToShowForm(query);
        }

        private void btnAsk_Click(object sender, EventArgs e)
        {
            new ASK_FORM().ShowDialog();
            
        }

        private void btnAsk_MouseHover(object sender, EventArgs e)
        {
            btnAsk.Text = ">> Đặt Câu Hỏi <<";
            btnAsk.FlatAppearance.BorderSize = 2;
          
        }

        private void btnAsk_MouseLeave(object sender, EventArgs e)
        {
            btnAsk.Text = "Đặt Câu Hỏi";
            btnAsk.FlatAppearance.BorderSize = 0;
        }

        

        private void btnAbout_Click(object sender, EventArgs e)
        {

        }

        private void btnShow_MouseHover(object sender, EventArgs e)
        {
            btnShow.Text = ">> Hiện Kết Quả <<";
            btnShow.FlatAppearance.BorderSize = 2;
        }

        private void btnShow_MouseLeave(object sender, EventArgs e)
        {
            btnShow.Text = "Hiện Kết Quả";
            btnShow.FlatAppearance.BorderSize = 0;
        }

        private void btnAbout_MouseHover(object sender, EventArgs e)
        {
            btnAbout.Text = ">> Thông Tin Phần Mềm <<";
            btnAbout.FlatAppearance.BorderSize = 2;
        }

        private void btnAbout_MouseLeave(object sender, EventArgs e)
        {
            btnAbout.Text = "Thông Tin Phần Mềm";
            btnAbout.FlatAppearance.BorderSize = 0;
        }

       
    }
}
