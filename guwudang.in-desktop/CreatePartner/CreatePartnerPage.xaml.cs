using System.Collections.Generic;
using Velacro.UIElements.Basic;
using Velacro.UIElements.Button;
using Velacro.UIElements.TextBlock;
using Velacro.UIElements.TextBox;
using System.Windows.Controls;
using System.Windows;
using guwudang.Model;

namespace guwudang.CreatePartner
{
    /// <summary>
    /// Interaction logic for CreateProductPage.xaml
    /// </summary>
    public partial class CreatePartnerPage : MyPage
    {
        private BuilderButton buttonBuilder;
        private BuilderTextBox txtBoxBuilder;
        private IMyButton createButton;
        private IMyTextBox companyNameTxtBox;
        private IMyTextBox emailTxtBox;
        private IMyTextBox phoneTxtBox;
        private IMyTextBox addressTxtBox;
        public CreatePartnerPage()
        {
            InitializeComponent();
            setController(new CreatePartnerController(this));
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
            createButton = buttonBuilder
                .activate(this, "createBtn")
                .addOnClick(this, "onCreateButtonClick");
            companyNameTxtBox = txtBoxBuilder.activate(this, "companyName_tb");
            emailTxtBox = txtBoxBuilder.activate(this, "email_tb");
            phoneTxtBox = txtBoxBuilder.activate(this, "phoneNumber_tb");
            addressTxtBox = txtBoxBuilder.activate(this, "address_tb");
        }

        public void onCreateButtonClick()
        {
            Model.Partner partner = new Model.Partner();
            partner.name = companyNameTxtBox.getText();
            partner.email = emailTxtBox.getText();
            partner.phone_number = phoneTxtBox.getText();
            partner.address = addressTxtBox.getText();

            getController().callMethod("createPartner", partner);
        }

        public void createSuccess()
        {
            this.Dispatcher.Invoke(() =>
            {
                utils.PageManagement.initPages();
                Sidebar.secFrame.Navigate(utils.PageManagement.getPage(utils.EPages.listPartnerPage));
            });
        }

    }
}
