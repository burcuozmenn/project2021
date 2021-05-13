using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StokKontrolSistemi_201735040
{
    public partial class SatisForm : Form
    {
        public SatisForm()
        {
            InitializeComponent();
        }
        Db db = new Db();
        private void SatisForm_Load(object sender, EventArgs e)
        {
            db.FillComboBox(cbproduct,"stok", "CONCAT(stok_id,'-',urun_adi)");
            db.RefreshTable(dataGridView1, "SELECT stok.urun_adi,satislar.adet,satislar.islem_tarihi,satislar.islem,satislar.odeme FROM satislar INNER JOIN stok ON stok.stok_id = satislar.urun_id");
            cbprocess.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string p_id = cbproduct.SelectedItem.ToString().Split('-')[0].Trim();
                if (cbprocess.SelectedItem.ToString() == "Satış")
                {
                    bool yeterli = db.CheckStock(p_id, tbcount.Text);
                    if (yeterli)
                    {
                        db.RunCommand("insert into satislar(urun_id,adet,islem_tarihi,islem,odeme) values(" + p_id + "," + tbcount.Text + ",'" + DateTime.Now.ToString("yyyy-MM-dd hh:mm") + "','" + cbprocess.SelectedItem.ToString() + "',(select satis_fiyati from stok where stok_id=" + p_id + ")*" + tbcount.Text + ")");
                        db.RunCommand("UPDATE stok SET stok=stok-" + tbcount.Text + ",sonislemtarihi='" + DateTime.Now.ToString("yyyy-MM-dd hh:mm") + "' WHERE stok_id=" + p_id);
                    }
                    else
                    {
                        MessageBox.Show("Stokta yeterli miktarda ürün yok!");
                    }
                }
                else if (cbprocess.SelectedItem.ToString() == "Alış")
                {
                    db.RunCommand("insert into satislar(urun_id,adet,islem_tarihi,islem,odeme) values(" + p_id + "," + tbcount.Text + ",'" + DateTime.Now.ToString("yyyy-MM-dd hh:mm") + "','" + cbprocess.SelectedItem.ToString() + "',(select satis_fiyati from stok where stok_id=" + p_id + ")*" + tbcount.Text + ")");
                    db.RunCommand("UPDATE stok SET stok=stok+" + tbcount.Text + ",sonislemtarihi='" + DateTime.Now.ToString("yyyy-MM-dd hh:mm") + "' WHERE stok_id=" + p_id);
                }
                db.RefreshTable(dataGridView1, "SELECT stok.urun_adi,satislar.adet,satislar.islem_tarihi,satislar.islem,satislar.odeme FROM satislar INNER JOIN stok ON stok.stok_id = satislar.urun_id");
            }
            catch (Exception)
            {
                MessageBox.Show("Lütfen Tüm Boşlukları Doldurunuz.");
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            db.RefreshTable(dataGridView1, "SELECT stok.urun_adi,satislar.adet,satislar.islem_tarihi,satislar.islem,satislar.odeme FROM satislar INNER JOIN stok ON stok.stok_id = satislar.urun_id");
        }
    }
}
