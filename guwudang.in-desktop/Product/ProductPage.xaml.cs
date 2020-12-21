using System.Collections.Generic;
using Velacro.UIElements.Basic;
using Velacro.UIElements.Button;
using Velacro.UIElements.TextBlock;
using Velacro.UIElements.TextBox;
using System.Windows.Controls;
using System.Windows;


namespace guwudang.Product
{
    /// <summary>
    /// Interaction logic for ProductPage.xaml
    /// </summary>
    public partial class ProductPage : MyPage
    {
        private Frame secondFrame;
        private MyPage QRGen;
        private BuilderButton buttonBuilder;
        private BuilderTextBox txtBoxBuilder;
        private BuilderTextBlock txtBlockBuilder;
        private IMyButton newProductButton;
        private IMyButton deleteProductButton;
        private IMyButton searchProductButton;
        private IMyTextBox searchProductTxtBox;
        private IMyTextBox passwordTxtBox;
        private IMyTextBlock loginStatusTxtBlock;
        private List<string> listProductID = new List<string>();

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
            this.secondFrame = Sidebar.secFrame;
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
            deleteProductButton = buttonBuilder.activate(this, "deleteProductBtn").addOnClick(this, "onClickBtnDelete");
            searchProductTxtBox = txtBoxBuilder.activate(this, "searchProductTxt");
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

        private void search_btn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            getController().callMethod("searchProduct", searchProductTxtBox.getText());
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

        public void onClickBtnDelete()
        {
            string txt = "Konfirmasi";
            string msgtext;
            if (listProductID.Count > 0)
            {
                msgtext = "Apakah Anda yakin ingin menghapus " + listProductID.Count + " data tersebut ? ";
            }
            else
            {
                msgtext = "Anda belum memilih data untuk dihapus.";
            }

            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxResult result = MessageBox.Show(msgtext, txt, button);

            switch (result)
            {
                case MessageBoxResult.Yes:
                    if (listProductID.Count > 0) { delProduct(); }
                    break;
                case MessageBoxResult.No:
                    // No Action
                    break;
            }
        }

        private void delProduct()
        {
            //foreach (string item in listProductID)
            //{
            //    getController().callMethod("deleteProduct", item);
            //}
            getController().callMethod("deleteProduct", listProductID);
            //getProduct();
        }

        public void onClickCreateQR(object sender, System.Windows.RoutedEventArgs e)
        {
            string id = (string)((Button)sender).Tag;
            QRGen = new QRGenerator.QRGeneratorPage(id);
            secondFrame.Navigate(QRGen);
        }

        public void backToLogin()
        {
            new MainWindow().Show();
            this.KeepAlive = false;
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