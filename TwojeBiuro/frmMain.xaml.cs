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
    /// Logika interakcji dla klasy Window1.xaml
    /// </summary>
    public partial class frmMain : Window
    {
        interactiveSQL iSql = new interactiveSQL();
        Ustawienia oUstawienia = new Ustawienia();
        public frmMain()
        {
            InitializeComponent();
        }

        void OnLoad(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            frmHome frm = new frmHome(oUstawienia);
            frm.Show();
        }
    }
}
