using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;

namespace AccoOnMuhasebe
{
    public partial class frmAyarlar : DevExpress.XtraEditors.XtraForm
    {
        public frmAyarlar()
        {
            InitializeComponent();
        }
        public string gelen_id, tur;
        sqlbaglantisi bgl = new sqlbaglantisi();
        string b_email,b_kullanici;
        void bilgiler_cek()
        {
            SqlCommand cmd_cek = new SqlCommand("Select * from TBL_UYELER where ID="+gelen_id+"",bgl.baglanti());
            SqlDataReader dr = cmd_cek.ExecuteReader();
            if (dr.Read())
            {
                txtAd.Text = dr["AD"].ToString();
                txtSoyad.Text = dr["SOYAD"].ToString();
                txtMail.Text = dr["EMAIL"].ToString();
                b_email = dr["EMAIL"].ToString();
                b_kullanici = dr["KULLANICIADI"].ToString();
                txtKullaniciAd.Text = dr["KULLANICIADI"].ToString();
                txtSifre.Text = dr["SIFRE"].ToString();
            }
            else
            {
                XtraMessageBox.Show("Okuma işlemi sırasında hata meydana geldi.");
            }
        }

        string durum_k="0", durum_e="0";
        void kullaniciadi_kontrol()
        {
            SqlCommand cmd = new SqlCommand("Select * from TBL_UYELER where KULLANICIADI='" + txtKullaniciAd.Text.Trim().ToString() + "'", bgl.baglanti());
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                durum_k = "1";
            }
            else
            {
                durum_k = "0";
            }
            bgl.baglanti().Close();
        }

        void email_kontrol()
        {
            SqlCommand cmd1 = new SqlCommand("Select * from TBL_UYELER where EMAIL='" + txtMail.Text.Trim().ToString() + "'", bgl.baglanti());
            SqlDataReader dr1 = cmd1.ExecuteReader();
            if (dr1.Read())
            {
                durum_e = "1";
            }
            else
            {
                durum_e = "0";
            }
            bgl.baglanti().Close();
        }

        private void frmAyarlar_Load(object sender, EventArgs e)
        {
            bilgiler_cek();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            DialogResult dg = XtraMessageBox.Show("İptal etmek istediğinizden emin misiniz?", "İptal", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
            if (dg == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            if (txtAd.Text.Trim().ToString() != "" && txtSoyad.Text.Trim().ToString() != "" && txtSifre.Text.Trim().ToString() != "" && txtMail.Text.Trim().ToString() != "" && txtKullaniciAd.Text.Trim().ToString() != "")
            {
                try
                {
                    if (txtKullaniciAd.Text.Trim().ToString()!=b_kullanici.ToString())
                    {
                        kullaniciadi_kontrol();
                    }
                    if(txtMail.Text.Trim().ToString()!=b_email.ToString())
                    {
                        email_kontrol();
                    }
                    if (durum_e == "0" && durum_k == "0")
                    {
                        SqlCommand komut = new SqlCommand("update TBL_UYELER set AD=@p1,SOYAD=@p2,EMAIL=@p3,KULLANICIADI=@p4,SIFRE=@p5,TUR=@p6 where ID=@p7", bgl.baglanti());
                        komut.Parameters.Clear();
                        komut.Parameters.AddWithValue("@p7", gelen_id);
                        komut.Parameters.AddWithValue("@p1", txtAd.Text.Trim().ToString());
                        komut.Parameters.AddWithValue("@p2", txtSoyad.Text.Trim().ToString());
                        komut.Parameters.AddWithValue("@p3", txtMail.Text.Trim().ToString());
                        komut.Parameters.AddWithValue("@p4", txtKullaniciAd.Text.Trim().ToString());
                        komut.Parameters.AddWithValue("@p5", txtSifre.Text.Trim().ToString());
                        komut.Parameters.AddWithValue("@p6", tur.ToString());
                        komut.ExecuteNonQuery();
                        bgl.baglanti().Close();
                        XtraMessageBox.Show("Kayıt güncelleme işlemi başarılı oldu.", "Kayıt olundu", MessageBoxButtons.OK);
                        bilgiler_cek();
                    }
                    else
                    {
                        XtraMessageBox.Show("Aynı email veya kullanıcı adında bir kayıt daha var.", "Çakışma", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                catch
                {
                    XtraMessageBox.Show("Bir hata meydana geldi lütfen tekrar deneyiniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                XtraMessageBox.Show("Boş alan bırakılamaz.", "Boş alan", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        int sayac = 0;
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (sayac % 2 == 0)
            {
                simpleButton2.Image = Properties.Resources.unsee;
                txtSifre.Properties.PasswordChar = '\0';

            }
            else
            {
                simpleButton2.Image = Properties.Resources.see;
                txtSifre.Properties.PasswordChar = '•';
            }
            sayac++;
        }
    }
}
