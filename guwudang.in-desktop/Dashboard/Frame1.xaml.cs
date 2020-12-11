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
using Velacro.UIElements.Basic;
using Velacro.UIElements.Button;
using Velacro.UIElements.TextBlock;
using Velacro.UIElements.TextBox;

namespace guwudang.Dashboard
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    /// 
    public partial class Frame1 : MyPage
    {
        private BuilderButton buttonBuilder;
        private BuilderTextBox txtBoxBuilder;
        private BuilderTextBlock txtBlockBuilder;
        private IMyTextBlock totalCustomersTxtBlock;
        private IMyTextBlock totalInvoiceTxtBlock;
        private IMyTextBlock totalShippingTxtBlock;

        public Frame1()
        {
            InitializeComponent();

            setController(new DashboardController(this));
            InitUIBuilders();
            InitUIElements();
        }

        private void InitUIBuilders()
        {
            buttonBuilder = new BuilderButton();
            txtBoxBuilder = new BuilderTextBox();
            txtBlockBuilder = new BuilderTextBlock();
        }

        private void InitUIElements()
        {
            totalCustomersTxtBlock = txtBlockBuilder.activate(this, "totalCustomers_txt");
            totalCustomersTxtBlock.setText("");

            totalInvoiceTxtBlock = txtBlockBuilder.activate(this, "totalInvoice_txt");
            totalInvoiceTxtBlock.setText("");

            totalShippingTxtBlock = txtBlockBuilder.activate(this, "totalShipping_txt");
            totalShippingTxtBlock.setText("");

            getController().callMethod("totalCostumers");
        }

        public void setTotalCustomers(List<guwudang.Model.Partner> partner)
        {
            this.Dispatcher.Invoke(() =>
            {
                totalCustomersTxtBlock.setText(partner.Count.ToString());
            });
            getController().callMethod("totalInvoices");
        }

        public void setTotalInvoices(String total)
        {
            this.Dispatcher.Invoke(() =>
            {
                totalInvoiceTxtBlock.setText(total);
            });
            getController().callMethod("totalShipping");
        }

        public void setTotalShipping(String total)
        {
            this.Dispatcher.Invoke(() =>
            {
                totalShippingTxtBlock.setText(total);
            });
        }
    }
}
