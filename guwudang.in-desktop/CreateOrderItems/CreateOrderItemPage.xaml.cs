using System.Collections.Generic;
using Velacro.UIElements.Basic;
using Velacro.UIElements.Button;
using Velacro.UIElements.TextBlock;
using Velacro.UIElements.TextBox;
using System.Windows.Controls;
using System.Windows;
using System.Collections.ObjectModel;
using System;

namespace guwudang.CreateOrderItems
{
    /// <summary>
    /// Interaction logic for CreateOrderItemPage.xaml
    /// </summary>
    public partial class CreateOrderItemPage : MyPage
    {
        private string idProduct, idType;
        private BuilderButton buttonBuilder;
        private BuilderTextBox txtBoxBuilder;
        private BuilderTextBlock txtBlockBuilder;
        private IMyButton createButton;
        private string idInvoice;
        public CreateOrderItemPage(string _idInvoice)
        {
            InitializeComponent();
            setController(new CreateOrderItemController(this));
            initUIBuilders();
            initUIElements();
            this.KeepAlive = false;
            DataContext = this;
            tbTransDate.Text = DateTime.Now.ToString("dddd , dd MMM yyyy");
            getProduct();
            getType();
            idInvoice = _idInvoice;
        }

        private void initUIBuilders()
        {
            buttonBuilder = new BuilderButton();
            txtBoxBuilder = new BuilderTextBox();
        }

        private void initUIElements()
        {
            //newPartnerButton = buttonBuilder.activate(this, "newPartnerBtn").addOnClick(this, "");
            createButton = buttonBuilder.activate(this, "createBtn").addOnClick(this, "onClickBtnCreate");
            //searchPartnerTxtBox = txtBoxBuilder.activate(this, "searchPartnerTxt");
        }

        public void onClickBtnCreate()
        {
            getController().callMethod("createOrderItem", idProduct, idType, tbOrderQty.Text, idInvoice);
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
                    // Action SUkses
                    break;
                case MessageBoxResult.No:
                    // No Action
                    break;
            }
        }

        private void getProduct()
        {
            getController().callMethod("product");
        }

        public void setProduct(List<Model.Product> products)
        {
            this.Dispatcher.Invoke(() =>
            {
                cbProduct.ItemsSource = products;
            });
            //this._partners = partners;
        }

        private void getType()
        {
            getController().callMethod("type");
        }

        public void setType(List<Model.TransType> types)
        {
            this.Dispatcher.Invoke(() =>
            {
                cbTransType.ItemsSource = types;
            });
            //this._partners = partners;
        }

        private void cbProduct_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            idProduct = ((Model.Product)(cbProduct.SelectedItem)).id;
            //lblPartner.Content = ((Model.Partner)(cbPartners.SelectedItem)).id;
        }

        private void cbTransType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            idType = ((Model.TransType)(cbTransType.SelectedItem)).id;
            //lblStatus.Content = ((Model.StatusInvoice)(cbStatus.SelectedItem)).id; ;
        }
    }
}
