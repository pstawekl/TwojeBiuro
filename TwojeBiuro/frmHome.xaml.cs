using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using Interactive;


namespace TwojeBiuro
{
    /// <summary>
    /// Logika interakcji dla klasy frmHome.xaml
    /// </summary>
    /// 
    public partial class frmHome : Window
    {
        interactiveSQL iSql = new interactiveSQL();
        Ustawienia oUstawienia;

        public frmHome(Ustawienia Ustawienia)
        {
            oUstawienia = Ustawienia;
            InitializeComponent();
        }

        private void OnLoad(object sender, RoutedEventArgs e)
        {
           try
           {
               oUstawienia.iConn = (System.Data.SqlClient.SqlConnection)iSql.CreateSQLConnection(oUstawienia.sqlServer, oUstawienia.sqlDatabase, oUstawienia.sqlUser, oUstawienia.sqlPasswd_, oUstawienia.iConn);
               string startQuery = $@"select ns_Caption from tes_News where ns_Id = (select MAX(ns_ID) from tes_News)";
               lblNews.Text = (iSql.GetScalarString(startQuery, oUstawienia.iConn)).Replace("/n", "");
            }
            catch (Exception ex)
            {
                throw new Exception($@"Wystąpił błąd podczas próby uruchomienia programu: {Environment.NewLine} {ex.Message}");
            }
    
        }
    }

}
