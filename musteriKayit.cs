using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace Market_Satis
{
    public partial class musteriKayit : Form
    {
        public musteriKayit()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-8UM19B5;Initial Catalog=vtysprojeodev;Integrated Security=True");
        private void musteriKayit_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into musteri(TC,isim,telefon)values(@TC,@isim,@telefon)", baglanti);
            komut.Parameters.AddWithValue("@TC", TC.Text);
            komut.Parameters.AddWithValue("@isim", isim.Text);
            komut.Parameters.AddWithValue("@telefon", telefon.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kayıt Gerçekleştirildi.");
            foreach (Control item in this.Controls)
            {
                if(item is TextBox)
                {
                    item.Text = "";
                }
            }
        }
    }
}
