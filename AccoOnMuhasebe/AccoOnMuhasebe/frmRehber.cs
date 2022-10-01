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
    public partial class frmRehber : DevExpress.XtraEditors.XtraForm
    {
        public frmRehber()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();
        private void frmRehber_Load(object sender, EventArgs e)
        {
            try
            {

                //Müşteri
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter("Select AD,SOYAD,TELEFON,TELEFON2,MAIL from TBL_MUSTERILER",bgl.baglanti());
                da.Fill(dt);
                gridControl1.DataSource = dt;

                //Firma
                DataTable dt2 = new DataTable();
                SqlDataAdapter da2 = new SqlDataAdapter("Select AD,YETKILIADSOYAD,TELEFON1,TELEFON2,TELEFON3,MAIL,FAX from TBL_FIRMALAR", bgl.baglanti());
                da2.Fill(dt2);
                gridControl2.DataSource = dt2;
            }
            catch
            {
                XtraMessageBox.Show("Bağlantı problemlerinden dolayı işlem yapılamıyor.", "Bağlantı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            frmMail form_mail = new frmMail();
            string mail_kontrol = "";
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr!=null)
            {
                form_mail.mail = dr["MAIL"].ToString();
                mail_kontrol = dr["MAIL"].ToString();
            }
            if (mail_kontrol!="")
            {
                form_mail.Show();
            }
            else
            {
                XtraMessageBox.Show("Seçili müşteriye mail göndermeniz için mail adresi bulunmalıdır.\nLütfen ilgili kayda gidip tekrar deneyiniz.","Mail yok",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void gridView2_DoubleClick(object sender, EventArgs e)
        {
            frmMail form_mail = new frmMail();
            string mail_kontrol = "";
            DataRow dr = gridView2.GetDataRow(gridView2.FocusedRowHandle);
            if (dr != null)
            {
                form_mail.mail = dr["MAIL"].ToString();
                mail_kontrol = dr["MAIL"].ToString();
            }
            if (mail_kontrol != "")
            {
                form_mail.Show();
            }
            else
            {
                XtraMessageBox.Show("Seçili firmaya mail göndermeniz için mail adresi bulunmalıdır.\nLütfen ilgili kayda gidip tekrar deneyiniz.", "Mail yok", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
