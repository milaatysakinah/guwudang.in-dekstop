using System.Collections.Generic;
using Velacro.UIElements.Basic;
using Velacro.UIElements.Button;
using Velacro.UIElements.TextBlock;
using Velacro.UIElements.TextBox;
using System.Windows.Controls;
using System.Windows;
using System.Collections.ObjectModel;

namespace guwudang.CreateInvoice
{
    /// <summary>
    /// Interaction logic for CreateInvoicePage.xaml
    /// </summary>
    public partial class CreateInvoicePage : MyPage
    {
        private string idPartner, idStatus;
        private List<string> listProductID = new List<string>();

        private BuilderButton buttonBuilder;
        private BuilderTextBox txtBoxBuilder;
        private BuilderTextBlock txtBlockBuilder;
        private IMyButton createInvoiceButton;
        private IMyButton deletePartnerButton;
        private IMyTextBox searchPartnerTxtBox;


        public CreateInvoicePage()
        {
            InitializeComponent();
            setController(new CreateInvoiceController(this));
            initUIBuilders();
            initUIElements();
            this.KeepAlive = false;
            DataContext = this;
            getPartner();
            getStatus();
            getProduct();
        }

        private void initUIBuilders()
        {
            buttonBuilder = new BuilderButton();
            txtBoxBuilder = new BuilderTextBox();
        }

        private void initUIElements()
        {
            createInvoiceButton = buttonBuilder.activate(this, "btnCreate").addOnClick(this, "onClickbtnCreate");
            //deletePartnerButton = buttonBuilder.activate(this, "deletePartnerBtn").addOnClick(this, "onClickBtnDelete");
            //searchPartnerTxtBox = txtBoxBuilder.activate(this, "searchPartnerTxt");
        }

        public void onClickbtnCreate()
        {
            getController().callMethod("createInvoice", idPartner, idStatus);
        }

        public void onSuccess(string msg)
        {
            string txt = "Konfirmasi";
            string msgtext = msg;

            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxResult result = MessageBox.Show(msgtext, txt, button);

            switch (result)
            {
                case MessageBoxResult.Yes:
                    //if (listProductID.Count > 0) { delProduct(); }
                    break;
                case MessageBoxResult.No:
                    // No Action
                    break;
            }
            //Sidebar.secFrame.Navigate(utils.PageManagement.getPage(utils.EPages.newInvoicePage)); Arahkan ke detail Invoice
        }

        private void getPartner()
        {
            getController().callMethod("partner");
        }

        public void setPartner(List<Model.Partner> partners)
        {
            this.Dispatcher.Invoke(() =>
            {
                cbPartners.ItemsSource = partners;
            });
            //this._partners = partners;
        }

        private void getStatus()
        {
            getController().callMethod("status");
        }

        public void setStatus(List<Model.StatusInvoice> status)
        {
            this.Dispatcher.Invoke(() =>
            {
                cbStatus.ItemsSource = status;
            });
            //this._partners = partners;
        }

        private void cbPartners_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            idPartner = ((Model.Partner)(cbPartners.SelectedItem)).id;
            //lblPartner.Content = ((Model.Partner)(cbPartners.SelectedItem)).id;
        }

        private void cbStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            idStatus = ((Model.StatusInvoice)(cbStatus.SelectedItem)).id;
            //lblStatus.Content = ((Model.StatusInvoice)(cbStatus.SelectedItem)).id; ;
        }

        private void getProduct()
        {
            getController().callMethod("product");
        }

        public void setProduct(List<guwudang.Model.Product> products)
        {
            this.Dispatcher.Invoke(() =>
            {
                //lvProduct.ItemsSource = products;
            });
        }

        private void lvProduct_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (guwudang.Model.Product item in e.RemovedItems)
            {
                listProductID.Remove(item.id);
            }

            foreach (guwudang.Model.Product item in e.AddedItems)
            {
                listProductID.Add(item.id);
            }
        }

    }
}
