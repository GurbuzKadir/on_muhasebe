using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;
using System.Data.SqlClient;
using System.IO;

namespace AccoOnMuhasebe
{
    class sqlbaglantisi
    {
        public string cumlecik;
        public sqlbaglantisi()
        {
            StreamReader oku = new StreamReader("" + AppDomain.CurrentDomain.BaseDirectory+ "\\acco_baglanti.txt");
            string satir = oku.ReadLine();
            while (satir!=null)
            {
                cumlecik = satir;
                satir = oku.ReadLine();
            }
        }
        public SqlConnection baglanti()
        {
            SqlConnection baglan = new SqlConnection(@"" + cumlecik.ToString() + "");
            //SqlConnection baglan = new SqlConnection(@"Data Source=KADIR;Initial Catalog=DboOnMuhasebe;Integrated Security=True");
            baglan.Open();
            return baglan;
        }
    }
}
