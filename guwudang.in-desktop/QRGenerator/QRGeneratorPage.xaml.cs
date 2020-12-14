using System.Collections.Generic;
using Velacro.UIElements.Basic;
using Velacro.UIElements.Button;
using Velacro.UIElements.TextBlock;
using Velacro.UIElements.TextBox;
using System.Windows.Controls;
using System.Windows;
using MessagingToolkit.QRCode.Codec;
using MessagingToolkit.QRCode.Codec.Data;
using System.Drawing;
using Microsoft.Win32;
using System.Drawing.Imaging;

namespace guwudang.QRGenerator
{
    /// <summary>
    /// Interaction logic for QRGeneratorPage.xaml
    /// </summary>
    public partial class QRGeneratorPage : MyPage
    {
        public string id;
        private BuilderButton buttonBuilder;
        private BuilderTextBox txtBoxBuilder;
        private BuilderTextBlock txtBlockBuilder;
        private IMyButton createQRButton;
        private IMyButton saveQRButton;
        private IMyTextBox txtQRTxtBox;
        private IMyTextBox passwordTxtBox;
        private IMyTextBlock loginStatusTxtBlock;
        QRCodeEncoder encoder = new QRCodeEncoder();
        Bitmap bitmap;
        SaveFileDialog sfd = new SaveFileDialog();

        public QRGeneratorPage(string id)
        {
            this.id = id;
            InitializeComponent();
            initUIBuilders();
            initUIElements();
            //txtQRTxtBox.setText(id);
            generateQR(id);
        }

        private void initUIBuilders()
        {
            buttonBuilder = new BuilderButton();
            txtBoxBuilder = new BuilderTextBox();
        }

        private void initUIElements()
        {
            //createQRButton = buttonBuilder.activate(this, "createQR").addOnClick(this, "onClickCreateQRBtn");
            saveQRButton = buttonBuilder.activate(this, "saveQR").addOnClick(this, "onClickSaveQRBtn");
            txtQRTxtBox = txtBoxBuilder.activate(this, "txtQR");
        }

        public void generateQR(string id)
        {
            encoder.QRCodeScale = 8;
            bitmap = encoder.Encode(id);
            imgQR.Source = ConvertImage.ToBitmapImage(bitmap);
        }

        //public void onClickCreateQRBtn()
        //{
        //    encoder.QRCodeScale = 8;
        //    bitmap = encoder.Encode(txtQRTxtBox.getText());
        //    imgQR.Source = ConvertImage.ToBitmapImage(bitmap);
        //}

        public void onClickSaveQRBtn()
        {
            sfd.Filter = "PNG|*.png";
            sfd.FileName = id;
            if (sfd.ShowDialog() == true)
            {
                if (bitmap != null)
                {
                    bitmap.Save(string.Concat(sfd.FileName, ".png"), ImageFormat.Png);
                }
            }
        }

    }
}
