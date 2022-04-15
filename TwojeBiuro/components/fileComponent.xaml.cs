using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Interactive;

namespace TwojeBiuro
{
    /// <summary>
    /// Logika interakcji dla klasy fileComponent.xaml
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        interactiveSQL iSql = new interactiveSQL();
        Ustawienia oUstawienia = new Ustawienia();
        string fName = "";
        string fPath = "";
        public UserControl1(string Name, string Path)
        {
            InitializeComponent();
            fName = Name;
            fPath = Path;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            fileName.Content = fName;
        }

        private void uploadFile_Click(object sender, RoutedEventArgs e)
        {
            oUstawienia.iConn = (SqlConnection)iSql.CreateSQLConnection(oUstawienia.sqlServer, oUstawienia.sqlDatabase, oUstawienia.sqlUser, oUstawienia.sqlPasswd_, oUstawienia.iConn);
            string filename = fPath;
            string contentType = "";
            using (Stream fs = new FileStream(fPath, FileMode.Open))
            {
                using (BinaryReader br = new BinaryReader(fs))
                {
                    byte[] bytes = br.ReadBytes((Int32)fs.Length);
                    using (oUstawienia.iConn)
                    {
                        string query = "insert into tblFiles values (@Name, @ContentType, @Data)";
                        using (SqlCommand cmd = new SqlCommand(query))
                        {
                            cmd.Connection = oUstawienia.iConn;
                            cmd.Parameters.AddWithValue("@Name", filename);
                            cmd.Parameters.AddWithValue("@ContentType", contentType);
                            cmd.Parameters.AddWithValue("@Data", bytes);
                            oUstawienia.iConn.Open();
                            cmd.ExecuteNonQuery();
                            oUstawienia.iConn.Close();
                        }
                    }
                }
            }
        }

        private void closeFile_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
