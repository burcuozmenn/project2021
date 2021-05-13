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
    public partial class Yonetim : Form
    {
        public Yonetim()
        {
            InitializeComponent();
        }
        Db db = new Db();
        private void Yonetim_Load(object sender, EventArgs e)
        {
            yenile();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            yenile();
        }
        void yenile()
        {
            db.FillComboBox(cbstockid, "stok", "CONCAT(stok_id,'-',urun_adi)");
            db.FillComboBox(cbproduct, "stok", "CONCAT(stok_id,'-',urun_adi)");
            db.RefreshTable(dataGridView1, "SELECT * FROM stok");
            if (cbproduct.Items.Count != 0)
            {
                cbstockid.SelectedIndex = 0;
                cbproduct.SelectedIndex = 0;
            }
        }
        private void button5_Click(object sender, EventArgs e)
        {
            string stokid = cbstockid.SelectedItem.ToString().Split('-')[0].Trim();
            db.RunCommand("update stok set stok=stok+"+textBox5.Text+" where stok_id="+stokid);
            yenile();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string stokid = cbstockid.SelectedItem.ToString().Split('-')[0].Trim();
            db.RunCommand("update stok set stok=stok-" + textBox5.Text + " where stok_id=" + stokid);
            yenile();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            db.RunCommand("insert into stok(urun_adi,stok,sonislemtarihi,satis_fiyati) values('" + textBox2.Text + "','" + textBox3.Text + "','" + DateTime.Now.ToString("yyyy-MM-dd hh:mm") + "','" + textBox4.Text + "')");
            yenile();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                string p_id = cbproduct.SelectedItem.ToString().Split('-')[0].Trim();
                db.RunCommand("delete from stok where stok_id=" + p_id);
                db.RunCommand("delete from satislar where urun_id=" + p_id);
            }
            catch (Exception)
            {
                MessageBox.Show("Lütfen Veri Seçiniz.");
            }
            yenile();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                string stokid = cbproduct.SelectedItem.ToString().Split('-')[0].Trim();
                if (textBox2.Text!="")
                {
                    db.RunCommand("update stok set urun_adi='" + textBox2.Text + "',sonislemtarihi='" + DateTime.Now.ToString("yyyy-MM-dd hh:mm") + "' where stok_id=" + stokid);
                }
                if (textBox3.Text != "")
                {
                    db.RunCommand("update stok set stok=" + textBox3.Text + ",sonislemtarihi='"+ DateTime.Now.ToString("yyyy-MM-dd hh:mm") + "' where stok_id=" + stokid);
                }
                if (textBox4.Text != "")
                {
                    db.RunCommand("update stok set satis_fiyati=" + textBox4.Text + ",sonislemtarihi='" + DateTime.Now.ToString("yyyy-MM-dd hh:mm") + "' where stok_id=" + stokid);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Lütfen Veri Seçiniz Veya Girilen Verileri Kontrol Ediniz.");
            }
            yenile();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string id = cbproduct.SelectedItem.ToString().Split('-')[0].Trim();
            List<string> urunBilgisi = db.GetProduct(id);
            textBox2.Text = urunBilgisi[1];
            textBox3.Text = urunBilgisi[2];
            textBox4.Text = urunBilgisi[4];
        }
    }
}
