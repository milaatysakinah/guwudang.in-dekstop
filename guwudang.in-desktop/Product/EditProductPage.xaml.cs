using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Velacro.Basic;
using Velacro.LocalFile;
using Velacro.UIElements.Basic;
using Velacro.UIElements.Button;
using Velacro.UIElements.TextBlock;
using Velacro.UIElements.TextBox;

namespace guwudang.Product
{
    /// <summary>
    /// Interaction logic for EditProductPage.xaml
    /// </summary>
    public partial class EditProductPage : MyPage
    {
        private String idProductType, idUnits, idProduct;
        private BuilderButton buttonBuilder;
        private BuilderTextBox txtBoxBuilder;
        private BuilderTextBlock txtBlockBuilder;
        private IMyButton editButton;
        private IMyButton uploadButton;
        private IMyTextBlock statusTextBlock;
        private IMyTextBox productNameTxtBox;
        private IMyTextBox stockTxtBox;
        private IMyTextBox priceTxtBox;
        private IMyTextBox descriptionTxtBox;
        private Model.Product thisProduct;

        private MyList<MyFile> uploadImage = new MyList<MyFile>();
        public EditProductPage(string _idProduct)
        {
            InitializeComponent();
            setController(new EditProductController(this));
            initUIBuilders();
            initUIElements();
            this.KeepAlive = false;
            DataContext = this;
            getProductType();
            getUnits();
            getProduct(_idProduct);
            idProduct = _idProduct;
        }

        private void initUIBuilders()
        {
            buttonBuilder = new BuilderButton();
            txtBoxBuilder = new BuilderTextBox();
            txtBlockBuilder = new BuilderTextBlock();
        }

        private void initUIElements()
        {
            uploadButton = buttonBuilder
                .activate(this, "upload_btn")
                .addOnClick(this, "onPictureButtonClick");
            editButton = buttonBuilder
                .activate(this, "edit_btn")
                .addOnClick(this, "onEditButtonClick");
            productNameTxtBox = txtBoxBuilder.activate(this, "productName_tb");
            stockTxtBox = txtBoxBuilder.activate(this, "stock_tb");
            priceTxtBox = txtBoxBuilder.activate(this, "price_tb");
            descriptionTxtBox = txtBoxBuilder.activate(this, "description_tb");
            statusTextBlock = txtBlockBuilder.activate(this, "status_txt");

        }

        public void onPictureButtonClick()
        {
            uploadImage.Clear();
            OpenFile openFile = new OpenFile();
            uploadImage.Add(openFile.openFile(false)[0]);
            if (uploadImage[0] != null)
            {
                if (uploadImage[0].extension.Equals(".png", StringComparison.InvariantCultureIgnoreCase) ||
                    uploadImage[0].extension.Equals(".jpg", StringComparison.InvariantCultureIgnoreCase) ||
                    uploadImage[0].extension.Equals(".jpeg", StringComparison.InvariantCultureIgnoreCase));
                picture.Source = new BitmapImage(new Uri(uploadImage[0].fullPath));
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("File format not supported !", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                uploadImage.Clear();
            }
            
        }

        public void onEditButtonClick()
        {
            Model.Product product = new Model.Product();
            product.product_name = productNameTxtBox.getText();
            product.units = idUnits;
            product.price = priceTxtBox.getText();
            product.description = descriptionTxtBox.getText();
            product.product_type_id = idProductType;
            product.product_picture = picture.Source.ToString();

            Console.WriteLine(product.product_picture);
            getController().callMethod("editProduct", product, uploadImage);
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
                    break;
                case MessageBoxResult.No:
                    break;
            }
        }

        public void setStatusError(String error)
        {
            this.Dispatcher.Invoke(() =>
            {
                statusTextBlock.setText(error);
            });
        }

        public void createSuccess()
        {
            this.Dispatcher.Invoke(() =>
            {
                productNameTxtBox.setText("");
                stockTxtBox.setText("");
                priceTxtBox.setText("");
                descriptionTxtBox.setText("");
                statusTextBlock.setText("");

                utils.PageManagement.initPages();
                Sidebar.secFrame.Navigate(utils.PageManagement.getPage(utils.EPages.listProductPage));
            });
        }

        public void getProduct(string idProduct)
        {
            getController().callMethod("getProduct", idProduct);
        }

        public void setProduct(Model.Product product)
        {
            this.Dispatcher.Invoke(() =>
            {
                thisProduct = product;
                productName_tb.Text = thisProduct.price;
                productType_cb.SelectedIndex = Int32.Parse(thisProduct.product_type_id) - 1;
                units_cb.SelectedIndex = Int32.Parse(thisProduct.units) - 1;
                idProductType = thisProduct.product_type_id;
                idUnits = thisProduct.units;
                price_tb.Text = thisProduct.price.ToString();
                description_tb.Text = thisProduct.description;
                //picture = thisProduct.product_picture.Source.toString();
            });
        }

        private void getProductType()
        {
            getController().callMethod("productType");
        }

        public void setProductType(List<Model.ProductType> productType)
        {
            this.Dispatcher.Invoke(() =>
            {
                productType_cb.ItemsSource = productType;
            });
        }

        private void getUnits()
        {
            getController().callMethod("units");
        }

        public void setUnits(List<Model.Units> units)
        {
            this.Dispatcher.Invoke(() =>
            {
                units_cb.ItemsSource = units;
            });
        }

        private void productType_cb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            idProductType = ((Model.ProductType)(productType_cb.SelectedItem)).id;
        }

        private void units_cb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            idUnits = ((Model.Units)(units_cb.SelectedItem)).id;
        }
    }
}
