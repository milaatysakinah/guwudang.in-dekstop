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

namespace guwudang.Dashboard
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class Frame4 : MyPage
    {
        private BuilderButton buttonBuilder;
        private BuilderTextBox txtBoxBuilder;
        private BuilderTextBlock txtBlockBuilder;
        public Frame4()
        {
            InitializeComponent();
            setController(new DashboardController(this));
            getPartner();
        }

        private void InitUIBuilders()
        {
            buttonBuilder = new BuilderButton();
            txtBoxBuilder = new BuilderTextBox();
            txtBlockBuilder = new BuilderTextBlock();
        }

        private void InitUIElements()
        {
            
        }

        private void getPartner()
        {
            getController().callMethod("totalCostumers");
        }

        public void setTotalCustomers(List<Model.Partner> partners)
        {
            List<Model.Partner> newPartner = new List<Model.Partner>();
            int i = 0;
            foreach (Model.Partner data in partners){
                newPartner.Add(data);

                if (i > 5)
                    break;
            }

            this.Dispatcher.Invoke(() =>
            {
                lvPartner.ItemsSource = newPartner;
            });
        }
    }
}
