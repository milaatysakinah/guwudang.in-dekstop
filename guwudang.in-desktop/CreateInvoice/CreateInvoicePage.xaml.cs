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

namespace guwudang.CreateInvoice
{
    /// <summary>
    /// Interaction logic for CreateInvoicePage.xaml
    /// </summary>
    public partial class CreateInvoicePage : MyPage
    {
        private BuilderButton buttonBuilder;
        private BuilderComboBox comboBoxBuilder;
        private IMyButton createButton;
        private IMyComboBox statusInvoiceBox;
        private IMyComboBox partnerNameBox;
        public CreateInvoicePage()
        {
            InitializeComponent();
            setController(new CreateInvoiceController(this));
            initUIBuilders();
            initUIElements();
        }

        private void initUIBuilders()
        {
            buttonBuilder = new BuilderButton();
            comboBoxBuilder = new BuilderComboBox();
        }

        private void initUIElements()
        {
            createButton = buttonBuilder
                .activate(this, "create_btn")
                .addOnClick(this, "onCreateButtonClick");
            partnerNameBox = comboBoxBuilder.activate(this, "partnerName_cb");
            statusInvoiceBox = comboBoxBuilder.activate(this, "statusInvoice_cb");
        }

        public void onCreateButtonClick()
        {
            getController().callMethod("createInvoice", partnerName_cb, statusInvoice_cb);
        }
    }
}
