using System.Collections.Generic;
using Velacro.UIElements.Basic;
using Velacro.UIElements.Button;
using Velacro.UIElements.TextBlock;
using Velacro.UIElements.TextBox;
using System.Windows.Controls;
using System.Windows;
using System.Collections.ObjectModel;

namespace guwudang.CreateProduct 
{
    /// <summary>
    /// Interaction logic for CreateProductPage.xaml
    /// </summary>
    public partial class CreateProductPage : MyPage
    {
        private BuilderButton buttonBuilder;
        private BuilderTextBox txtBoxBuilder;
        private BuilderTextBlock txtBlockBuilder;
        private IMyButton createButton;
        private IMyButton uploadButton;
        private IMyTextBox productNameTxtBox;
        private IMyTextBox stockTxtBox;
        private IMyTextBox priceTxtBox;
        private IMyTextBox descriptionTxtBox;
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
        }

        public void onPictureButtonClick()
        {
            getController().callMethod("picture", upload_btn);
        }

        public void onCreateButtonClick()
        {
            getController().callMethod("createProduct", productName_tb, stock_tb, price_tb,
                description_tb);
        }
    }
}
