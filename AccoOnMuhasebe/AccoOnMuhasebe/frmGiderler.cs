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
    public partial class frmGiderler : DevExpress.XtraEditors.XtraForm
    {
        public frmGiderler()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();
        string ID = "-1";

        void listele_giderler()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * from TBL_GIDERLER ", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
            this.gridView1.Columns[0].Width = 25;
            this.gridView1.Columns[0].Visible = false;
        }
        void txtTemizle()
        {
            txtAciklama.Text = "";
            txtAy.Text = "";
            txtDogalgaz.Text = "";
            txtEkstra.Text = "";
            txtElektrik.Text = "";
            txtInternet.Text = "";
            txtMaas.Text = "";
            txtSu.Text = "";
            txtYil.Text = "";
        }

        private void btnYeni_Click(object sender, EventArgs e)
        {
            ID = "-1";
            txtTemizle();
        }

        private void frmGiderler_Load(object sender, EventArgs e)
        {
            try
            {
                listele_giderler();
            }
            catch
            {
                XtraMessageBox.Show("Bağlantı problemlerinden dolayı işlem yapılamıyor.", "Bağlantı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            listele_giderler();
        }
        int sifir = 0;
        private void btnKaydet_Click(object sender, EventArgs e)
        {
            XtraMessageBox.AllowHtmlText = true;
            if (txtAy.Text.Trim().ToString()!=""&&txtElektrik.Text.Trim().ToString()!=""&&txtInternet.Text.Trim().ToString()!=""&&txtSu.Text.Trim().ToString()!=""&&txtYil.Text.Trim().ToString()!="")
            {
                try
                {
                    SqlCommand komut = new SqlCommand("insert into TBL_GIDERLER (AY,YIL,ELEKTRIK,SU,DOGALGAZ,INTERNET,MAASLAR,EKSTRA,NOTLAR) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9)", bgl.baglanti());
                    komut.Parameters.Clear();
                    komut.Parameters.AddWithValue("@p1", txtAy.Text.Trim().ToString());
                    komut.Parameters.AddWithValue("@p2", txtYil.Text.Trim().ToString());
                    komut.Parameters.AddWithValue("@p3", decimal.Parse(txtElektrik.Text.Trim()));
                    komut.Parameters.AddWithValue("@p4", decimal.Parse(txtSu.Text.Trim().ToString()));
                    komut.Parameters.AddWithValue("@p5", decimal.Parse(txtDogalgaz.Text.Trim()));
                    komut.Parameters.AddWithValue("@p6", decimal.Parse(txtInternet.Text.Trim()));
                    komut.Parameters.AddWithValue("@p7", decimal.Parse(txtMaas.Text.Trim()));
                    if (txtEkstra.Text.Trim().ToString()=="")
                    {
                        komut.Parameters.AddWithValue("@p8", decimal.Parse(sifir.ToString()));
                    }
                    else
                    {
                        komut.Parameters.AddWithValue("@p8", decimal.Parse(txtEkstra.Text.Trim()));
                    }
                    komut.Parameters.AddWithValue("@p9", txtAciklama.Text.Trim().ToString());
                    komut.ExecuteNonQuery();
                    listele_giderler();
                    bgl.baglanti().Close();
                    alertControl1.Show(this, "Gider eklendi.", "Bilgilerini girdiğiniz gider başarıyla eklenmiştir.");

                    //XtraMessageBox.Show("<color=green><b>Gider eklendi.</b></color>\nBilgilerini girdiğiniz gider başarıyla eklenmiştir.", "Gider ekleme", MessageBoxButtons.OK);
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

        private void btnSil_Click(object sender, EventArgs e)
        {
            XtraMessageBox.AllowHtmlText = true;
            DialogResult dg_sil;
            dg_sil = XtraMessageBox.Show("<color=orange><b>Emin misiniz?</b></color>\nSeçili kaydı silmek istediğinizden emin misiniz?", "Kayıt silme", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
            if (dg_sil == DialogResult.Yes)
            {
                try
                {
                    SqlCommand komut_sil = new SqlCommand("Delete from TBL_GIDERLER where ID=@pr1", bgl.baglanti());
                    komut_sil.Parameters.Clear();
                    komut_sil.Parameters.AddWithValue("@pr1", ID);
                    komut_sil.ExecuteNonQuery();
                    bgl.baglanti().Close();
                    listele_giderler();
                    alertControl1.Show(this, "Gider silindi.", "Seçtiğiniz gider başarıyla silindi.");

                    //XtraMessageBox.Show("<color=green><b>Başarılı</b></color>\nSeçtiğiniz gider başarıyla silindi.", "Silme başarılı", MessageBoxButtons.OK);
                    txtTemizle();

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
                txtTemizle();
                ID = "-1";
            }
        }

        private void btnTamamla_Click(object sender, EventArgs e)
        {
            //Güncelleme işlemi burada yapılacak
            XtraMessageBox.AllowHtmlText = true;
            if (txtAy.Text.Trim().ToString() != "" && txtElektrik.Text.Trim().ToString() != "" && txtInternet.Text.Trim().ToString() != "" && txtSu.Text.Trim().ToString() != "" && txtYil.Text.Trim().ToString() != "")
            {
                DialogResult dg_guncelle;
                dg_guncelle = XtraMessageBox.Show("<color=orange><b>Emin misiniz?</b></color>\nSeçili kaydı güncellemek istediğinizden emin misiniz?", "Kayıt güncelleme", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                if (dg_guncelle == DialogResult.Yes)
                {
                    try
                    {
                        SqlCommand komut_guncelle = new SqlCommand("update TBL_GIDERLER set AY=@pr1,YIL=@pr2,ELEKTRIK=@pr3,SU=@pr4,DOGALGAZ=@pr5,INTERNET=@pr6,MAASLAR=@pr7,EKSTRA=@pr8,NOTLAR=@pr9 where ID=@pr10", bgl.baglanti());
                        komut_guncelle.Parameters.Clear();
                        komut_guncelle.Parameters.AddWithValue("@pr10", ID);
                        komut_guncelle.Parameters.AddWithValue("@pr1", txtAy.Text.Trim().ToString());
                        komut_guncelle.Parameters.AddWithValue("@pr2", txtYil.Text.Trim().ToString());
                        komut_guncelle.Parameters.AddWithValue("@pr3", decimal.Parse(txtElektrik.Text.Trim().ToString()));
                        komut_guncelle.Parameters.AddWithValue("@pr4", decimal.Parse(txtSu.Text.Trim().ToString()));
                        komut_guncelle.Parameters.AddWithValue("@pr5", decimal.Parse(txtDogalgaz.Text.Trim().ToString()));
                        komut_guncelle.Parameters.AddWithValue("@pr6", decimal.Parse(txtInternet.Text.Trim().ToString()));
                        komut_guncelle.Parameters.AddWithValue("@pr7", decimal.Parse(txtMaas.Text.Trim().ToString()));
                        if (txtEkstra.Text.Trim().ToString() == "")
                        {
                            komut_guncelle.Parameters.AddWithValue("@pr8", decimal.Parse(sifir.ToString()));
                        }
                        else
                        {
                            komut_guncelle.Parameters.AddWithValue("@pr8", decimal.Parse(txtEkstra.Text.Trim().ToString()));
                        }
                        komut_guncelle.Parameters.AddWithValue("@pr9", txtAciklama.Text.Trim().ToString());
                        komut_guncelle.ExecuteNonQuery();
                        bgl.baglanti().Close();
                        listele_giderler();
                        alertControl1.Show(this, "Gider güncellendi.", "Seçtiğiniz gider başarıyla güncellendi.");

                        //XtraMessageBox.Show("<color=green><b>Başarılı</b></color>\nSeçtiğiniz gider başarıyla güncellendi.", "Güncelleme başarılı", MessageBoxButtons.OK);
                        
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
                ID = dr["ID"].ToString();
                txtAy.Text = dr["AY"].ToString();
                txtYil.Text = dr["YIL"].ToString();
                txtElektrik.Text = dr["ELEKTRIK"].ToString();
                txtSu.Text = dr["SU"].ToString();
                txtDogalgaz.Text = dr["DOGALGAZ"].ToString();
                txtInternet.Text = dr["INTERNET"].ToString();
                txtMaas.Text = dr["MAASLAR"].ToString();
                txtEkstra.Text = dr["EKSTRA"].ToString();
                txtAciklama.Text = dr["NOTLAR"].ToString();
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            txtAy.Text = DateTime.Now.ToString("MMMM");
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            txtYil.Text = DateTime.Now.Year.ToString();
        }
    }
}
