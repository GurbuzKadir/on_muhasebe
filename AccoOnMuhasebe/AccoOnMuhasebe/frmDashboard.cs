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
    public partial class frmDashboard : DevExpress.XtraEditors.XtraForm
    {
        public frmDashboard()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();

        void stoklar()
        {
            DataTable tbl_stoklar = new DataTable();
            SqlDataAdapter da_stoklar = new SqlDataAdapter("Select URUNAD,SUM(ADET) as 'ADET' from TBL_URUNLER group by URUNAD having SUM(ADET)<=20 order by SUM(ADET)",bgl.baglanti());
            da_stoklar.Fill(tbl_stoklar);
            gridControlStoklar.DataSource = tbl_stoklar;
            gridView1.Columns[1].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
        }
        void ajanda()
        {
            DataTable tbl_ajanda = new DataTable();
            SqlDataAdapter da_ajanda = new SqlDataAdapter("Select TARIH,SAAT,BASLIK from TBL_NOTLAR order by NOTID desc", bgl.baglanti());
            da_ajanda.Fill(tbl_ajanda);
            gridControlAjanda.DataSource = tbl_ajanda;
            gridView2.Columns[1].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            gridView2.Columns[0].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            gridView2.Columns[2].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
        }

        void rehberFirma()
        {
            DataTable tbl_rehber = new DataTable();
            SqlDataAdapter da_rehber = new SqlDataAdapter("Select AD,TELEFON1 from TBL_FIRMALAR", bgl.baglanti());
            da_rehber.Fill(tbl_rehber);
            gridControlRehber.DataSource = tbl_rehber;
        }
        void rehberMusteri()
        {
            DataTable tbl_rehber1 = new DataTable();
            SqlDataAdapter da_rehber1 = new SqlDataAdapter("select (AD + ' ' + SOYAD),TELEFON from TBL_MUSTERILER", bgl.baglanti());
            da_rehber1.Fill(tbl_rehber1);
            gridControlMusteriRehber.DataSource = tbl_rehber1;
        }

        void doviz()
        {
            try
            {
                browserDoviz.Navigate("https://www.tcmb.gov.tr/kurlar/today.xml");
            }
            catch
            {
                
            }
        }

        void musteriSayisi()
        {
            SqlCommand komut4 = new SqlCommand("Select Count(*) from TBL_MUSTERILER", bgl.baglanti());
            SqlDataReader dr4 = komut4.ExecuteReader();
            while (dr4.Read())
            {
                lblMusteriS.Text = dr4[0].ToString();
            }
            bgl.baglanti().Close();
        }
        void firmaSayisi()
        {
            SqlCommand komut5 = new SqlCommand("Select Count(*) from TBL_FIRMALAR", bgl.baglanti());
            SqlDataReader dr5 = komut5.ExecuteReader();
            while (dr5.Read())
            {
                lblFirmaS.Text = dr5[0].ToString();
            }
            bgl.baglanti().Close();
        }

        void personelSayisi()
        {
            SqlCommand komut8 = new SqlCommand("Select Count(*) from TBL_PERSONELLER", bgl.baglanti());
            SqlDataReader dr8 = komut8.ExecuteReader();
            while (dr8.Read())
            {
                lblPersonelS.Text = dr8[0].ToString();
            }
            bgl.baglanti().Close();
        }
        void stokSayisi()
        {
            SqlCommand komut9 = new SqlCommand("Select SUM(ADET) from TBL_URUNLER", bgl.baglanti());
            SqlDataReader dr9 = komut9.ExecuteReader();
            while (dr9.Read())
            {
                lblStokS.Text = dr9[0].ToString();
            }
            bgl.baglanti().Close();
        }

        void toplamTutarHesapla()
        {
            SqlCommand komut1 = new SqlCommand("Select SUM(TUTAR) from TBL_FATURADETAY", bgl.baglanti());
            SqlDataReader dr1 = komut1.ExecuteReader();
            while (dr1.Read())
            {
                lblToplamS.Text = dr1[0].ToString() + " TL";
            }
            bgl.baglanti().Close();
        }
        void sonAyFatura()
        {
            SqlCommand komut2 = new SqlCommand("Select (ELEKTRIK+SU+DOGALGAZ+INTERNET+EKSTRA) from TBL_GIDERLER order by ID asc", bgl.baglanti());
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                lblOdemelerS.Text = dr2[0].ToString() + " TL";
            }
            bgl.baglanti().Close();
        }

        void sonAyMaaslar()
        {
            SqlCommand komut3 = new SqlCommand("Select MAASLAR from TBL_GIDERLER order by ID asc", bgl.baglanti());
            SqlDataReader dr3 = komut3.ExecuteReader();
            while (dr3.Read())
            {
                lblMaaslarS.Text = dr3[0].ToString() + " TL";
            }
            bgl.baglanti().Close();
        }
        public string tip;
        private void frmDashboard_Load(object sender, EventArgs e)
        {
            try
            {
                stoklar();
                ajanda();
                rehberFirma();
                rehberMusteri();
                doviz();
                musteriSayisi();
                firmaSayisi();
                stokSayisi();
                personelSayisi();
                if (tip=="1")
                {
                    toplamTutarHesapla();
                    sonAyFatura();
                    sonAyMaaslar();
                }
            }
            catch
            {
                XtraMessageBox.Show("Bağlantı problemlerinden dolayı işlem yapılamıyor.","Bağlantı",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
    }
}
