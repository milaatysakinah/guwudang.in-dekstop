using guwudang.utils;
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
using Velacro.UIElements.TextBlock;

namespace guwudang
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class Dummy : MyPage
    {
        private BuilderTextBlock txtBlockBuilder;
        private IMyTextBlock tokenTxtBlock;

        public Dummy()
        {
            InitializeComponent();

            txtBlockBuilder = new BuilderTextBlock();
            tokenTxtBlock = txtBlockBuilder.activate(this, "token_field");
            User user = new User();

            if(user.getToken() != null)
                tokenTxtBlock.setText("token available");
            else
                tokenTxtBlock.setText("token not available");

        }
    }
}
