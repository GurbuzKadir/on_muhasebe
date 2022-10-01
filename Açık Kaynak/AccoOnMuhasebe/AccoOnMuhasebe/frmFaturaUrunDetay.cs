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
    public partial class frmFaturaUrunDetay : DevExpress.XtraEditors.XtraForm
    {
        public frmUrunListesi frm_urun_listesi;
        public frmFaturaUrunDetay()
        {
            InitializeComponent();
            frm_urun_listesi = new frmUrunListesi();
            frm_urun_listesi.frm_Fatura_Detay = this;
        }
        public string id;

        sqlbaglantisi bgl3 = new sqlbaglantisi();
        string ID = "-1";
        public string urun_ad,urun_fiyat,urun_id="-1";
        
        public void verileri_cek()
        {
            txtUrunAd.Text = urun_ad.ToString();
            txtFiyat.Text = urun_fiyat.ToString();
            txtMiktar.Text = "";
            txtTutar.Text = "";
        }

        void listele_faturalar()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * from TBL_FATURADETAY where FATURAID='"+id.ToString()+"'", bgl3.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
            //this.gridView1.Columns[0].Width = 25;
            this.gridView1.Columns[0].Visible = false;
            this.gridView1.Columns[6].Visible = false;
            this.gridView1.Columns[5].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.gridView1.Columns[2].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
            this.gridView1.Columns[3].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.gridView1.Columns[1].Visible = false;
        }

        void txtTemizle()
        {
            txtFiyat.Text = "";
            txtMiktar.Text = "";
            txtTutar.Text = "";
            txtUrunAd.Text = "";
            urun_id = "-1";
        }

        private void frmFaturaUrunDetay_Load(object sender, EventArgs e)
        {
            //MessageBox.Show(id.ToString());
            listele_faturalar();
        }
        double miktar, tutar, fiyat;
        private void btnKaydet_Click(object sender, EventArgs e)
        {
            fiyat = Convert.ToDouble(txtFiyat.Text.Trim());
            miktar = Convert.ToDouble(txtMiktar.Text.Trim());
            tutar = fiyat * miktar;
            txtTutar.Text = tutar.ToString();
            XtraMessageBox.AllowHtmlText = true;
            if (txtUrunAd.Text.Trim().ToString()!=""&&txtFiyat.Text.Trim().ToString()!=""&&txtMiktar.Text.Trim().ToString()!=""&&txtTutar.Text.Trim().ToString()!="")
            {
                try
                {
                SqlCommand komut = new SqlCommand("update TBL_FATURADETAY set URUNAD=@p1,MIKTAR=@p2,FIYAT=@p3,TUTAR=@p4,FATURAID=@p6 where FATURAURUNID=@p5", bgl3.baglanti());
                    komut.Parameters.Clear();
                    komut.Parameters.AddWithValue("@p5", ID.ToString());
                    komut.Parameters.AddWithValue("@p1", txtUrunAd.Text.Trim().ToString());
                    komut.Parameters.AddWithValue("@p2", Convert.ToInt32(txtMiktar.Text.Trim().ToString()));
                    komut.Parameters.AddWithValue("@p3", Convert.ToDecimal(txtFiyat.Text.Trim().ToString()));
                    komut.Parameters.AddWithValue("@p4", Convert.ToDecimal(txtTutar.Text.Trim().ToString()));
                    komut.Parameters.AddWithValue("@p6", id.ToString());
                    komut.ExecuteNonQuery();
                    listele_faturalar();
                    bgl3.baglanti().Close();
                    alertControl1.Show(this, "Fatura ürünü güncellendi.", "Bilgilerini girdiğiniz fatura ürünü başarıyla güncellendi.");
                    //XtraMessageBox.Show("<color=green><b>Fatura ürünü güncellendi.</b></color>\nBilgilerini girdiğiniz fatura ürünü başarıyla güncellendi.", "Fatura ürünü güncelleme", MessageBoxButtons.OK);
                    txtTemizle();
                }
                catch
                {
                    XtraMessageBox.Show("<color=red><b>Hata</b></color>\nBir hata meydana geldi lütfen tekrar deneyiniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnYeni_Click(object sender, EventArgs e)
        {
            txtTemizle();
            ID = "-1";
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            fiyat = Convert.ToDouble(txtFiyat.Text.Trim());
            miktar = Convert.ToDouble(txtMiktar.Text.Trim());
            tutar = fiyat * miktar;
            txtTutar.Text = tutar.ToString();
            XtraMessageBox.AllowHtmlText = true;
            if (txtUrunAd.Text.Trim().ToString() != "" && txtFiyat.Text.Trim().ToString() != "" && txtMiktar.Text.Trim().ToString() != "" && txtTutar.Text.Trim().ToString() != "")
            {
                try
                {
                    SqlCommand komut = new SqlCommand("insert into TBL_FATURADETAY (URUNID,URUNAD,MIKTAR,FIYAT,TUTAR,FATURAID) values (@p0,@p1,@p2,@p3,@p4,@p5)", bgl3.baglanti());
                    komut.Parameters.Clear();
                    komut.Parameters.AddWithValue("@p0", urun_id.ToString());
                    komut.Parameters.AddWithValue("@p1", txtUrunAd.Text.Trim().ToString());
                    komut.Parameters.AddWithValue("@p2", Convert.ToInt32(txtMiktar.Text.Trim().ToString()));
                    komut.Parameters.AddWithValue("@p3", Convert.ToDecimal(txtFiyat.Text.Trim().ToString()));
                    komut.Parameters.AddWithValue("@p4", Convert.ToDecimal(txtTutar.Text.Trim().ToString()));
                    komut.Parameters.AddWithValue("@p5", id.ToString());
                    komut.ExecuteNonQuery();
                    listele_faturalar();
                    bgl3.baglanti().Close();
                    alertControl1.Show(this, "Fatura ürünü eklendi.", "Bilgilerini girdiğiniz fatura ürünü başarıyla eklenmiştir.");
                    //XtraMessageBox.Show("<color=green><b>Fatura ürünü eklendi.</b></color>\nBilgilerini girdiğiniz fatura ürünü başarıyla eklenmiştir.", "Fatura ürünü ekleme", MessageBoxButtons.OK);
                    
                    //Hareket burada eklenecek
                    
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

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            listele_faturalar();
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
                    SqlCommand komut_sil = new SqlCommand("Delete from TBL_FATURADETAY where FATURAURUNID=@pr1", bgl3.baglanti());
                    komut_sil.Parameters.Clear();
                    komut_sil.Parameters.AddWithValue("@pr1", ID);
                    komut_sil.ExecuteNonQuery();
                    bgl3.baglanti().Close();
                    listele_faturalar();
                    alertControl1.Show(this, "Fatura ürünü silindi.", "Seçtiğiniz fatura ürünü başarıyla silindi.");
                    //XtraMessageBox.Show("<color=green><b>Başarılı</b></color>\nSeçtiğiniz fatura ürünü başarıyla silindi.", "Silme başarılı", MessageBoxButtons.OK);
                    txtTemizle();

                }
                catch
                {
                    XtraMessageBox.Show("<color=red><b>Hata</b></color>\nBir hata meydana geldi. Tekrar deneyiniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            frm_urun_listesi.ShowDialog();
        }

        private void gridView1_Click(object sender, EventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                urun_id = dr["URUNID"].ToString();
                ID = dr["FATURAURUNID"].ToString();
                txtUrunAd.Text = dr["URUNAD"].ToString();
                txtMiktar.Text = dr["MIKTAR"].ToString();
                txtFiyat.Text = dr["FIYAT"].ToString();
                txtTutar.Text = dr["TUTAR"].ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(urun_id.ToString());
        }

        private void ozellik_goster_Click(object sender, EventArgs e)
        {
            frmUrunOzellik ozellik = new frmUrunOzellik();
            ozellik.urun_ozellik_id = urun_id.ToString();
            ozellik.Show();
        }
    }
}
