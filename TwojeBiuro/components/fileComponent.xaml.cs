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
        interactiveOther iOther = new interactiveOther();
        Ustawienia oUstawienia = new Ustawienia();
        string fName = "";
        string fPath = "";
        int userId = 0;
        SqlCommand cmd;
        public UserControl1(string Name, string Path, int usrId)
        {
            InitializeComponent();
            fName = Name;
            fPath = Path;
            userId = usrId;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            fileName.Content = fName;
        }

        private void uploadFile_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                oUstawienia.iConn = (SqlConnection)iSql.CreateSQLConnection(oUstawienia.sqlServer, oUstawienia.sqlDatabase, oUstawienia.sqlUser, oUstawienia.sqlPasswd_, oUstawienia.iConn);
                string contentType = "";
                using (Stream fs = new FileStream(fPath, FileMode.Open))
                {
                    using (BinaryReader br = new BinaryReader(fs))
                    {
                        byte[] bytes = br.ReadBytes((Int32)fs.Length);
                        using (oUstawienia.iConn)
                        {
                            string query = "insert into tes_UsersFiles values (@fl_Name, @fl_Content, @fl_UserId, @fl_Data)";
                            using (cmd = new SqlCommand(query))
                            {
                                cmd.Connection = oUstawienia.iConn;
                                cmd.Parameters.AddWithValue("@fl_Name", fPath);
                                cmd.Parameters.AddWithValue("@fl_Content", contentType);
                                cmd.Parameters.AddWithValue(@"fl_UserId", userId);
                                cmd.Parameters.AddWithValue("@fl_Data", bytes);
                                oUstawienia.iConn.Open();
                                cmd.ExecuteNonQuery();
                                oUstawienia.iConn.Close();
                                fileProgress.Maximum = 100;
                                fileProgress.SmallStep = 1;
                                fileProgress.LargeStep = 5;
                                fileProgress.Value = 0;


                                for (int oneStep = bytes.Length / 100; oneStep == bytes.Length; oneStep += oneStep)
                                {
                                    if (fileProgress.Value < 100) { fileProgress.Value += 1; }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                iOther.SaveToLog($@"Błąd podczas wywołania procedury uploadFile_Click w fileComponent");
                throw new Exception($@"Wystąpił błąd podczas próby upload'u pliku na serwer. Treść błedu: {ex.Message} {Environment.NewLine} Skontaktuj się z administratorem systemu.");
            }
        }

        private void closeFile_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open) { cmd.Connection.Close(); }
                Window.GetWindow(this).Close();
            }
            catch (Exception ex)
            {
                iOther.SaveToLog($@"Błąd podczas wywołania procedury closeFile_Click w fileComponent");
                throw new Exception($@"Wystąpił błąd podczas próby wycofania pliku z upload'u do serwera. Treść błędu: {Environment.NewLine} {ex.Message} {Environment.NewLine} Skontaktuj się z administratorem systemu."); 
            }
        }
    }
}
