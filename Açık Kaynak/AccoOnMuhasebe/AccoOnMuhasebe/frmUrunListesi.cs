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
    public partial class frmUrunListesi : Form
    {
        public frmFaturaUrunDetay frm_Fatura_Detay;
        public frmUrunListesi()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();
        void listele_Urunler()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * from TBL_URUNLER ", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
            this.gridView1.Columns[0].Width = 25;
            this.gridView1.Columns[0].Visible = false;
            this.gridView1.Columns[5].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.gridView1.Columns[1].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
            this.gridView1.Columns[9].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Max;
        }

        private void frmUrunListesi_Load(object sender, EventArgs e)
        {
            listele_Urunler();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                frm_Fatura_Detay.urun_id = dr["ID"].ToString();
                frm_Fatura_Detay.urun_ad = dr["URUNAD"].ToString();
                frm_Fatura_Detay.urun_fiyat = dr["SATISFIYAT"].ToString();
                frm_Fatura_Detay.verileri_cek();
                this.Close();
            }
        }
    }
}
