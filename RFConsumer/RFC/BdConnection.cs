using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFC
{
    class BdConnection
    {
        SqlConnection conn;

        public BdConnection() {

            conn = new SqlConnection("Data Source=185.144.157.97;" +
                "Initial Catalog=Rex;" +
                "User Id=omarch1409;Password=1409Chumioque;" +
                "connect timeout=2000;");

        }

        public void AgregarMovimientos() {

        } 

    }
}
