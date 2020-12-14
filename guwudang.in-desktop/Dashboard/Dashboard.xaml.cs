using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
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
using guwudang.Annotations;
using Velacro.Basic;
using Velacro.Chart.LineChart;
using Velacro.UIElements.Basic;

namespace guwudang.Dashboard {
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    ///
    ///
    public partial class Dashboard : MyPage {
        private MyPage frame_1;
        private MyPage frame_2;
        private MyPage frame_3;
        private MyPage frame_4;

        public Dashboard()
        {
            InitializeComponent();

            //frame_1 = new Frame1();
            //frame_2 = new Frame2();
            //frame_3 = new Frame3();
            //frame_4 = new Frame4();
            initialize();
        }

        private void initialize()
        {
            startFrame1();
            startFrame2();
            startFrame3();
            startFrame4();
        }

        public void startFrame1()
        {
            frame1.Navigate(frame_1);
        }

        public void startFrame2()
        {
            frame2.Navigate(frame_2);
        }
        public void startFrame3()
        {
            frame3.Navigate(frame_3);
        }
        public void startFrame4()
        {
            frame4.Navigate(frame_4);
        }
    }
}

