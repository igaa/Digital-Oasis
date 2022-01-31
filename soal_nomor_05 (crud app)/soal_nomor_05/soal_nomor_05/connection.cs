using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;

namespace soal_nomor_05
{
    public class connection
    {
        public string connection_str()
        {
            string koneksi = ConfigurationManager.ConnectionStrings["soal_nomor_05.Properties.Settings.Connector"].ConnectionString; 

            return koneksi; 
        }
    }
}
