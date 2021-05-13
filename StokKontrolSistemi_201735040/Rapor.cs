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

namespace StokKontrolSistemi_201735040
{
    public partial class Rapor : Form
    {
        public Rapor()
        {
            InitializeComponent();
        }
        Db db = new Db();
        private void Rapor_Load(object sender, EventArgs e)
        {
            db.FillComboBox(cbproduct, "stok", "CONCAT(stok_id,'-',urun_adi)");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            db.GetReport("Tüm");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string p_id = cbproduct.SelectedItem.ToString().Split('-')[0].Trim();
                db.GetReport(p_id);
            }
            catch (Exception)
            {
                MessageBox.Show("Lütfen Bir Ürün Seçiniz.");
            }
        }
    }
}
