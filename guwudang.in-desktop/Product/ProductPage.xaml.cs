using System.Collections.Generic;
using Velacro.UIElements.Basic;
using Velacro.UIElements.Button;
using Velacro.UIElements.TextBlock;
using Velacro.UIElements.TextBox;


namespace guwudang.Product
{
    /// <summary>
    /// Interaction logic for ProductPage.xaml
    /// </summary>
    public partial class ProductPage : MyPage
    {
        private BuilderButton buttonBuilder;
        private BuilderTextBox txtBoxBuilder;
        private BuilderTextBlock txtBlockBuilder;
        private IMyButton newProductButton;
        private IMyButton deleteProductButton;
        private IMyTextBox searchProductTxtBox;
        private IMyTextBox passwordTxtBox;
        private IMyTextBlock loginStatusTxtBlock;

        public ProductPage()
        {
            InitializeComponent();
            this.KeepAlive = true;
            //List<Product> items = new List<Product>();
            //items.Add(new Product() { id = "1", product_type_id = "1", user_id = "1", product_name = "Baju Badut", price = "120000", units = "24", description = "Ini Deskripsi product 1", product_picture = "/img/", created_at = "", updated_at = "" });
            //lvProduct.ItemsSource = items;
            setController(new ProductController(this));
            initUIBuilders();
            initUIElements();
            getProduct();
        }

        private void initUIBuilders()
        {
            buttonBuilder = new BuilderButton();
            txtBoxBuilder = new BuilderTextBox();
        }

        private void initUIElements()
        {
            newProductButton = buttonBuilder.activate(this, "newProductBtn").addOnClick(this, "");
            deleteProductButton = buttonBuilder.activate(this, "deleteProductBtn").addOnClick(this, "");
        }

        private void getProduct()
        {
            getController().callMethod("product");
        }

        public void setProduct(List<guwudang.Model.Product> products)
        {
            this.Dispatcher.Invoke(() =>
            {
                lvProduct.ItemsSource = products;
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