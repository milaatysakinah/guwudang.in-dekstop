﻿using System;
using System.Net.Http;
using System.Windows.Controls;
using System.Windows;
using guwudang.Register;
using guwudang;
using Velacro.Basic;
using Velacro.UIElements.Basic;
using Velacro.UIElements.Button;
using Velacro.UIElements.TextBlock;
using Velacro.UIElements.PasswordBox;
using Velacro.UIElements.TextBox;
using Velacro.Api;
using guwudang.utils;

namespace guwudang.Login {
    
    public partial class LoginPage : MyPage {
        private BuilderButton buttonBuilder;
        private BuilderTextBox txtBoxBuilder;
        private BuilderTextBlock txtBlockBuilder;
        private BuilderPasswordBox builderPasswordBox;
        private IMyButton loginButton;
        private IMyTextBox emailTxtBox;
        private IMyPasswordBox passwordTxtBox;
        private IMyTextBlock loginStatusTxtBlock;
        private MyPage registerPage;
        private MyPage aboutPage;
        private MyPage dashboardPage;
        private Frame mainFrame;

        public LoginPage(Frame mainFrame) {
            InitializeComponent();
            this.KeepAlive = true;
            setController(new LoginController(this));
            this.mainFrame = mainFrame;
            initUIBuilders();
            initUIElements();
            email_txt.Focus();

            registerPage = new RegisterPage(mainFrame);
            aboutPage = new AboutPage.AboutPage();
            //dashboardPage = new Dashboard.Dashboard();
        }

        private void initUIBuilders(){
            buttonBuilder = new BuilderButton();
            txtBoxBuilder = new BuilderTextBox();
            builderPasswordBox = new BuilderPasswordBox();
            txtBlockBuilder = new BuilderTextBlock();
        }

        private void initUIElements(){
            loginButton = buttonBuilder
                .activate(this, "loginButton_btn")
                .addOnClick(this, "onLoginButtonClick");
            emailTxtBox = txtBoxBuilder.activate(this, "email_txt");
            passwordTxtBox = builderPasswordBox.activate(this, "password_txt");
            loginStatusTxtBlock = txtBlockBuilder.activate(this, "status_txt");
        }

        public void onLoginButtonClick() {
            getController().callMethod("login", email_txt.Text, password_txt.Password);
        }


        public void setLoginStatus(string _status){
            this.Dispatcher.Invoke(() =>
            {
                loginStatusTxtBlock.setText(_status);
            });
        }

        private void register_btn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            mainFrame.Navigate(registerPage);

        }
        private void loginButton_btn_Click(object sender, RoutedEventArgs e)
        {
            getController().callMethod("login",
                emailTxtBox.getText(),
                passwordTxtBox.getPassword());
        }

        public void changeToDashboard(String nothing)
        {
            User user = new User();
            Console.WriteLine("Login" + user.getToken());

            if (user.getToken() != null)
                mainFrame.Navigate(new Sidebar(mainFrame));
        }

    
    }
}