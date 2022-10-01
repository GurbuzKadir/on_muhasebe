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
    public partial class frmNotDetay : DevExpress.XtraEditors.XtraForm
    {
        public frmNotDetay()
        {
            InitializeComponent();
        }
        public string metin;
        private void frmNotDetay_Load(object sender, EventArgs e)
        {
            txtAciklama.Text = metin.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
