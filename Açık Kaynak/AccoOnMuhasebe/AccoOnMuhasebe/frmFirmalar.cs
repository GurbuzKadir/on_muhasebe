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
    public partial class frmFirmalar : DevExpress.XtraEditors.XtraForm
    {
        public frmFirmalar()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl3 = new sqlbaglantisi();
        string ID = "-1";
        string firma_Banka_Durum;
        void listele_firmalar()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * from TBL_FIRMALAR ", bgl3.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
            this.gridView1.Columns[0].Width = 25;
            this.gridView1.Columns[0].Visible = false;
        }

        void firma_Banka_Kontrol()
        {
            SqlCommand cmd = new SqlCommand("Select * from TBL_BANKALAR where FIRMAID='" + ID.ToString() + "'", bgl3.baglanti());
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                firma_Banka_Durum = "var";
            }
            else
            {
                firma_Banka_Durum = "yok";
            }
            bgl3.baglanti().Close();
        }

        void banka_sil()
        {
            SqlCommand komut_sil = new SqlCommand("Delete from TBL_BANKALAR where FIRMAID=@pr1", bgl3.baglanti());
            komut_sil.Parameters.Clear();
            komut_sil.Parameters.AddWithValue("@pr1", ID);
            komut_sil.ExecuteNonQuery();
            bgl3.baglanti().Close();
        }

        void txtTemizle()
        {
            txtAd.Text = "";
            txtYetkiliGorev.Text = "";
            txtYetkili.Text = "";
            txtTcKimlik.Text = "";
            txtSektor.Text = "";
            txtTelefon1.Text = "";
            txtTelefon2.Text = "";
            txtTelefon3.Text = "";
            txtMail.Text = "";
            txtFax.Text = "";
            cmbIl.Text = "";
            cmbIlce.Text = "";
            txtVergi.Text = "";
            txtAdres.Text = "";
            txtKod1.Text = "";
            txtKod2.Text = "";
            txtKod3.Text = "";
            txtKod1.Text = "";
            txtKod2.Text = "";
            txtKod3.Text = "";
        }

        void sehirlistesi()
        {
            SqlCommand cmd = new SqlCommand("Select SEHIR from TBL_ILLER", bgl3.baglanti());
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cmbIl.Properties.Items.Add(dr[0]);
            }
            bgl3.baglanti().Close();
        }

        void cariKodAciklama()
        {
            SqlCommand komut = new SqlCommand("Select FIRMAKOD1 from TBL_KODLAR",bgl3.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                txtKodAnlam1.Text = dr[0].ToString();
            }
            bgl3.baglanti().Close();
        }

        private void frmFirmalar_Load(object sender, EventArgs e)
        {
            listele_firmalar();
            sehirlistesi();
            cariKodAciklama();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            listele_firmalar();
        }

        private void btnYeni_Click(object sender, EventArgs e)
        {
            ID = "-1";
            txtTemizle();
        }
        /**/
        private void btnKaydet_Click(object sender, EventArgs e)
        {
            XtraMessageBox.AllowHtmlText = true;
            if (txtAd.Text.Trim().ToString() != "" && txtSektor.Text.Trim().ToString() != "" && txtYetkili.Text.Trim().ToString() != "" && txtYetkiliGorev.Text.Trim().ToString() != "" && txtTcKimlik.Text.Trim().ToString() != "" && txtTelefon1.Text.Trim().ToString() != "" && txtMail.Text.Trim().ToString() != "" && cmbIl.Text.Trim().ToString() != "" && cmbIlce.Text.Trim().ToString() != "" && txtVergi.Text.Trim().ToString() != "" && txtAdres.Text.Trim().ToString() != "")
            {
                try
                {
                    SqlCommand komut = new SqlCommand("insert into TBL_FIRMALAR (AD,YETKILISTATU,YETKILIADSOYAD,YETKILITC,SEKTOR,TELEFON1,TELEFON2,TELEFON3,MAIL,FAX,IL,ILCE,VERGIDAIRE,ADRES,OZELKOD1,OZELKOD2,OZELKOD3) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12,@p13,@p14,@p15,@p16,@p17)", bgl3.baglanti());
                    komut.Parameters.Clear();
                    komut.Parameters.AddWithValue("@p1", txtAd.Text.Trim().ToString());
                    komut.Parameters.AddWithValue("@p2", txtYetkiliGorev.Text.Trim().ToString());
                    komut.Parameters.AddWithValue("@p3", txtYetkili.Text.Trim().ToString());
                    komut.Parameters.AddWithValue("@p4", txtTcKimlik.Text.Trim().ToString());
                    komut.Parameters.AddWithValue("@p5", txtSektor.Text.Trim().ToString());
                    komut.Parameters.AddWithValue("@p6", txtTelefon1.Text.Trim().ToString());
                    komut.Parameters.AddWithValue("@p7", txtTelefon2.Text.Trim().ToString());
                    komut.Parameters.AddWithValue("@p8", txtTelefon3.Text.Trim().ToString());
                    komut.Parameters.AddWithValue("@p9", txtMail.Text.Trim().ToString());
                    komut.Parameters.AddWithValue("@p10", txtFax.Text.Trim().ToString());
                    komut.Parameters.AddWithValue("@p11", cmbIl.Text.Trim().ToString());
                    komut.Parameters.AddWithValue("@p12", cmbIlce.Text.Trim().ToString());
                    komut.Parameters.AddWithValue("@p13", txtVergi.Text.Trim().ToString());
                    komut.Parameters.AddWithValue("@p14", txtAdres.Text.Trim().ToString());
                    komut.Parameters.AddWithValue("@p15", txtKod1.Text.Trim().ToString());
                    komut.Parameters.AddWithValue("@p16", txtKod2.Text.Trim().ToString());
                    komut.Parameters.AddWithValue("@p17", txtKod3.Text.Trim().ToString());
                    komut.ExecuteNonQuery();
                    listele_firmalar();
                    bgl3.baglanti().Close();
                    alertControl1.Show(this, "Firma eklendi.", "Bilgilerini girdiğiniz firma başarıyla eklenmiştir.");
                    //XtraMessageBox.Show("<color=green><b>Firma eklendi.</b></color>\nBilgilerini girdiğiniz firma başarıyla eklenmiştir.", "Firma ekleme", MessageBoxButtons.OK);
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
        private void cmbIl_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbIlce.Properties.Items.Clear();
            cmbIlce.Text = "";
            SqlCommand komut1 = new SqlCommand("Select ILCE from TBL_ILCELER where SEHIR=@pr1", bgl3.baglanti());
            komut1.Parameters.Clear();
            komut1.Parameters.AddWithValue("@pr1", (cmbIl.SelectedIndex) + 1);
            SqlDataReader dr2 = komut1.ExecuteReader();
            while (dr2.Read())
            {
                cmbIlce.Properties.Items.Add(dr2[0]);
            }
            bgl3.baglanti().Close();
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

        private void btnTamamla_Click(object sender, EventArgs e)
        {
            //Güncelleme işlemi burada yapılacak
            XtraMessageBox.AllowHtmlText = true;
            if (txtAd.Text.Trim().ToString() != "" && txtSektor.Text.Trim().ToString() != "" && txtYetkili.Text.Trim().ToString() != "" && txtYetkiliGorev.Text.Trim().ToString() != "" && txtTcKimlik.Text.Trim().ToString() != "" && txtTelefon1.Text.Trim().ToString() != "" && txtMail.Text.Trim().ToString() != "" && cmbIl.Text.Trim().ToString() != "" && cmbIlce.Text.Trim().ToString() != "" && txtVergi.Text.Trim().ToString() != "" && txtAdres.Text.Trim().ToString() != "")
            {
                DialogResult dg_guncelle;
                dg_guncelle = XtraMessageBox.Show("<color=orange><b>Emin misiniz?</b></color>\nSeçili kaydı güncellemek istediğinizden emin misiniz?", "Kayıt güncelleme", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                if (dg_guncelle == DialogResult.Yes)
                {
                    try
                    {
                        SqlCommand komut_guncelle = new SqlCommand("update TBL_FIRMALAR set AD=@pr1,YETKILISTATU=@pr2,YETKILIADSOYAD=@pr3,YETKILITC=@pr4,SEKTOR=@pr5,TELEFON1=@pr6,TELEFON2=@pr7,TELEFON3=@pr8,MAIL=@pr9,FAX=@pr10,IL=@pr11,ILCE=@pr12,VERGIDAIRE=@pr13,ADRES=@pr14,OZELKOD1=@pr15,OZELKOD2=@pr16,OZELKOD3=@pr17 where ID=@pr18", bgl3.baglanti());
                        komut_guncelle.Parameters.Clear();
//AD,YETKILISTATU,YETKILIADSOYAD,YETKILITC,SEKTOR,TELEFON1,TELEFON2,TELEFON3,MAIL,FAX,IL,ILCE,VERGIDAIRE,ADRES,OZELKOD1,OZELKOD2,OZELKOD3
                        komut_guncelle.Parameters.AddWithValue("@pr18", ID);
                        komut_guncelle.Parameters.AddWithValue("@pr1", txtAd.Text.Trim().ToString());
                        komut_guncelle.Parameters.AddWithValue("@pr2", txtYetkiliGorev.Text.Trim().ToString());
                        komut_guncelle.Parameters.AddWithValue("@pr3", txtYetkili.Text.Trim().ToString());
                        komut_guncelle.Parameters.AddWithValue("@pr4", txtTcKimlik.Text.Trim().ToString());
                        komut_guncelle.Parameters.AddWithValue("@pr5", txtSektor.Text.Trim().ToString());
                        komut_guncelle.Parameters.AddWithValue("@pr6", txtTelefon1.Text.Trim().ToString());
                        komut_guncelle.Parameters.AddWithValue("@pr7", txtTelefon2.Text.Trim().ToString());
                        komut_guncelle.Parameters.AddWithValue("@pr8", txtTelefon3.Text.Trim().ToString());
                        komut_guncelle.Parameters.AddWithValue("@pr9", txtMail.Text.Trim().ToString());
                        komut_guncelle.Parameters.AddWithValue("@pr10", txtFax.Text.Trim().ToString());
                        komut_guncelle.Parameters.AddWithValue("@pr11", cmbIl.Text.Trim().ToString());
                        komut_guncelle.Parameters.AddWithValue("@pr12", cmbIlce.Text.Trim().ToString());
                        komut_guncelle.Parameters.AddWithValue("@pr13", txtVergi.Text.Trim().ToString());
                        komut_guncelle.Parameters.AddWithValue("@pr14", txtAdres.Text.Trim().ToString());
                        komut_guncelle.Parameters.AddWithValue("@pr15", txtKod1.Text.Trim().ToString());
                        komut_guncelle.Parameters.AddWithValue("@pr16", txtKod2.Text.Trim().ToString());
                        komut_guncelle.Parameters.AddWithValue("@pr17", txtKod3.Text.Trim().ToString());
                        komut_guncelle.ExecuteNonQuery();
                        bgl3.baglanti().Close();
                        listele_firmalar();
                        alertControl1.Show(this, "Firma güncellendi.", "Seçtiğiniz firma başarıyla güncellendi.");

                        //XtraMessageBox.Show("<color=green><b>Başarılı</b></color>\nSeçtiğiniz firma başarıyla güncellendi.", "Güncelleme başarılı", MessageBoxButtons.OK);
                        
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

        private void btnSil_Click(object sender, EventArgs e)
        {
            XtraMessageBox.AllowHtmlText = true;
            DialogResult dg_sil;
            dg_sil = XtraMessageBox.Show("<color=orange><b>Emin misiniz?</b></color>\nSeçili kaydı silmek istediğinizden emin misiniz?", "Kayıt silme", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
            if (dg_sil == DialogResult.Yes)
            {
                try
                {
                    firma_Banka_Kontrol();
                    if (firma_Banka_Durum == "yok")
                    {
                        SqlCommand komut_sil = new SqlCommand("Delete from TBL_FIRMALAR where ID=@pr1", bgl3.baglanti());
                        komut_sil.Parameters.Clear();
                        komut_sil.Parameters.AddWithValue("@pr1", ID);
                        komut_sil.ExecuteNonQuery();
                        bgl3.baglanti().Close();
                        listele_firmalar();
                        alertControl1.Show(this, "Firma silindi.", "Seçtiğiniz firma başarıyla silindi.");

                        //XtraMessageBox.Show("<color=green><b>Başarılı</b></color>\nSeçtiğiniz firma başarıyla silindi.", "Silme başarılı", MessageBoxButtons.OK);
                        txtTemizle();
                    }
                    else
                    {
                        DialogResult dg_kayitlar;
                        dg_kayitlar = XtraMessageBox.Show("<color=orange><b>Emin misiniz?</b></color>\nSeçili kayda ait banka bilgileri bulunmaktadır. Yinede silmek istediğinizden emin misiniz?", "Kayıt mevcut", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                        if (dg_kayitlar == DialogResult.Yes)
                        {
                            banka_sil();
                            SqlCommand komut_sil = new SqlCommand("Delete from TBL_FIRMALAR where ID=@pr1", bgl3.baglanti());
                            komut_sil.Parameters.Clear();
                            komut_sil.Parameters.AddWithValue("@pr1", ID);
                            komut_sil.ExecuteNonQuery();
                            bgl3.baglanti().Close();
                            listele_firmalar();
                            alertControl1.Show(this, "Firma silindi.", "Seçtiğiniz firma başarıyla silindi.");

                            //XtraMessageBox.Show("<color=green><b>Başarılı</b></color>\nSeçtiğiniz firma başarıyla silindi.", "Silme başarılı", MessageBoxButtons.OK);
                            txtTemizle();
                        }
                    }
                }
                catch
                {
                    XtraMessageBox.Show("<color=red><b>Hata</b></color>\nBir hata meydana geldi. Tekrar deneyiniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void gridView1_Click(object sender, EventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                ID = dr["ID"].ToString();
                txtAd.Text = dr["AD"].ToString();
                txtYetkiliGorev.Text = dr["YETKILISTATU"].ToString();
                txtYetkili.Text = dr["YETKILIADSOYAD"].ToString();
                txtTcKimlik.Text = dr["YETKILITC"].ToString();
                txtSektor.Text = dr["SEKTOR"].ToString();
                txtTelefon1.Text = dr["TELEFON1"].ToString();
                txtTelefon2.Text = dr["TELEFON2"].ToString();
                txtTelefon3.Text = dr["TELEFON3"].ToString();
                txtMail.Text = dr["MAIL"].ToString();
                txtFax.Text = dr["FAX"].ToString();
                cmbIl.Text = dr["IL"].ToString();
                cmbIlce.Text = dr["ILCE"].ToString();
                txtVergi.Text = dr["VERGIDAIRE"].ToString();
                txtAdres.Text = dr["ADRES"].ToString();
                txtKod1.Text = dr["OZELKOD1"].ToString();
                txtKod2.Text = dr["OZELKOD2"].ToString();
                txtKod3.Text = dr["OZELKOD3"].ToString();
            }
        }
    }
}
