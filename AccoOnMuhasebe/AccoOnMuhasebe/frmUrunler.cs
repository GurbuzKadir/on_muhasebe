using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.Sql;
using DevExpress.XtraEditors;

namespace AccoOnMuhasebe
{
    public partial class frmUrunler : DevExpress.XtraEditors.XtraForm
    {
        public frmUrunler()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();
        string ID = "-1";
        void listele_Urunler()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * from TBL_URUNLER ",bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
            this.gridView1.Columns[0].Width = 25;
            this.gridView1.Columns[0].Visible = false;
            this.gridView1.Columns[5].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.gridView1.Columns[1].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
            this.gridView1.Columns[9].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Max;
        }

        void txtTemizle()
        {
            txtAd.Text = "";
            txtAlis.Text = "";
            txtDetay.Text = "";
            txtMarka.Text = "";
            txtModel.Text = "";
            txtSatis.Text = "";
            txtYil.Text = "";
            txtTarih.Text = "";
            nudAdet.Value = 0;
        }

        void toplam_cek()
        {
            
        }

        private void frmUrunler_Load(object sender, EventArgs e)
        {
            try
            {
                listele_Urunler();
            }
            catch
            {
                XtraMessageBox.Show("Bağlantı problemlerinden dolayı giriş yapılamıyor.", "Bağlantı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            XtraMessageBox.AllowHtmlText = true;
            if (txtAd.Text.Trim().ToString() != "" && txtAlis.Text.Trim().ToString() != "" && txtMarka.Text.Trim().ToString() != "" && txtModel.Text.Trim().ToString() != "" && txtSatis.Text.Trim().ToString() != "" && txtYil.Text.Trim().ToString() != "" && nudAdet.Value.ToString() != "")
            {
                try
                {
                    SqlCommand komut = new SqlCommand("insert into TBL_URUNLER (URUNAD,MARKA,MODEL,YIL,ADET,ALISFIYAT,SATISFIYAT,DETAY,TARIH) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9)", bgl.baglanti());
                    komut.Parameters.Clear();
                    komut.Parameters.AddWithValue("@p1", txtAd.Text.Trim().ToString());
                    komut.Parameters.AddWithValue("@p2", txtMarka.Text.Trim().ToString());
                    komut.Parameters.AddWithValue("@p3", txtModel.Text.Trim().ToString());
                    komut.Parameters.AddWithValue("@p4", txtYil.Text.Trim().ToString());
                    komut.Parameters.AddWithValue("@p5", int.Parse((nudAdet.Value).ToString()));
                    komut.Parameters.AddWithValue("@p6", decimal.Parse(txtAlis.Text.Trim().ToString()));
                    komut.Parameters.AddWithValue("@p7", decimal.Parse(txtSatis.Text.Trim().ToString()));
                    komut.Parameters.AddWithValue("@p8", txtDetay.Text.Trim().ToString());
                    if (txtTarih.Text.Trim().ToString()=="")
                    {
                        komut.Parameters.AddWithValue("@p9",DateTime.Now.ToShortDateString().ToString());
                    }
                    else
                    {
                        komut.Parameters.AddWithValue("@p9", txtTarih.Text.Trim().ToString());
                    }
                    komut.ExecuteNonQuery();
                    listele_Urunler();
                    bgl.baglanti().Close();
                    alertControl1.Show(this, "Ürün eklendi.", "Bilgilerini girdiğiniz ürün başarıyla eklenmiştir.");

                    //XtraMessageBox.Show("<color=green><b>Ürün eklendi.</b></color>\nBilgilerini girdiğiniz ürün başarıyla eklenmiştir.", "Ürün ekleme", MessageBoxButtons.OK);
                    txtTemizle();
                }
                catch
                {
                    XtraMessageBox.Show("<color=red><b>Hata</b></color>\nBir hata meydana geldi lütfen tekrar deneyiniz.","Hata",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
            }
            else
            {
                XtraMessageBox.Show("<color=red><b>Hata</b></color>\nHatalı alan bulunmaktadır.Lütfen kontrol edip tekrar deneyiniz.\nNot : Boş alan bırakılamaz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            //Disable = kaydet,güncelle,sil,yeni
            //Visible_true=btnTamamla,btnIptal
            //ID=selectedrow.cells[0]
            btnKaydet.Enabled = false;
            btnGuncelle.Enabled = false;
            btnSil.Enabled=false;
            btnYeni.Enabled = false;
            btnTamamla.Visible = true;
            btnIptal.Visible = true;
        }

        private void btnIptal_Click(object sender, EventArgs e)
        {
            XtraMessageBox.AllowHtmlText = true;
            DialogResult dg;
            dg = XtraMessageBox.Show("<color=orange><b>Emin misiniz?</b></color>\nGüncelleme işlemini iptal etmek istediğinizden emin misiniz?","Güncelle iptal",MessageBoxButtons.YesNoCancel,MessageBoxIcon.Warning);
            if (dg==DialogResult.Yes)
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
            //Enable = kaydet,güncelle,sil,yeni
            //Visible_false=btnTamamla,btnIptal
            //ID=-1
            //textboxlar temizle
            
        }

        private void btnTamamla_Click(object sender, EventArgs e)
        {
            //Güncelleme işlemi burada yapılacak
            XtraMessageBox.AllowHtmlText = true;
            if (txtAd.Text.Trim().ToString() != "" && txtAlis.Text.Trim().ToString() != "" && txtMarka.Text.Trim().ToString() != "" && txtModel.Text.Trim().ToString() != "" && txtSatis.Text.Trim().ToString() != "" && txtYil.Text.Trim().ToString() != "" && nudAdet.Value.ToString() != "")
            {
                DialogResult dg_guncelle;
                dg_guncelle = XtraMessageBox.Show("<color=orange><b>Emin misiniz?</b></color>\nSeçili kaydı güncellemek istediğinizden emin misiniz?", "Kayıt güncelleme", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                if (dg_guncelle==DialogResult.Yes)
                {
                    try
                    {
                        SqlCommand komut_guncelle = new SqlCommand("update TBL_URUNLER set URUNAD=@pr1,MARKA=@pr2,MODEL=@pr3,YIL=@pr4,ADET=@pr5,ALISFIYAT=@pr6,SATISFIYAT=@pr7,DETAY=@pr8,TARIH=@pr10 where ID=@pr9", bgl.baglanti());
                        komut_guncelle.Parameters.Clear();
                        komut_guncelle.Parameters.AddWithValue("@pr9", ID);
                        komut_guncelle.Parameters.AddWithValue("@pr1", txtAd.Text.Trim().ToString());
                        komut_guncelle.Parameters.AddWithValue("@pr2", txtMarka.Text.Trim().ToString());
                        komut_guncelle.Parameters.AddWithValue("@pr3", txtModel.Text.Trim().ToString());
                        komut_guncelle.Parameters.AddWithValue("@pr4", txtYil.Text.Trim().ToString());
                        komut_guncelle.Parameters.AddWithValue("@pr5", int.Parse((nudAdet.Value).ToString()));
                        komut_guncelle.Parameters.AddWithValue("@pr6", decimal.Parse(txtAlis.Text.Trim().ToString()));
                        komut_guncelle.Parameters.AddWithValue("@pr7", decimal.Parse(txtSatis.Text.Trim().ToString()));
                        komut_guncelle.Parameters.AddWithValue("@pr8", txtDetay.Text.Trim().ToString());
                        if (txtTarih.Text.Trim().ToString()=="")
                        {
                            komut_guncelle.Parameters.AddWithValue("@pr10", DateTime.Now.ToShortDateString().ToString());
                        }
                        else
                        {
                            komut_guncelle.Parameters.AddWithValue("@pr10", txtTarih.Text.Trim().ToString());
                        }
                        komut_guncelle.ExecuteNonQuery();
                        bgl.baglanti().Close();
                        listele_Urunler();
                        alertControl1.Show(this, "Ürün güncellendi.", "Seçtiğiniz ürün başarıyla güncellendi.");

                        //XtraMessageBox.Show("<color=green><b>Başarılı</b></color>\nSeçtiğiniz ürün başarıyla güncellendi.", "Güncelleme başarılı", MessageBoxButtons.OK);
                        
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

        private void btnYeni_Click(object sender, EventArgs e)
        {
            ID = "-1";
            txtTemizle();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            XtraMessageBox.AllowHtmlText = true;
            DialogResult dg_sil;
            dg_sil = XtraMessageBox.Show("<color=orange><b>Emin misiniz?</b></color>\nSeçili kaydı silmek istediğinizden emin misiniz?","Kayıt silme",MessageBoxButtons.YesNoCancel,MessageBoxIcon.Warning);
            if (dg_sil==DialogResult.Yes)
            {
                try
                {
                    SqlCommand komut_sil = new SqlCommand("Delete from TBL_URUNLER where ID=@pr1",bgl.baglanti());
                    komut_sil.Parameters.Clear();
                    komut_sil.Parameters.AddWithValue("@pr1",ID);
                    komut_sil.ExecuteNonQuery();
                    bgl.baglanti().Close();
                    listele_Urunler();
                    alertControl1.Show(this, "Ürün silindi.", "Seçtiğiniz ürün başarıyla silindi.");

                    //XtraMessageBox.Show("<color=green><b>Başarılı</b></color>\nSeçtiğiniz ürün başarıyla silindi.","Silme başarılı",MessageBoxButtons.OK);
                    txtTemizle();
                    
                }
                catch
                {
                    XtraMessageBox.Show("<color=red><b>Hata</b></color>\nBir hata meydana geldi. Tekrar deneyiniz.","Hata",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            listele_Urunler();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            txtTarih.Text = DateTime.Now.ToShortDateString().ToString();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            frmUrunlerGrup frm_UrunlerGrup = new frmUrunlerGrup();
            frm_UrunlerGrup.Show();
        }

        private void gridView1_Click(object sender, EventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                ID = dr["ID"].ToString();
                txtAd.Text = dr["URUNAD"].ToString();
                txtMarka.Text = dr["MARKA"].ToString();
                txtModel.Text = dr["MODEL"].ToString();
                txtYil.Text = dr["YIL"].ToString();
                nudAdet.Value = int.Parse(dr["ADET"].ToString());
                txtAlis.Text = dr["ALISFIYAT"].ToString();
                txtSatis.Text = dr["SATISFIYAT"].ToString();
                txtDetay.Text = dr["DETAY"].ToString();
                txtTarih.Text = dr["TARIH"].ToString();
            }
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            txtYil.Text = DateTime.Now.Year.ToString();
        }
    }
}
