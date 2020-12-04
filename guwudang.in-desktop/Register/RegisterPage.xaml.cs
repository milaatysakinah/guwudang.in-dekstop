using System.Windows.Controls;
using guwudang.Login;
using Velacro.DataStructures;
using Velacro.UIElements.Basic;
using Velacro.UIElements.Button;
using Velacro.UIElements.TextBlock;
using Velacro.UIElements.TextBox;

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
        
        private void initUIBuilders() {
            buttonBuilder = new BuilderButton();
            txtBoxBuilder = new BuilderTextBox();
            txtBlockBuilder = new BuilderTextBlock();
        }

        private IMyButton registerButton;
        private IMyTextBox emailTxtBox;
        private IMyTextBox nameTxtBox;
        private IMyTextBox phoneNumberTxtBox;
        private IMyTextBox addressTxtBox;
        private IMyTextBox companyNameTxtBox;
        private IMyTextBox passwordTxtBox;
        private IMyTextBox passwordcTxtBox;
        private IMyTextBlock registerStatusTxtBlock;

        private void initUIElements() {
            registerButton = buttonBuilder.activate(this, "register_btn")
                .addOnClick(this, "onRegisterButtonClick");
            nameTxtBox = txtBoxBuilder.activate(this, "name_txt");
            emailTxtBox = txtBoxBuilder.activate(this, "email_txt");
            phoneNumberTxtBox = txtBoxBuilder.activate(this, "phone_number_txt");
            addressTxtBox = txtBoxBuilder.activate(this, "address_txt");
            companyNameTxtBox = txtBoxBuilder.activate(this, "company_name_txt");
            passwordTxtBox = txtBoxBuilder.activate(this, "password_txt");
            passwordcTxtBox = txtBoxBuilder.activate(this, "passwordc_txt");
            registerStatusTxtBlock = txtBlockBuilder.activate(this, "status_field");
        }

        public void onRegisterButtonClick() {
            getController().callMethod("register",  
                nameTxtBox.getText(), 
                emailTxtBox.getText(),
                phoneNumberTxtBox.getText(),
                addressTxtBox.getText(),
                companyNameTxtBox.getText(),
                passwordTxtBox.getText(), 
                passwordcTxtBox.getText());
        }

        public void setRegisterStatus(string _status) {
            this.Dispatcher.Invoke(() => {
                //registerStatusTxtBlock.setText(_status);
            });

        }

        public void toDummy()
        {
            mainFrame.Navigate(new Dummy());
        }

        private void back_btn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            mainFrame.Navigate(new LoginPage(mainFrame));
        }

    }
}
