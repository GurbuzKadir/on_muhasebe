using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AccoOnMuhasebe
{
    public partial class frmAna : DevExpress.XtraEditors.XtraForm
    {
        public frmAna()
        {
            InitializeComponent();
        }
        frmUrunler frm_Urunler;
        frmMusteriler frm_Musteriler;
        private void btnUrunler_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (Application.OpenForms["frmUrunler"]==null)
            {
                frm_Urunler = new frmUrunler();
                frm_Urunler.MdiParent = this;
                frm_Urunler.Show();
            }
        }

        private void btnMusteriler_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (Application.OpenForms["frmMusteriler"] == null)
            {
                frm_Musteriler = new frmMusteriler();
                frm_Musteriler.MdiParent = this;
                frm_Musteriler.Show();
            }
        }
    }
}
