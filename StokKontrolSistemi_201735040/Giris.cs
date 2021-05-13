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
    public partial class Giris : Form
    {
        public Giris()
        {
            InitializeComponent();
        }
        public bool loggedin = false;
        public string authority;
        Db database = new Db();
        private void button1_Click(object sender, EventArgs e)
        {
           authority= database.Login(textBox1.Text, textBox2.Text);
            if (authority!="")
            {
                loggedin = true;
                this.Close();
            }
        }

        private void Giris_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!loggedin)
            {
                Application.Exit();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
