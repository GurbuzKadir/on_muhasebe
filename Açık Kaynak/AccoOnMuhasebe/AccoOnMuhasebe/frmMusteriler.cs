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
    public partial class frmMusteriler : DevExpress.XtraEditors.XtraForm
    {
        public frmMusteriler()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl2 = new sqlbaglantisi();
        string ID = "-1";
        //
        void listele_musteriler()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * from TBL_MUSTERILER ", bgl2.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
            this.gridView1.Columns[0].Width = 25;
            this.gridView1.Columns[0].Visible = false;
        }
        //
        void sehirlistesi()
        {
            SqlCommand cmd = new SqlCommand("Select SEHIR from TBL_ILLER",bgl2.baglanti());
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cmbIl.Properties.Items.Add(dr[0]);
            }
            bgl2.baglanti().Close();
        }
        //
        void txtTemizle()
        {
            txtAd.Text = "";
            txtAdres.Text = "";
            txtMail.Text = "";
            txtSoyad.Text = "";
            txtTcKimlik.Text = "";
            txtTelefon1.Text = "";
            txtTelefon2.Text = "";
            txtVergi.Text = "";
            cmbIl.Text = "";
            cmbIlce.Text = "";
            cmbIlce.Properties.Items.Clear();
        }
        //
        private void frmMusteriler_Load(object sender, EventArgs e)
        {
            listele_musteriler();
            sehirlistesi();
        }
        //
        private void cmbIl_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbIlce.Properties.Items.Clear();
            cmbIlce.Text = "";
            SqlCommand komut1 = new SqlCommand("Select ILCE from TBL_ILCELER where SEHIR=@pr1",bgl2.baglanti());
            komut1.Parameters.Clear();
            komut1.Parameters.AddWithValue("@pr1",(cmbIl.SelectedIndex)+1);
            SqlDataReader dr2 = komut1.ExecuteReader();
            while (dr2.Read())
            {
                cmbIlce.Properties.Items.Add(dr2[0]);
            }
            bgl2.baglanti().Close();
        }
        //
        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            btnKaydet.Enabled = false;
            btnGuncelle.Enabled = false;
            btnSil.Enabled = false;
            btnYeni.Enabled = false;
            btnTamamla.Visible = true;
            btnIptal.Visible = true;
        }
        //
        private void btnIptal_Click(object sender, EventArgs e)
        {
            XtraMessageBox.AllowHtmlText = true;
            DialogResult dg;
            dg = XtraMessageBox.Show("<color=orange><b>Emin misiniz?</b></color>\nGüncelleme işlemini iptal etmek istediğinizden emin misiniz?", "Güncelle iptal", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
            if (dg == DialogResult.Yes)
            {
                btnKaydet.Enabled = true;
                btnGuncelle.Enabled = true;
                btnSil.Enabled = true;
                btnYeni.Enabled = true;
                btnTamamla.Visible = false;
                btnIptal.Visible = false;
                txtTemizle();
                ID = "-1";
            }
        }
        //
        private void btnTamamla_Click(object sender, EventArgs e)
        {
            //Güncelleme işlemi burada yapılacak
            XtraMessageBox.AllowHtmlText = true;
            if (txtAd.Text.Trim().ToString() != "" && txtAdres.Text.Trim().ToString() != "" && txtMail.Text.Trim().ToString() != "" && txtSoyad.Text.Trim().ToString() != "" && txtTcKimlik.Text.Trim().ToString() != "" && txtTelefon1.Text.Trim().ToString() != "" && txtVergi.Text.Trim().ToString() != "" && cmbIl.Text.Trim().ToString() != "" && cmbIlce.Text.Trim().ToString() != "")
            {
                DialogResult dg_guncelle;
                dg_guncelle = XtraMessageBox.Show("<color=orange><b>Emin misiniz?</b></color>\nSeçili kaydı güncellemek istediğinizden emin misiniz?", "Kayıt güncelleme", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                if (dg_guncelle == DialogResult.Yes)
                {
                    try
                    {
                        SqlCommand komut_guncelle = new SqlCommand("update TBL_MUSTERILER set AD=@pr1,SOYAD=@pr2,TELEFON=@pr3,TELEFON2=@pr4,TC=@pr5,MAIL=@pr6,IL=@pr7,ILCE=@pr8,ADRES=@pr9,VERGIDAIRE=@pr10 where ID=@pr11", bgl2.baglanti());
                        komut_guncelle.Parameters.Clear();
                        komut_guncelle.Parameters.AddWithValue("@pr11", ID);
                        komut_guncelle.Parameters.AddWithValue("@pr1", txtAd.Text.Trim().ToString());
                        komut_guncelle.Parameters.AddWithValue("@pr2", txtSoyad.Text.Trim().ToString());
                        komut_guncelle.Parameters.AddWithValue("@pr3", txtTelefon1.Text.Trim().ToString());
                        komut_guncelle.Parameters.AddWithValue("@pr4", txtTelefon2.Text.Trim().ToString());
                        komut_guncelle.Parameters.AddWithValue("@pr5", txtTcKimlik.Text.Trim().ToString());
                        komut_guncelle.Parameters.AddWithValue("@pr6", txtMail.Text.Trim().ToString());
                        komut_guncelle.Parameters.AddWithValue("@pr7", cmbIl.Text.Trim().ToString());
                        komut_guncelle.Parameters.AddWithValue("@pr8", cmbIlce.Text.Trim().ToString());
                        komut_guncelle.Parameters.AddWithValue("@pr9", txtAdres.Text.Trim().ToString());
                        komut_guncelle.Parameters.AddWithValue("@pr10", txtVergi.Text.Trim().ToString());
                        komut_guncelle.ExecuteNonQuery();
                        bgl2.baglanti().Close();
                        listele_musteriler();
                        alertControl1.Show(this, "Müşteri güncellendi.", "Seçtiğiniz müşteri başarıyla güncellendi.");

                        //XtraMessageBox.Show("<color=green><b>Başarılı</b></color>\nSeçtiğiniz müşteri başarıyla güncellendi.", "Güncelleme başarılı", MessageBoxButtons.OK);
                        
                        //Enable = kaydet,güncelle,sil,yeni
                        //Visible_false=btnTamamla,btnIptal
                        //ID=-1
                        //textboxlar temizle
                        btnKaydet.Enabled = true;
                        btnSil.Enabled = true;
                        btnGuncelle.Enabled = true;
                        btnYeni.Enabled = true;
                        btnTamamla.Visible = false;
                        btnIptal.Visible = false;
                        ID = "-1";
                        txtTemizle();
                    }
                    catch
                    {
                        XtraMessageBox.Show("<color=red><b>Hata</b></color>\nBir hata meydana geldi. Tekrar deneyiniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                XtraMessageBox.Show("<color=red><b>Hata</b></color>\nHatalı alan bulunmaktadır.Lütfen kontrol edip tekrar deneyiniz.\nNot : Boş alan bırakılamaz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //
        private void btnKaydet_Click(object sender, EventArgs e)
        {
            XtraMessageBox.AllowHtmlText = true;
            if (txtAd.Text.Trim().ToString() != "" && txtAdres.Text.Trim().ToString() != "" && txtMail.Text.Trim().ToString() != "" && txtSoyad.Text.Trim().ToString() != "" && txtTcKimlik.Text.Trim().ToString() != "" && txtTelefon1.Text.Trim().ToString() != "" && txtVergi.Text.Trim().ToString() != "" && cmbIl.Text.Trim().ToString() != "" && cmbIlce.Text.Trim().ToString() != "")
            {
                try
                {
                    SqlCommand komut = new SqlCommand("insert into TBL_MUSTERILER (AD,SOYAD,TELEFON,TELEFON2,TC,MAIL,IL,ILCE,ADRES,VERGIDAIRE) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10)", bgl2.baglanti());
                    komut.Parameters.Clear();
                    komut.Parameters.AddWithValue("@p1", txtAd.Text.Trim().ToString());
                    komut.Parameters.AddWithValue("@p2", txtSoyad.Text.Trim().ToString());
                    komut.Parameters.AddWithValue("@p3", txtTelefon1.Text.Trim().ToString());
                    komut.Parameters.AddWithValue("@p4", txtTelefon2.Text.Trim().ToString());
                    komut.Parameters.AddWithValue("@p5", txtTcKimlik.Text.Trim().ToString());
                    komut.Parameters.AddWithValue("@p6", txtMail.Text.Trim().ToString());
                    komut.Parameters.AddWithValue("@p7", cmbIl.Text.Trim().ToString());
                    komut.Parameters.AddWithValue("@p8", cmbIlce.Text.Trim().ToString());
                    komut.Parameters.AddWithValue("@p9", txtAdres.Text.Trim().ToString());
                    komut.Parameters.AddWithValue("@p10", txtVergi.Text.Trim().ToString());
                    komut.ExecuteNonQuery();
                    listele_musteriler();
                    bgl2.baglanti().Close();
                    alertControl1.Show(this, "Müşteri eklendi.", "Bilgilerini girdiğiniz müşteri başarıyla eklenmiştir.");

                    //XtraMessageBox.Show("<color=green><b>Müşteri eklendi.</b></color>\nBilgilerini girdiğiniz müşteri başarıyla eklenmiştir.", "Müşteri ekleme", MessageBoxButtons.OK);
                    txtTemizle();
                }
                catch
                {
                    XtraMessageBox.Show("<color=red><b>Hata</b></color>\nBir hata meydana geldi lütfen tekrar deneyiniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                XtraMessageBox.Show("<color=red><b>Hata</b></color>\nHatalı alan bulunmaktadır.Lütfen kontrol edip tekrar deneyiniz.\nNot : Boş alan bırakılamaz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //
        //
        private void btnYeni_Click(object sender, EventArgs e)
        {
            ID = "-1";
            txtTemizle();
        }
        //
        private void btnSil_Click(object sender, EventArgs e)
        {
            XtraMessageBox.AllowHtmlText = true;
            DialogResult dg_sil;
            dg_sil = XtraMessageBox.Show("<color=orange><b>Emin misiniz?</b></color>\nSeçili kaydı silmek istediğinizden emin misiniz?", "Kayıt silme", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
            if (dg_sil == DialogResult.Yes)
            {
                try
                {
                    SqlCommand komut_sil = new SqlCommand("Delete from TBL_MUSTERILER where ID=@pr1", bgl2.baglanti());
                    komut_sil.Parameters.Clear();
                    komut_sil.Parameters.AddWithValue("@pr1", ID);
                    komut_sil.ExecuteNonQuery();
                    bgl2.baglanti().Close();
                    listele_musteriler();
                    alertControl1.Show(this, "Müşteri silindi.", "Seçtiğiniz müşteri başarıyla silindi.");

                    //XtraMessageBox.Show("<color=green><b>Başarılı</b></color>\nSeçtiğiniz müşteri başarıyla silindi.", "Silme başarılı", MessageBoxButtons.OK);
                    txtTemizle();

                }
                catch
                {
                    XtraMessageBox.Show("<color=red><b>Hata</b></color>\nBir hata meydana geldi. Tekrar deneyiniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        //
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            listele_musteriler();
        }

        private void gridView1_Click(object sender, EventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                ID = dr["ID"].ToString();
                txtAd.Text = dr["AD"].ToString();
                txtSoyad.Text = dr["SOYAD"].ToString();
                txtTelefon1.Text = dr["TELEFON"].ToString();
                txtTelefon2.Text = dr["TELEFON2"].ToString();
                txtTcKimlik.Text = dr["TC"].ToString();
                txtMail.Text = dr["MAIL"].ToString();
                cmbIl.Text = dr["IL"].ToString();
                cmbIlce.Text = dr["ILCE"].ToString();
                txtAdres.Text = dr["ADRES"].ToString();
                txtVergi.Text = dr["VERGIDAIRE"].ToString();
            }
        }
    }
}
