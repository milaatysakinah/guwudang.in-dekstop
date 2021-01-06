using Microsoft.Win32;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Windows;
using System.Windows.Media.Imaging;
using Velacro.Basic;
using Velacro.LocalFile;
using Velacro.UIElements.Basic;
using Velacro.UIElements.Button;
using Velacro.UIElements.TextBlock;
using Velacro.UIElements.TextBox;

namespace guwudang.Account
{
    /// <summary>
    /// Interaction logic for AccountPage.xaml
    /// </summary>
    public partial class EditAccountPage : MyPage
    {
        private BuilderButton buttonBuilder;
        private BuilderTextBox txtBoxBuilder;
        private BuilderTextBlock txtBlockBuilder;
        private IMyTextBox nameTxtBox;
        private IMyTextBox emailTxtBox;
        private IMyTextBox companyNameTxtBox;
        private IMyTextBox phoneNumberTxtBox;
        private IMyTextBlock statusTxtBlock;
        private MyList<MyFile> uploadImage = new MyList<MyFile>();
        private String id;

        public EditAccountPage()
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
            nameTxtBox = txtBoxBuilder.activate(this, "username_txt");
            emailTxtBox = txtBoxBuilder.activate(this, "email_txt");
            companyNameTxtBox = txtBoxBuilder.activate(this, "companyName_txt");
            phoneNumberTxtBox = txtBoxBuilder.activate(this, "phoneNumber_txt");
            statusTxtBlock = txtBlockBuilder.activate(this, "status_txt");

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
                nameTxtBox.setText(account.username);
                companyNameTxtBox.setText(account.company_name);
                emailTxtBox.setText(account.email);
                phoneNumberTxtBox.setText(account.phone_number);

                Uri resourceUri = new Uri(account.profile_picture);

                if (resourceUri != null)
                {
                    var bi = new BitmapImage();
                    bi.BeginInit();
                    bi.UriSource = resourceUri;
                    bi.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                    bi.CacheOption = BitmapCacheOption.OnLoad;
                    bi.EndInit();

                    profilePic_img.Source = bi;
                }

                id = account.id;
            });
        }

        private void image_btn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            uploadImage.Clear();
            OpenFile openFile = new OpenFile();
            uploadImage.Add(openFile.openFile(false)[0]);
            if (uploadImage[0] != null)
            {
                if (uploadImage[0].extension.Equals(".png", StringComparison.InvariantCultureIgnoreCase) ||
                    uploadImage[0].extension.Equals(".jpg", StringComparison.InvariantCultureIgnoreCase) ||
                    uploadImage[0].extension.Equals(".jpeg", StringComparison.InvariantCultureIgnoreCase))
                    profilePic_img.Source = new BitmapImage(new Uri(uploadImage[0].fullPath));
                else
                {
                    MessageBoxResult result = MessageBox.Show("File format not supported !", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                    uploadImage.Clear();
                }
            }
            /*
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select an Image";
            op.Filter = "All supported graphics|.jpg;.jpeg;.png|" +
                          "JPEG (.jpg;.jpeg)|.jpg;.jpeg|" +
                          "Portable Network Graphic (.png)|*.png";
            if(op.ShowDialog() == true)
            {
                image = new BitmapImage(new Uri(op.FileName));
                fullPath = op.FileName;
                profilePic_img.Source = image;
            }*/
        }

        private void doneBtn_Click(object sender, RoutedEventArgs e)
        {
            Model.Account account = new Model.Account();
            account.id = id;
            account.email = emailTxtBox.getText();
            account.username = nameTxtBox.getText();
            account.phone_number = phoneNumberTxtBox.getText();
            account.company_name = companyNameTxtBox.getText();
            account.profile_picture = profilePic_img.Source.ToString();

            Console.WriteLine(account.profile_picture);
            

            getController().callMethod("updateData", account, uploadImage);
        }

        public void setEditSuccess()
        {
            this.Dispatcher.Invoke(() =>
            {
                utils.PageManagement.initPages();
                Sidebar.secFrame.Navigate(utils.PageManagement.getPage(utils.EPages.listAccountPage));
            });
        }

        public void setEditFailed(String status)
        {
            this.Dispatcher.Invoke(() =>
            {
                statusTxtBlock.setText(status);
            });
        }
    }
}
