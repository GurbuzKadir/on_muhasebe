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
    public partial class frmBankalar : Form
    {
        public frmBankalar()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl3 = new sqlbaglantisi();
        string ID = "-1";

        void listele_bankalar()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Execute BankaBilgileri ", bgl3.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
            this.gridView1.Columns[0].Visible = false;
            this.gridView1.Columns[11].Caption = "FIRMAADI";
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

        void firmalistesi()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select ID,AD from TBL_FIRMALAR", bgl3.baglanti());
            da.Fill(dt);
            cmbFirma.Properties.ValueMember = "ID";
            cmbFirma.Properties.DisplayMember = "AD";
            //cmbFirma.Properties.Columns[0].Visible = false;
            cmbFirma.Properties.DataSource = dt;
            cmbFirma.Properties.PopulateColumns();
            cmbFirma.Properties.Columns["ID"].Visible = false;
            cmbFirma.Properties.Columns["AD"].Caption = "FIRMA ADI";
        }

        void txtTemizle()
        {
            txtBankaAd.Text = "";
            cmbFirma.Text = "";
            cmbFirma.EditValue = null;
            txtHesapNo.Text = "";
            txtHesapTuru.Text = "";
            txtIban.Text = "";
            txtSube.Text = "";
            txtTarih.Text = "";
            txtTelefon.Text = "";
            txtYetkili.Text = "";
            cmbIl.Text = "";
            cmbIlce.Text = "";
        }

        private void frmBankalar_Load(object sender, EventArgs e)
        {
            try
            {
                listele_bankalar();
                sehirlistesi();
                firmalistesi();
            }
            catch
            {
                XtraMessageBox.Show("Bağlantı problemlerinden dolayı işlem yapılamıyor.", "Bağlantı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void btnYeni_Click(object sender, EventArgs e)
        {
            ID = "-1";
            txtTemizle();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            /*
                *txtBankaAd.Text = "";
                *txtFirma.Text = "";
                *txtHesapNo.Text = "";
                *txtHesapTuru.Text = "";
                *txtIban.Text = "";
                *txtSube.Text = "";
                *txtTarih.Text = "";
                *txtTelefon.Text = "";
                *txtYetkili.Text = "";
                *cmbIl.Text = "";
                *cmbIlce.Text = "";
             */
            XtraMessageBox.AllowHtmlText = true;
            if (txtBankaAd.Text.Trim().ToString() != "" && cmbFirma.Text.Trim().ToString() != "" && txtYetkili.Text.Trim().ToString() != "" && txtHesapNo.Text.Trim().ToString() != "" && txtHesapTuru.Text.Trim().ToString() != "" && txtIban.Text.Trim().ToString() != "" && txtSube.Text.Trim().ToString() != "" && cmbIl.Text.Trim().ToString() != "" && cmbIlce.Text.Trim().ToString() != "" && txtTarih.Text.Trim().ToString() != "" && txtTelefon.Text.Trim().ToString() != "")
            {
                try
                {
                    SqlCommand komut = new SqlCommand("insert into TBL_BANKALAR (BANKAADI,IL,ILCE,SUBE,IBAN,HESAPNO,YETKILI,TELEFON,TARIH,HESAPTURU,FIRMAID) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11)", bgl3.baglanti());
                    komut.Parameters.Clear();
                    komut.Parameters.AddWithValue("@p1", txtBankaAd.Text.Trim().ToString());
                    komut.Parameters.AddWithValue("@p2", cmbIl.Text.Trim().ToString());
                    komut.Parameters.AddWithValue("@p3", cmbIlce.Text.Trim().ToString());
                    komut.Parameters.AddWithValue("@p4", txtSube.Text.Trim().ToString());
                    komut.Parameters.AddWithValue("@p5", txtIban.Text.Trim().ToString());
                    komut.Parameters.AddWithValue("@p6", txtHesapNo.Text.Trim().ToString());
                    komut.Parameters.AddWithValue("@p7", txtYetkili.Text.Trim().ToString());
                    komut.Parameters.AddWithValue("@p8", txtTelefon.Text.Trim().ToString());
                    komut.Parameters.AddWithValue("@p9", txtTarih.Text.Trim().ToString());
                    komut.Parameters.AddWithValue("@p10", txtHesapTuru.Text.Trim().ToString());
                    komut.Parameters.AddWithValue("@p11", cmbFirma.EditValue);//pin
                    komut.ExecuteNonQuery();
                    listele_bankalar();
                    bgl3.baglanti().Close();
                    alertControl1.Show(this, "Banka eklendi.", "Bilgilerini girdiğiniz banka başarıyla eklenmiştir.");
                    //XtraMessageBox.Show("<color=green><b>Banka eklendi.</b></color>\nBilgilerini girdiğiniz banka başarıyla eklenmiştir.", "Banka ekleme", MessageBoxButtons.OK);
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

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            btnKaydet.Enabled = false;
            btnGuncelle.Enabled = false;
            btnSil.Enabled = false;
            btnYeni.Enabled = false;
            btnTamamla.Visible = true;
            btnIptal.Visible = true;
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
                    SqlCommand komut_sil = new SqlCommand("Delete from TBL_BANKALAR where ID=@pr1", bgl3.baglanti());
                    komut_sil.Parameters.Clear();
                    komut_sil.Parameters.AddWithValue("@pr1", ID);
                    komut_sil.ExecuteNonQuery();
                    bgl3.baglanti().Close();
                    listele_bankalar();
                    alertControl1.Show(this, "Banka silindi.", "Seçtiğiniz banka başarıyla silindi.");
                    //XtraMessageBox.Show("<color=green><b>Başarılı</b></color>\nSeçtiğiniz banka başarıyla silindi.", "Silme başarılı", MessageBoxButtons.OK);
                    txtTemizle();
                }
                catch
                {
                    XtraMessageBox.Show("<color=red><b>Hata</b></color>\nBir hata meydana geldi. Tekrar deneyiniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
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
            //Güncelleme burada
            //BANKAADI,IL,ILCE,SUBE,IBAN,HESAPNO,YETKILI,TELEFON,TARIH,HESAPTURU,FIRMAID
            XtraMessageBox.AllowHtmlText = true;
            if (txtBankaAd.Text.Trim().ToString() != "" && cmbFirma.Text.Trim().ToString() != "" && txtYetkili.Text.Trim().ToString() != "" && txtHesapNo.Text.Trim().ToString() != "" && txtHesapTuru.Text.Trim().ToString() != "" && txtIban.Text.Trim().ToString() != "" && txtSube.Text.Trim().ToString() != "" && cmbIl.Text.Trim().ToString() != "" && cmbIlce.Text.Trim().ToString() != "" && txtTarih.Text.Trim().ToString() != "" && txtTelefon.Text.Trim().ToString() != "")
            {
                DialogResult dg_guncelle;
                dg_guncelle = XtraMessageBox.Show("<color=orange><b>Emin misiniz?</b></color>\nSeçili kaydı güncellemek istediğinizden emin misiniz?", "Kayıt güncelleme", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                if (dg_guncelle == DialogResult.Yes)
                {
                    try
                    {
                        SqlCommand komut_guncelle = new SqlCommand("update TBL_BANKALAR set BANKAADI=@pr1,IL=@pr2,ILCE=@pr3,SUBE=@pr4,IBAN=@pr5,HESAPNO=@pr6,YETKILI=@pr7,TELEFON=@pr8,TARIH=@pr9,HESAPTURU=@pr10,FIRMAID=@pr11 where ID=@pr12", bgl3.baglanti());
                        komut_guncelle.Parameters.Clear();
                        //AD,YETKILISTATU,YETKILIADSOYAD,YETKILITC,SEKTOR,TELEFON1,TELEFON2,TELEFON3,MAIL,FAX,IL,ILCE,VERGIDAIRE,ADRES,OZELKOD1,OZELKOD2,OZELKOD3
                        komut_guncelle.Parameters.AddWithValue("@pr12", ID);
                        komut_guncelle.Parameters.AddWithValue("@pr1", txtBankaAd.Text.Trim().ToString());
                        komut_guncelle.Parameters.AddWithValue("@pr2", cmbIl.Text.Trim().ToString());
                        komut_guncelle.Parameters.AddWithValue("@pr3", cmbIlce.Text.Trim().ToString());
                        komut_guncelle.Parameters.AddWithValue("@pr4", txtSube.Text.Trim().ToString());
                        komut_guncelle.Parameters.AddWithValue("@pr5", txtIban.Text.Trim().ToString());
                        komut_guncelle.Parameters.AddWithValue("@pr6", txtHesapNo.Text.Trim().ToString());
                        komut_guncelle.Parameters.AddWithValue("@pr7", txtYetkili.Text.Trim().ToString());
                        komut_guncelle.Parameters.AddWithValue("@pr8", txtTelefon.Text.Trim().ToString());
                        komut_guncelle.Parameters.AddWithValue("@pr9", txtTarih.Text.Trim().ToString());
                        komut_guncelle.Parameters.AddWithValue("@pr10", txtHesapTuru.Text.Trim().ToString());
                        komut_guncelle.Parameters.AddWithValue("@pr11", cmbFirma.EditValue);
                        komut_guncelle.ExecuteNonQuery();
                        bgl3.baglanti().Close();
                        listele_bankalar();
                        alertControl1.Show(this, "Banka güncellendi.", "Seçtiğiniz banka başarıyla güncellendi.");
                        //XtraMessageBox.Show("<color=green><b>Başarılı</b></color>\nSeçtiğiniz banka başarıyla güncellendi.", "Güncelleme başarılı", MessageBoxButtons.OK);
                        
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

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            listele_bankalar();
        }

        private void gridView1_Click(object sender, EventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                ID = dr["ID"].ToString();
                txtBankaAd.Text = dr["BANKAADI"].ToString();
                cmbIl.Text = dr["IL"].ToString();
                cmbIlce.Text = dr["ILCE"].ToString();
                txtSube.Text = dr["SUBE"].ToString();
                txtIban.Text = dr["IBAN"].ToString();
                txtHesapNo.Text = dr["HESAPNO"].ToString();
                txtYetkili.Text = dr["YETKILI"].ToString();
                txtTelefon.Text = dr["TELEFON"].ToString();
                txtTarih.Text = dr["TARIH"].ToString();
                txtHesapTuru.Text = dr["HESAPTURU"].ToString();
                cmbFirma.Text = dr[11].ToString();
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            txtTarih.Text = DateTime.Now.ToShortDateString().ToString();
        }
    }
}
