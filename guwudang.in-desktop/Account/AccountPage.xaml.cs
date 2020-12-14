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
using Velacro.UIElements.Basic;
using Velacro.UIElements.Button;
using Velacro.UIElements.TextBlock;
using Velacro.UIElements.TextBox;

namespace guwudang.Account
{
    /// <summary>
    /// Interaction logic for AccountPage.xaml
    /// </summary>
    public partial class AccountPage : MyPage
    {
        private BuilderButton buttonBuilder;
        private BuilderTextBox txtBoxBuilder;
        private BuilderTextBlock txtBlockBuilder;
        private IMyTextBlock nameTxtBlock;
        private IMyTextBlock emailTxtBlock;
        private IMyTextBlock companyNameTxtBlock;
        private IMyTextBlock phoneNumberTxtBlock;

        public AccountPage()
        {
            InitializeComponent();
            setController(new AccountPageController(this));
            InitUIBuilders();
            InitUIElements();
        }

        private void InitUIBuilders()
        {
            buttonBuilder = new BuilderButton();
            txtBoxBuilder = new BuilderTextBox();
            txtBlockBuilder = new BuilderTextBlock();
        }

        private void InitUIElements()
        {
            nameTxtBlock = txtBlockBuilder.activate(this, "username_txt");
            emailTxtBlock = txtBlockBuilder.activate(this, "email_txt");
            companyNameTxtBlock = txtBlockBuilder.activate(this, "company_txt");
            phoneNumberTxtBlock = txtBlockBuilder.activate(this, "phoneNumber_txt");

            getAccountPage();
        }

        private void getAccountPage()
        {
            getController().callMethod("getAccountData");
        }

        public void setAccountPage(Model.Account account)
        {
            this.Dispatcher.Invoke(() =>
            {
                nameTxtBlock.setText(account.username);
                companyNameTxtBlock.setText(account.company_name);
                emailTxtBlock.setText(account.email);
                phoneNumberTxtBlock.setText(account.phone_number);
            });
        }
    }
}
