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
    public partial class frmFaturalar : Form
    {
        /*Fatura id ile normal id birbirlerinin aynısı olacaktır.*/
        public frmFirmaListesi frm_firma_listesi;
        public frmMusteriListesi frm_musteri_listesi;
        public frmFaturalar()
        {
            InitializeComponent();
            frm_firma_listesi = new frmFirmaListesi();
            frm_musteri_listesi = new frmMusteriListesi();
            frm_firma_listesi.frm_Faturalar_firma = this;
            frm_musteri_listesi.frm_Faturalar_Musteri = this;
        }

        sqlbaglantisi bgl3 = new sqlbaglantisi();
        string ID = "-1";
        string fatura_detay_durum;
        
        void listele_faturalar()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * from TBL_FATURABILGI ", bgl3.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
            this.gridView1.Columns[0].Width = 25;
            this.gridView1.Columns[0].Visible = false;
            this.gridView1.Columns[9].Visible = false;
            this.gridView1.Columns[10].Visible = false;
        }



        void fatura_Detay_Kontrol()
        {
            SqlCommand cmd = new SqlCommand("Select * from TBL_FATURADETAY where FATURAID='" + ID.ToString()+ "'", bgl3.baglanti());
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                fatura_detay_durum = "var";
            }
            else
            {
                fatura_detay_durum = "yok";
            }
            bgl3.baglanti().Close();
        }

        void urun_sil()
        {
            SqlCommand komut_sil = new SqlCommand("Delete from TBL_FATURADETAY where FATURAID=@pr1", bgl3.baglanti());
            komut_sil.Parameters.Clear();
            komut_sil.Parameters.AddWithValue("@pr1", ID);
            komut_sil.ExecuteNonQuery();
            bgl3.baglanti().Close();
        }

        void txtTemizleBilgi()
        {
            txtSeri.Text = "";
            txtSiraNo.Text = "";
            txtTarih.Text = "";
            txtSaat.Text = "";
            txtVergiDairesi.Text = "";
            txtAlici.Text = "";
            txtTeslimAlan.Text = "";
            txtTeslimEden.Text = "";
            lblSatis.Text = "";
        }

        void txtTemizleTumu()
        {
            txtTemizleBilgi();
        }
        public string lbl_satis_text = "";
        public void lbl_satis_doldur()
        {
            lblSatis.Text = lbl_satis_text.ToString();
        }

        private void frmFaturalar_Load(object sender, EventArgs e)
        {
            try
            {
                listele_faturalar();
            }
            catch
            {
                XtraMessageBox.Show("Bağlantı problemlerinden dolayı işlem yapılamıyor.", "Bağlantı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            listele_faturalar();
        }

        private void btnYeni_Click(object sender, EventArgs e)
        {
            ID = "-1";
            txtTemizleTumu();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            XtraMessageBox.AllowHtmlText = true;
            if (txtSeri.Text.Trim().ToString() != "" && txtSiraNo.Text.Trim().ToString() != "" && txtTarih.Text.Trim().ToString() != "" && txtSaat.Text.Trim().ToString() != "" && txtVergiDairesi.Text.Trim().ToString() != "" && txtAlici.Text.Trim().ToString() != ""&&(firma_id!="-1"||musteri_id!="-1"))
            {
                try
                {
                    SqlCommand komut = new SqlCommand("insert into TBL_FATURABILGI (SERI,SIRANO,TARIH,SAAT,VERGIDAIRE,ALICI,TESLIMEDEN,TESLIMALAN,FIRMAID,MUSTERIID) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10)", bgl3.baglanti());
                    komut.Parameters.Clear();
                    komut.Parameters.AddWithValue("@p1", txtSeri.Text.Trim().ToString());
                    komut.Parameters.AddWithValue("@p2", txtSiraNo.Text.Trim().ToString());
                    komut.Parameters.AddWithValue("@p3", txtTarih.Text.Trim().ToString());
                    komut.Parameters.AddWithValue("@p4", txtSaat.Text.Trim().ToString());
                    komut.Parameters.AddWithValue("@p5", txtVergiDairesi.Text.Trim().ToString());
                    komut.Parameters.AddWithValue("@p6", txtAlici.Text.Trim().ToString());
                    komut.Parameters.AddWithValue("@p7", txtTeslimEden.Text.Trim().ToString());
                    komut.Parameters.AddWithValue("@p8", txtTeslimAlan.Text.Trim().ToString());
                    komut.Parameters.AddWithValue("@p9", int.Parse(firma_id.ToString()));
                    komut.Parameters.AddWithValue("@p10", int.Parse(musteri_id.ToString()));
                    komut.ExecuteNonQuery();
                    listele_faturalar();
                    bgl3.baglanti().Close();
                    alertControl1.Show(this, "Fatura eklendi.", "Bilgilerini girdiğiniz fatura başarıyla eklenmiştir.");
                    //XtraMessageBox.Show("<color=green><b>Fatura eklendi.</b></color>\nBilgilerini girdiğiniz fatura başarıyla eklenmiştir.", "Fatura ekleme", MessageBoxButtons.OK);
                    
                    //Hareketler tablosuna veri girişi burada yapılacak.



                    txtTemizleBilgi();
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

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            txtTarih.Text = DateTime.Now.ToShortDateString().ToString();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            txtSaat.Text = DateTime.Now.ToShortTimeString().ToString();
        }
        /*
         
         */
        /*Burada silme işlemi yapılırken eğer faturaya ait bir kayıt varsa o kayıtlar önce temizlenecek daha sonra silme işlemi yapılacak.
         yani faturadetay tablosuna ait kayıtlar 'delete from TBL_FATURADETAY where FATURAID='"+ID.TosString()+"'' şeklinde olacaktır.
         */

        /*Burada ürün seçimi yapma işlemi biraz daha iyi bir hale getirilecektir.Fiyat kısmı vb. otomatik çekilecektir.*/
        private void btnSil_Click(object sender, EventArgs e)
        {
            XtraMessageBox.AllowHtmlText = true;
            DialogResult dg_sil;
            dg_sil = XtraMessageBox.Show("<color=orange><b>Emin misiniz?</b></color>\nSeçili kaydı silmek istediğinizden emin misiniz?", "Kayıt silme", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
            if (dg_sil == DialogResult.Yes)
            {
                try
                {
                    fatura_Detay_Kontrol();
                    if (fatura_detay_durum=="yok")
                    {
                        SqlCommand komut_sil = new SqlCommand("Delete from TBL_FATURABILGI where FATURABILGIID=@pr1", bgl3.baglanti());
                        komut_sil.Parameters.Clear();
                        komut_sil.Parameters.AddWithValue("@pr1", ID);
                        komut_sil.ExecuteNonQuery();
                        bgl3.baglanti().Close();
                        listele_faturalar();
                        alertControl1.Show(this, "Fatura silindi.", "Seçtiğiniz fatura başarıyla silindi.");

                        //XtraMessageBox.Show("<color=green><b>Başarılı</b></color>\nSeçtiğiniz fatura başarıyla silindi.", "Silme başarılı", MessageBoxButtons.OK);
                        txtTemizleTumu();
                    }
                    else
                    {
                        DialogResult dg_kayitlar;
                        dg_kayitlar = XtraMessageBox.Show("<color=orange><b>Emin misiniz?</b></color>\nSeçili kayda ait ürünler bulunmaktadır. Yinede silmek istediğinizden emin misiniz?", "Kayıt mevcut", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                        if (dg_kayitlar == DialogResult.Yes)
                        {
                            //ilk önce ürünleri sil sonra faturayı sil
                            urun_sil();
                            SqlCommand komut_sil = new SqlCommand("Delete from TBL_FATURABILGI where FATURABILGIID=@pr1", bgl3.baglanti());
                            komut_sil.Parameters.Clear();
                            komut_sil.Parameters.AddWithValue("@pr1", ID);
                            komut_sil.ExecuteNonQuery();
                            bgl3.baglanti().Close();
                            listele_faturalar();
                            alertControl1.Show(this, "Fatura silindi.", "Seçtiğiniz fatura başarıyla silindi.");

                            //XtraMessageBox.Show("<color=green><b>Başarılı</b></color>\nSeçtiğiniz fatura başarıyla silindi.", "Silme başarılı", MessageBoxButtons.OK);
                            txtTemizleTumu();
                        }
                    }

                }
                catch
                {
                    XtraMessageBox.Show("<color=red><b>Hata</b></color>\nBir hata meydana geldi. Tekrar deneyiniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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
                txtTemizleTumu();
                ID = "-1";
            }
        }

        private void btnTamamla_Click(object sender, EventArgs e)
        {
            /*TBL_FATURABILGI (SERI,SIRANO,TARIH,SAAT,VERGIDAIRE,ALICI,TESLIMEDEN,TESLIMALAN) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8*/
            //Güncelleme işlemi burada yapılacak
            XtraMessageBox.AllowHtmlText = true;
            if (txtSeri.Text.Trim().ToString() != "" && txtSiraNo.Text.Trim().ToString() != "" && txtTarih.Text.Trim().ToString() != "" && txtSaat.Text.Trim().ToString() != "" && txtVergiDairesi.Text.Trim().ToString() != "" && txtAlici.Text.Trim().ToString() != "" && (firma_id != "-1" || musteri_id != "-1"))
            {

                DialogResult dg_guncelle;
                dg_guncelle = XtraMessageBox.Show("<color=orange><b>Emin misiniz?</b></color>\nSeçili kaydı güncellemek istediğinizden emin misiniz?", "Kayıt güncelleme", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                if (dg_guncelle == DialogResult.Yes)
                {
                    try
                    {
                        SqlCommand komut_guncelle = new SqlCommand("update TBL_FATURABILGI set SERI=@pr1,SIRANO=@pr2,TARIH=@pr3,SAAT=@pr4,VERGIDAIRE=@pr5,ALICI=@pr6,TESLIMEDEN=@pr7,TESLIMALAN=@pr8,FIRMAID=@pr9,MUSTERIID=@pr10 where FATURABILGIID=@pr18", bgl3.baglanti());
                        komut_guncelle.Parameters.Clear();
                        //AD,YETKILISTATU,YETKILIADSOYAD,YETKILITC,SEKTOR,TELEFON1,TELEFON2,TELEFON3,MAIL,FAX,IL,ILCE,VERGIDAIRE,ADRES,OZELKOD1,OZELKOD2,OZELKOD3
                        komut_guncelle.Parameters.AddWithValue("@pr18", ID);
                        komut_guncelle.Parameters.AddWithValue("@pr1", txtSeri.Text.Trim().ToString());
                        komut_guncelle.Parameters.AddWithValue("@pr2", txtSiraNo.Text.Trim().ToString());
                        komut_guncelle.Parameters.AddWithValue("@pr3", txtTarih.Text.Trim().ToString());
                        komut_guncelle.Parameters.AddWithValue("@pr4", txtSaat.Text.Trim().ToString());
                        komut_guncelle.Parameters.AddWithValue("@pr5", txtVergiDairesi.Text.Trim().ToString());
                        komut_guncelle.Parameters.AddWithValue("@pr6", txtAlici.Text.Trim().ToString());
                        komut_guncelle.Parameters.AddWithValue("@pr7", txtTeslimEden.Text.Trim().ToString());
                        komut_guncelle.Parameters.AddWithValue("@pr8", txtTeslimAlan.Text.Trim().ToString());
                        komut_guncelle.Parameters.AddWithValue("@pr9", int.Parse(firma_id.ToString()));
                        komut_guncelle.Parameters.AddWithValue("@pr10", int.Parse(musteri_id.ToString()));
                        komut_guncelle.ExecuteNonQuery();
                        bgl3.baglanti().Close();
                        listele_faturalar();
                        alertControl1.Show(this, "Fatura güncellendi.", "Seçtiğiniz fatura başarıyla güncellendi.");
                        //XtraMessageBox.Show("<color=green><b>Başarılı</b></color>\nSeçtiğiniz fatura başarıyla güncellendi.", "Güncelleme başarılı", MessageBoxButtons.OK);
                        
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
                        txtTemizleTumu();
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
        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            frmFaturaUrunDetay frm_detay = new frmFaturaUrunDetay();
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr!=null)
            {
                frm_detay.id = dr["FATURABILGIID"].ToString();
            }
            frm_detay.Show();
        }

        private void gridView1_Click(object sender, EventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                ID = dr["FATURABILGIID"].ToString();
                txtSeri.Text = dr["SERI"].ToString();
                txtSiraNo.Text = dr["SIRANO"].ToString();
                txtTarih.Text = dr["TARIH"].ToString();
                txtSaat.Text = dr["SAAT"].ToString();
                txtVergiDairesi.Text = dr["VERGIDAIRE"].ToString();
                txtAlici.Text = dr["ALICI"].ToString();
                txtTeslimEden.Text = dr["TESLIMEDEN"].ToString();
                txtTeslimAlan.Text = dr["TESLIMALAN"].ToString();
                firma_id = dr["FIRMAID"].ToString();
                musteri_id = dr["MUSTERIID"].ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            fatura_Detay_Kontrol();
        }
        public string firma_id = "-1";
        public string musteri_id = "-1";
        private void simpleButton4_Click(object sender, EventArgs e)
        {
            frm_firma_listesi.ShowDialog();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            MessageBox.Show("Müşteri id :"+musteri_id.ToString()+"\nFirma id :"+firma_id.ToString()+"");
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            frm_musteri_listesi.ShowDialog();
        }
        void satiscek_firma()
        {
            SqlCommand cmd_cek = new SqlCommand("Select * from TBL_FIRMALAR where ID="+firma_id+"",bgl3.baglanti());
            SqlDataReader dr_cek = cmd_cek.ExecuteReader();
            while (dr_cek.Read())
            {
                firma_isim = dr_cek["AD"].ToString();
            }
            bgl3.baglanti().Close();
        }
        string firma_isim = "", musteri_isim = "";
        void satiscek_musteri()
        {
            SqlCommand cmd_cek1 = new SqlCommand("Select * from TBL_MUSTERILER where ID=" + musteri_id + "", bgl3.baglanti());
            SqlDataReader dr_cek1 = cmd_cek1.ExecuteReader();
            while (dr_cek1.Read())
            {
                musteri_isim = dr_cek1["AD"].ToString() + " " + dr_cek1["SOYAD"].ToString();
            }
            bgl3.baglanti().Close();
        }
        private void simpleButton6_Click(object sender, EventArgs e)
        {

            if (firma_id.ToString()=="-1"&&musteri_id.ToString()!="-1")
            {
                satiscek_musteri();
                XtraMessageBox.Show("Seçilen Müşteri\n" + musteri_isim.ToString()+ "");
            }
            else if (firma_id.ToString()!="-1"&&musteri_id.ToString()=="-1")
            {
                satiscek_firma();
                XtraMessageBox.Show("Seçilen Firma\n" + firma_isim.ToString() + "");
            }
            else
            {
                XtraMessageBox.Show("Seçim işlemleri doğru şekilde yapılmadı.");
            }
        }
    }
}
