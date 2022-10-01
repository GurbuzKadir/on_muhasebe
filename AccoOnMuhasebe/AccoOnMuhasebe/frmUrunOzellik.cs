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
    public partial class frmUrunOzellik : DevExpress.XtraEditors.XtraForm
    {
        public frmUrunOzellik()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl3 = new sqlbaglantisi();

        void ozellik_listele()
        {
            SqlCommand cmd = new SqlCommand("Select * from TBL_URUNLER where ID=" + urun_ozellik_id + "", bgl3.baglanti());
            SqlDataReader dr = cmd.ExecuteReader();
            if(dr.Read())
            {
                lblAdet.Text = dr["ADET"].ToString();
                lblAlisFiyat.Text = dr["ALISFIYAT"].ToString();
                lblMarka.Text = dr["MARKA"].ToString();
                lblModel.Text = dr["MODEL"].ToString();
                lblSatisFiyat.Text = dr["SATISFIYAT"].ToString();
                lblTarih.Text = dr["TARIH"].ToString();
                lblUrunAd.Text = dr["URUNAD"].ToString();
                lblYil.Text = dr["YIL"].ToString();
                lblDetay.Text = dr["DETAY"].ToString();
                
            }
            else
            {
                lblAdet.Text = "--";
                lblAlisFiyat.Text = "--";
                lblMarka.Text = "--";
                lblModel.Text = "--";
                lblSatisFiyat.Text = "--";
                lblTarih.Text = "--";
                lblUrunAd.Text = "--";
                lblYil.Text = "--";
                lblDetay.Text = "--";
            }
            bgl3.baglanti().Close();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public string urun_ozellik_id="-1";
        private void frmUrunOzellik_Load(object sender, EventArgs e)
        {
            try
            {
                ozellik_listele();
            }
            catch
            {
                XtraMessageBox.Show("Bağlantı problemlerinden dolayı işlem yapılamıyor.", "Bağlantı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }
    }
}
