﻿using Velacro.UIElements.Basic;
using Velacro.UIElements.TextBlock;
using Velacro.UIElements.Button;
using System.Windows.Controls;
using System.Collections.Generic;
using System;

namespace guwudang.DetailInvoice
{
    /// <summary>
    /// Interaction logic for DetailInvoice.xaml
    /// </summary>
    public partial class DetailInvoice : MyPage
    {
        private IMyTextBlock nameTxtBlock;
        private IMyTextBlock statusTxtBlock;
        private IMyButton addOrderItemButton;
        private BuilderButton buttonBuilder;
        private BuilderTextBlock txtBlockBuilder;
        private string id;

        public DetailInvoice(string id)
        {
            InitializeComponent();
            this.KeepAlive = false;
            setController(new DetailInvoiceController(this));
            this.id = id;
            initUIBuilders();
            initUIElements();
            getDetailinvoice(id);
            getDetailorder(id);
        }

        private void initUIElements()
        {
            nameTxtBlock = txtBlockBuilder.activate(this, "name");
            statusTxtBlock = txtBlockBuilder.activate(this, "status");
            addOrderItemButton = buttonBuilder.activate(this, "btnAddOrderItem").addOnClick(this, "onClickAddOrderItem");
        }

        private void initUIBuilders()
        {
            txtBlockBuilder = new BuilderTextBlock();
            buttonBuilder = new BuilderButton();
        }

        public void onClickAddOrderItem()
        {
            CreateOrderItems.CreateOrderItemPage coi = new CreateOrderItems.CreateOrderItemPage(id);
            Sidebar.secFrame.Navigate(coi);
        }

        private void getDetailinvoice(string id)
        {
            getController().callMethod("Detailinvoice", id);
        }

        private void getDetailorder(string id)
        {
            getController().callMethod("Detailorder", id);
        }

        public void setdetailinvoice(Model.Detailinvoice detailinvoices)
        {
            this.Dispatcher.Invoke(() =>
            {
                name.Text = detailinvoices.name;
                status.Text = detailinvoices.status;
            });
        }
        public void setDetailorder(List<guwudang.Model.Detailorder> Detailorders)
        {
            this.Dispatcher.Invoke(() =>
            {
                lvDetailorder.ItemsSource = Detailorders;
            });
        }

        private void lvDetailorder_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (guwudang.Model.Detailorder item in e.RemovedItems)
            {
                //listProductID.Remove(item.id);
            }

            foreach (guwudang.Model.Detailorder item in e.AddedItems)
            {
                //listProductID.Add(item.id);
            }
        }
    }
}