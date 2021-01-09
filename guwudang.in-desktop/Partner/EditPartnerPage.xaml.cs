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

namespace guwudang.Partner
{
    /// <summary>
    /// Interaction logic for EditPartnerPage.xaml
    /// </summary>
    public partial class EditPartnerPage : MyPage
    {
        private BuilderButton buttonBuilder;
        private BuilderTextBox txtBoxBuilder;
        private IMyButton createButton;
        private IMyTextBox companyNameTxtBox;
        private IMyTextBox emailTxtBox;
        private IMyTextBox phoneTxtBox;
        private IMyTextBox addressTxtBox;
        public string idPartner;
        private Model.Partner thisPartner;
        public EditPartnerPage(string _idPartner)
        {
            InitializeComponent();
            setController(new EditPartnerController(this));
            initUIBuilders();
            initUIElements();
            this.KeepAlive = false;
            getPartner(_idPartner);
            idPartner = _idPartner;
        }

        private void initUIBuilders()
        {
            buttonBuilder = new BuilderButton();
            txtBoxBuilder = new BuilderTextBox();
        }

        private void initUIElements()
        {
            createButton = buttonBuilder
                .activate(this, "edit_btn")
                .addOnClick(this, "onEditButtonClick");
            companyNameTxtBox = txtBoxBuilder.activate(this, "companyName_tb");
            emailTxtBox = txtBoxBuilder.activate(this, "email_tb");
            phoneTxtBox = txtBoxBuilder.activate(this, "phoneNumber_tb");
            addressTxtBox = txtBoxBuilder.activate(this, "address_tb");
        }

        public void getPartner(string idPartner)
        {
            getController().callMethod("getPartner", idPartner);
        }

        public void setPartner(Model.Partner partner)
        {
            this.Dispatcher.Invoke(() =>
            {
                thisPartner = partner;
                companyName_tb.Text = thisPartner.name;
                email_tb.Text = thisPartner.email;
                phoneNumber_tb.Text = thisPartner.phone_number;
                address_tb.Text = thisPartner.address;             
            });
        }

        public void onEditButtonClick()
        {
            Model.Partner partner = new Model.Partner();
            partner.name = companyNameTxtBox.getText();
            partner.email = emailTxtBox.getText();
            partner.phone_number = phoneTxtBox.getText();
            partner.address = addressTxtBox.getText();

            getController().callMethod("editPartner", partner, idPartner);
        }
    }
}
