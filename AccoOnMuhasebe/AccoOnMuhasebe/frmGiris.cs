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
using System.Data.Sql;
using System.Data.SqlClient;
using System.Diagnostics;

namespace AccoOnMuhasebe
{
    public partial class frmGiris : DevExpress.XtraEditors.XtraForm
    {
        public frmGiris()
        {
            InitializeComponent();
        }
        int sayac = 0;
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (sayac%2==0)
            {
                simpleButton2.Image = Properties.Resources.unsee;
                txtSifre.Properties.PasswordChar = '\0';
                
            }
            else
            {
                simpleButton2.Image = Properties.Resources.see;
                txtSifre.Properties.PasswordChar = '•';
            }
            sayac++;
        }
        sqlbaglantisi bgl3 = new sqlbaglantisi();

        void server_kontrol()
        {
            try
            {
                bgl3.baglanti();
                bgl3.baglanti().Close();
                lblServerDurum.Text = "Bağlı";
                lblServerDurum.ForeColor = Color.Green;
            }
            catch
            {
                lblServerDurum.Text = "Bağlı değil";
                lblServerDurum.ForeColor = Color.Red;
            }

        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            server_kontrol();
        }

        private void frmGiris_Load(object sender, EventArgs e)
        {
            try
            {
                server_kontrol();
            }
            catch
            {
                XtraMessageBox.Show("Bağlantı problemlerinden dolayı işlem yapılamıyor.", "Bağlantı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //MessageBox.Show(bgl3.cumlecik.ToString());
            
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            DialogResult dg_iptal = XtraMessageBox.Show("İptal etmek istediğinizden emin misiniz?","İptal",MessageBoxButtons.YesNoCancel,MessageBoxIcon.Warning);
            if (dg_iptal==DialogResult.Yes)
            {
                Application.Exit();
            }
        }
        frmAna ana = new frmAna();
        void kontrol()
        {
            SqlCommand cmd_kontrol = new SqlCommand("Select * from TBL_UYELER where KULLANICIADI='" + txtKullaniciAd.Text.Trim().ToString() + "' and SIFRE='"+txtSifre.Text.Trim().ToString()+"'",bgl3.baglanti());
            SqlDataReader dr = cmd_kontrol.ExecuteReader();
            if (dr.Read())
            {
                ana.yetki = dr["TUR"].ToString();
                ana.giris_id = dr["ID"].ToString();
                XtraMessageBox.Show("Giriş başarılı oldu.\nHoşgeldiniz Sn. "+dr["AD"].ToString()+" "+dr["SOYAD"].ToString()+".\nAnasayfa'ya yönlendiriliyorsunuz.");
                ana.Show();
                this.Hide();
            }
            else
            {
                XtraMessageBox.Show("Kullanıcı adı veya parola hatalı.","Hatalı giriş",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            bgl3.baglanti().Close();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            kontrol();
        }

        private void labelControl5_Click(object sender, EventArgs e)
        {
            frmSifreUnuttum unuttum = new frmSifreUnuttum();
            unuttum.Show();
        }

        private void labelControl3_Click(object sender, EventArgs e)
        {
            frmKayitOl kayit = new frmKayitOl();
            kayit.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            Process.Start("" + Application.StartupPath + "\\acco_baglanti.txt");
        }
    }
}
