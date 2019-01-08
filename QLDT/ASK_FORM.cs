using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
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
        public ASK_FORM()
        {
            InitializeComponent();
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
            //SqlConnection conn = DBUtil
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
                    query = "Select * From [QLDT_V1].[dbo].[cosodaotao] WHERE " + criteria_field + " LIKE N'%" + input + "%'";
                else if (numberSearch == 0)
                {
                    query = "Select chuyennganhdaotao.MaNganh, chuyennganhdaotao.TenChuyenNganh, cosonganh.ChiTieu, cosonganh.DiemChuan From [QLDT_V1].[dbo].[chuyennganhdaotao] LEFT JOIN [QLDT_V1].[dbo].[cosonganh] ON [QLDT_V1].[dbo].[cosonganh].[MaNganh] = [QLDT_V1].[dbo].[chuyennganhdaotao].[MaNganh] " + "WHERE [QLDT_V1].[dbo].[chuyennganhdaotao].[" + criteria_field + "] LIKE N'%" + input + "%'";
                }
                MessageBox.Show(query);
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
                    query = "Select * From [QLDT_V1].[dbo].[cosodaotao] WHERE " + criteria_field + " LIKE N'%" + input + "%' AND TinhThanh LIKE N'%" + cbbTextTinhThanh + "%' AND DVChuQuan LIKE N'%" + cbbTextDVChuQuan + "%';";
                }
                else if (numberSearch == 0) {
                    query = "SELECT [QLDT_V1].[dbo].[cosodaotao].[TenTruong] as 'Trường' ,[QLDT_V1].[dbo].[chuyennganhdaotao].[TenChuyenNganh] as 'Tên Chuyên Ngành', [QLDT_V1].[dbo].[cosonganh].[MaNganh] as 'Mã Ngành' FROM [QLDT_V1].[dbo].[cosonganh],[QLDT_V1].[dbo].[cosodaotao], [QLDT_V1].[dbo].[chuyennganhdaotao] WHERE [QLDT_V1].[dbo].[cosonganh].[MaTruong]=[QLDT_V1].[dbo].[cosodaotao].[MaTruong] AND [QLDT_V1].[dbo].[cosonganh].[MaNganh]=[QLDT_V1].[dbo].[chuyennganhdaotao].[MaNganh] AND [QLDT_V1].[dbo].[chuyennganhdaotao].[" + criteria_field + "] LIKE N'%" + input + "%'AND TinhThanh LIKE N'%" + cbbTextTinhThanh + "%' AND DVChuQuan LIKE N'%" + cbbTextDVChuQuan + "%';";
                }
            }
            //MessageBox.Show(query);
            new SHOW_FORM(query).Show();
        }

        private void llblQ1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //Các trường đào tạo CNTT ?
            query = "Select MaTruong as 'Mã Trường', TenTruong as 'Tên Trường', Website  From [QLDT_V1].[dbo].[cosodaotao] ";
            SHOW_FORM sf = new SHOW_FORM(query);
            sf.Show();
        }

        private void llblQ2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            query = "Select TenTruong as 'Tên Trường', DiaChi as 'Địa Chỉ' From [QLDT_V1].[dbo].[cosodaotao] ";
            SHOW_FORM sf = new SHOW_FORM(query);
            sf.Show();
        }

        private void llblQ3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            query = "Select TenTruong, Website From [QLDT_V1].[dbo].[cosodaotao] ";
            SHOW_FORM sf = new SHOW_FORM(query);
            sf.Show();
        }

        private void pnlAdvance_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cbbTinhThanh_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void searchTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSearch.PerformClick();  
            }
        }

        private void llblQ4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            query = "Select *  From [QLDT_V1].[dbo].[chuyennganhdaotao] ";
            SHOW_FORM sf = new SHOW_FORM(query);
            sf.Show();
        }

        private void llblQ5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            query = "Select DISTINCT DVChuquan  From [QLDT_V1].[dbo].[cosodaotao] ";
            SHOW_FORM sf = new SHOW_FORM(query);
            sf.Show();
        }

        private void llblQ6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //So Co so dao tao CNTT tai HN
            query = "SELECT COUNT(DISTINCT MaTruong)  FROM [QLDT_V1].[dbo].[cosodaotao] WHERE TinhThanh LIKE N'%Hà Nội%'";
            SHOW_FORM sf = new SHOW_FORM(query);
            sf.Show();
        }

        private void llblQ7_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //Chi tieu tuyen sinh cac truong
            query = "SELECT [QLDT_V1].[dbo].[cosodaotao].[TenTruong] as 'Trường' ,SUM([QLDT_V1].[dbo].[cosonganh].[ChiTieu]) as 'Tổng Chỉ Tiêu' FROM [QLDT_V1].[dbo].[cosonganh],[QLDT_V1].[dbo].[cosodaotao] WHERE [QLDT_V1].[dbo].[cosonganh].[MaTruong]=[QLDT_V1].[dbo].[cosodaotao].[MaTruong] GROUP BY [QLDT_V1].[dbo].[cosodaotao].[TenTruong]";
            SHOW_FORM sf = new SHOW_FORM(query);
            sf.Show();
        }

        private void llblQ8_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //So luong csdt cac tinh thanh
            query = "SELECT TinhThanh, COUNT(*) FROM [QLDT_V1].[dbo].[cosodaotao] GROUP BY TinhThanh";
            SHOW_FORM sf = new SHOW_FORM(query);
            sf.Show();
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
