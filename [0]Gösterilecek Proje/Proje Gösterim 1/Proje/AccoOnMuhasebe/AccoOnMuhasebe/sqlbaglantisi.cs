using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;
using System.Data.SqlClient;
namespace AccoOnMuhasebe
{
    class sqlbaglantisi
    {
        public SqlConnection baglanti()
        {
            SqlConnection baglan = new SqlConnection(@"Data Source=KADIR;Initial Catalog=DboOnMuhasebe;Integrated Security=True");
            baglan.Open();
            return baglan;
        }
    }
}
