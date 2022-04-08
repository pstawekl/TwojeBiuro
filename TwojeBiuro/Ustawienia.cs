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
        public string sqlServer = $@".\InteractiveSrv";
        public string sqlDatabase = $@"TesApp";
        public string sqlUser = $@"sa";
        public string sqlPasswd_ = "EL4505to";

        public SqlConnection iConn;
    }
}
