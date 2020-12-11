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
    /// Interaction logic for Frame2.xaml
    /// </summary>
    public partial class Frame2 : MyPage
    {
        private BuilderButton buttonBuilder;
        private BuilderTextBox txtBoxBuilder;
        private BuilderTextBlock txtBlockBuilder;
        private IMyTextBlock totalProductsTxtBlock;
        private IMyTextBlock totalOrderINTxtBlock;
        private IMyTextBlock totalOrderOUTTxtBlock;

        public Frame2()
        {
            InitializeComponent();

            setController(new DashboardController(this));
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
            totalProductsTxtBlock = txtBlockBuilder.activate(this, "totalProducts_txt");
            totalProductsTxtBlock.setText("");

            totalOrderINTxtBlock = txtBlockBuilder.activate(this, "totalOrderIN_txt");
            totalOrderOUTTxtBlock = txtBlockBuilder.activate(this, "totalOrderOUT_txt");

            getController().callMethod("totalProducts");
        }

        public void setTotalProducts(String total)
        {
            this.Dispatcher.Invoke(() =>
            {
                totalProductsTxtBlock.setText(total);
            });
            getController().callMethod("totalOrderIN");
        }

        public void setTotalOrderIN(String total)
        {
            this.Dispatcher.Invoke(() =>
            {
                totalOrderINTxtBlock.setText(total);
            });
            getController().callMethod("totalOrderOUT");
        }

        public void setTotalOrderOUT(String total)
        {
            this.Dispatcher.Invoke(() =>
            {
                totalOrderOUTTxtBlock.setText(total);
            });
        }
    }
}
