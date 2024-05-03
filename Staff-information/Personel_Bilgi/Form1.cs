using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
// sql'deki değerleri kullanabilmem için kullanınlan kütüphaneler
using System.Data.SqlClient;

namespace Personel_Bilgi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-0VNHJA3;Initial Catalog=PersonelVeriTabanı;Integrated Security=True;");
        void temizle()
        {
            txtboxad.Text = "";
            txtboxsoyad.Text = "";
            txtboxmaas.Text = "";
            txtboxmeslek.Text = "";
            txtboxsehir.Text = "";
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            txtboxad.Focus();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtboxid.Focus();
            this.tbl_Personel_listTableAdapter.Fill(this.personelVeriTabanıData.tbl_Personel_list);
        }

        private void btnListele_Click(object sender, EventArgs e)
        {
            this.tbl_Personel_listTableAdapter.Fill(this.personelVeriTabanıData.tbl_Personel_list);

        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            baglanti.Open(); // baglanti ile sql e baglandik
            // sqlcommand oluşturduk adını komut olarak degistirdik. 
            SqlCommand komut = new SqlCommand("insert into dbo.tbl_Personel_list (PerAd,PerSoyad,PerSehir,PerMaas,PerMeslek,PerDurum) values (@p1,@p2,@p3,@p4,@p5,@p6)", baglanti);
           
            komut.Parameters.AddWithValue("@p1", txtboxad.Text); // komuttaki parametrelerle birlikte @p1 parametresine txtboxad'dan değer eklenir.
            komut.Parameters.AddWithValue("@p2", txtboxsoyad.Text);
            komut.Parameters.AddWithValue("@p3", txtboxsehir.Text);
            komut.Parameters.AddWithValue("@p4", txtboxmaas.Text);
            komut.Parameters.AddWithValue("@p5", txtboxmeslek.Text);
            
            if (radioButton1.Checked == true)
            {
                //label8.Text = "True";
                komut.Parameters.AddWithValue("@p6","True");
            }
            else
            {
                //label8.Text = "False";
                komut.Parameters.AddWithValue("@p6", "False");
            }
            komut.ExecuteNonQuery();// sorguyu çalıştırır.
            baglanti.Close();
            MessageBox.Show("Personel Eklendi");
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand guncelle = new SqlCommand("Update tbl_Personel_list set PerAd = @perad , PerSoyad = @persoyad , PerSehir = @persehir ,PerMaas = @permaas Where PerId = @perıd",baglanti);
            guncelle.Parameters.AddWithValue("@perad", txtboxad.Text);
            guncelle.Parameters.AddWithValue("@persoyad", txtboxsoyad.Text);
            guncelle.Parameters.AddWithValue("@persehir", txtboxsehir.Text);
            guncelle.Parameters.AddWithValue("@permaas", txtboxmaas.Text);
            guncelle.Parameters.AddWithValue("@perıd", txtboxid.Text);
            guncelle.ExecuteNonQuery();
            baglanti.Close();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            // secilen değişkenine datagridviewden secilen değerin indeksini aktardık.
            // cells sütün değerleri 
            // cells[0] ==> id 
            // cells[1] ==> adı 
            // cells[2] ==> soyadı vb.

            txtboxid.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            txtboxad.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            txtboxsoyad.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            txtboxsehir.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            txtboxmaas.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            label8.Text = dataGridView1.Rows[secilen].Cells[5].Value.ToString();
            txtboxmeslek.Text = dataGridView1.Rows[secilen].Cells[6].Value.ToString();
        }

        private void label8_TextChanged(object sender, EventArgs e)
        {
            if (label8.Text == "True")
            {
                radioButton1.Checked = true;
            }
            else if(label8.Text == "False")
            {
                radioButton2.Checked = true;
            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand kayitsil = new SqlCommand("Delete From tbl_Personel_list Where PerId = @k1",baglanti);
            kayitsil.Parameters.AddWithValue("@k1", txtboxid.Text);
            kayitsil.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("KAYIT SİLİNDİ");
        }
    }
}
