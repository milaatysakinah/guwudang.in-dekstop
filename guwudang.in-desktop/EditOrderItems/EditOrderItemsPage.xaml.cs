using System.Collections.Generic;
using Velacro.UIElements.Basic;
using Velacro.UIElements.Button;
using Velacro.UIElements.TextBlock;
using Velacro.UIElements.TextBox;
using System.Windows.Controls;
using System.Windows;
using System.Collections.ObjectModel;
using System;

namespace guwudang.EditOrderItems
{
    /// <summary>
    /// Interaction logic for EditOrderItemsPage.xaml
    /// </summary>
    public partial class EditOrderItemsPage : MyPage
    {

        private string idProduct, idType;
        private BuilderButton buttonBuilder;
        private BuilderTextBox txtBoxBuilder;
        private BuilderTextBlock txtBlockBuilder;
        private IMyButton createButton;
        public string idOrder;
        private Model.OrderItemGet thisOrder;

        public EditOrderItemsPage(string _idOrder)
        {
            InitializeComponent();
            setController(new EditOrderItemsController(this));
            initUIBuilders();
            initUIElements();
            this.KeepAlive = false;
            DataContext = this;
            tbTransDate.Text = DateTime.Now.ToString("dddd , dd MMM yyyy");
            getProduct();
            getType();
            getOrderItem(_idOrder);
            idOrder = _idOrder;
           // lbProduct.Content = idOrder;
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
            getController().callMethod("UpdateOrderItem", idProduct, idType, tbOrderQty.Text, idOrder);
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
                    //DetailInvoice.DetailInvoice di = new DetailInvoice.DetailInvoice(idInvoice);
                    //Sidebar.secFrame.Navigate(di);
                    break;
                case MessageBoxResult.No:
                    // No Action
                    break;
            }
        }

        public void getOrderItem(string idO)
        {
            getController().callMethod("getOrder", idO);
        }

        public void setOrder(Model.OrderItemGet order)
        {
            this.Dispatcher.Invoke(() =>
            {
                thisOrder = order;
                cbProduct.SelectedIndex = Int32.Parse(thisOrder.product_id) - 1;
                cbTransType.SelectedIndex = Int32.Parse(thisOrder.transaction_type_id) - 1;
                idProduct = thisOrder.product_id;
                idType = thisOrder.transaction_type_id;
                tbOrderQty.Text = thisOrder.order_quantity.ToString();
                //lbProduct.Content = thisOrder.id.ToString();
            });
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
