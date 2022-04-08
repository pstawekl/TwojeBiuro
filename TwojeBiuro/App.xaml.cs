using Interactive;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;

namespace TwojeBiuro
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public static class Program
    {
        public static interactiveSQL iSQl = new interactiveSQL();
        public static interactiveXML iXML = new interactiveXML();
        public static interactive iA = new interactive();
        public static Ustawienia oUstawienia = new Ustawienia();

        [STAThread]
        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        public static void Main(string[] args)
        {
            static void RunApp()
            {
                string appPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
                if (!File.Exists($@"{appPath}\config.xml"))
                {
                    //Zapisanie configu do pliku XML w folderze apki
                    XmlSerializer xmlS = new XmlSerializer(typeof(Ustawienia));
                    TextWriter txtW = new StreamWriter($@"{appPath}\config.xml");
                    xmlS.Serialize(txtW, oUstawienia);
                }
                else
                {
                    //Wczytanie configu z pliku XML z folderu apki
                    using (var sr = new StreamReader($@"{appPath}\config.xml"))
                    {
                        XmlSerializer xmlS = new XmlSerializer(typeof(Ustawienia));
                        oUstawienia = (Ustawienia)xmlS.Deserialize(sr);
                    }
                    

                    //połączenie do bazy danych
                    iSQl.CreateSQLConnection(oUstawienia.sqlServer, oUstawienia.sqlDatabase, oUstawienia.sqlUser, oUstawienia.sqlPasswd_, oUstawienia.iConn);
                    if (oUstawienia.iConn.State == System.Data.ConnectionState.Open)
                    {
                        loginWindow frm = new loginWindow();
                        var app = new TwojeBiuro.App();
                        app.InitializeComponent();
                        app.Run(frm);
                    }
                }




            }
        }
    }
} 
