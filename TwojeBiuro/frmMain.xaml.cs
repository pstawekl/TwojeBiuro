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
using System.IO;
using Microsoft.Win32;

namespace TwojeBiuro
{
    /// <summary>
    /// Logika interakcji dla klasy Window1.xaml
    /// </summary>
    public partial class frmMain : Window
    {
        interactiveSQL iSql = new interactiveSQL();
        interactiveOther iOthers = new interactiveOther();
        public Ustawienia oUstawienia = new Ustawienia();
        double hamburgerMenuWidthBefore;
        bool isFormLoad = true;
        StackPanel actualPanel;
        public frmMain()
        {
            InitializeComponent();
        }

        void OnLoad(object sender, RoutedEventArgs e)
        {
            try
            {
                object fileIC;
                btnDelete.Glyph = ImageAwesome.CreateImageSource(FontAwesomeIcon.Trash, new SolidColorBrush(Colors.White));
                btnEdit.Glyph = ImageAwesome.CreateImageSource(FontAwesomeIcon.Edit, new SolidColorBrush(Colors.White));
                btnAdd.Glyph = ImageAwesome.CreateImageSource(FontAwesomeIcon.File, new SolidColorBrush(Colors.White));
                fileIcon.Source = ImageAwesome.CreateImageSource(FontAwesomeIcon.File, new SolidColorBrush(Color.FromRgb(59, 147, 255)));
                hamburgerMenuWidthBefore = hamburgerMenu.Width;
                isFormLoad = false;
            }
            finally
            {

            }
        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (pnlHome.Visibility == Visibility.Collapsed)
                {
                    pnlHome.Visibility = Visibility.Visible;
                    actualPanel = pnlHome;
                    if (pnlDocs.Visibility == Visibility.Visible) { pnlDocs.Visibility = Visibility.Collapsed; }
                }
            }
            catch (Exception ex)
            {
                iOthers.SaveToLog($@"Błąd podczas wywołania eventu btnHome_Click w frmMain.");
                throw new Exception($@"Wystąpił błąd podczas próby wywołania panelu Home. Treść błędu: {ex.Message} {Environment.NewLine} Skontaktuj się z administratorem systemu.");
            }
        }

        private void btnDocs_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (pnlDocs.Visibility == Visibility.Collapsed)
                {
                    actualPanel = pnlDocs;
                    pnlDocs.Visibility = Visibility.Visible;
                    if (pnlHome.Visibility == Visibility.Visible) { pnlHome.Visibility = Visibility.Collapsed; }
                }
            }
            catch (Exception ex)
            {
                iOthers.SaveToLog($@"Błąd podczas wywołania eventu btnDocs_Click w frmMain.");
                throw new Exception($@"Wystąpił błąd podczas próby wywołania panelu Dokumentów. Treść błędu: {ex.Message} {Environment.NewLine} Skontaktuj się z administratorem systemu.");
            }
        }

        private void btnContact_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Closed(object sender, EventArgs e)
        {
            App.Current.Shutdown();
        }

        private void btnAdd_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            try
            {
                OpenFileDialog fileDialog = new OpenFileDialog();
                fileDialog.Filter = "All files (*.*)|*.*";
                fileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                fileDialog.Multiselect = false;
                if (fileDialog.ShowDialog() == true)
                {
                    string filePath = fileDialog.FileName;
                    string fileName = fileDialog.SafeFileName;
                    MessageBox.Show($@"{fileName} {Environment.NewLine} {filePath}");
                }
            }
            catch (Exception ex)
            {
                iOthers.SaveToLog($@"Błąd podczas wywołania eventu btnAdd_ItemClick w frmMain.");
                throw new Exception($@"Wystąpił błąd podczas próby dodania pliku przez użytkownika. Treść błędu: {ex.Message} {Environment.NewLine} Skontaktuj się z administratorem systemu.");
            }
        }

        private void hamburgerMenu_ViewStateChanged(object sender, DevExpress.Xpf.WindowsUI.HamburgerMenuViewStateChangedEventArgs e)
        {
            try
            {
                if (isFormLoad) { return; }
                if (hamburgerMenu.ViewState == DevExpress.Xpf.WindowsUI.HamburgerMenuViewState.CompactInline)
                {
                    hamburgerMenu.Width = hamburgerMenu.CompactWidth;
                    actualPanel.Width += hamburgerMenuWidthBefore - hamburgerMenu.CompactWidth;
                }
                else
                {
                    hamburgerMenu.Width = hamburgerMenuWidthBefore;
                    actualPanel.Width -= hamburgerMenuWidthBefore - hamburgerMenu.CompactWidth;
                }
            }
            catch (Exception ex)
            {
                iOthers.SaveToLog($@"Błąd podczas wywołanie procedury hamburgerMenu_ViewStateChanged w frmMain.");
                throw new Exception($@"Wystąpił błąd podczas próby przekonwertowania layout'u programu. Treść błędu: {ex.Message} {Environment.NewLine} Skontaktuj się z administratorem systemu.");
            }
    }
    }
}
