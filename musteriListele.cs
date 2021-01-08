using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Market_Satis
{
    public partial class musteriListele : Form
    {
        public musteriListele()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-8UM19B5;Initial Catalog=vtysprojeodev;Integrated Security=True");
        DataSet daset = new DataSet();
        private void musteriListele_Load(object sender, EventArgs e)
        {

            Kayit_Goster();
        }
        private void Kayit_Goster()
        {
            baglanti.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select *from musteri", baglanti);
            adtr.Fill(daset, "musteri");
            dataGridView1.DataSource = daset.Tables["musteri"];
            baglanti.Close();

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txtTc.Text = dataGridView1.CurrentRow.Cells["tc"].Value.ToString();
            txtAdSoyad.Text = dataGridView1.CurrentRow.Cells["adSoyad"].Value.ToString();
            txtTelefon.Text = dataGridView1.CurrentRow.Cells["telefon"].Value.ToString();

        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("update musteri set adSoyad=@adSoyad, telefon=@telefon  where   tc=@tc", baglanti);
            komut.Parameters.AddWithValue("@tc", txtTc.Text);
            komut.Parameters.AddWithValue("@adSoyad", txtAdSoyad.Text);
            komut.Parameters.AddWithValue("@telefon", txtTelefon.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            daset.Tables["musteri"].Clear();
            Kayit_Goster();
            MessageBox.Show("Kayıt Güncellendi.");
            foreach (Control item in this.Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }

            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("delete from musteri where tc ='" + dataGridView1.CurrentRow.Cells["tc"].Value.ToString() + "'", baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            daset.Tables["musteri"].Clear();
            Kayit_Goster();
            MessageBox.Show("Kayıt Silindi.");

        }

        private void tcArama_TextChanged(object sender, EventArgs e)
        {
            DataTable tablo = new DataTable();
            baglanti.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select *from musteri where tc like '%"+txtTcAra.Text+"%'", baglanti);
            adtr.Fill(tablo);
            dataGridView1.DataSource = tablo;
            baglanti.Close();


        }
    }
}
