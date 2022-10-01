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
using System.Net;
using System.Net.Mail;

namespace AccoOnMuhasebe
{
    public partial class frmSifreUnuttum : DevExpress.XtraEditors.XtraForm
    {
        public frmSifreUnuttum()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();
        private void btnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("Select * from TBL_UYELER where EMAIL='" + txtKullaniciAd.Text.Trim().ToString() + "'", bgl.baglanti());
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                try
                {
                    MailMessage mesaj = new MailMessage();
                    SmtpClient istem = new SmtpClient();
                    istem.Credentials = new System.Net.NetworkCredential("accomuhasebe@gmail.com", "galata1230aa");
                    istem.Port = 587;
                    istem.Host = "smtp.gmail.com";
                    istem.EnableSsl = true;
                    mesaj.To.Add(dr["EMAIL"].ToString());
                    mesaj.From = new MailAddress("accomuhasebe@gmail.com");
                    mesaj.Subject = ".ACCO ÖN MUHASEBE ŞİFRENİZ";
                    mesaj.Body = "Kullanıcı adı : "+dr["KULLANICIADI"].ToString() + ", Şifre : " + dr["SIFRE"].ToString();
                    istem.Send(mesaj);
                    XtraMessageBox.Show("Email adresinize şifreniz gönderildi.");
                }
                catch (Exception)
                {
                    XtraMessageBox.Show("Mail gönderme işlemi sırasında bir hata meydana geldi.","Hata",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
            }
            else
            {
                XtraMessageBox.Show("Böyle bir kayıt bulunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            bgl.baglanti().Close();
        }
    }
}
