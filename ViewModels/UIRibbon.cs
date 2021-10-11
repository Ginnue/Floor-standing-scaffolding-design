using Autodesk.Revit.Attributes;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;


namespace Floor_standing_scaffolding_design_software.ViewModels
{
    [Transaction(TransactionMode.Manual)]
    class UIRibbon : IExternalApplication
    {
        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }

        public Result OnStartup(UIControlledApplication application)
        {
            try
            {
            //1.创建一个RibbonTab
            application.CreateRibbonTab("Scaffold");
            //2.在刚才的RibbonTab中创建UIPanel
            RibbonPanel rp = application.CreateRibbonPanel("Scaffold", "脚手架设计");
            //3.指定程序集的名称以及所使用的类名            
            //获取程序集路径
            string assemblyPath = Assembly.GetExecutingAssembly().Location;
            string classNameCreate = "Floor_standing_scaffolding_design_software.ViewModels.MainProgram"; //命名空间.类名
            //4.创建 PushButton,参数1：在程序内部的名称，必须唯一；参数2：在按钮上显示的名称；参数3：程序集的路径；参数4：命名空间以及类名
             PushButtonData pbd = new PushButtonData("Create", "创建脚手架", assemblyPath, classNameCreate);
            //4.1将 pushButton添加到面板中
            PushButton pushButton = rp.AddItem(pbd) as PushButton;
            //4.2给按钮设置一个图片（大图标一般是32pX，小图标一般是16px）            
            pushButton.LargeImage = new BitmapImage(new Uri("pack://application:,,,/Floor-standing scaffolding design software;component/Assets/Images/create.png", UriKind.Absolute));
            //4.3给按钮设置一个默认提示信息
            pushButton.ToolTip = "Create";

            string classNameCalculation= "Floor_standing_scaffolding_design_software.ViewModels.SafeCalculationDataProgram";
            PushButtonData pbd1 = new PushButtonData("Calculation", "脚手架计算", assemblyPath, classNameCalculation);
            PushButton pushButton1 = rp.AddItem(pbd1) as PushButton;
            pushButton1.LargeImage = new BitmapImage(new Uri("pack://application:,,,/Floor-standing scaffolding design software;component/Assets/Images/calculation.png", UriKind.Absolute));
            pushButton.ToolTip = "Calculation";

            string classNameSchedule = "Floor_standing_scaffolding_design_software.ViewModels.CreatScheduleProgram";
            PushButtonData pbd2 = new PushButtonData("Schedule", "脚手架统计", assemblyPath, classNameSchedule);
            PushButton pushButton2 = rp.AddItem(pbd2) as PushButton;
            pushButton2.LargeImage = new BitmapImage(new Uri("pack://application:,,,/Floor-standing scaffolding design software;component/Assets/Images/Material.png", UriKind.Absolute));
            pushButton.ToolTip = "Schedule";


            return Result.Succeeded;
            }
            catch(Exception ex)
            {
                TaskDialog.Show("Ribbon", ex.ToString());
                return Result.Failed;
            }

        }
    }
}
