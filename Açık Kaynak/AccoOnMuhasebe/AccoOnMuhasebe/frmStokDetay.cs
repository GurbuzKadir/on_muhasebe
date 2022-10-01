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
    public partial class frmStokDetay : DevExpress.XtraEditors.XtraForm
    {
        sqlbaglantisi bgl_dty = new sqlbaglantisi();
        public string isim;
        void listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * from TBL_URUNLER where URUNAD='"+isim.ToString()+"'", bgl_dty.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
            this.gridView1.Columns[0].Width = 25;
            this.gridView1.Columns[0].Visible = false;
            this.gridView1.Columns[5].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.gridView1.Columns[1].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
            this.gridView1.Columns[9].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Max;
        }

        public frmStokDetay()
        {
            InitializeComponent();
        }

        private void frmStokDetay_Load(object sender, EventArgs e)
        {
            try
            {
                listele();
            }
            catch
            {
                XtraMessageBox.Show("Veriler çekilirken bir hata oluştu.","Hata",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
    }
}
