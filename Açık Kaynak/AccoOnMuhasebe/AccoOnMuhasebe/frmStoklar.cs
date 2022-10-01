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
    public partial class frmStoklar : DevExpress.XtraEditors.XtraForm
    {
        public frmStoklar()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();

        void listele_stoklar()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select URUNAD,SUM(ADET) as 'MIKTAR' from TBL_URUNLER Group by URUNAD", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
            gridView1.Columns[1].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
        }
        void listele_chart_miktar()
        {
            SqlCommand cmd = new SqlCommand("Select URUNAD,SUM(ADET) as 'MIKTAR' from TBL_URUNLER Group by URUNAD", bgl.baglanti());
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                chartControl1.Series["Series 1"].Points.AddPoint(Convert.ToString(dr[0]),int.Parse(dr[1].ToString()));
            }
            bgl.baglanti().Close();
        }

        void listele_chart_sehir()
        {
            SqlCommand cmd = new SqlCommand("Select IL,Count(*) as 'ADET' from TBL_FIRMALAR Group by IL", bgl.baglanti());
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                chartControl2.Series["Series 1"].Points.AddPoint(Convert.ToString(dr[0]), int.Parse(dr[1].ToString()));
            }
            bgl.baglanti().Close();
        }

        private void frmStoklar_Load(object sender, EventArgs e)
        {
            listele_stoklar();
            listele_chart_miktar();
            listele_chart_sehir();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            frmStokDetay stokdetay = new frmStokDetay();
            DataRow dr_detay = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr_detay!=null)
            {
                stokdetay.isim = dr_detay["URUNAD"].ToString();
            }
            stokdetay.Show();
        }
    }
}
