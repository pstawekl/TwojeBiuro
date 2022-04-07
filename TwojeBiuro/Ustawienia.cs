using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace TwojeBiuro
{
    public class Ustawienia
    {
        public string sqlServer = $@".\SQLEXPRESS";
        public string sqlDatabase = $@"tesApp";
        public string sqlUser = $@"sa";
        public string sqlPasswd_ = "1234";

        public SqlConnection iConn;
    }
}
