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
using DevExpress.XtraEditors;

namespace AccoOnMuhasebe
{
    public partial class frmKayitOl : DevExpress.XtraEditors.XtraForm
    {
        public frmKayitOl()
        {
            InitializeComponent();
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
        sqlbaglantisi bgl = new sqlbaglantisi();
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            DialogResult dg = XtraMessageBox.Show("İptal etmek istediğinizden emin misiniz?","İptal",MessageBoxButtons.YesNoCancel,MessageBoxIcon.Warning);
            if (dg==DialogResult.Yes)
            {
                this.Close();
            }
        }
        string durum_k, durum_e;
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
        string turu = "2";
        private void btnKaydet_Click(object sender, EventArgs e)
        {
            if (txtKod.Text.Trim().ToString()=="123456")
            {
                turu = "1";
            }
            else
            {
                turu = "2";
            }
            if (txtAd.Text.Trim().ToString()!=""&&txtSoyad.Text.Trim().ToString()!=""&&txtSifre.Text.Trim().ToString()!=""&&txtMail.Text.Trim().ToString()!=""&&txtKullaniciAd.Text.Trim().ToString()!="")
            {
                try
                {
                    kullaniciadi_kontrol();
                    email_kontrol();
                    if (durum_e=="0"&&durum_k=="0")
                    {
                        SqlCommand komut = new SqlCommand("insert into TBL_UYELER (AD,SOYAD,EMAIL,KULLANICIADI,SIFRE,TUR) values (@p1,@p2,@p3,@p4,@p5,@p6)", bgl.baglanti());
                        komut.Parameters.Clear();
                        komut.Parameters.AddWithValue("@p1", txtAd.Text.Trim().ToString());
                        komut.Parameters.AddWithValue("@p2", txtSoyad.Text.Trim().ToString());
                        komut.Parameters.AddWithValue("@p3", txtMail.Text.Trim().ToString());
                        komut.Parameters.AddWithValue("@p4", txtKullaniciAd.Text.Trim().ToString());
                        komut.Parameters.AddWithValue("@p5", txtSifre.Text.Trim().ToString());
                        komut.Parameters.AddWithValue("@p6", turu.ToString());
                        komut.ExecuteNonQuery();
                        bgl.baglanti().Close();
                        XtraMessageBox.Show("Yeni kayıt işlemi başarılı oldu.", "Kayıt olundu", MessageBoxButtons.OK);
                        this.Close();
                    }
                    else
                    {
                        XtraMessageBox.Show("Aynı email veya kullanıcı adında bir kayıt daha var.","Çakışma",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    }
                    
                }
                catch
                {
                    XtraMessageBox.Show("Bir hata meydana geldi lütfen tekrar deneyiniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                XtraMessageBox.Show("Boş alan bırakılamaz.","Boş alan",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
    }
}
