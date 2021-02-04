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
using Newtonsoft.Json;

namespace notDefteri
{
    public partial class kullaniciİslemleri : Form
    {
        public string path = Environment.ExpandEnvironmentVariables(@"%Appdata%\NotDefteri\data\");
        private anaEkran context;
        private Kullanicilar k;
        public kullaniciİslemleri(anaEkran form , Kullanicilar k)
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.None;
            TopLevel = false;
            AutoScroll = true;
            this.context = form;
            this.k = k;
        }

        private void kullaniciİslemleri_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            panel2.Visible = false;
        }

        public void islem(byte secim,string eskiSifre,string yeniSifre)//0 ise şifre değiş 1 ise hesap sil
        {
            string text = File.ReadAllText(path + @"kullanicilar.dat");
            List<Kullanicilar> json;
            int j=-1;
            int i = 0;
            byte tekrarlama = 0;//0 hata 1 sifre değişti 2 silindi
            if (text != "")
            {
                json = JsonConvert.DeserializeObject<List<Kullanicilar>>(text);
                foreach (Kullanicilar k in json)
                {
                    if (k.kullaniciAdi == this.k.kullaniciAdi)
                    {
                        
                        if (secim == 0 && eskiSifre == k.sifre)
                        {
                            k.sifre = yeniSifre;
                            tekrarlama = 1;
                        }
                        else if(secim==1)
                        {
                            j = i;
                        }
                        else if(secim == 0 && eskiSifre != k.sifre)
                        {
                            tekrarlama = 2;
                        }
                    }
                    i++;
                }
                if (j != -1)
                {
                    json.RemoveAt(j);
                }
            }
            else { json = new List<Kullanicilar>(); }
            if (tekrarlama==1)
            {
                label3.Text = "Şifre değişti";
            }
            else if(tekrarlama==2)
            {
                label3.Text = "Hatali şifre";
            }
            File.WriteAllText(path + @"kullanicilar.dat", JsonConvert.SerializeObject(json));
            Directory.CreateDirectory(path + textBox1.Text.ToLower());
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string eskiSifre = textBox1.Text;
            string yeniSifre = textBox2.Text;
            if (yeniSifre != "" && eskiSifre != "")
            {
                islem(0, sifreleme.sha256(eskiSifre), sifreleme.sha256(yeniSifre));
            }
            else
            {
                label3.Text = "Şifre girilmedi";
            }
            
        }


        private void button3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "Hesap silinsinmi?", "Hesabınız Silinecektir onaylıyomusunuz?", MessageBoxButtons.OKCancel)==DialogResult.OK)
            {
                islem(1, "", "");

                DirectoryInfo d = new DirectoryInfo(path + k.kullaniciAdi);
                FileInfo []Files = d.GetFiles();
                foreach (FileInfo file in Files)
                {
                    File.Delete(file.FullName);
                }

                Directory.Delete(path + @"\" + k.kullaniciAdi);

                context.CikisYap();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            context.menuDonus2();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel2.Visible = true;

            listBox1.Items.Clear();
            string text = File.ReadAllText(path + @"kullanicilar.dat");
            List<Kullanicilar> json = JsonConvert.DeserializeObject<List<Kullanicilar>>(text);
            bool giris = false;//eğerki aynı kullanıcı adı varsa iptal eder 
            if (json != null)
            {
                foreach (Kullanicilar k in json)
                {
                    listBox1.Items.Add(k.kullaniciAdi);
                }
            }
        }
    }
}
