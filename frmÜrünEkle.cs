using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Market_Satis
{
    public partial class frmUrunEkleme : Form
    {
        public frmUrunEkleme()
        {
            InitializeComponent();
        }
        private void kategorigetir()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select *from markabilgileri where kategori='"+comboKategori.SelectedItem+"'", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                comboKategori.Items.Add(read["kategori"].ToString());
            }
            baglanti.Close();
        }
        private void frmUrunEkleme_Load(object sender, EventArgs e)
        {
            kategorigetir();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            if (BarkodNotxt.Text=="") 
            {
                lblMiktari.Text = ""; 
                foreach (Control item in groupBox2.Controls) 
                {
                   if (item is TextBox )
                    {
                        item.Text = "";
                    }
                }
            }

            baglanti.Open();
            SqlCommand komut = new SqlCommand("select *from urun where barkodno like '"+BarkodNotxt.Text+"'", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read()) 
            {
                Kategoritxt.Text = read["kategori"].ToString();
                Markatxt.Text = read["marka"].ToString();
                UrunAditxt.Text = read["urunadi"].ToString();
                lblMiktari.Text = read["miktari"].ToString();
                AlisFiyatitxt.Text = read["alisfiyati"].ToString();
                SatisFiyatitxt.Text = read["satisfiyati"].ToString();
            }
            baglanti.Close();
        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox12_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnVarOlanaEkle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("update urun set miktari=miktari+'"+int.Parse(Miktaritxt.Text)+"'where barkodno='"+BarkodNotxt.Text+"'",baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            foreach (Control item in groupBox2.Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }
            }
            MessageBox.Show("Var olan ürüne ekleme yapıldı");

        }

        private void comboKategori_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboMarka.Items.Clear();
            comboMarka.Text = "";
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select *from markabilgileri", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                comboMarka.Items.Add(read["marka"].ToString());
            }
            baglanti.Close();
        }

        private void btnYeniEkle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into urun(barkodno,kategori,marka,urunadi,miktari,alisfiyati,satisfiyati,tarih) values(@barkodno,@kategori,@marka,@urunadi,@miktari,@alisfiyati,@satisfiyati,@tarih)", baglanti);
            komut.Paremeters.AddWithValue("@barkodno",txtBarkodNo.Text);
            komut.Paremeters.AddWithValue("@kategori", comboKategori.Text);
            komut.Paremeters.AddWithValue("@marka", comboMarka.Text);
            komut.Paremeters.AddWithValue("@urunadi", txtUrunAdi.Text);
            komut.Paremeters.AddWithValue("@miktari", int.Parse(txtMiktari.Text));
            komut.Paremeters.AddWithValue("@alisfiyati", double.Parse (txtAlisFiyati.Text));
            komut.Paremeters.AddWithValue("@satisfiyati", double.Parse(txtSatisFiyati.Text));
            komut.Paremeters.AddWithValue("@tarih",DateTime.Now.ToString()); 
            
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Ürün eklendi");
            comboMarka.Items.Clear();

            foreach (Control item in groupBox1.Controls) 
            {
                if (item is TextBox) 
                {
                    item.Text = "";
                }
                if (item is ComboBox)
                {
                    item.Text = "";
                }
            }
        }
    }
}
