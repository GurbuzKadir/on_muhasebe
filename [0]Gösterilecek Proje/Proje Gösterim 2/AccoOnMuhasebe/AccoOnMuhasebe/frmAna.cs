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
        frmFirmalar frm_Firmalar;
        frmPersonel frm_Personel;
        frmRehber frm_Rehber;

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

        private void btnFirmalar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (Application.OpenForms["frmFirmalar"] == null)
            {
                frm_Firmalar = new frmFirmalar();
                frm_Firmalar.MdiParent = this;
                frm_Firmalar.Show();
            }
        }

        private void btnPersoneller_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (Application.OpenForms["frmPersonel"] == null)
            {
                frm_Personel = new frmPersonel();
                frm_Personel.MdiParent = this;
                frm_Personel.Show();
            }
        }

        private void btnRehber_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (Application.OpenForms["frmRehber"] == null)
            {
                frm_Rehber = new frmRehber();
                frm_Rehber.MdiParent = this;
                frm_Rehber.Show();
            }
        }
    }
}
