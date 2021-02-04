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

namespace notDefteri
{
    public partial class duzenle : Form
    {
        private anaEkran context;
        private Kullanicilar k;
        private string path;

        private sifreleme sif;
        public duzenle(anaEkran m, Kullanicilar k)
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.None;
            TopLevel = false;
            AutoScroll = true;
            this.context = m;
            sif = new sifreleme(k);
        }

        public void dosyaAc(string path)
        {
            this.path = path;
            richTextBox1.Text = sif.sifreCoz(File.ReadAllText(path));
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void kaydetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            File.WriteAllText(path, sif.sifrele(richTextBox1.Text));
        }

        private void silToolStripMenuItem_Click(object sender, EventArgs e)
        {
            File.Delete(path);
            context.menuDonus();
        }

        private void çıkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            context.menuDonus();
        }

        private void duzenle_Load(object sender, EventArgs e)
        {
            
        }
    }
}
