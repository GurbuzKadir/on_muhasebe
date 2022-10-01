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
    public partial class frmPersonel : Form
    {
        public frmPersonel()
        {
            InitializeComponent();
        }
        //
        sqlbaglantisi bgl = new sqlbaglantisi();
        string ID = "-1";
        //
         void listele_personeller()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * from TBL_PERSONELLER ", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
            this.gridView1.Columns[0].Width = 25;
            this.gridView1.Columns[0].Visible = false;
        }
        //
         void sehirlistesi()
         {
             SqlCommand cmd = new SqlCommand("Select SEHIR from TBL_ILLER", bgl.baglanti());
             SqlDataReader dr = cmd.ExecuteReader();
             while (dr.Read())
             {
                 cmbIl.Properties.Items.Add(dr[0]);
             }
             bgl.baglanti().Close();
         }
        //
        void txtTemizle()
        {
            txtAd.Text = "";
            txtAdres.Text = "";
            txtGorev.Text = "";
            txtMail.Text = "";
            txtSoyad.Text = "";
            txtTcKimlik.Text = "";
            txtTelefon1.Text = "";
            cmbIl.Text = "";
            cmbIlce.Text = "";
            cmbIlce.Properties.Items.Clear();
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
            if (txtAd.Text.Trim().ToString() != "" && txtAdres.Text.Trim().ToString() != "" && txtMail.Text.Trim().ToString() != "" && txtSoyad.Text.Trim().ToString() != "" && txtTcKimlik.Text.Trim().ToString() != "" && txtTelefon1.Text.Trim().ToString() != "" && txtGorev.Text.Trim().ToString() != "" && cmbIl.Text.Trim().ToString() != "" && cmbIlce.Text.Trim().ToString() != "")
            {
                DialogResult dg_guncelle;
                dg_guncelle = XtraMessageBox.Show("<color=orange><b>Emin misiniz?</b></color>\nSeçili kaydı güncellemek istediğinizden emin misiniz?", "Kayıt güncelleme", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                if (dg_guncelle == DialogResult.Yes)
                {
                    try
                    {
                        SqlCommand komut_guncelle = new SqlCommand("update TBL_PERSONELLER set AD=@pr1,SOYAD=@pr2,TELEFON=@pr3,TC=@pr4,MAIL=@pr5,IL=@pr6,ILCE=@pr7,ADRES=@pr8,GOREV=@pr9 where ID=@pr10", bgl.baglanti());
                        komut_guncelle.Parameters.Clear();
                        komut_guncelle.Parameters.AddWithValue("@pr10", ID);
                        komut_guncelle.Parameters.AddWithValue("@pr1", txtAd.Text.Trim().ToString());
                        komut_guncelle.Parameters.AddWithValue("@pr2", txtSoyad.Text.Trim().ToString());
                        komut_guncelle.Parameters.AddWithValue("@pr3", txtTelefon1.Text.Trim().ToString());
                        komut_guncelle.Parameters.AddWithValue("@pr4", txtTcKimlik.Text.Trim().ToString());
                        komut_guncelle.Parameters.AddWithValue("@pr5", txtMail.Text.Trim().ToString());
                        komut_guncelle.Parameters.AddWithValue("@pr6", cmbIl.Text.Trim().ToString());
                        komut_guncelle.Parameters.AddWithValue("@pr7", cmbIlce.Text.Trim().ToString());
                        komut_guncelle.Parameters.AddWithValue("@pr8", txtAdres.Text.Trim().ToString());
                        komut_guncelle.Parameters.AddWithValue("@pr9", txtGorev.Text.Trim().ToString());
                        komut_guncelle.ExecuteNonQuery();
                        bgl.baglanti().Close();
                        listele_personeller();
                        alertControl1.Show(this, "Personel güncellendi.", "Seçtiğiniz personel başarıyla güncellendi.");

                        //XtraMessageBox.Show("<color=green><b>Başarılı</b></color>\nSeçtiğiniz personel başarıyla güncellendi.", "Güncelleme başarılı", MessageBoxButtons.OK);
                        
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
        private void btnSil_Click(object sender, EventArgs e)
        {
            XtraMessageBox.AllowHtmlText = true;
            DialogResult dg_sil;
            dg_sil = XtraMessageBox.Show("<color=orange><b>Emin misiniz?</b></color>\nSeçili kaydı silmek istediğinizden emin misiniz?", "Kayıt silme", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
            if (dg_sil == DialogResult.Yes)
            {
                try
                {
                    SqlCommand komut_sil = new SqlCommand("Delete from TBL_PERSONELLER where ID=@pr1", bgl.baglanti());
                    komut_sil.Parameters.Clear();
                    komut_sil.Parameters.AddWithValue("@pr1", ID);
                    komut_sil.ExecuteNonQuery();
                    bgl.baglanti().Close();
                    listele_personeller();
                    alertControl1.Show(this, "Personel silindi.", "Seçtiğiniz personel başarıyla silindi.");

                    //XtraMessageBox.Show("<color=green><b>Başarılı</b></color>\nSeçtiğiniz personel başarıyla silindi.", "Silme başarılı", MessageBoxButtons.OK);
                    txtTemizle();

                }
                catch
                {
                    XtraMessageBox.Show("<color=red><b>Hata</b></color>\nBir hata meydana geldi. Tekrar deneyiniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        //
        private void btnKaydet_Click(object sender, EventArgs e)
        {
            XtraMessageBox.AllowHtmlText = true;
            if (txtAd.Text.Trim().ToString() != "" && txtAdres.Text.Trim().ToString() != "" && txtMail.Text.Trim().ToString() != "" && txtSoyad.Text.Trim().ToString() != "" && txtTcKimlik.Text.Trim().ToString() != "" && txtTelefon1.Text.Trim().ToString() != "" && txtGorev.Text.Trim().ToString() != "" && cmbIl.Text.Trim().ToString() != "" && cmbIlce.Text.Trim().ToString() != "")
            {
                try
                {
                    SqlCommand komut = new SqlCommand("insert into TBL_PERSONELLER (AD,SOYAD,TELEFON,TC,MAIL,IL,ILCE,ADRES,GOREV) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9)", bgl.baglanti());
                    komut.Parameters.Clear();
                    komut.Parameters.AddWithValue("@p1", txtAd.Text.Trim().ToString());
                    komut.Parameters.AddWithValue("@p2", txtSoyad.Text.Trim().ToString());
                    komut.Parameters.AddWithValue("@p3", txtTelefon1.Text.Trim().ToString());
                    komut.Parameters.AddWithValue("@p4", txtTcKimlik.Text.Trim().ToString());
                    komut.Parameters.AddWithValue("@p5", txtMail.Text.Trim().ToString());
                    komut.Parameters.AddWithValue("@p6", cmbIl.Text.Trim().ToString());
                    komut.Parameters.AddWithValue("@p7", cmbIlce.Text.Trim().ToString());
                    komut.Parameters.AddWithValue("@p8", txtAdres.Text.Trim().ToString());
                    komut.Parameters.AddWithValue("@p9", txtGorev.Text.Trim().ToString());
                    komut.ExecuteNonQuery();
                    listele_personeller();
                    bgl.baglanti().Close();
                    alertControl1.Show(this, "Personel eklendi.", "Bilgilerini girdiğiniz personel başarıyla eklenmiştir.");

                    //XtraMessageBox.Show("<color=green><b>Personel eklendi.</b></color>\nBilgilerini girdiğiniz personel başarıyla eklenmiştir.", "Personel ekleme", MessageBoxButtons.OK);
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
        private void btnYeni_Click(object sender, EventArgs e)
        {
            ID = "-1";
            txtTemizle();
        }
        //
        private void frmPersonel_Load(object sender, EventArgs e)
        {
            try
            {
                listele_personeller();
                sehirlistesi();
            }
            catch
            {
                XtraMessageBox.Show("Bağlantı problemlerinden dolayı giriş yapılamıyor.", "Bağlantı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }
        //
        private void cmbIl_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbIlce.Properties.Items.Clear();
            cmbIlce.Text = "";
            SqlCommand komut1 = new SqlCommand("Select ILCE from TBL_ILCELER where SEHIR=@pr1", bgl.baglanti());
            komut1.Parameters.Clear();
            komut1.Parameters.AddWithValue("@pr1", (cmbIl.SelectedIndex) + 1);
            SqlDataReader dr2 = komut1.ExecuteReader();
            while (dr2.Read())
            {
                cmbIlce.Properties.Items.Add(dr2[0]);
            }
            bgl.baglanti().Close();
        }
        //
        //
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            listele_personeller();
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
                txtTcKimlik.Text = dr["TC"].ToString();
                txtMail.Text = dr["MAIL"].ToString();
                cmbIl.Text = dr["IL"].ToString();
                cmbIlce.Text = dr["ILCE"].ToString();
                txtAdres.Text = dr["ADRES"].ToString();
                txtGorev.Text = dr["GOREV"].ToString();
            }
        }
    }
}
