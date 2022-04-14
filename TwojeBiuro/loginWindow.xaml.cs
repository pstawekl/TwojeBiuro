using Interactive;
using System;
using System.Collections.Generic;
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
using System.Xml.Serialization;

namespace TwojeBiuro
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        interactiveSQL iSql = new interactiveSQL();
        interactiveOther iOther = new interactiveOther();
        Ustawienia oUstawienia = new Ustawienia();
        public LoginWindow()
        {
            #region trash
            //string appPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            //if (!File.Exists($@"{appPath}\config.xml"))
            //{
            //    //Zapisanie configu do pliku XML w folderze apki
            //    XmlSerializer xmlS = new XmlSerializer(typeof(Ustawienia));
            //    TextWriter txtW = new StreamWriter($@"{appPath}\config.xml");
            //    xmlS.Serialize(txtW, oUstawienia);
            //}
            //else
            //{
            ////Wczytanie configu z pliku XML z folderu apki
            //using (var sr = new StreamReader($@"{appPath}\config.xml"))
            //{
            //    XmlSerializer xmlS = new XmlSerializer(typeof(Ustawienia));
            //    oUstawienia = (Ustawienia)xmlS.Deserialize(sr);
            //}
            #endregion

            //połączenie do bazy danych
            oUstawienia.iConn = (System.Data.SqlClient.SqlConnection)iSql.CreateSQLConnection(oUstawienia.sqlServer, oUstawienia.sqlDatabase, oUstawienia.sqlUser, oUstawienia.sqlPasswd_, oUstawienia.iConn);
            
            if (oUstawienia.iConn.State == System.Data.ConnectionState.Closed)
            {
            MessageBox.Show($@"Wystąpił błąd podczas próby uruchomienia programu. 
            {Environment.NewLine} Powód: Brak połączenia z bazą danych. 
            {Environment.NewLine} Spróbuj ponownie uruchomić program lub skontaktuj się z jego twórcą za pomocą maila: jakub.stawski@interactive.net.pl");
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtUser.Focus();
        }

        #region ButtonsEvents
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LogIntoApp();

        }

        private void btnCloseMessage_Click(object sender, RoutedEventArgs e)
        {
            pnlMessage.Visibility = Visibility.Hidden;
        }
        #endregion

        #region KeyDowns
        private void txtUser_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                LogIntoApp();
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                LogIntoApp();
            }
        }
        #endregion

        #region Placeholders
        private void txtUser_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtUser.Text.Length == 0)
            {
                txtUser.Text = "User";
            }
        }

        private void txtPassword_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtPassword.Password.Length == 0)
            {
                txtPassword.Password = "Password";
            }
        }

        private void txtUser_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtUser.Text == "User")
            {
                txtUser.Text = "";
            }

            if (txtUser.BorderBrush.ToString() == "#FFFF0000")
            {
                txtUser.BorderBrush = new SolidColorBrush(Colors.White);
            }
        }

        private void txtPassword_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtPassword.Password == "Password")
            {
                txtPassword.Password = "";
            }
            if (txtPassword.BorderBrush.ToString() == "#FFFF0000")
            {
                txtPassword.BorderBrush = new SolidColorBrush(Colors.White);
            }
        }
        #endregion

        #region scripts
        void LogIntoApp()
        {
            if (txtPassword.Password != "Password" & txtUser.Text != "User" & txtPassword.Password.Length > 0 & txtUser.Text.Length > 0)
            {
                if (oUstawienia.iConn == null)
                {
                    oUstawienia.iConn = (System.Data.SqlClient.SqlConnection)iSql.CreateSQLConnection(oUstawienia.sqlServer, oUstawienia.sqlDatabase, oUstawienia.sqlUser, oUstawienia.sqlPasswd_, oUstawienia.iConn);
                }
                int czyUserIstnieje = iSql.GetScalarInt($@"select count(*) from tes_Users where us_Login = '{txtUser.Text}' and us_Password = '{txtPassword.Password}'", oUstawienia.iConn);
                if (czyUserIstnieje == 1)
                {
                    Window frmMain = new frmMain();
                    this.Close();
                    frmMain.ShowDialog();
                }
                else
                {
                    pnlMessage.Visibility = Visibility.Visible;
                    txtUser.Text = "User";
                    txtPassword.Password = "Password";
                }
            }
            else
            {
                if (txtUser.Text == "User")
                {
                    txtUser.BorderBrush = new SolidColorBrush(Colors.Red);
                }
                if (txtPassword.Password == "Password")
                {
                    txtPassword.BorderBrush = new SolidColorBrush(Colors.Red);
                }
            }
        }
        #endregion
    }
}
