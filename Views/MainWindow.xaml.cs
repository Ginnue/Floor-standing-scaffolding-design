using Autodesk.Revit.UI;
using Floor_standing_scaffolding_design_software.Models;
using Floor_standing_scaffolding_design_software.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Shapes;

namespace Floor_standing_scaffolding_design_software.Views
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        //1.注册外部事件
        StraightScaffold SSCommand = null;
        ExternalEvent SSEvent = null;
        LoopScaffold LSCommand = null;
        ExternalEvent LSEvent = null;
        public MainWindow()
        {
            InitializeComponent();
            //2.初始化
            SSCommand = new StraightScaffold();
            SSEvent = ExternalEvent.Create(SSCommand);

            LSCommand = new LoopScaffold();
            LSEvent = ExternalEvent.Create(LSCommand);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            //更新控件中的数据到数据库
            double h = Convert.ToDouble(height.Text);
            string sqlH = $"update datainfo set data_Height='{h}'  where data_id=1";
            int resultH = MySQLHelper.Update(sqlH);

            double lad = Convert.ToDouble(LateralDistanceText.Text);
            string sqllad = $"update datainfo set data_LateralDistance='{lad}'  where data_id=1";
            int resultlad = MySQLHelper.Update(sqllad);

            double lod = Convert.ToDouble(LongitudinalDistanceText.Text);
            string sqllod = $"update datainfo set data_LongitudinalDistance='{lod}'  where data_id=1";
            int resultlod = MySQLHelper.Update(sqllod);

            double fd = Convert.ToDouble(FloorDistanceText.Text);
            string sqlfd = $"update datainfo set data_FloorDistance='{fd}'  where data_id=1";
            int resultfd = MySQLHelper.Update(sqlfd);
            if (workfloor.Text == "两步一设")
            {
                int WF = 2;
                string sqlwf = $"update datainfo set data_WorkFloor='{WF}'  where data_id=1";
                int resultwf = MySQLHelper.Update(sqlwf);
            }
            else if (workfloor.Text == "三步一设")
            {
                int WF = 3;
                string sqlwf = $"update datainfo set data_WorkFloor='{WF}'  where data_id=1";
                int resultwf = MySQLHelper.Update(sqlwf);
            }
           //3.执行命令
            if (looptype.Text == "一字型脚手架")
            {
                SSEvent.Raise();
            }

            else if (looptype.Text == "封闭型脚手架")
            {
                LSEvent.Raise();
            }
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //combobox数据初始化
            UIViewModel uIViewModel = new UIViewModel();
            List<string> list1 = uIViewModel.ScaffoldTypes();
            type.DataContext = list1;
            type.SelectedIndex = 0;
            List<string> list2 = uIViewModel.LoopTypes();
            looptype.DataContext = list2;
            looptype.SelectedIndex = 0;
            List<string> list3 = uIViewModel.OperationFloors();
            workfloor.DataContext = list3;
            workfloor.SelectedIndex = 0;
            //textbox数据初始化
            ScaffoldData sd_WPF = new ScaffoldData();
            sd_WPF.GetData();
            height.Text = sd_WPF.Height.ToString();
            LateralDistanceText.Text = sd_WPF.LateralDistance.ToString();
            LongitudinalDistanceText.Text = sd_WPF.LongitudinalDistance.ToString();
            FloorDistanceText.Text = sd_WPF.FloorDistance.ToString();
        }

        private void Volt_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            Dat_input_box.TextBox_Pasting(sender, e);
        }
        private void Volt_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            Dat_input_box.TextBox_OnPreviewKeyDown(e);
        }
        private void Volt_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Dat_input_box.TextBox_PreviewTextInput(sender, e);
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void WinMove_LeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                this.DragMove();
        }
    }
}
