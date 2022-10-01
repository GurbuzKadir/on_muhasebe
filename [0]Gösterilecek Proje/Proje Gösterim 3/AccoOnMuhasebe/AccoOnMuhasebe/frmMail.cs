using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;

namespace AccoOnMuhasebe
{
    public partial class frmMail : DevExpress.XtraEditors.XtraForm
    {
        public frmMail()
        {
            InitializeComponent();
        }
        public string mail;
        private void frmMail_Load(object sender, EventArgs e)
        {
            txtMail.Text = mail;
        }
        string DosyaYolu = "";
        string DosyaUzanti = "";
        private void btnGonder_Click(object sender, EventArgs e)
        {
            if (txtBaslik.Text.Trim().ToString()!=null&&txtMesaj.Text.Trim().ToString()!=null)
            {
                try
                {
                    if (DosyaYolu=="")
                    {
                        MailMessage mesaj = new MailMessage();
                        SmtpClient istem = new SmtpClient();
                        istem.Credentials = new System.Net.NetworkCredential("accomuhasebe@gmail.com", "acco123456");
                        istem.Port = 587;
                        istem.Host = "smtp.gmail.com";
                        istem.EnableSsl = true;
                        mesaj.To.Add(txtMail.Text.Trim().ToString());
                        mesaj.From = new MailAddress("accomuhasebe@gmail.com");
                        mesaj.Subject = txtBaslik.Text.Trim().ToString();
                        mesaj.Body = txtMesaj.Text.Trim().ToString();
                        istem.Send(mesaj);
                        XtraMessageBox.Show("Mail başarıyla gönderildi.", "Başarılı");
                    }
                    else
                    {
                        MailMessage mesaj = new MailMessage();
                        SmtpClient istem = new SmtpClient();
                        istem.Credentials = new System.Net.NetworkCredential("accomuhasebe@gmail.com", "acco123456");
                        istem.Port = 587;
                        istem.Host = "smtp.gmail.com";
                        istem.EnableSsl = true;
                        mesaj.To.Add(txtMail.Text.Trim().ToString());
                        mesaj.From = new MailAddress("accomuhasebe@gmail.com");
                        mesaj.Subject = txtBaslik.Text.Trim().ToString();
                        mesaj.Body = txtMesaj.Text.Trim().ToString();
                        mesaj.Attachments.Add(new Attachment(@"" + DosyaYolu.ToString() + ""));
                        istem.Send(mesaj);
                        XtraMessageBox.Show("Mail başarıyla gönderildi.", "Başarılı");
                    }
                }
                catch
                {
                    XtraMessageBox.Show("Bir hata oluştu.\nLütfen internet bağlantınızı kontrol edip tekrar deneyiniz.\nDosya uzantılarına dikkat ediniz.","Hata",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
            }
            else
            {
                XtraMessageBox.Show("Boş alan bırakılamaz","Boş alan",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            
            OpenFileDialog file = new OpenFileDialog();
            if (file.ShowDialog()==DialogResult.OK)
            {
                DosyaYolu = file.FileName;
                txtYol.Text = DosyaYolu.ToString();
                System.IO.FileInfo ff = new System.IO.FileInfo(file.FileName);
                DosyaUzanti = ff.Extension;    
            }
            //MessageBox.Show(DosyaUzanti.ToString());
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            //txt_clear ve durum=""
            txtYol.Text = "";
            DosyaYolu = "";
            DosyaUzanti = "";
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            frmMailInfo uyari = new frmMailInfo();
            uyari.ShowDialog();
        }
    }
}
