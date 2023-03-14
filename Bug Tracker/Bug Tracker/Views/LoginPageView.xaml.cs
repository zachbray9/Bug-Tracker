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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Bug_Tracker.Views
{
    /// <summary>
    /// Interaction logic for LoginPageView.xaml
    /// </summary>
    public partial class LoginPageView : UserControl
    {
        public static readonly DependencyProperty LoginCommandProperty = DependencyProperty.Register("LoginCommand", typeof(ICommand), typeof(LoginPageView), new PropertyMetadata(null));

        public ICommand LoginCommand
        {
            get{ return (ICommand)GetValue(LoginCommandProperty); }
            set{ SetValue(LoginCommandProperty, value);  }
        }

        public LoginPageView()
        {
            InitializeComponent();
        }

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {

            if(LoginCommand != null)
            {
                string password = passwordTextbox.Password;
                LoginCommand.Execute(password);
            }
        }
    }
}
