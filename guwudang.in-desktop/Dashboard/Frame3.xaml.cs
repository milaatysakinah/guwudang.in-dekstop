using guwudang.Model;
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
    public partial class Frame3 : MyPage
    {
        private BuilderButton buttonBuilder;
        private BuilderTextBox txtBoxBuilder;
        private BuilderTextBlock txtBlockBuilder;
        private List<WeeklyOrder> weeklyOrders = new List<WeeklyOrder>();
        private bool status = false;

        public Frame3()
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
            getWeeklyOrderIN();
        }
        private void getWeeklyOrderIN()
        {
            getController().callMethod("weeklyOrder", "1");
            
        }

        public void setWeeklyOrder(WeeklyOrder weekly)
        {
            weeklyOrders.Add(weekly);
            Console.WriteLine(weeklyOrders.Count);

            if(status == false) { 
                getController().callMethod("weeklyOrder", "2");
                status = true;
            } else
            {
                this.Dispatcher.Invoke(() =>
                {
                    lvWeek.ItemsSource = weeklyOrders;
                });
                status = false;
            }
        }

    }
}
