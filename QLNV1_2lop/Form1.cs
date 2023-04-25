using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace QLNV1_2lop
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //Phương thức load dữ liệu phòng ban lên cboPhongBan
        public void loadPB()
        {
            string sql = "select * from PHONGBAN";

            cboPhongBan.DataSource = DataProvider.getTable(sql);
            cboPhongBan.DisplayMember = "TenPB";
            cboPhongBan.ValueMember = "MaPB";
        }
        //Phương thức load dữ liệu Nhân viên lên DataGridView
        public void loadData()
        {
            string sql = "select * from NhanVien";
            data.DataSource = DataProvider.getTable(sql);

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            DataProvider.moKetNoi();
            loadPB();
            loadData();
            DataProvider.dongKetNoi();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string sql = "insert into NhanVien(MaNV,TenNV,NgaySinh,GioiTinh,SoDT,maPB) values(@ma,@ht,@ns,@gt,@sdt,@maPB)";
            string[] name = {"@ma", "@ht", "@ns", "@gt", "@sdt", "@maPB" };
            bool gt = rdNam.Checked == true ? true : false;
            object[] value = { txtMaNV.Text,txtTenNV.Text,dtpNgaySinh.Value,gt,txtSDT.Text,cboPhongBan.SelectedValue};
           
            DataProvider.moKetNoi();
            //Kiểm tra mã nhân viên có bị trùng không
            string sql1 = string.Format("select count(*) from NhanVien where MaNV ='{0}'", txtMaNV.Text);
            if (DataProvider.executeScalar(sql1)==0)
            {
                DataProvider.updateData(sql, name, value);
                MessageBox.Show("Thêm thành công");
                loadData();
            }    
            else
            {
                MessageBox.Show("Trùng mã nhân viên");
                txtMaNV.Clear();
                txtMaNV.Focus();
            }    
            DataProvider.dongKetNoi();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Bạn có chắc chắn muốn xóa không?",
               "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dr == DialogResult.Yes)
            {
                int i = data.CurrentCell.RowIndex;
                string ma = data.Rows[i].Cells[0].Value.ToString();
                string sql = string.Format("delete from NhanVien where maNv ='{0}'", ma);
                DataProvider.moKetNoi();
                DataProvider.updateData(sql);
                loadData();
                DataProvider.dongKetNoi();
            }    
                
        }

        private void data_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = data.CurrentCell.RowIndex;
            txtMaNV.Text = data.Rows[i].Cells[0].Value.ToString();
            txtTenNV.Text = data.Rows[i].Cells[1].Value.ToString();
            dtpNgaySinh.Text = data.Rows[i].Cells[2].Value.ToString();
            string gt = data.Rows[i].Cells[3].Value.ToString();
            if (gt == "True")
            {
                rdNam.Checked = true;
            }
            else
                rdNu.Checked = true;
            txtSDT.Text = data.Rows[i].Cells[4].Value.ToString();
            cboPhongBan.SelectedValue = data.Rows[i].Cells[5].Value.ToString();
        }
    }
}
