using System.Collections.Generic;
using Velacro.UIElements.Basic;
using Velacro.UIElements.Button;
using Velacro.UIElements.TextBlock;
using Velacro.UIElements.TextBox;


namespace guwudang.Partner
{
    /// <summary>
    /// Interaction logic for ProductPage.xaml
    /// </summary>
    public partial class PartnerPage : MyPage
    {
        private BuilderButton buttonBuilder;
        private BuilderTextBox txtBoxBuilder;
        private BuilderTextBlock txtBlockBuilder;
        private IMyButton newPartnerButton;
        private IMyButton deletePartnerButton;
        private IMyTextBox searchPartnerTxtBox;
        private IMyTextBox passwordTxtBox;
        private IMyTextBlock loginStatusTxtBlock;

        public PartnerPage()
        {
            InitializeComponent();
            this.KeepAlive = true;
            //List<Product> items = new List<Product>();
            //items.Add(new Product() { id = "1", product_type_id = "1", user_id = "1", product_name = "Baju Badut", price = "120000", units = "24", description = "Ini Deskripsi product 1", product_picture = "/img/", created_at = "", updated_at = "" });
            //lvProduct.ItemsSource = items;
            setController(new PartnerController(this));
            initUIBuilders();
            initUIElements();
            getPartner();
        }

        private void initUIBuilders()
        {
            buttonBuilder = new BuilderButton();
            txtBoxBuilder = new BuilderTextBox();
        }

        private void initUIElements()
        {
            newPartnerButton = buttonBuilder.activate(this, "newPartnerBtn").addOnClick(this, "");
            deletePartnerButton = buttonBuilder.activate(this, "deletePartnerBtn").addOnClick(this, "");
        }

        private void getPartner()
        {
            getController().callMethod("partner");
        }

        public void setPartner(List<Model.Partner> partners)
        {
            this.Dispatcher.Invoke(() =>
            {
                lvPartner.ItemsSource = partners;
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