using System.Windows;
using guwudang.Login;
using guwudang.Register;
using guwudang.Invoice;
using Velacro.DataStructures;
using Velacro.UIElements.Basic;

namespace guwudang {
    public partial class MainWindow : MyWindow {
        private MyPage loginPage;
        private MyPage registerPage;
        private MyPage dashboardPage;

        public MainWindow() {
            InitializeComponent();
            registerPage = new RegisterPage(mainFrame);
            loginPage = new LoginPage(mainFrame);
            dashboardPage = new Dashboard.Dashboard();
            

            mainFrame.Navigate(loginPage);
        }

        public void changeFrame(MyPage nextPage)
        {
            mainFrame.Navigate(nextPage);
        }

        private void loginButton_btn_Click(object sender, RoutedEventArgs e){
            mainFrame.Navigate(loginPage);
        }

        private void registerButton_btn_Click(object sender, RoutedEventArgs e){
            mainFrame.Navigate(registerPage);
        }

        private void dashboardButton_btn_Click(object sender, RoutedEventArgs e){
            mainFrame.Navigate(dashboardPage);
        }
    }
}
