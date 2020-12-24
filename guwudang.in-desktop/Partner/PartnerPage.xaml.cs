using System.Collections.Generic;
using Velacro.UIElements.Basic;
using Velacro.UIElements.Button;
using Velacro.UIElements.TextBlock;
using Velacro.UIElements.TextBox;
using System.Windows;
using System.Windows.Controls;


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
        private List<string> listPartnerID = new List<string>();

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
            deletePartnerButton = buttonBuilder.activate(this, "deletePartnerBtn").addOnClick(this, "onClickBtnDelete");
            searchPartnerTxtBox = txtBoxBuilder.activate(this, "searchPartnerTxt");
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

        private void search_btn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            getController().callMethod("searchPartner", searchPartnerTxtBox.getText());
        }

        private void lvPartner_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (guwudang.Model.Partner item in e.RemovedItems)
            {
                listPartnerID.Remove(item.id);
            }

            foreach (guwudang.Model.Partner item in e.AddedItems)
            {
                listPartnerID.Add(item.id);
            }
        }

        public void onClickBtnDelete()
        {
            string txt = "Konfirmasi";
            string msgtext;
            if (listPartnerID.Count > 0)
            {
                msgtext = "Apakah Anda yakin ingin menghapus " + listPartnerID.Count + " data tersebut ? ";
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
                    if (listPartnerID.Count > 0) { delPartner(); }
                    break;
                case MessageBoxResult.No:
                    // No Action
                    break;
            }
        }

        private void delPartner()
        {
            //foreach (string item in listProductID)
            //{
            //    getController().callMethod("deleteProduct", item);
            //}
            getController().callMethod("deletePartner", listPartnerID);
            //getProduct();
        }

        public void onClickDetailPartner(object sender, System.Windows.RoutedEventArgs e)
        {
            string id = (string)((Button)sender).Tag;
            DetailPartner.DetailPartner detail = new DetailPartner.DetailPartner(id);
            Sidebar.secFrame.Navigate(detail);
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