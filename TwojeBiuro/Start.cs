using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interactive;
using System.Xml.Serialization;

namespace TwojeBiuro
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class Start
    {
        public static interactiveSQL iSQl = new interactiveSQL();
        public static interactiveXML iXML = new interactiveXML();
        public static interactive iA = new interactive();
        public static Ustawienia oUstawienia = new Ustawienia();

        void RunApp()
        {
            string appPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            if(!File.Exists($@"{appPath}\config.xml"))
            { 
                //Zapisanie configu do pliku XML w folderze apki
                XmlSerializer xmlS = new XmlSerializer(typeof(Ustawienia));
                TextWriter txtW = new StreamWriter($@"{appPath}\config.xml");
                xmlS.Serialize(txtW, oUstawienia);
            }
            else
            {
                //Wczytanie configu z pliku XML z folderu apki
                using(var sr=new StreamReader($@"{appPath}\config.xml"))
                {
                    XmlSerializer xmlS = new XmlSerializer(typeof(Ustawienia));
                    oUstawienia = (Ustawienia)xmlS.Deserialize(sr);
                }
                loginWindow frm = new loginWindow();
                var app = new TwojeBiuro.App();
                app.Run(frm);

                ////połączenie do bazy danych
                //iSQl.CreateSQLConnection(oUstawienia.sqlServer, oUstawienia.sqlDatabase, oUstawienia.sqlUser, oUstawienia.sqlPasswd_, oUstawienia.iConn);
                //if(oUstawienia.iConn.State == System.Data.ConnectionState.Open)
                //{
                //    loginWindow frm = new loginWindow();
                //    var app = new TwojeBiuro.App();
                //    app.Run(frm);
                //}
            }

            
            

        }
    }
}
