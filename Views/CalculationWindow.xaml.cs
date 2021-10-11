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
using System.Windows.Shapes;
using Floor_standing_scaffolding_design_software.Models;
using Floor_standing_scaffolding_design_software.ViewModels;

namespace Floor_standing_scaffolding_design_software.Views
{
    /// <summary>
    /// CalculationWindow.xaml 的交互逻辑
    /// </summary>
    public partial class CalculationWindow : Window
    {
        public CalculationWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SafeCalculation SCD = new SafeCalculation();
            SCD.Data();
            TBa.Text = SCD.CalculationXHGQD();
            TBb.Text = SCD.CalculationXHGND();
            TBc.Text = SCD.CalculationDHGQD();
            TBd.Text = SCD.CalculationDHGND();
            TBe.Text = SCD.CalculationKHY();
            TBf.Text = SCD.CalculationCXB();
            TBg.Text = SCD.CalculationWFWDX();
            TBh.Text = SCD.CalculationYFWDX();
            TBi.Text = SCD.CalculationDSGD();
            TBj.Text = SCD.CalculationLQJ();
            TBk.Text = SCD.CalculationDJCZL();

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
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            PDFHelper crt = new PDFHelper();
            crt.Xiaohengganqiangdu = TBa.Text;
            crt.Xiaohenggannaodu = TBb.Text;
            crt.Dahengganqiangdu = TBc.Text;
            crt.Dahenggannaodu = TBd.Text;
            crt.Goujiankanghuayi = TBe.Text;
            crt.Changxibi = TBf.Text;
            crt.Nfwdx = TBg.Text;
            crt.Fwdx = TBh.Text;
            crt.Lianqiangjian = TBj.Text;
            crt.Djczl = TBk.Text;

            SafeCalculation SCD = new SafeCalculation();
            SCD.Data();
            SCD.CalculationXHGQD();
            SCD.CalculationXHGND();
            SCD.CalculationDHGQD();
            SCD.CalculationDHGND();
            SCD.CalculationKHY();
            SCD.CalculationCXB();
            SCD.CalculationWFWDX();
            SCD.CalculationYFWDX();
            SCD.CalculationDSGD();
            SCD.CalculationLQJ();
            SCD.CalculationDJCZL();
            crt.XSIGMA = SCD.XSIGMA;
            crt.XV1 = SCD.XV1;
            crt.DSIGMA = SCD.DSIGMA;
            crt.DVND = SCD.DVND;
            crt.GJHY = SCD.GJHY;
            crt.HZNG = SCD.HZNG;
            crt.HZWK = SCD.HZWK;
            crt.HZN1 = SCD.HZN1;
            crt.HZMW = SCD.HZMW;
            crt.Sigma = SCD.Sigma;
            crt.Sigma1 = SCD.Sigma1;
            crt.H = SCD.H;

            crt.GenerateCalculations();
            this.Close();
        }
    }
}
