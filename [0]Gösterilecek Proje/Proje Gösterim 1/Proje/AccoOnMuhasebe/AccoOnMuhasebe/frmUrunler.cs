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
        int yil;
        void listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * from TBL_URUNLER ",bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
            this.gridView1.Columns[0].Width = 25;
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
            nudAdet.Value = 0;
        }

        private void frmUrunler_Load(object sender, EventArgs e)
        {
            listele();
            yil = int.Parse(DateTime.Now.Year.ToString());
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            XtraMessageBox.AllowHtmlText = true;
            int kontrol_yil=Convert.ToInt32(txtYil.Text.Trim().ToString());
            if (txtAd.Text.Trim().ToString() != null && txtAlis.Text.Trim().ToString() != null && txtMarka.Text.Trim().ToString() != null && txtModel.Text.Trim().ToString() != null && txtSatis.Text.Trim().ToString() != null && txtYil.Text.Trim().ToString() != null && nudAdet.Value.ToString() != null&&kontrol_yil<=yil)
            {
                try
                {
                    SqlCommand komut = new SqlCommand("insert into TBL_URUNLER (URUNAD,MARKA,MODEL,YIL,ADET,ALISFIYAT,SATISFIYAT,DETAY) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8)", bgl.baglanti());
                    komut.Parameters.Clear();
                    komut.Parameters.AddWithValue("@p1", txtAd.Text.Trim().ToString());
                    komut.Parameters.AddWithValue("@p2", txtMarka.Text.Trim().ToString());
                    komut.Parameters.AddWithValue("@p3", txtModel.Text.Trim().ToString());
                    komut.Parameters.AddWithValue("@p4", txtYil.Text.Trim().ToString());
                    komut.Parameters.AddWithValue("@p5", int.Parse((nudAdet.Value).ToString()));
                    komut.Parameters.AddWithValue("@p6", decimal.Parse(txtAlis.Text.Trim().ToString()));
                    komut.Parameters.AddWithValue("@p7", decimal.Parse(txtSatis.Text.Trim().ToString()));
                    komut.Parameters.AddWithValue("@p8", txtDetay.Text.Trim().ToString());
                    komut.ExecuteNonQuery();
                    listele();
                    bgl.baglanti().Close();
                    XtraMessageBox.Show("<color=green><b>Ürün eklendi.</b></color>\nBilgilerini girdiğiniz ürün başarıyla eklenmiştir.", "Ürün ekleme", MessageBoxButtons.OK);
                    txtTemizle();
                }
                catch
                {
                    XtraMessageBox.Show("<color=red><b>Hata</b></color>\nBir hata meydana geldi lütfen tekrar deneyiniz.","Hata",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
            }
            else
            {
                XtraMessageBox.Show("<color=red><b>Hata</b></color>\nHatalı alan bulunmaktadır.Lütfen kontrol edip tekrar deneyiniz.\nNot : Yıl bulunulan yıldan daha büyük girilemez ve boş alan bırakılamaz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void f(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnSortEventArgs e)
        {

        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            ID = dr["ID"].ToString();
            txtAd.Text=dr["URUNAD"].ToString();
            txtMarka.Text = dr["MARKA"].ToString();
            txtModel.Text = dr["MODEL"].ToString();
            txtYil.Text = dr["YIL"].ToString();
            nudAdet.Value = int.Parse(dr["ADET"].ToString());
            txtAlis.Text = dr["ALISFIYAT"].ToString();
            txtSatis.Text = dr["SATISFIYAT"].ToString();
            txtDetay.Text = dr["DETAY"].ToString();
        }

        private void btnTamamla_Click(object sender, EventArgs e)
        {
            //Güncelleme işlemi burada yapılacak
            XtraMessageBox.AllowHtmlText = true;
            int kontrol_yil = Convert.ToInt32(txtYil.Text.Trim().ToString());
            if (txtAd.Text.Trim().ToString() != null && txtAlis.Text.Trim().ToString() != null && txtMarka.Text.Trim().ToString() != null && txtModel.Text.Trim().ToString() != null && txtSatis.Text.Trim().ToString() != null && txtYil.Text.Trim().ToString() != null && nudAdet.Value.ToString() != null&&kontrol_yil<=yil)
            {
                DialogResult dg_guncelle;
                dg_guncelle = XtraMessageBox.Show("<color=orange><b>Emin misiniz?</b></color>\nSeçili kaydı güncellemek istediğinizden emin misiniz?", "Kayıt güncelleme", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                if (dg_guncelle==DialogResult.Yes)
                {
                    try
                    {
                        SqlCommand komut_guncelle = new SqlCommand("update TBL_URUNLER set URUNAD=@pr1,MARKA=@pr2,MODEL=@pr3,YIL=@pr4,ADET=@pr5,ALISFIYAT=@pr6,SATISFIYAT=@pr7,DETAY=@pr8 where ID=@pr9", bgl.baglanti());
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
                        komut_guncelle.ExecuteNonQuery();
                        bgl.baglanti().Close();
                        listele();
                        XtraMessageBox.Show("<color=green><b>Başarılı</b></color>\nSeçtiğiniz ürün başarıyla güncellendi.", "Güncelleme başarılı", MessageBoxButtons.OK);
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
                XtraMessageBox.Show("<color=red><b>Hata</b></color>\nHatalı alan bulunmaktadır.Lütfen kontrol edip tekrar deneyiniz.\nNot : Yıl bulunulan yıldan daha büyük girilemez ve boş alan bırakılamaz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    listele();
                    XtraMessageBox.Show("<color=green><b>Başarılı</b></color>\nSeçtiğiniz ürün başarıyla silindi.","Silme başarılı",MessageBoxButtons.OK);
                    
                }
                catch
                {
                    XtraMessageBox.Show("<color=red><b>Hata</b></color>\nBir hata meydana geldi. Tekrar deneyiniz.","Hata",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
            }
        }
    }
}
