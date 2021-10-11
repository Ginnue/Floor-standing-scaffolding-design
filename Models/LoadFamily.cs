using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Floor_standing_scaffolding_design_software.Models
{
    class LoadFamily
    {
        // 对活动文档的引用
        private Document m_doc;
        public static FailureDefinitionId m_idWarning;
        public static FailureDefinitionId m_idError;
        private FailureDefinition m_fdWarning;
        private FailureDefinition m_fdError;

        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }
        public Result OnStartup(UIControlledApplication application)
        {
            try
            {
                // Create failure definition Ids
                Guid guid1 = new Guid("0C3F66B5-3E26-4d24-A228-7A8358C76D39");
                Guid guid2 = new Guid("93382A45-89A9-4cfe-8B94-E0B0D9542D34");
                Guid guid3 = new Guid("A16D08E2-7D06-4bca-96B0-C4E4CC0512F8");
                m_idWarning = new FailureDefinitionId(guid1);
                m_idError = new FailureDefinitionId(guid2);

                // Create failure definitions and add resolutions
                //创建故障定义并添加解决方案
                m_fdWarning = FailureDefinition.CreateFailureDefinition(m_idWarning, FailureSeverity.Warning, "I am the warning.");
                m_fdError = FailureDefinition.CreateFailureDefinition(m_idError, FailureSeverity.Error, "I am the error");

                m_fdWarning.AddResolutionType(FailureResolutionType.MoveElements, "MoveElements", typeof(DeleteElements));
                m_fdWarning.AddResolutionType(FailureResolutionType.DeleteElements, "DeleteElements", typeof(DeleteElements));
                m_fdWarning.SetDefaultResolutionType(FailureResolutionType.DeleteElements);

                m_fdError.AddResolutionType(FailureResolutionType.DetachElements, "DetachElements", typeof(DeleteElements));
                m_fdError.AddResolutionType(FailureResolutionType.DeleteElements, "DeleteElements", typeof(DeleteElements));
                m_fdError.SetDefaultResolutionType(FailureResolutionType.DeleteElements);
            }
            catch (System.Exception)
            {
                return Result.Failed;
            }

            return Result.Succeeded;
        }
        public LoadFamily(Document doc)
        {         
            m_doc =doc;
        }
        public void Loadscaffold()
        {
            FamilySymbol planksType = null;
            //在文档中找到名字为"一字型落地脚手架"的常规模型类型
            ElementFilter planksCategoryFilter = new ElementCategoryFilter(BuiltInCategory.OST_GenericModel);
            ElementFilter familySymbolFilter = new ElementClassFilter(typeof(FamilySymbol));
            LogicalAndFilter andFilter = new LogicalAndFilter(planksCategoryFilter, familySymbolFilter);
            FilteredElementCollector planksSymbols = new FilteredElementCollector(m_doc);
            planksSymbols = planksSymbols.WherePasses(andFilter);
            bool symbolFound = false;
            foreach (FamilySymbol element in planksSymbols)
            {
                if (element.Name == "一字型落地脚手架")
                {
                    symbolFound = true;
                    planksType = element;
                    break;
                }
            }
            //如果没有找到，就加载一个族文件
            if (!symbolFound)
            {
                try
                {
                    try
                    {
                        string file = @"C:\Users\l'L\Desktop\脚手架族（共享参数）\一字型脚手架\一字型落地脚手架.rfa";
                        //开事务载入族
                        Transaction Trans0 = new Transaction(m_doc, "载入脚手架");
                        FailureHandlingOptions options = Trans0.GetFailureHandlingOptions();
                        FailurePreproccessor preproccessor = new FailurePreproccessor();
                        options.SetFailuresPreprocessor(preproccessor);
                        Trans0.SetFailureHandlingOptions(options);
                        Trans0.Start();
                        m_doc.LoadFamily(file);
                        Trans0.Commit();
                    }
                    catch (System.Exception)
                    {
                        TaskDialog.Show("Revit", "Failed to commit transaction Warning_FailurePreproccessor_OverlappedScaffold");
                    }
                }
                catch
                {
                    TaskDialog.Show("Revit", "无法载入族！");
                }

            }
        }
        public void LoadLoopscaffold()
        {
            FamilySymbol planksType = null;
            //在文档中找到名字为"闭合型脚手架（转角90度）"的常规模型类型
            ElementFilter planksCategoryFilter = new ElementCategoryFilter(BuiltInCategory.OST_GenericModel);
            ElementFilter familySymbolFilter = new ElementClassFilter(typeof(FamilySymbol));
            LogicalAndFilter andFilter = new LogicalAndFilter(planksCategoryFilter, familySymbolFilter);
            FilteredElementCollector planksSymbols = new FilteredElementCollector(m_doc);
            planksSymbols = planksSymbols.WherePasses(andFilter);
            bool symbolFound = false;
            foreach (FamilySymbol element in planksSymbols)
            {
                if (element.Name == "闭合型脚手架（转角90度）")
                {
                    symbolFound = true;
                    planksType = element;
                    break;
                }
            }
            //如果没有找到，就加载一个族文件
            if (!symbolFound)
            {
                try
                {                  
                    try
                    {
                        string file = @"C:\Users\l'L\Desktop\脚手架族（共享参数）\闭合脚手架\闭合型脚手架（转角90度）.rfa";
                        //开事务载入族
                        Transaction Trans0 = new Transaction(m_doc, "载入脚手架");
                        FailureHandlingOptions options = Trans0.GetFailureHandlingOptions();
                        FailurePreproccessor preproccessor = new FailurePreproccessor();
                        options.SetFailuresPreprocessor(preproccessor);
                        Trans0.SetFailureHandlingOptions(options);
                        Trans0.Start();
                        m_doc.LoadFamily(file);
                        Trans0.Commit();
                    }
                    catch (System.Exception)
                    {
                        TaskDialog.Show("Revit", "Failed to commit transaction Warning_FailurePreproccessor_OverlappedScaffold");
                    }
                }
                catch
                {
                    TaskDialog.Show("Revit", "无法生成载入族！");
                }
            }
        }
        public void LoadCorner()
        {

            FamilySymbol planksType = null;
            //在文档中找到名字为"端点立杆90"的常规模型类型
            ElementFilter planksCategoryFilter = new ElementCategoryFilter(BuiltInCategory.OST_GenericModel);
            ElementFilter familySymbolFilter = new ElementClassFilter(typeof(FamilySymbol));
            LogicalAndFilter andFilter = new LogicalAndFilter(planksCategoryFilter, familySymbolFilter);
            FilteredElementCollector planksSymbols = new FilteredElementCollector(m_doc);
            planksSymbols = planksSymbols.WherePasses(andFilter);
            bool symbolFound = false;
            foreach (FamilySymbol element in planksSymbols)
            {
                if (element.Name == "端点立杆90")
                {
                    symbolFound = true;
                    planksType = element;
                    break;
                }
            }
            //如果没有找到，就加载一个族文件
            if (!symbolFound)
            {
                try
                {
                    try
                    {
                        string file = @"C:\Users\l'L\Desktop\脚手架族（共享参数）\闭合脚手架\端点立杆90.rfa";
                        //开事务载入族
                        Transaction Trans0 = new Transaction(m_doc, "载入脚手架");
                        FailureHandlingOptions options = Trans0.GetFailureHandlingOptions();
                        FailurePreproccessor preproccessor = new FailurePreproccessor();
                        options.SetFailuresPreprocessor(preproccessor);
                        Trans0.SetFailureHandlingOptions(options);
                        Trans0.Start();
                        m_doc.LoadFamily(file);
                        Trans0.Commit();
                    }
                    catch (System.Exception)
                    {
                        TaskDialog.Show("Revit", "Failed to commit transaction Warning_FailurePreproccessor_OverlappedScaffold");
                    }
                }
                catch
                {
                    TaskDialog.Show("Revit", "无法生成载入族！");
                }
            }
        }
    }
}
