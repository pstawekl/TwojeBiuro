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
using FontAwesome.WPF;

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
            btnDelete.Glyph = ImageAwesome.CreateImageSource(FontAwesomeIcon.Trash, new SolidColorBrush(Colors.White));
            btnEdit.Glyph = ImageAwesome.CreateImageSource(FontAwesomeIcon.Edit, new SolidColorBrush(Colors.White));
            btnAdd.Glyph = ImageAwesome.CreateImageSource(FontAwesomeIcon.File, new SolidColorBrush(Colors.White));
        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            if (pnlHome.Visibility == Visibility.Collapsed)
            {
                pnlHome.Visibility = Visibility.Visible;
                if(pnlDocs.Visibility == Visibility.Visible) { pnlDocs.Visibility = Visibility.Collapsed;}
            }
        }

        private void btnDocs_Click(object sender, RoutedEventArgs e)
        {
            if (pnlDocs.Visibility == Visibility.Collapsed)
            {
                pnlDocs.Visibility = Visibility.Visible;
                if(pnlHome.Visibility == Visibility.Visible) { pnlHome.Visibility = Visibility.Collapsed; }
            }
        }

        private void btnContact_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Closed(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
