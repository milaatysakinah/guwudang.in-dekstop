using guwudang.Invoice;
using guwudang.Partner;
using guwudang.Product;
using guwudang.Dashboard;
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
using Velacro.UIElements.Basic;
using guwudang.Account;
using Velacro.DataStructures;
using guwudang.utils;

namespace guwudang
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Sidebar : MyPage
    {
        private static MyDictionary<string, MyPage> pages;


        public Sidebar()
        {
            InitializeComponent();

            listDashboardPage = new Dashboard.Dashboard();
            listInvoicePage = new ListInvoicePage();
            listPartnerPage = new PartnerPage();
            listProductPage = new ProductPage(secondFrame);
            listAccountPage = new Account.AccountPage();
            setController(new SidebarController(this));

            lblDate.Content = DateTime.Now.ToString("dddd , dd MMM yyyy");

            secondFrame.Navigate(listDashboardPage);
            getAccountPage();
        }

        private void getAccountPage()
        {
            getController().callMethod("getAccountData");
        }

        public void setAccountPage(Model.Account account)
        {
            this.Dispatcher.Invoke(() =>
            {
                lblUsername.Content = "Welcome, " + account.username;
            });
            secondFrame.Navigate(PageManagement.getPage(EPages.listDashboardPage));
        }

        private void dashboard_btn_Click(object sender, RoutedEventArgs e) { 
        
            Console.WriteLine("CLICK");
            secondFrame.Navigate(PageManagement.getPage(EPages.listDashboardPage));
        }

        private void product_btn_Click(object sender, RoutedEventArgs e)
        {
            secondFrame.Navigate(PageManagement.getPage(EPages.listProductPage));
        }

        private void partner_btn_Click(object sender, RoutedEventArgs e)
        {
            secondFrame.Navigate(PageManagement.getPage(EPages.listPartnerPage));
        }

        private void invoice_btn_Click(object sender, RoutedEventArgs e)
        {
            secondFrame.Navigate(PageManagement.getPage(EPages.listInvoicePage));
        }

        private void logout_btn_Click(object sender, RoutedEventArgs e)
        {
            //utils.User user = new utils.User();
            //user.setToken(null);

        }

        private void account_btn_Click(object sender, RoutedEventArgs e)
        {
            secondFrame.Navigate(PageManagement.getPage(EPages.listAccountPage));
        }
    }
}
