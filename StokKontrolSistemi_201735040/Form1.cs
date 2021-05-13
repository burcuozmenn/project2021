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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Giris loginform = new Giris();
        private void Form1_Load(object sender, EventArgs e)
        {
            loginform.ShowDialog();
            labelauth.Text = loginform.authority;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (loginform.authority=="Yönetici"||loginform.authority=="Kasa")
            {
                SatisForm st = new SatisForm();
                st.ShowDialog();
            }
            else
            {
                MessageBox.Show("Yetkiniz bulunmuyor.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (loginform.authority=="Yönetici"||loginform.authority=="Rapor")
            {
                Rapor rprform = new Rapor();
                rprform.ShowDialog();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (loginform.authority == "Yönetici")
            {
                Yonetim yntmfrm = new Yonetim();
                yntmfrm.ShowDialog();
            }
        }
    }
}
