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
    public partial class frmFirmaListesi : DevExpress.XtraEditors.XtraForm
    {
        public frmFaturalar frm_Faturalar_firma; 
        public frmFirmaListesi()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl3 = new sqlbaglantisi();
        void listele_firmalar()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * from TBL_FIRMALAR ", bgl3.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
            this.gridView1.Columns[0].Width = 25;
            this.gridView1.Columns[0].Visible = false;
        }
        private void frmFirmaListesi_Load(object sender, EventArgs e)
        {
            try
            {
                listele_firmalar();
            }
            catch
            {
                XtraMessageBox.Show("Bağlantı problemlerinden dolayı işlem yapılamıyor.", "Bağlantı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                frm_Faturalar_firma.firma_id = dr["ID"].ToString();
                frm_Faturalar_firma.musteri_id = "-1";
                frm_Faturalar_firma.lbl_satis_text = dr["AD"].ToString();
                frm_Faturalar_firma.lbl_satis_doldur();
                this.Close();
            }
        }
    }
}
