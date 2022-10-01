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
    public partial class frmUrunlerGrup : DevExpress.XtraEditors.XtraForm
    {
        public frmUrunlerGrup()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl3 = new sqlbaglantisi();
        void listele_urun_grup()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select URUNAD,SUM(ADET) AS TOPLAMADET from TBL_URUNLER Group by URUNAD", bgl3.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
            gridView1.Columns[1].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
        }
        void listele_urun_grup1()
        {
            DataTable dt1 = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter("Select MARKA,SUM(ADET) AS TOPLAMADET from TBL_URUNLER Group by MARKA", bgl3.baglanti());
            da1.Fill(dt1);
            gridMarkaAdet.DataSource = dt1;
            gridView2.Columns[1].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
        }
        void listele_urun_grup2()
        {
            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter("Select YIL,SUM(ADET) AS TOPLAMADET from TBL_URUNLER Group by YIL", bgl3.baglanti());
            da2.Fill(dt2);
            gridYilAdet.DataSource = dt2;
            gridView3.Columns[1].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
        }
        void listele_urun_grup3()
        {
            DataTable dt3 = new DataTable();
            SqlDataAdapter da3 = new SqlDataAdapter("Select TARIH,SUM(ADET) AS TOPLAMADET from TBL_URUNLER Group by TARIH", bgl3.baglanti());
            da3.Fill(dt3);
            gridTarihAdet.DataSource = dt3;
            gridView4.Columns[1].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
        }
        private void frmUrunlerGrup_Load(object sender, EventArgs e)
        {
            try
            {
                listele_urun_grup();
                listele_urun_grup1();
                listele_urun_grup2();
                listele_urun_grup3();
            }
            catch
            {
                XtraMessageBox.Show("Bağlantı problemlerinden dolayı işlem yapılamıyor.", "Bağlantı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }
    }
}
