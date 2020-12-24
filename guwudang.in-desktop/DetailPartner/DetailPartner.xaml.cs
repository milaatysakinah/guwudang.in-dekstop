using System.Collections.Generic;
using Velacro.UIElements.Basic;
using Velacro.UIElements.TextBlock;

namespace guwudang.DetailPartner
{
    /// <summary>
    /// Interaction logic for DetailPartner.xaml
    /// </summary>
    public partial class DetailPartner : MyPage
    {
        private IMyTextBlock nameTxtBlock;
        private IMyTextBlock emailTxtBlock;
        private IMyTextBlock phone_numberTxtBlock;
        private IMyTextBlock addressTxtBlock;
        private BuilderTextBlock txtBlockBuilder;
        private string id;

        public DetailPartner(string id)
        {
            InitializeComponent();
            this.KeepAlive = false;
            setController(new DetailPartnerC(this));
            this.id = id; // gunakan id ini biar gak static kirim via parameter di method getDetail dan getProductDetail mu yaaa
            initUIBuilders();
            initUIElements();
            getPartner();
        }

        public void setDetailPartner(Model.Partner partners)
            
        {
            this.Dispatcher.Invoke(() => {
                name.Text = partners.name;
                email.Text = partners.email;
                phone_number.Text = partners.phone_number;
                address.Text = partners.address;
            });
        }

        private void initUIBuilders()
        {
            txtBlockBuilder = new BuilderTextBlock();
        }

        private void initUIElements()
        {
            nameTxtBlock = txtBlockBuilder.activate(this, "name");
            emailTxtBlock = txtBlockBuilder.activate(this, "email");
            phone_numberTxtBlock = txtBlockBuilder.activate(this, "phone_number");
            addressTxtBlock = txtBlockBuilder.activate(this, "address");
        }

        private void getPartner()
        {
            getController().callMethod("partner");
        }
    }
}