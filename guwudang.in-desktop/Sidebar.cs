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

namespace guwudang
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Sidebar : MyPage
    {
        private MyPage listDashboardPage;
        private MyPage listInvoicePage;
        private MyPage listPartnerPage;
        private MyPage listProductPage;
        private MyPage listAccountPage;

        public Sidebar()
        {
            InitializeComponent();

            listDashboardPage = new Dashboard.Dashboard();
            listInvoicePage = new ListInvoicePage();
            listPartnerPage = new PartnerPage();
            listProductPage = new ProductPage(secondFrame);
            listAccountPage = new Account.AccountPage();

            secondFrame.Navigate(listDashboardPage);
        }

        private void dashboard_btn_Click(object sender, RoutedEventArgs e)
        {
            secondFrame.Navigate(listDashboardPage);   
        }

        private void product_btn_Click(object sender, RoutedEventArgs e)
        {
            secondFrame.Navigate(listProductPage);
        }

        private void partner_btn_Click(object sender, RoutedEventArgs e)
        {
            secondFrame.Navigate(listPartnerPage);
        }

        private void invoice_btn_Click(object sender, RoutedEventArgs e)
        {
            secondFrame.Navigate(listInvoicePage);
        }

        private void logout_btn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void account_btn_Click(object sender, RoutedEventArgs e)
        {
            secondFrame.Navigate(listAccountPage);
        }
    }
}
