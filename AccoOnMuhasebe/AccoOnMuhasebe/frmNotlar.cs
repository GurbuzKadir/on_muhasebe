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
    public partial class frmNotlar : Form
    {
        public frmNotlar()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl2 = new sqlbaglantisi();
        string ID = "-1";
        void listele_notlar()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * from TBL_NOTLAR ", bgl2.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
            this.gridView1.Columns[0].Width = 25;
            this.gridView1.Columns[0].Visible = false;
        }

        void txtTemizle()
        {
            txtBaslik.Text = "";
            txtDetay.Text = "";
            txtHitap.Text = "";
            txtOlusturan.Text = "";
            txtSaat.Text = "";
            txtTarih.Text = "";
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            txtTarih.Text = DateTime.Now.ToShortDateString().ToString();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            txtSaat.Text = DateTime.Now.ToShortTimeString().ToString();
        }

        private void btnYeni_Click(object sender, EventArgs e)
        {
            ID = "-1";
            txtTemizle();
        }

        private void frmNotlar_Load(object sender, EventArgs e)
        {
            try
            {
                listele_notlar();
            }
            catch
            {
                XtraMessageBox.Show("Bağlantı problemlerinden dolayı giriş yapılamıyor.", "Bağlantı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            XtraMessageBox.AllowHtmlText = true;
            if (txtBaslik.Text.Trim().ToString()!=""&&txtDetay.Text.Trim().ToString()!=""&&txtHitap.Text.Trim().ToString()!=""&&txtOlusturan.Text.Trim().ToString()!=""&&txtSaat.Text.Trim().ToString()!=""&&txtTarih.Text.Trim().ToString()!="")
            {
                try
                {
                    SqlCommand komut = new SqlCommand("insert into TBL_NOTLAR (TARIH,SAAT,BASLIK,DETAY,OLUSTURAN,HITAP) values (@p1,@p2,@p3,@p4,@p5,@p6)", bgl2.baglanti());
                    komut.Parameters.Clear();
                    komut.Parameters.AddWithValue("@p1", txtTarih.Text.Trim().ToString());
                    komut.Parameters.AddWithValue("@p2", txtSaat.Text.Trim().ToString());
                    komut.Parameters.AddWithValue("@p3", txtBaslik.Text.Trim().ToString());
                    komut.Parameters.AddWithValue("@p4", txtDetay.Text.Trim().ToString());
                    komut.Parameters.AddWithValue("@p5", txtOlusturan.Text.Trim().ToString());
                    komut.Parameters.AddWithValue("@p6", txtHitap.Text.Trim().ToString());
                    komut.ExecuteNonQuery();
                    listele_notlar();
                    bgl2.baglanti().Close();
                    alertControl1.Show(this, "Not eklendi.", "Bilgilerini girdiğiniz not başarıyla eklenmiştir.");

                    //XtraMessageBox.Show("<color=green><b>Not eklendi.</b></color>\nBilgilerini girdiğiniz not başarıyla eklenmiştir.", "Not ekleme", MessageBoxButtons.OK);
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

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            listele_notlar();
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
                    SqlCommand komut_sil = new SqlCommand("Delete from TBL_NOTLAR where NOTID=@pr1", bgl2.baglanti());
                    komut_sil.Parameters.Clear();
                    komut_sil.Parameters.AddWithValue("@pr1", ID);
                    komut_sil.ExecuteNonQuery();
                    bgl2.baglanti().Close();
                    listele_notlar();
                    alertControl1.Show(this, "Not silindi.", "Seçtiğiniz not başarıyla silindi.");

                    //XtraMessageBox.Show("<color=green><b>Başarılı</b></color>\nSeçtiğiniz not başarıyla silindi.", "Silme başarılı", MessageBoxButtons.OK);
                    txtTemizle();

                }
                catch
                {
                    XtraMessageBox.Show("<color=red><b>Hata</b></color>\nBir hata meydana geldi. Tekrar deneyiniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnTamamla_Click(object sender, EventArgs e)
        {
            XtraMessageBox.AllowHtmlText = true;
            if (txtBaslik.Text.Trim().ToString() != "" && txtDetay.Text.Trim().ToString() != "" && txtHitap.Text.Trim().ToString() != "" && txtOlusturan.Text.Trim().ToString() != "" && txtSaat.Text.Trim().ToString() != "" && txtTarih.Text.Trim().ToString() != "")
            {
                DialogResult dg_guncelle;
                dg_guncelle = XtraMessageBox.Show("<color=orange><b>Emin misiniz?</b></color>\nSeçili kaydı güncellemek istediğinizden emin misiniz?", "Kayıt güncelleme", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                if (dg_guncelle == DialogResult.Yes)
                {
                    try
                    {
                        SqlCommand komut_guncelle = new SqlCommand("update TBL_NOTLAR set TARIH=@pr1,SAAT=@pr2,BASLIK=@pr3,DETAY=@pr4,OLUSTURAN=@pr5,HITAP=@pr6 where NOTID=@pr11", bgl2.baglanti());
                        komut_guncelle.Parameters.Clear();
                        komut_guncelle.Parameters.AddWithValue("@pr11", ID);
                        komut_guncelle.Parameters.AddWithValue("@pr1", txtTarih.Text.Trim().ToString());
                        komut_guncelle.Parameters.AddWithValue("@pr2", txtSaat.Text.Trim().ToString());
                        komut_guncelle.Parameters.AddWithValue("@pr3", txtBaslik.Text.Trim().ToString());
                        komut_guncelle.Parameters.AddWithValue("@pr4", txtDetay.Text.Trim().ToString());
                        komut_guncelle.Parameters.AddWithValue("@pr5", txtOlusturan.Text.Trim().ToString());
                        komut_guncelle.Parameters.AddWithValue("@pr6", txtHitap.Text.Trim().ToString());
                        komut_guncelle.ExecuteNonQuery();
                        bgl2.baglanti().Close();
                        listele_notlar();
                        alertControl1.Show(this, "Not güncellendi.", "Seçtiğiniz not başarıyla güncellendi.");

                        //XtraMessageBox.Show("<color=green><b>Başarılı</b></color>\nSeçtiğiniz not başarıyla güncellendi.", "Güncelleme başarılı", MessageBoxButtons.OK);
                        
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

        private void gridView1_Click(object sender, EventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                ID = dr["NOTID"].ToString();
                txtTarih.Text = dr["TARIH"].ToString();
                txtSaat.Text = dr["SAAT"].ToString();
                txtBaslik.Text = dr["BASLIK"].ToString();
                txtDetay.Text = dr["DETAY"].ToString();
                txtOlusturan.Text = dr["OLUSTURAN"].ToString();
                txtHitap.Text = dr["HITAP"].ToString();
            }
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            frmNotDetay frm_detay = new frmNotDetay();
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr!=null)
            {
                frm_detay.metin = dr["DETAY"].ToString();
            }
            frm_detay.Show();
        }
    }
}
