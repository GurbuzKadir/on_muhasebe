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

namespace AccoOnMuhasebe
{
    public partial class frmMusteriListesi : DevExpress.XtraEditors.XtraForm
    {
        public frmFaturalar frm_Faturalar_Musteri;
        public frmMusteriListesi()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl2 = new sqlbaglantisi();
        void listele_musteriler()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * from TBL_MUSTERILER", bgl2.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
            this.gridView1.Columns[0].Width = 25;
            this.gridView1.Columns[0].Visible = false;
        }

        private void frmMusteriListesi_Load(object sender, EventArgs e)
        {
            listele_musteriler();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                frm_Faturalar_Musteri.firma_id = "-1";
                frm_Faturalar_Musteri.musteri_id = dr["ID"].ToString();
                frm_Faturalar_Musteri.lbl_satis_text = dr["AD"].ToString() + " " + dr["SOYAD"].ToString();
                frm_Faturalar_Musteri.lbl_satis_doldur();
                this.Close();
            }
        }
    }
}
