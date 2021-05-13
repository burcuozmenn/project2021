using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;

namespace StokKontrolSistemi_201735040
{
    
    class Db
    {
        public static SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\StokVt.mdf;Integrated Security=True");
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter da = new SqlDataAdapter();
        SqlDataReader dr;
        public static DataSet ds = new DataSet();
        
        public void RunCommand(string komut)
        {
            try
            {
                con.Open();
                cmd = new SqlCommand(komut, con);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bir hata oluştu. Lütfen veri girişlerini kontrol ediniz. "+ex.Message);
            }
            finally
            {
                con.Close();
            }
            
        }   
        public string Login(string kad,string sfr)
        {
            string authority = "";
            try
            {
                con.Open();
                cmd = new SqlCommand("select * from kullanicilar where kullaniciadi='"+kad+"' and sifre='"+sfr+"'", con);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    MessageBox.Show("Giriş Başarılı.");
                    authority = dr[3].ToString();
                }
                else
                {
                    MessageBox.Show("Hatalı Kullanıcı Adı / Şifre.");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Bir hata oluştu.");
                authority = "Giriş Yapınız.";
            }
            finally
            {
                con.Close();
            }
            return authority;
        }
        public void FillComboBox(ComboBox cmb, string tablo, string sutun)
        {
            try
            {
                cmb.Items.Clear();
                con.Open();
                cmd = new SqlCommand("select " + sutun + " from " + tablo, con);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    cmb.Items.Add(dr[0]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bir hata oluştu. "+ ex.Message);
            }
            finally
            {
                con.Close();
            }
        }
        public bool CheckStock(string p_id,string adet)
        {
            con.Open();
            cmd = new SqlCommand("select stok from stok where stok_id=" + p_id,con);
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                if (Convert.ToInt32(dr[0])>=Convert.ToInt32(adet))
                {
                    con.Close();
                    return true;
                }
                else
                {
                    con.Close();
                    return false;
                }
            }
            else
            {
                con.Close();
                return false;
            }
            
        }
        public void RefreshTable(DataGridView dg, string sorgu)
        {
            ds = new DataSet();
            con.Open();
            da = new SqlDataAdapter(sorgu,con);
            da.Fill(ds);
            dg.DataSource = ds.Tables[0];
            con.Close();
        }
        public List<string> GetProduct(string id)
        {
            List<string> productdata = new List<string>();
            con.Open();
            cmd = new SqlCommand("SELECT * FROM stok WHERE stok_id=" + id,con);
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                productdata.Add(dr[0].ToString());
                productdata.Add(dr[1].ToString());
                productdata.Add(dr[2].ToString());
                productdata.Add(dr[3].ToString());
                productdata.Add(dr[4].ToString());
            }
            con.Close();
            return productdata;
        }
        public void GetReport(string p_id)
        {
            con.Open();
            StreamWriter txt = new StreamWriter("rapor.txt");
            txt.WriteLine(String.Format("{0,-15} | {1,-15} | {2,-15} | {3,-30} | {4,-15}","Stok ID","Ürün Adı","Stok Durumu","Son İşlem Tarihi","Satış Fiyatı"));
            if (p_id=="Tüm")
            {
                cmd = new SqlCommand("select * from stok", con);
            }
            else
            {
                cmd = new SqlCommand("select * from stok where stok_id="+p_id, con);
            }
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                txt.WriteLine(String.Format("{0,-15} | {1,-15} | {2,-15} | {3,-30} | {4,-15}", dr[0].ToString() ,dr[1].ToString(),dr[2].ToString(), dr[3].ToString(),dr[4].ToString()));
            }
            txt.Close();
            System.Diagnostics.Process.Start("rapor.txt");
            con.Close();

        }
    }
}
