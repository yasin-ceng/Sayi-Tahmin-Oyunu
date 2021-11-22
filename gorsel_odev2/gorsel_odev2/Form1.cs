using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gorsel_odev2
{
    public partial class Form1 : Form
    {

        //global değişkenler sayinin rakamlarını almak için ve rastgele üretilen sayı için tanımlanmıştır
        private int[] sayiRakamlariCan = new int[4];
        private int sayimizYasin = 0;
        //rakamları farklı sayıların kontrolü için tanımlanmıştır
        bool rakamlariFarkliYasin;


        public Form1()
        {
            InitializeComponent();

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            SayiUretimYasin();
        }


        private void btn_kontrol_Click(object sender, EventArgs e)
        {
           
            TahminKontrolBandirma();

        }
        
        public void SayiUretimYasin()
        {
            Random rastgele = new Random();
            
            //rastgele bir sayı üretilir ve sayının rakamları farklı değil ise döngü devam eder
            do
            {
                //üretilen sayının rakamlarının farklı olup olmadığının kontrolü için bir değişken oluşturulmuştur
                rakamlariFarkliYasin = true;
            
                sayimizYasin = rastgele.Next(1000, 10000);

                //sayının rakamları ayrılarak int tipindeki bir diziye alınır
            
                for (int i = 0; i < 4; i++)
                {
                    sayiRakamlariCan[i] = sayimizYasin % 10;
                    sayimizYasin /= 10;
                }
                //rakamların farklı olup olmadığı kontrol edilir

                for (int i = 0; i < 4; i++)
                {
                    for (int t = 0; t < 4; t++)
                    {
                        //sayı kendi dışındaki bir sayı ile aynı ise rakam kontrol değişkeni false eder
                        if (i != t && sayiRakamlariCan[i] == sayiRakamlariCan[t])
                                rakamlariFarkliYasin = false;
                    }
                }
                
                //döngü, sadece rakam kontrol değişkeni baştan sona true olarak gelmesi durumunda sona erer
            } while (!rakamlariFarkliYasin);
        }

        public void TahminKontrolBandirma()
        {
            //puanlama değişkenleri tanımlamaları
            int pozitifIpucu = 0;
            int negatifIpucu = 0;

        //kullanıcının girdiği sayının kontrolleri yapılır 
        //uygun formatta veri girilmediyse buraya tekrar geri döner
        basaDon:
            //eğer sayısal değer dışında veri girilirse hata yakalanacaktır
            try{ 
            rakamlariFarkliYasin = true;
            //sıfır ile başlayan değer girilirse uyarı gösterilir
            if (txtbx_tahmin.Text.StartsWith("0"))
                MessageBox.Show("Girilen Sayı 0 ile başlayamaz");
            
            else
            {
                //gelen sayı integer değere çevrilerek bir değişkene atanır
                int tahminSayisiYasin = Convert.ToInt32(txtbx_tahmin.Text);
                
                //kullanıcı 4 basamaklı bir sayı girmediyse uyarı alır ve başa döner
                if (txtbx_tahmin.TextLength != 4)
                {
                        txtbx_tahmin.Text = "";
                        MessageBox.Show("Lütfen 4 Basamaklı Bir Sayı Giriniz");
                        goto basaDon;
                }
                // kullanıcının girdiği sayı listbox'a eklenir
                int temp = tahminSayisiYasin;
                //gelen sayının rakamlarının eklenmesi için 4 elemanlı bir dizi oluşturulur
                int[] tahminRakamlariCan = new int[4];
                
                // tahmin edilen sayı rakamlarına ayrılır
                for (int i = 0; i < 4; i++)
                {
                    tahminRakamlariCan[i] = temp % 10;
                    temp = temp / 10;
                }
                    
                //girilen sayıdaki rakamların farklı olup olmadığı kontrol edilir

                for (int i = 0; i < 4; i++)
                {
                    for (int t = 0; t < 4; t++)
                    {
                        //sayı kendi dışındaki bir sayı ile aynı ise rakam kontrol değişkeni false eder
                        if (i != t && tahminRakamlariCan[i] == tahminRakamlariCan[t])
                            rakamlariFarkliYasin = false;
                    }
                }
                if (!rakamlariFarkliYasin)
                {
                    MessageBox.Show(" Sadece Rakamları Farklı Değerler Girebilirsiniz");
                    txtbx_tahmin.Text = "";
                    goto basaDon;
                }

                //tahmin rakamları ile sayını rakamları karşılaştırılır ve puanlama yapılır
                for (int i = 0; i < 4; i++)
                {
                    if (tahminRakamlariCan[i].Equals(sayiRakamlariCan[i]))
                        pozitifIpucu++;
                    else
                        for (int n = 0; n < 4; n++)
                        {
                            if (tahminRakamlariCan[n].Equals(sayiRakamlariCan[i]))
                                negatifIpucu--;
                        }
                }
                //kullanıcıya tahmin sonuçları gösterilir 
                lbl_sonuc.Text = "+" + pozitifIpucu + "," + negatifIpucu;
                //kullanıcının tahmin yürüttüğü sayılar ve tahmin sonuçları not edilir
                listBox1.Items.Add(tahminSayisiYasin + " [+" + pozitifIpucu + "," + negatifIpucu + "]");

                    //eğer doğru sonuca ulaşıldıysa tebrik mesajı görüntülenerek program sonlandırılır
                    if (pozitifIpucu == 4 && negatifIpucu == 0)
                {
                    MessageBox.Show("Tebrikler, Buldunuz \nSayı: " + txtbx_tahmin.Text);
                    SayiUretimYasin();
                    listBox1.Items.Clear();
                    lbl_sonuc.Text = "0,0";
                    this.Close();
                }
                
            }
            } catch (System.FormatException) //girilen veri sayısala çevrilemediğinde çalışır
                {
                    MessageBox.Show("Lütfen Geçerli Bir Değer Girin"); 
                }
        }

        //Form2 ekranında kullanıcıya oyun hakkında yardım bilgisi görüntülenir
        private void button1_Click(object sender, EventArgs e)
        {
            Form2 yardimYasin = new Form2();
            yardimYasin.Show();
        }
    }
}
