using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace QLNV1_2lop
{
   static class DataProvider
    {
        public static SqlConnection cnn;
        public static SqlDataAdapter da;
        public static SqlCommand cmd;

        public static void moKetNoi()
        {
            cnn = new SqlConnection();
            cnn.ConnectionString = ConfigurationManager.ConnectionStrings["KetNoi"].ToString();
            try
            {
                cnn.Open();
            }
            catch (Exception ex) { };
        }
        public static void dongKetNoi()
        {
            cnn.Close();
        }
        //Lấy dữ liệu
        public static DataTable  getTable(string sql)
        {
            DataTable dt = new DataTable();
            da = new SqlDataAdapter(sql, cnn);
            da.Fill(dt);
            da.Dispose();
            return dt;
        }
        //Cập nhật dữ liệu
        public static void updateData(string sql, string [] name =null, object[] value =null)
        {
            cmd = new SqlCommand(sql, cnn);
            cmd.Parameters.Clear();
            if (value!=null)
            {
                for (int i = 0; i < value.Length; i++)
                    cmd.Parameters.AddWithValue(name[i], value[i]);
            }    
            cmd.ExecuteNonQuery();
            cmd.Dispose();
        }
        //Kiểm tra dữ liệu dùng Select count(*)
        public static int executeScalar(string sql)
        {
            int i = 0;
            cmd = new SqlCommand(sql, cnn);
            i =(int)cmd.ExecuteScalar();
            return i;
        }
    }
}
