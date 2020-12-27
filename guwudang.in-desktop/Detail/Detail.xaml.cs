using Velacro.UIElements.Basic;
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
            getDetail(id);
            getProduct_detail(id);
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

        private void getDetail(string id)
        {
            getController().callMethod("detail",id);
        }

        private void getProduct_detail(string id)
        {
            getController().callMethod("product_detail",id);
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
                    image.Source = new BitmapImage(new Uri("http://localhost:8000/" + products.product_picture));
            });
        }
        public void setDetailproduct(List<guwudang.Model.ProductDetail> Detailproducts)
        {
            this.Dispatcher.Invoke(() =>
            {
                lvDetailproduct.ItemsSource = Detailproducts;
            });
        }
        private void lvDetailproduct_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (guwudang.Model.ProductDetail item in e.RemovedItems)
            {
                //listProductID.Remove(item.id);
            }

            foreach (guwudang.Model.ProductDetail item in e.AddedItems)
            {
                //listProductID.Add(item.id);
            }
        }
    }
}
