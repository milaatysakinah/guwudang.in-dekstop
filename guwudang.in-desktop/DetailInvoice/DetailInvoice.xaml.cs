using Velacro.UIElements.Basic;
using Velacro.UIElements.TextBlock;
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
        private BuilderTextBlock txtBlockBuilder;

        public DetailInvoice()
        {
            InitializeComponent();
            this.KeepAlive = true;
            setController(new DetailInvoiceController(this));
            initUIBuilders();
            initUIElements();
            getDetailinvoice();
            getDetailorder();
        }

        private void initUIElements()
        {
            nameTxtBlock = txtBlockBuilder.activate(this, "name");
            statusTxtBlock = txtBlockBuilder.activate(this, "status");
        }

        private void initUIBuilders()
        {
            txtBlockBuilder = new BuilderTextBlock();
        }

        private void getDetailinvoice()
        {
            getController().callMethod("Detailinvoice");
        }

        private void getDetailorder()
        {
            getController().callMethod("Detailorder");
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
    }
}
