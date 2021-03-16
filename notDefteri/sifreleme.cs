using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace notDefteri
{
    class sifreleme
    {
        private Kullanicilar k;
        //private anaEkran context;
        private RSA rsa;
        public sifreleme(Kullanicilar k)
        {
            //this.context = form;
            this.k = k;
            //rsa.ImportParameters(k.kurtarmaKey);
        }

        public string rsaSifreUret()
        {
            //rsa = RSA.Create(2048);
            //return rsa.ExportParameters(true);
            RSACryptoServiceProvider dec = new RSACryptoServiceProvider(2048);
            return dec.ToXmlString(true);
        }

        public static byte[] ByteDonustur(string deger)
        {
            UnicodeEncoding ByteConverter = new UnicodeEncoding();
            return ByteConverter.GetBytes(deger);
        }
        public string sifrele(string mesaj)
        {
            string sonuc = "";
            if(mesaj != "" && mesaj != null)
            {
                byte[] byteDizi = ByteDonustur(mesaj);
                RSACryptoServiceProvider dec = new RSACryptoServiceProvider();
                //dec.ImportParameters(k.kurtarmaKey);
                dec.FromXmlString(k.kurtarmaKey);
                byte[] byteCikti = dec.Encrypt(byteDizi, false);
                sonuc = Convert.ToBase64String(byteCikti);
            }
            return sonuc;
        }

        public string sifreCoz(string mesaj)
        {
            string sonuc = "";
            if (mesaj != "" && mesaj != null)
            {
                RSACryptoServiceProvider dec = new RSACryptoServiceProvider();
                byte[] byteDizi = Convert.FromBase64String(mesaj);
                UnicodeEncoding ue = new UnicodeEncoding();
                //dec.ImportParameters(k.kurtarmaKey);
                dec.FromXmlString(k.kurtarmaKey);
                byte[] byteCikti = dec.Decrypt(byteDizi, false);
                sonuc = ue.GetString(byteCikti);
            }
            return sonuc;
        }

        public static string sha256(string yazi)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(yazi));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
