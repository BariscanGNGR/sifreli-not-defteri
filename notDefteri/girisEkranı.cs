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
using System.Security.Cryptography;

namespace notDefteri
{
    public partial class girisEkranı : Form
    {
        public Kullanicilar k1;
        private anaEkran context;
        public string path = Environment.ExpandEnvironmentVariables(@"%Appdata%\NotDefteri\data\");
        public girisEkranı(anaEkran form)
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.None;
            TopLevel = false;
            AutoScroll = true;
            this.context= form;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "" && textBox1.Text == "")
            {
                label3.Text = "Kutuları doldurunuz!";
            }
            else
            {
                string text = File.ReadAllText(path + @"kullanicilar.dat");
                List<Kullanicilar> json = JsonConvert.DeserializeObject<List<Kullanicilar>>(text);
                bool giris = false;//eğerki aynı kullanıcı adı varsa iptal eder 
                if (json != null)
                {
                    foreach (Kullanicilar k in json)
                    {
                        if (k.kullaniciAdi == textBox1.Text.ToLower() && k.sifre == sifreleme.sha256(textBox2.Text))
                        {
                            giris = true;
                            k1 = new Kullanicilar(k.kullaniciAdi, k.sifre, k.kurtarmaKey);
                            //anaEkran.k1 = k1;
                            break;
                        }
                    }
                }
                if (!giris)
                {
                    label3.Text = "Hatalı giriş!";
                }
                else
                {
                    label3.Text = "Giriş başarılı!";
                    context.girisBasarili(k1);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == ""  || textBox1.Text == "")
            {
                label3.Text = "Kutuları doldurunuz!";
            }
            else
            {
                string text = File.ReadAllText(path + @"kullanicilar.dat");
                List<Kullanicilar> json;

                bool tekrarlama = false;//eğerki aynı kullanıcı adı varsa iptal eder 

                if (text != "")
                {
                    json = JsonConvert.DeserializeObject<List<Kullanicilar>>(text);
                    foreach (Kullanicilar k in json)
                    {
                        if (k.kullaniciAdi == textBox1.Text.ToLower())
                        {
                            tekrarlama = true;
                            break;
                        }
                    }
                }
                else { json = new List<Kullanicilar>(); }
                if (tekrarlama)
                {
                    label3.Text = "HATA: Kullanıcı adı mevcut!";
                }
                else
                {
                    sifreleme sif = new sifreleme(k1);
                    json.Add(new Kullanicilar(textBox1.Text.ToLower(), sifreleme.sha256(textBox2.Text), sif.rsaSifreUret()));
                    File.WriteAllText(path + @"kullanicilar.dat", JsonConvert.SerializeObject(json));
                    Directory.CreateDirectory(path + textBox1.Text.ToLower());
                    label3.Text = "Kullanıcı oluşturuldu lütfen giriş yapınız..";
                }
            }
        }

        private void girisEkranı_Load(object sender, EventArgs e)
        {
            if (!File.Exists(Environment.ExpandEnvironmentVariables(@"%Appdata%\NotDefteri\data\kullanicilar.dat")))
            {
                Directory.CreateDirectory(Environment.ExpandEnvironmentVariables(@"%Appdata%\NotDefteri\"));
                Directory.CreateDirectory(path);
                //Directory.CreateDirectory(path + @"\d");
                //File.Create(path + @"kullanicilar.dat");
                //File.ReadAllText(path + @"kullanicilar.dat");
                File.WriteAllText(path + @"kullanicilar.dat", "");
            }
        }
    }
    public class Kullanicilar
        {
            public Kullanicilar(string kullaniciAdi,string sifre,string kurtarmaKey)
            {
            this.kullaniciAdi = kullaniciAdi;
            this.sifre = sifre;
            this.kurtarmaKey = kurtarmaKey;
            }
            
            public string kullaniciAdi { get; set; }
            public string sifre { get; set; }
            public string kurtarmaKey { get; set; }
        }
}
