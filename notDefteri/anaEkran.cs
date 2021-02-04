using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace notDefteri
{
    public partial class anaEkran : Form
    {
        public Kullanicilar k;
        public string path = Environment.ExpandEnvironmentVariables(@"%Appdata%\NotDefteri\data\");

        private girisEkranı ge;
        private menu me;
        private duzenle du;
        private olustur ol;
        private kullaniciİslemleri ki;

        //private sifreleme sif;

        public anaEkran()
        {
            InitializeComponent();
            ge = new girisEkranı(this);
            panel1.Controls.Add(ge);
            ge.Show();
        }

        public void girisBasarili(Kullanicilar k1)
        {
            this.k = k1;
            me = new menu(this);
            me.kullaniciAta(k1);
            ge.Close();
            panel1.Controls.Clear();
            panel1.Controls.Add(me);
            me.Show();
            //sif = new sifreleme(this,k);
        }

        public void dosyaAc(string path)
        {
            du = new duzenle(this,k);
            du.dosyaAc(path);
            me.Close();
            //ol.Close();
            panel1.Controls.Clear();
            panel1.Controls.Add(du);
            du.Show();
        }

        public void menuDonus()
        {
            me = new menu(this);
            du.Close();
            panel1.Controls.Clear();
            panel1.Controls.Add(me);
            me.kullaniciAta(k);
            me.Show();
        }
        public void menuDonus2()
        {
            me = new menu(this);
            ki.Close();
            panel1.Controls.Clear();
            panel1.Controls.Add(me);
            me.kullaniciAta(k);
            me.Show();
        }
        public void notOlustur(string path,byte secim,string eskiDosya)
        {
            ol = new olustur(this,path, secim,eskiDosya);
            panel1.Controls.Clear();
            me.Close();
            panel1.Controls.Add(ol);
            ol.Show();
        }

        public void CikisYap()
        {
            k = null;
            //sif = null;
            ge.Close();
            me.Close();
            //ol.Close();
            //du.Close();
            panel1.Controls.Clear();

            ge = new girisEkranı(this);
            panel1.Controls.Add(ge);
            ge.Show();
            
        }
        public void kullaniciIslemleri()
        {
            me.Close();
            panel1.Controls.Clear();
            ki = new kullaniciİslemleri(this, k);
            panel1.Controls.Add(ki);
            ki.Show();
        }
        

        private void anaEkran_Load(object sender, EventArgs e)
        {
            
        }
    }
}
