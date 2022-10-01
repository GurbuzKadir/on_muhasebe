using DevExpress.XtraReports.UI;
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
    public partial class frmRaporlar : DevExpress.XtraEditors.XtraForm
    {
        public frmRaporlar()
        {
            InitializeComponent();
        }

        private void frmRaporlar_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'DboOnMuhasebeDataSet4.TBL_URUNLER' table. You can move, or remove it, as needed.
            this.TBL_URUNLERTableAdapter.Fill(this.DboOnMuhasebeDataSet4.TBL_URUNLER);
            // TODO: This line of code loads data into the 'DboOnMuhasebeDataSet3.TBL_PERSONELLER' table. You can move, or remove it, as needed.
            this.TBL_PERSONELLERTableAdapter.Fill(this.DboOnMuhasebeDataSet3.TBL_PERSONELLER);
            // TODO: This line of code loads data into the 'DboOnMuhasebeDataSet2.TBL_GIDERLER' table. You can move, or remove it, as needed.
            this.TBL_GIDERLERTableAdapter.Fill(this.DboOnMuhasebeDataSet2.TBL_GIDERLER);
            // TODO: This line of code loads data into the 'DboOnMuhasebeDataSet1.TBL_MUSTERILER' table. You can move, or remove it, as needed.
            this.TBL_MUSTERILERTableAdapter.Fill(this.DboOnMuhasebeDataSet1.TBL_MUSTERILER);
            // TODO: This line of code loads data into the 'DboOnMuhasebeDataSet.TBL_FIRMALAR' table. You can move, or remove it, as needed.
            this.TBL_FIRMALARTableAdapter.Fill(this.DboOnMuhasebeDataSet.TBL_FIRMALAR);

            this.reportViewer1.RefreshReport();
            this.reportViewer1.RefreshReport();
            this.reportViewer2.RefreshReport();
            this.reportViewer4.RefreshReport();
            this.reportViewer5.RefreshReport();
            this.reportViewer6.RefreshReport();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            XtraReport1 rp1 = new XtraReport1();
            ReportPrintTool print = new ReportPrintTool(rp1);
            print.ShowPreview();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            XtraReport2 rp2 = new XtraReport2();
            ReportPrintTool print = new ReportPrintTool(rp2);
            print.ShowPreview();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            XtraReport3 rp3 = new XtraReport3();
            ReportPrintTool print = new ReportPrintTool(rp3);
            print.ShowPreview();
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            XtraReport4 rp4 = new XtraReport4();
            ReportPrintTool print = new ReportPrintTool(rp4);
            print.ShowPreview();
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            XtraReport5 rp5 = new XtraReport5();
            ReportPrintTool print = new ReportPrintTool(rp5);
            print.ShowPreview();
        }
    }
}
