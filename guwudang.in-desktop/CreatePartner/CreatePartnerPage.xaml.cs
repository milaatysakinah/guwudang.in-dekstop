using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
                .activate(this, "create_btn")
                .addOnClick(this, "onCreateButtonClick");
            companyNameTxtBox = txtBoxBuilder.activate(this, "companyName_tb");
            emailTxtBox = txtBoxBuilder.activate(this, "email_tb");
            phoneTxtBox = txtBoxBuilder.activate(this, "phoneNumber_tb");
            addressTxtBox = txtBoxBuilder.activate(this, "address_tb");
        }

        public void onCreateButtonClick()
        {
            getController().callMethod("createPartner", companyName_tb, email_tb, phone_tb,
                address_tb);
        }

    }
}
