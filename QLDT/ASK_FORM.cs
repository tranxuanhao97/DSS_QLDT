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
    public partial class ASK_FORM : Form
    {
        string query;
        bool advanceClicked = false;
        bool cbbLoaded = false;

        Controller controller;
        public ASK_FORM()
        {
            InitializeComponent();
            controller = new Controller();
            Init();
        }

        private void Init()
        {
            cbbTieuChi.SelectedIndex = 0;
        }

        private void btnAdvanced_Click(object sender, EventArgs e)
        {
            advanceClicked = true;
            if (cbbLoaded == false)
            {
                loadToComboBox(cbbTinhThanh, getItem("TinhThanh"));
                loadToComboBox(cbbDVChuQuan, getItem("DVChuQuan"));
                cbbLoaded = true;
            }
            if (pnlAdvance.Visible == true)
            {
                pnlAdvance.Visible = false;
                this.MinimumSize = new Size(this.Width, this.Height - pnlAdvance.Height);
                this.Height = this.Height - pnlAdvance.Height;
            }
            else if (pnlAdvance.Visible == false)
            {
                pnlAdvance.Visible = true;
                this.Height = this.Height + pnlAdvance.Height;
                this.MinimumSize = new Size(this.Width, this.Height);
            }
        }

        private List<String> getItem(String conditionQuery)
        {
            var list = new List<String>();
            list.Add("None");
            SqlConnection conn = DBConnection.GetDBConnection();
            conn.Open();
            string sql = "SELECT * FROM [QLDT_V1].[dbo].[cosodaotao]";
            using (var command = new SqlCommand(sql, conn))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        bool isContain = false;
                        String item = reader[conditionQuery].ToString();
                        for (int i = 0; i < list.Count; i++)
                        {
                            if (item.Equals(list.ElementAt(i)))
                            {
                                isContain = true;
                                break;
                            }
                        }
                        if (!isContain)
                        {
                            list.Add(item);
                        }
                    }
                }
            }
            return list;
        }

        private void loadToComboBox(ComboBox cbb, List<String> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                ComboboxItem item = new ComboboxItem();
                item.Text = list.ElementAt(i);
                item.Value = i;
                cbb.Items.Add(item);
            }
            cbb.SelectedIndex = 0;
        }

        private void rbtnS1_CheckedChanged(object sender, EventArgs e)
        {
            gbQuestion.Enabled = true;
            gbSearch.Enabled = false;
        }

        private void rbtnS2_CheckedChanged(object sender, EventArgs e)
        {
            gbQuestion.Enabled = false;
            gbSearch.Enabled = true;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            //if (advanceClicked == false && searchTextBox.Text == "") {
            //    MessageBox.Show("Mời nhập từ khóa để tìm kiếm");
            //    return;
            //}
            //if (advanceClicked == true && cbbTinhThanh.SelectedItem.ToString() == "None" && cbbDVChuQuan.SelectedItem.ToString() == "None" && searchTextBox.Text == "")
            //{
            //    MessageBox.Show("Mời nhập từ khóa hoặc chọn tiêu chí để tìm kiếm");
            //    return;
            //}
            int criteria_index = cbbTieuChi.SelectedIndex;
            String criteria_field ="";
            int numberSearch = -1;
            switch (criteria_index)
            {
                case 0:
                    criteria_field = "TenTruong";
                    numberSearch = 1;
                    break;
                case 1:
                    criteria_field = "TenChuyenNganh";
                    numberSearch = 0;
                    break;
                case 2:
                    criteria_field = "MaTruong";
                    numberSearch = 1;
                    break;
                case 3:
                    criteria_field = "MaNganh";
                    numberSearch = 0;
                    break;
                default:
                    break;
            }
            String input = searchTextBox.Text;
            String query ="";
            if (!advanceClicked)
            {
                if (numberSearch == 1)
                    query = "Select [MaTruong] 'Mã Trường' ,[TenTruong] 'Tên Trường' ,[DiaChi] 'Địa Chỉ' ,[Website] ,[TinhThanh] 'Tỉnh Thành' ,[DVChuquan] 'Đơn Vị Chủ Quản' From [QLDT_V1].[dbo].[cosodaotao] WHERE " + criteria_field + " LIKE N'%" + input + "%'";
                else if (numberSearch == 0)
                {
                    query = "Select chuyennganhdaotao.MaNganh 'Mã Ngành', chuyennganhdaotao.TenChuyenNganh 'Tên Chuyên Ngành', cosonganh.ChiTieu 'Chỉ Tiêu', cosonganh.DiemChuan 'Điểm chuẩn' From [QLDT_V1].[dbo].[chuyennganhdaotao] LEFT JOIN [QLDT_V1].[dbo].[cosonganh] ON [QLDT_V1].[dbo].[cosonganh].[MaNganh] = [QLDT_V1].[dbo].[chuyennganhdaotao].[MaNganh] " + "WHERE [QLDT_V1].[dbo].[chuyennganhdaotao].[" + criteria_field + "] LIKE N'%" + input + "%'";
                }
                //MessageBox.Show(query);
            }
            else
            {
                String cbbTextTinhThanh = cbbTinhThanh.SelectedItem.ToString();
                String cbbTextDVChuQuan = cbbDVChuQuan.SelectedItem.ToString();
                if (cbbTextDVChuQuan.Equals("None"))
                    cbbTextDVChuQuan = "";
                if (cbbTextTinhThanh.Equals("None"))
                    cbbTextTinhThanh = "";
                if (numberSearch == 1)
                {
                    query = "Select [MaTruong] 'Mã Trường' ,[TenTruong] 'Tên Trường' ,[DiaChi] 'Địa Chỉ' ,[Website] ,[TinhThanh] 'Tỉnh Thành' ,[DVChuquan] 'Đơn Vị Chủ Quản' From [QLDT_V1].[dbo].[cosodaotao] WHERE " + criteria_field + " LIKE N'%" + input + "%' AND TinhThanh LIKE N'%" + cbbTextTinhThanh + "%' AND DVChuQuan LIKE N'%" + cbbTextDVChuQuan + "%';";
                }
                else if (numberSearch == 0) {
                    query = "SELECT [QLDT_V1].[dbo].[cosodaotao].[TenTruong] as 'Tên Trường' ,[QLDT_V1].[dbo].[chuyennganhdaotao].[TenChuyenNganh] as 'Tên Chuyên Ngành', [QLDT_V1].[dbo].[cosonganh].[MaNganh] as 'Mã Ngành' FROM [QLDT_V1].[dbo].[cosonganh],[QLDT_V1].[dbo].[cosodaotao], [QLDT_V1].[dbo].[chuyennganhdaotao] WHERE [QLDT_V1].[dbo].[cosonganh].[MaTruong]=[QLDT_V1].[dbo].[cosodaotao].[MaTruong] AND [QLDT_V1].[dbo].[cosonganh].[MaNganh]=[QLDT_V1].[dbo].[chuyennganhdaotao].[MaNganh] AND [QLDT_V1].[dbo].[chuyennganhdaotao].[" + criteria_field + "] LIKE N'%" + input + "%'AND TinhThanh LIKE N'%" + cbbTextTinhThanh + "%' AND DVChuQuan LIKE N'%" + cbbTextDVChuQuan + "%';";
                }
            }
            //MessageBox.Show(query);
            controller.PushDataToShowForm(query);
        }
        private void searchTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSearch.PerformClick();
            }
        }

        private void llblQ1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //Các trường đào tạo CNTT ?
            query = "Select MaTruong as 'Mã Trường', TenTruong as 'Tên Trường', Website  From [QLDT_V1].[dbo].[cosodaotao] ";
            controller.PushDataToShowForm(query);
        }

        private void llblQ2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //Địa Chỉ các trường đào tạo CNTT ?
            query = "Select TenTruong as 'Tên Trường', DiaChi as 'Địa Chỉ' From [QLDT_V1].[dbo].[cosodaotao] ";
            controller.PushDataToShowForm(query);
        }

        private void llblQ3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //Website các trường đào tạo CNTT ?
            query = "Select TenTruong as 'Tên Trường', Website From [QLDT_V1].[dbo].[cosodaotao] ";
            controller.PushDataToShowForm(query);
        }


        
        private void llblQ4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //Danh mục giáo dục, đào tạo cấp IV trình độ đại học chuyên ngành CNTT?
            query = "Select MaNganh as 'Mã Ngành', TenChuyenNganh as 'Tên Chuyên Ngành'  From [QLDT_V1].[dbo].[chuyennganhdaotao] ";
            controller.PushDataToShowForm(query);
        }

        private void llblQ5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //Danh sách các đơn vị chủ quản ?
            query = "Select DISTINCT DVChuquan as 'Đơn vị chủ quản'  From [QLDT_V1].[dbo].[cosodaotao] ";
            controller.PushDataToShowForm(query);
        }

        private void llblQ6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //Số Cơ sở đào tạo CNTT tại Hà Nội?
            query = "SELECT [MaTruong] 'Mã Trường' ,[TenTruong] 'Tên Trường' ,[DiaChi] 'Địa Chỉ' ,[Website] ,[DVChuquan] 'Đơn Vị Chủ Quản' FROM [QLDT_V1].[dbo].[cosodaotao] WHERE TinhThanh=N'Hà Nội'";
            //SHOW_FORM sf = new SHOW_FORM(query);
            controller.PushDataToShowForm(query);
            //sf.Show();
        }

        private void llblQ7_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //Chi tieu tuyen sinh cac truong
            query = "SELECT [QLDT_V1].[dbo].[cosodaotao].[TenTruong] as 'Trường' ,SUM([QLDT_V1].[dbo].[cosonganh].[ChiTieu]) as 'Tổng Chỉ Tiêu' FROM [QLDT_V1].[dbo].[cosonganh],[QLDT_V1].[dbo].[cosodaotao] WHERE [QLDT_V1].[dbo].[cosonganh].[MaTruong]=[QLDT_V1].[dbo].[cosodaotao].[MaTruong] GROUP BY [QLDT_V1].[dbo].[cosodaotao].[TenTruong]";
            controller.PushDataToShowForm(query);
        }

        private void llblQ8_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //So luong csdt cac tinh thanh
            query = "SELECT TinhThanh as 'Tỉnh Thành', COUNT(*) as 'Số lượng CSDT' FROM [QLDT_V1].[dbo].[cosodaotao] GROUP BY TinhThanh";
            controller.PushDataToShowForm(query);
        }

        private void llblQ9_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //Số lượng cơ sở đào tạo theo đơn vị chủ quản ?
            query = "SELECT [QLDT_V1].[dbo].[cosodaotao].DVChuquan as 'Đơn Vị Chủ Quản',COUNT(*) as 'Số trường' FROM [QLDT_V1].[dbo].[cosodaotao] group by [QLDT_V1].[dbo].[cosodaotao].DVChuquan";
            controller.PushDataToShowForm(query);
        }

        private void llblQ10_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //10. Các cơ sở đào tạo CNTT tại Thành phố Hồ Chí Minh ?
            query = "SELECT [MaTruong] 'Mã Trường' ,[TenTruong] 'Tên Trường' ,[DiaChi] 'Địa Chỉ' ,[Website] ,[DVChuquan] 'Đơn Vị Chủ Quản' FROM [QLDT_V1].[dbo].[cosodaotao] WHERE TinhThanh=N'TP. Hồ Chí Minh'";
            controller.PushDataToShowForm(query);
        }

        private void llblQ11_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //11. Điểm chuẩn theo từng chuyên ngành tại các trường ?
            query = "SELECT [QLDT_V1].[dbo].[cosodaotao].[TenTruong] as 'Tên Trường' ,[QLDT_V1].[dbo].[chuyennganhdaotao].[MaNganh] as 'Mã Ngành' ,[QLDT_V1].[dbo].[chuyennganhdaotao].[TenChuyenNganh] as 'Tên Chuyên Ngành' ,[QLDT_V1].[dbo].[cosonganh].[DiemChuan] 'Điểm Chuẩn' FROM [QLDT_V1].[dbo].[chuyennganhdaotao],[QLDT_V1].[dbo].[cosonganh],[QLDT_V1].[dbo].[cosodaotao] WHERE [QLDT_V1].[dbo].[chuyennganhdaotao].MaNganh=[QLDT_V1].[dbo].[cosonganh].MaNganh AND [QLDT_V1].[dbo].[cosonganh].[MaTruong]=[QLDT_V1].[dbo].[cosodaotao].[MaTruong]";
            controller.PushDataToShowForm(query);
        }

        private void llblQ12_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //12. Tên các trường có chuyên ngành có chỉ tiêu không it hơn 100 chỉ tiêu ?
            query = "SELECT [cosodaotao].[TenTruong] as 'Tên Trường' ,[chuyennganhdaotao].[TenChuyenNganh] as 'Tên Chuyên Ngành' ,[cosonganh].[DiemChuan] as 'Điểm Chuẩn' ,[ChiTieu] as 'Chỉ Tiêu' FROM [QLDT_V1].[dbo].[cosonganh] INNER JOIN [QLDT_V1].[dbo].[chuyennganhdaotao] ON [cosonganh].MaNganh=[chuyennganhdaotao].MaNganh INNER JOIN [QLDT_V1].[dbo].[cosodaotao] ON [cosonganh].MaTruong=[cosodaotao].MaTruong WHERE [ChiTieu] >= 100";
            controller.PushDataToShowForm(query);
        }
    }
    public class ComboboxItem
    {
        public string Text { get; set; }
        public object Value { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }
}
