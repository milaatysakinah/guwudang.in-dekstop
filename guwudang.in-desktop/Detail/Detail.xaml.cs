﻿using Velacro.UIElements.Basic;
using Velacro.UIElements.TextBlock;
using System.Windows.Media.Imaging;
using System.Windows.Controls;
using System.Collections.Generic;
using System;

namespace guwudang.Detail
{
    /// <summary>
    /// Interaction logic for Detail.xaml
    /// </summary>
    public partial class Detail : MyPage
    {

        private IMyTextBlock product_type_idTxtBlock;
        private IMyTextBlock user_idTxtBlock;
        private IMyTextBlock product_nameTxtBlock;
        private IMyTextBlock priceTxtBlock;
        private IMyTextBlock unitsTxtBlock;
        private IMyTextBlock descriptionTxtBlock;
        private BuilderTextBlock txtBlockBuilder;
        private Image image;
        private List<string> listProductID = new List<string>();
        private string id;

        public Detail(string id)
        {
            InitializeComponent();
            this.KeepAlive = false;
            setController(new DetailController(this));
            initUIBuilders();
            initUIElements();
            this.id = id; // gunakan id ini biar gak static kirim via parameter di method getDetail dan getProductDetail mu yaaa
            getDetail();
            getProduct_detail();
        }

        private void initUIElements()
        {
            product_type_idTxtBlock = txtBlockBuilder.activate(this, "product_type_id");
            user_idTxtBlock = txtBlockBuilder.activate(this, "user_id");
            product_nameTxtBlock = txtBlockBuilder.activate(this, "product_name");
            priceTxtBlock = txtBlockBuilder.activate(this, "price");
            //unitsTxtBlock = txtBlockBuilder.activate(this, "units");
            descriptionTxtBlock = txtBlockBuilder.activate(this, "description");
            image = this.FindName("product_picture") as Image;
        }

        private void initUIBuilders()
        {
            txtBlockBuilder = new BuilderTextBlock();
            image = new Image();
        }

        private void getDetail()
        {
            getController().callMethod("detail");
        }

        private void getProduct_detail()
        {
            getController().callMethod("product_detail");
        }

        public void setProduct(Model.Product products)
        {
            this.Dispatcher.Invoke(() =>
            {
                product_type_id.Text = products.product_type_id;
                user_id.Text = products.user_id;
                product_name.Text = products.product_name;
                price.Text = products.price;
                //units.list = products.units;
                description.Text = products.description;

                if (products.product_picture != null)
                    image.Source = new BitmapImage(new Uri("http://127.0.0.1:8000/" + products.product_picture));
            });
        }
        public void setDetailProduct(List<guwudang.Model.ProductDetail> productDetails)
        {
            this.Dispatcher.Invoke(() =>
            {
                lvProductDetail.ItemsSource = productDetails;
            });
        }
    }
}