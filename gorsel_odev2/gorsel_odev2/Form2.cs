using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gorsel_odev2
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();

            richTextBox1.Text= "Oyunda aklımdaki sayıyı tahmin etmeye çalışacaksınız. " +
                " Yaptığınız tahmin ile aklımdaki sayı içerisinde hem varlığını " +
                "hem de yerini doğru bildiğiniz basamak değerleri için sizlere + bir ipucu," +
                " varlığı doğru ama basamak değerini tutturamadığınız tahminler için ise – bir ipucu verilecektir.";
        }

    }
}
