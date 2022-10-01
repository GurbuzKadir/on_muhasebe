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
using DevExpress.Charts;

namespace AccoOnMuhasebe
{
    public partial class frmKasa : DevExpress.XtraEditors.XtraForm
    {
        public frmKasa()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl3 = new sqlbaglantisi();

        void kasagirisleri()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * from TBL_FATURADETAY", bgl3.baglanti());
            da.Fill(dt);
            gridControl3.DataSource = dt;
            this.gridView3.Columns[0].Visible = false;
            this.gridView3.Columns[1].Visible = false;
            this.gridView3.Columns[6].Visible = false;
            this.gridView3.Columns[5].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.gridView3.Columns[2].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
            this.gridView3.Columns[3].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.gridView3.Columns[4].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
        }

        void toplamTutarHesapla()
        {
            SqlCommand komut1 = new SqlCommand("Select SUM(TUTAR) from TBL_FATURADETAY",bgl3.baglanti());
            SqlDataReader dr1 = komut1.ExecuteReader();
            while (dr1.Read())
            {
                lblKasaToplam.Text = dr1[0].ToString()+" TL";
            }
            bgl3.baglanti().Close();
        }
        void sonAyFatura()
        {
            SqlCommand komut2 = new SqlCommand("Select (ELEKTRIK+SU+DOGALGAZ+INTERNET+EKSTRA) from TBL_GIDERLER order by ID asc", bgl3.baglanti());
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                lblOdemeler.Text = dr2[0].ToString() + " TL";
            }
            bgl3.baglanti().Close();
        }
        void sonAyMaaslar()
        {
            SqlCommand komut3 = new SqlCommand("Select MAASLAR from TBL_GIDERLER order by ID asc", bgl3.baglanti());
            SqlDataReader dr3 = komut3.ExecuteReader();
            while (dr3.Read())
            {
                lblPersonelMaas.Text = dr3[0].ToString() + " TL";
            }
            bgl3.baglanti().Close();
        }
        void musteriSayisi()
        {
            SqlCommand komut4 = new SqlCommand("Select Count(*) from TBL_MUSTERILER", bgl3.baglanti());
            SqlDataReader dr4 = komut4.ExecuteReader();
            while (dr4.Read())
            {
                lblMusteriSayisi.Text = dr4[0].ToString();
            }
            bgl3.baglanti().Close();
        }
        void firmaSayisi()
        {
            SqlCommand komut5 = new SqlCommand("Select Count(*) from TBL_FIRMALAR", bgl3.baglanti());
            SqlDataReader dr5 = komut5.ExecuteReader();
            while (dr5.Read())
            {
                lblFirmaSayisi.Text = dr5[0].ToString();
            }
            bgl3.baglanti().Close();
        }
        void sehirSayisi()
        {
            string yazi = "";
            SqlCommand komut6 = new SqlCommand("Select Count(Distinct(IL)) from TBL_FIRMALAR", bgl3.baglanti());
            SqlDataReader dr6 = komut6.ExecuteReader();
            while (dr6.Read())
            {
                yazi += dr6[0].ToString() + " - ";
            }
            bgl3.baglanti().Close();

            SqlCommand komut7 = new SqlCommand("Select Count(Distinct(IL)) from TBL_MUSTERILER", bgl3.baglanti());
            SqlDataReader dr7 = komut7.ExecuteReader();
            while (dr7.Read())
            {
                yazi += dr7[0].ToString();
            }
            bgl3.baglanti().Close();
            lblSehirSayisi.Text = yazi.ToString();
        }
        void personelSayisi()
        {
            SqlCommand komut8 = new SqlCommand("Select Count(*) from TBL_PERSONELLER", bgl3.baglanti());
            SqlDataReader dr8 = komut8.ExecuteReader();
            while (dr8.Read())
            {
                lblPersonelSayisi.Text = dr8[0].ToString();
            }
            bgl3.baglanti().Close();
        }
        void stokSayisi()
        {
            SqlCommand komut9 = new SqlCommand("Select SUM(ADET) from TBL_URUNLER", bgl3.baglanti());
            SqlDataReader dr9 = komut9.ExecuteReader();
            while (dr9.Read())
            {
                lblStokSayisi.Text = dr9[0].ToString();
            }
            bgl3.baglanti().Close();
        }
        void listeleKasaCikis()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * from TBL_GIDERLER ", bgl3.baglanti());
            da.Fill(dt);
            gridControl2.DataSource = dt;
            gridView2.Columns[0].Visible = false;
            this.gridView2.Columns[1].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
            this.gridView2.Columns[3].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.gridView2.Columns[4].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.gridView2.Columns[5].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.gridView2.Columns[6].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.gridView2.Columns[7].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.gridView2.Columns[8].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;

        }
        private void frmKasa_Load(object sender, EventArgs e)
        {
            //musterihareket();
            //firmahareket();
            toplamTutarHesapla();
            sonAyFatura();
            sonAyMaaslar();
            musteriSayisi();
            firmaSayisi();
            sehirSayisi();
            personelSayisi();
            stokSayisi();
            listeleKasaCikis();
            kasagirisleri();
        }

        void chartElektrik() 
        {
            lblChart1.Text = "ELEKTRİK";
            chartControl1.Series["AYLAR"].Points.Clear();
            SqlCommand komutc1 = new SqlCommand("Select top 5 AY,ELEKTRIK from TBL_GIDERLER order by ID desc", bgl3.baglanti());
            SqlDataReader drc1 = komutc1.ExecuteReader();
            while (drc1.Read())
            {
                chartControl1.Series["AYLAR"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(drc1[0],drc1[1]));
            }
            bgl3.baglanti().Close();
        }
        void chartSu()
        {
            lblChart1.Text = "SU";
            chartControl1.Series["AYLAR"].Points.Clear();
            SqlCommand komutc2 = new SqlCommand("Select top 5 AY,SU from TBL_GIDERLER order by ID desc", bgl3.baglanti());
            SqlDataReader drc2 = komutc2.ExecuteReader();
            while (drc2.Read())
            {
                chartControl1.Series["AYLAR"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(drc2[0], drc2[1]));
            }
            bgl3.baglanti().Close();
        }
        void chartDogalgaz()
        {
            lblChart1.Text = "DOĞALGAZ";
            chartControl1.Series["AYLAR"].Points.Clear();
            SqlCommand komutc3 = new SqlCommand("Select top 5 AY,DOGALGAZ from TBL_GIDERLER order by ID desc", bgl3.baglanti());
            SqlDataReader drc3 = komutc3.ExecuteReader();
            while (drc3.Read())
            {
                chartControl1.Series["AYLAR"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(drc3[0], drc3[1]));
            }
            bgl3.baglanti().Close();
        }
        void chartInternet()
        {
            lblChart1.Text = "İNTERNET";
            chartControl1.Series["AYLAR"].Points.Clear();
            SqlCommand komutc4 = new SqlCommand("Select top 5 AY,INTERNET from TBL_GIDERLER order by ID desc", bgl3.baglanti());
            SqlDataReader drc4 = komutc4.ExecuteReader();
            while (drc4.Read())
            {
                chartControl1.Series["AYLAR"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(drc4[0], drc4[1]));
            }
            bgl3.baglanti().Close();
        }
        void chartEkstra()
        {
            lblChart1.Text = "EKSTRA";
            chartControl1.Series["AYLAR"].Points.Clear();
            SqlCommand komutc5 = new SqlCommand("Select top 5 AY,EKSTRA from TBL_GIDERLER order by ID desc", bgl3.baglanti());
            SqlDataReader drc5 = komutc5.ExecuteReader();
            while (drc5.Read())
            {
                chartControl1.Series["AYLAR"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(drc5[0], drc5[1]));
            }
            bgl3.baglanti().Close();
        }
        int sayac = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                sayac++;
                if (sayac > 0 && sayac <= 5)
                {
                    chartElektrik();
                }
                if (sayac > 5 && sayac <= 10)
                {
                    chartSu();
                }
                if (sayac > 10 && sayac <= 15)
                {
                    chartDogalgaz();
                }
                if (sayac > 15 && sayac <= 20)
                {
                    chartInternet();
                }
                if (sayac > 20 && sayac <= 25)
                {
                    chartEkstra();
                }
                if (sayac == 26)
                {
                    sayac = 0;
                }
            }
            catch
            {
                
            }
        }

        private void frmKasa_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer1.Stop();
            timer1.Enabled = false;
        }
    }
}
