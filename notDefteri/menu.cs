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
    public partial class menu : Form
    {
        private anaEkran context;
        private Kullanicilar k;
        private duzenle d;
        private FileInfo[] Files;
        public string path = Environment.ExpandEnvironmentVariables(@"%Appdata%\NotDefteri\data\");
        public menu(anaEkran form)
        {
            InitializeComponent();
            this.context = form;
            //d = new duzenle(this, );
            FormBorderStyle = FormBorderStyle.None;
            TopLevel = false;
            AutoScroll = true;
        }
        public void kullaniciAta(Kullanicilar k)
        {
            this.k = k;
        }

        private void menu_Load(object sender, EventArgs e)
        {
            yenile();
        }

       
        private void yenile()
        {
            listBox1.Items.Clear();
            //string[] Files = Directory.GetFiles(path + k.kullaniciAdi, "*.not");
            DirectoryInfo d = new DirectoryInfo(path + k.kullaniciAdi);
            Files = d.GetFiles("*.not");
            foreach(FileInfo file in Files)
            {
                listBox1.Items.Add(file.Name.Substring(0,file.Name.Length-4));
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            context.notOlustur(path + k.kullaniciAdi + @"\",0,"0");
        }

        private void button1_Click(object sender, EventArgs e)
        {
                if (listBox1.SelectedItem != null)
                {
                //d.dosyaAc(path + k.kullaniciAdi +@"\"+ listBox1.SelectedItem+@".not");
                context.dosyaAc(path + k.kullaniciAdi + @"\" + listBox1.SelectedItem + @".not");
                }    
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                File.Delete(path + k.kullaniciAdi + @"\" + listBox1.SelectedItem + @".not");
                yenile();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                context.notOlustur(path + k.kullaniciAdi + @"\", 1, path + k.kullaniciAdi + @"\"+ listBox1.SelectedItem + ".not");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            context.CikisYap();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            context.kullaniciIslemleri();
        }
    }
}
