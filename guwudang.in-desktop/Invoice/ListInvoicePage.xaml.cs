using System.Collections.Generic;
using Velacro.UIElements.Basic;
using Velacro.UIElements.Button;
using Velacro.UIElements.TextBlock;
using Velacro.UIElements.TextBox;


namespace guwudang.Invoice
{
    /// <summary>
    /// Interaction logic for ProductPage.xaml
    /// </summary>
    public partial class InvoicePage : MyPage
    {
        private BuilderButton buttonBuilder;
        private BuilderTextBox txtBoxBuilder;
        private BuilderTextBlock txtBlockBuilder;
        private IMyButton newInvoiceButton;
        private IMyButton deleteInvoiceButton;
        private IMyTextBox searchInvoiceTxtBox;

        public void ListInvoicePage()
        {
            InitializeComponent();
            this.KeepAlive = true;
            //List<Product> items = new List<Product>();
            //items.Add(new Product() { id = "1", product_type_id = "1", user_id = "1", product_name = "Baju Badut", price = "120000", units = "24", description = "Ini Deskripsi product 1", product_picture = "/img/", created_at = "", updated_at = "" });
            //lvProduct.ItemsSource = items;
            setController(new InvoiceController(this));
            InitUIBuilders();
            InitUIElements();
            getInvoice();
        }

        private void InitUIBuilders()
        {
            buttonBuilder = new BuilderButton();
            txtBoxBuilder = new BuilderTextBox();
        }

        private void InitUIElements()
        {
            newInvoiceButton = buttonBuilder.activate(this, "newInvoiceBtn").addOnClick(this, "");
            deleteInvoiceButton = buttonBuilder.activate(this, "deleteInvoiceBtn").addOnClick(this, "");
        }

        private void getInvoice()
        {
            getController().callMethod("listInvoice");
        }

        public void setInvoice(List<Model.Invoice> invoice)
        {
            this.Dispatcher.Invoke(() =>
            {
                //lvListInvoice.ItemsSource = invoice;
            });
        }


    }

    //public class Product
    //{
    //    public string id { get; set; }
    //    public string product_type_id { get; set; }
    //    public string user_id { get; set; }
    //    public string product_name { get; set; }
    //    public string price { get; set; }
    //    public string units { get; set; }
    //    public string description { get; set; }
    //    public string product_picture { get; set; }
    //    public string created_at { get; set; }
    //    public string updated_at { get; set; }

    //}
}