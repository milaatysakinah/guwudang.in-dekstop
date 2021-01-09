using guwudang.Product;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Velacro.Basic;
using Velacro.LocalFile;
using Velacro.UIElements.Basic;
using Velacro.UIElements.Button;
using Velacro.UIElements.TextBlock;
using Velacro.UIElements.TextBox;

namespace guwudang.CreateProduct
{
    /// <summary>
    /// Interaction logic for CreateProductPage.xaml
    /// </summary>
    public partial class CreateProductPage : MyPage
    {
        private String idProductType, idUnits;
        private BuilderButton buttonBuilder;
        private BuilderTextBox txtBoxBuilder;
        private BuilderTextBlock txtBlockBuilder;
        private IMyButton createButton;
        private IMyButton uploadButton;
        private IMyTextBlock statusTextBlock;
        private IMyTextBox productNameTxtBox;
        private IMyTextBox stockTxtBox;
        private IMyTextBox priceTxtBox;
        private IMyTextBox descriptionTxtBox;
        private Model.Product thisProduct;

        private MyList<MyFile> uploadImage = new MyList<MyFile>();
        public CreateProductPage()
        {
            InitializeComponent();
            setController(new CreateProductController(this));
            initUIBuilders();
            initUIElements();
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
            createButton = buttonBuilder
                .activate(this, "create_btn")
                .addOnClick(this, "onCreateButtonClick");
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

        public void onCreateButtonClick()
        {
            Model.Product product = new Model.Product();
            product.product_name = productNameTxtBox.getText();
            product.units = idUnits;
            product.price = priceTxtBox.getText();
            product.description = descriptionTxtBox.getText();
            product.product_type_id = idProductType;
            product.product_picture = picture.Source.ToString();

            Console.WriteLine(product.product_picture);
            getController().callMethod("createProduct", picture, uploadImage);
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
