using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
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
        frmDiagram frm_Diagram;
        frmDashboard frm_Dashboard;
        frmGiderler frm_Giderler;
        frmBankalar frm_Bankalar;
        frmFaturalar frm_Faturalar;
        frmNotlar frm_Notlar;
        frmRaporlar frm_Raporlar;
        frmStoklar frm_Stoklar;
        frmKasa frm_Kasa; 

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

        private void btnDiagram_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (Application.OpenForms["frmDiagram"] == null)
            {
                frm_Diagram = new frmDiagram();
                frm_Diagram.Show();
            }

        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (Application.OpenForms["frmDashboard"] == null)
            {
                frm_Dashboard = new frmDashboard();
                frm_Dashboard.MdiParent = this;
                frm_Dashboard.Show();
            }
        }

        private void btnGiderler_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (Application.OpenForms["frmGiderler"] == null)
            {
                frm_Giderler = new frmGiderler();
                frm_Giderler.MdiParent = this;
                frm_Giderler.Show();
            }
        }

        private void btnBankalar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (Application.OpenForms["frmBankalar"] == null)
            {
                frm_Bankalar = new frmBankalar();
                frm_Bankalar.MdiParent = this;
                frm_Bankalar.Show();
            }
        }

        private void btnFaturalar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (Application.OpenForms["frmFaturalar"] == null)
            {
                frm_Faturalar = new frmFaturalar();
                frm_Faturalar.MdiParent = this;
                frm_Faturalar.Show();
            }
        }

        private void btnNotlar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (Application.OpenForms["frmNotlar"] == null)
            {
                frm_Notlar = new frmNotlar();
                frm_Notlar.MdiParent = this;
                frm_Notlar.Show();
            }
        }

        private void btnRaporlar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (Application.OpenForms["frmRaporlar"] == null)
            {
                frm_Raporlar = new frmRaporlar();
                frm_Raporlar.MdiParent = this;
                frm_Raporlar.Show();
            }
        }

        private void btnStoklar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (Application.OpenForms["frmStoklar"] == null)
            {
                frm_Stoklar = new frmStoklar();
                frm_Stoklar.MdiParent = this;
                frm_Stoklar.Show();
            }
        }

        private void btnKasa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (Application.OpenForms["frmKasa"] == null)
            {
                frm_Kasa = new frmKasa();
                frm_Kasa.MdiParent = this;
                frm_Kasa.Show();
            }
        }
        public string yetki,giris_id;//Burası değiştirilecektir.Buraya giriş yaptıktan sonraki id verisi çekilecek
        private void frmAna_Load(object sender, EventArgs e)
        {
            barButtonItem2.PerformClick();
            if (yetki=="2")
            {
                btnPersoneller.Enabled = false;
                btnKasa.Enabled = false;
                btnGiderler.Enabled = false;
                btnRaporlar.Enabled = false;
            }
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DialogResult dg = XtraMessageBox.Show("Oturumu kapatmak istediğinizden emin misiniz?","Oturum kapat",MessageBoxButtons.YesNoCancel,MessageBoxIcon.Warning);
            if (dg==DialogResult.Yes)
            {
                Application.Restart();
            }
        }

        private void frmAna_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void btnAyarlar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmAyarlar ayar;
            if (Application.OpenForms["frmAyarlar"] == null)
            {
                ayar = new frmAyarlar();
                ayar.gelen_id = giris_id.ToString();
                ayar.tur = yetki.ToString();
                ayar.Show();
            }
        }
    }
}
