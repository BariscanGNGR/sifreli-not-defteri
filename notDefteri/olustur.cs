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
    public partial class olustur : Form
    {
        private anaEkran context;
        private string path;
        private byte secim;
        private string eskiDosya;
        public olustur(anaEkran form,string path,byte neYapilacak,string eskiDosya)//0 olustur 1 isim değiştir
        {
            InitializeComponent();
            this.context = form;
            this.path = path;
            this.secim = neYapilacak;
            this.eskiDosya = eskiDosya;
            FormBorderStyle = FormBorderStyle.None;
            TopLevel = false;
            AutoScroll = true;

            if(secim == 1)
            {
                button1.Text = "Değiştir";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string newPath = path + @"\" + textBox1.Text + ".not";
            if (secim == 0)
            {
                File.WriteAllText(newPath, "");            
            }
            else
            {
                string buffer = File.ReadAllText(eskiDosya);
                File.WriteAllText(newPath, buffer);
                File.Delete(eskiDosya);
            }
            context.dosyaAc(newPath);
        }
    }
}
