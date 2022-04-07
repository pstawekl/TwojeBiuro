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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class loginWindow : Window
    {
        interactiveSQL iSql = Program.iSQl;
        Ustawienia oUstawienia = Program.oUstawienia;
        public loginWindow()
        {
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //if (iSql.GetScalarInt($@"select count(*) from ...", oUstawienia.iConn) > 0)
            //{

            //}
            if (txtPassword.Password != "Password" & txtUser.Text != "User" & txtPassword.Password.Length > 0 & txtUser.Text.Length > 0)
            {
                if(txtUser.Text == "pstawekl" & txtPassword.Password == "el4505to")
                {
                    Main frmMain = new Main();
                    this.Close();
                    frmMain.ShowDialog();
                    frmMain.Close();
                    frmMain = null;
                }
            }
            else
            {
                if(txtUser.Text == "User")
                {
                    txtUser.BorderBrush = new SolidColorBrush(Colors.Red);
                }
                if (txtPassword.Password == "Password")
                {
                    txtPassword.BorderBrush= new SolidColorBrush(Colors.Red);
                }
            }
        }

        private void txtUser_LostFocus(object sender, RoutedEventArgs e)
        {
            if(txtUser.Text.Length == 0)
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

        //private void txtUser_MouseDown(object sender, MouseButtonEventArgs e)
        //{
        //    if (e.ButtonState == MouseButtonState.Pressed)
        //    {
        //        if (txtUser.Text == "User")
        //        {
        //            txtUser.Text = "";
        //        }
        //    }
        //}
    }
}
