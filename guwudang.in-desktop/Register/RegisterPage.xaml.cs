using System.Windows.Controls;
using guwudang.Login;
using Velacro.DataStructures;
using Velacro.UIElements.Basic;
using Velacro.UIElements.Button;
using Velacro.UIElements.TextBlock;
using Velacro.UIElements.TextBox;
using Velacro.UIElements.PasswordBox;

namespace guwudang.Register {
    /// <summary>
    /// Interaction logic for RegisterPage.xaml
    /// </summary>
    public partial class RegisterPage : MyPage {
        private Frame mainFrame;
        public RegisterPage(Frame mainFrame) {
            InitializeComponent();
            this.KeepAlive = true;
            setController(new RegisterController(this));
            this.mainFrame = mainFrame;
            initUIBuilders();
            initUIElements();
        }

        private BuilderButton buttonBuilder;
        private BuilderTextBox txtBoxBuilder;
        private BuilderTextBlock txtBlockBuilder;
        private BuilderPasswordBox builderPasswordBox;

        private void initUIBuilders() {
            buttonBuilder = new BuilderButton();
            txtBoxBuilder = new BuilderTextBox();
            txtBlockBuilder = new BuilderTextBlock();
            builderPasswordBox = new BuilderPasswordBox();
        }

        private IMyButton registerButton;
        private IMyTextBox emailTxtBox;
        private IMyTextBox nameTxtBox;
        private IMyTextBox phoneNumberTxtBox;
        private IMyTextBox addressTxtBox;
        private IMyTextBox companyNameTxtBox;
        private IMyPasswordBox passwordTxtBox;
        private IMyPasswordBox passwordcTxtBox;
        private IMyTextBlock registerStatusTxtBlock;

        private void initUIElements() {
            registerButton = buttonBuilder.activate(this, "register_btn")
                .addOnClick(this, "onRegisterButtonClick");
            nameTxtBox = txtBoxBuilder.activate(this, "name_txt");
            emailTxtBox = txtBoxBuilder.activate(this, "email_txt");
            phoneNumberTxtBox = txtBoxBuilder.activate(this, "phone_number_txt");
            addressTxtBox = txtBoxBuilder.activate(this, "address_txt");
            companyNameTxtBox = txtBoxBuilder.activate(this, "company_name_txt");
            passwordTxtBox = builderPasswordBox.activate(this, "password_txt");
            passwordcTxtBox = builderPasswordBox.activate(this, "passwordc_txt");
            registerStatusTxtBlock = txtBlockBuilder.activate(this, "status_field");
        }

        public void onRegisterButtonClick() {
            getController().callMethod("register",  
                nameTxtBox.getText(), 
                emailTxtBox.getText(),
                phoneNumberTxtBox.getText(),
                addressTxtBox.getText(),
                companyNameTxtBox.getText(),
                passwordTxtBox.getPassword(), 
                passwordcTxtBox.getPassword());
        }

        public void setRegisterStatus(string _status) {
            this.Dispatcher.Invoke(() => {
                //registerStatusTxtBlock.setText(_status);
            });

        }

        public void toDummy()
        {
            mainFrame.Navigate(new Dashboard.Dashboard());
        }

        private void back_btn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            mainFrame.Navigate(new LoginPage(mainFrame));
        }

    }
}
