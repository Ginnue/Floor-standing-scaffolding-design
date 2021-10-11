using Autodesk.Revit.Creation;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
using Autodesk.Revit.UI;
using Floor_standing_scaffolding_design_software.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Document = Autodesk.Revit.DB.Document;

namespace Floor_standing_scaffolding_design_software.Models
{
    class ElementsCreation
    {
        public static FailureDefinitionId m_idWarning;
        public static FailureDefinitionId m_idError;
        private FailureDefinition m_fdWarning;
        private FailureDefinition m_fdError;
        private Application m_revitApp;
        

        private Document m_doc;
        private Level m_level;
        private ModelCurve thiscurve;

        private double Height;
        private double LateralDistance; //横距
        private double LongitudinalDistance;//纵距
        private double FloorDistance;
        private int WorkFloor;

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

        /// <summary>
        /// ElementsBatchCreation 的构造函数1
        /// </summary>
        /// <param name="cmdData">对外部应用程序的引用</param>
        public ElementsCreation(Document doc, ModelCurve modelCurve, double hg, double lad, double lod, double fd,int WF)
        {
            m_doc = doc;
            thiscurve = modelCurve;
            Height = hg;
            LateralDistance = lad;
            LongitudinalDistance = lod;
            FloorDistance = fd;
            WorkFloor = WF;
        }
        // 创建一字型脚手架的方法
        public void Createscaffold()
        {
            try
            {
                try
                {
                    Transaction tran = new Transaction(m_doc, "CreateElement()");
                    FailureHandlingOptions options = tran.GetFailureHandlingOptions();
                    FailurePreproccessor preproccessor = new FailurePreproccessor();
                    options.SetFailuresPreprocessor(preproccessor);
                    tran.SetFailureHandlingOptions(options);
                    tran.Start();
                    CreateElement();
                    m_doc.AutoJoinElements();
                    m_doc.Regenerate();
                    tran.Commit();
                }
                catch (System.Exception)
                {
                    //message = "Failed to commit transaction Warning_FailurePreproccessor_OverlappedWall";
                    //return Result.Failed;
                    TaskDialog.Show("Revit", "Failed to commit transaction Warning_FailurePreproccessor_OverlappedScaffold");
                }
            }
            catch
            {
                TaskDialog.Show("Revit", "无法生成脚手架！");
            }
            
        }
        // 创建封闭脚手架的方法
        public void CreateLoopscaffold()
        {
            try
            {              
                try
                {
                    Transaction tran = new Transaction(m_doc, "CreateElement()");
                    FailureHandlingOptions options = tran.GetFailureHandlingOptions();
                    FailurePreproccessor preproccessor = new FailurePreproccessor();
                    options.SetFailuresPreprocessor(preproccessor);
                    tran.SetFailureHandlingOptions(options);
                    tran.Start();
                    CreateLoopElement();
                    CreateCornerElement();
                    m_doc.AutoJoinElements();
                    m_doc.Regenerate();
                    tran.Commit();
                }
                catch (System.Exception)
                {
                    TaskDialog.Show("Revit", "Failed to commit transaction Warning_FailurePreproccessor_OverlappedScaffold");
                }
            }
            catch
            {
                TaskDialog.Show("Revit", "无法生成脚手架！");
            }
            
        }
        // 生成一字型脚手架
        #region
        private void CreateElement()
        {
            FilteredElementCollector collector = new FilteredElementCollector(m_doc);
            Element ele = collector.OfCategory(BuiltInCategory.OST_GenericModel).OfClass(typeof(FamilySymbol))
                          .FirstOrDefault(x => x.Name == "一字型落地脚手架");
            FamilySymbol scaffold = ele as FamilySymbol;
            if (!scaffold.IsActive)
                scaffold.Activate();
            Curve geoCurve = thiscurve.GeometryCurve;
            m_doc.Create.NewFamilyInstance(geoCurve, scaffold, m_level, StructuralType.NonStructural);
            StraightParameterModification();
        }
        #endregion
        // 生成封闭脚手架
        #region
        private void CreateLoopElement()
        {
            FilteredElementCollector collector = new FilteredElementCollector(m_doc);
            Element ele = collector.OfCategory(BuiltInCategory.OST_GenericModel).OfClass(typeof(FamilySymbol))
                          .FirstOrDefault(x => x.Name == "闭合型脚手架（转角90度）");
            FamilySymbol Loopscaffold = ele as FamilySymbol;
            if (!Loopscaffold.IsActive)
                Loopscaffold.Activate();
            Curve geoCurve = thiscurve.GeometryCurve;
            m_doc.Create.NewFamilyInstance(geoCurve, Loopscaffold, m_level, StructuralType.NonStructural);
            LoopParameterModification();
        }
        #endregion
        // 生成转角立杆
        #region
        private void CreateCornerElement()
        {
            FilteredElementCollector collector = new FilteredElementCollector(m_doc);
            Element ele = collector.OfCategory(BuiltInCategory.OST_GenericModel).OfClass(typeof(FamilySymbol))
                          .FirstOrDefault(x => x.Name == "端点立杆90");
            FamilySymbol Cornerscaffold = ele as FamilySymbol;
            if (!Cornerscaffold.IsActive)
                Cornerscaffold.Activate();
            Curve geoCurve = thiscurve.GeometryCurve;
            XYZ first = geoCurve.GetEndPoint(0);
            m_doc.Create.NewFamilyInstance(first, Cornerscaffold, StructuralType.NonStructural);
            EndpointParameterModification();
        }
        #endregion
        // 此方法用于修改立杆高度及标高等参数
        private void StraightParameterModification()
        {
            FilteredElementCollector collector = new FilteredElementCollector(m_doc);
            collector.OfCategory(BuiltInCategory.OST_GenericModel).OfClass(typeof(FamilyInstance));
            foreach (var item in collector)
            {
                if (item.Name == "一字型落地脚手架")
                {
                    item.LookupParameter("脚手架高度").Set(Height / 0.3048);
                    item.LookupParameter("立杆横距").Set(LateralDistance / 0.3048);
                    item.LookupParameter("立杆纵距").Set(LongitudinalDistance / 0.3048);
                    item.LookupParameter("步距").Set(FloorDistance / 0.3048);
                    item.LookupParameter("作业层n步一设").Set(WorkFloor);
                }
            }
        }
        private void LoopParameterModification()
        {
            FilteredElementCollector collector = new FilteredElementCollector(m_doc);
            collector.OfCategory(BuiltInCategory.OST_GenericModel).OfClass(typeof(FamilyInstance));
            foreach (var item in collector)
            {
                if (item.Name == "闭合型脚手架（转角90度）")
                {
                    item.LookupParameter("脚手架高度").Set(Height / 0.3048);
                    item.LookupParameter("立杆横距").Set(LateralDistance / 0.3048);
                    item.LookupParameter("立杆纵距").Set(LongitudinalDistance / 0.3048);
                    item.LookupParameter("步距").Set(FloorDistance / 0.3048);
                    item.LookupParameter("作业层n步一设").Set(WorkFloor);
                }
            }
        }
        private void EndpointParameterModification()
        {
            FilteredElementCollector collector = new FilteredElementCollector(m_doc);
            collector.OfCategory(BuiltInCategory.OST_GenericModel).OfClass(typeof(FamilyInstance));

            foreach (var item in collector)
            {
                if (item.Name == "端点立杆90")
                {
                    item.LookupParameter("脚手架高度").Set(Height / 0.3048);
                    item.LookupParameter("立杆横距").Set(LateralDistance / 0.3048);                   
                }
            }
        }

        private void FailuresProcessing(object sender, Autodesk.Revit.DB.Events.FailuresProcessingEventArgs e)
        {
            FailuresAccessor failuresAccessor = e.GetFailuresAccessor();
            //failuresAccessor
            String transactionName = failuresAccessor.GetTransactionName();

            IList<FailureMessageAccessor> fmas = failuresAccessor.GetFailureMessages();
            if (fmas.Count == 0)
            {
                e.SetProcessingResult(FailureProcessingResult.Continue);
                return;
            }

            if (transactionName.Equals("Error_FailuresProcessingEvent"))
            {
                foreach (FailureMessageAccessor fma in fmas)
                {
                    FailureDefinitionId id = fma.GetFailureDefinitionId();
                    if (id == ElementsCreation.m_idError)
                    {
                        failuresAccessor.ResolveFailure(fma);
                    }
                }

                e.SetProcessingResult(FailureProcessingResult.ProceedWithCommit);
                return;
            }

            e.SetProcessingResult(FailureProcessingResult.Continue);
        }
    }
    public class FailurePreproccessor : IFailuresPreprocessor
    {
        /// <summary>
        /// This method is called when there have been failures found at the end of a transaction and Revit is about to start processing them. 
        /// 当在事务结束时发现失败并且 Revit 即将开始处理它们时，将调用此方法。
        /// </summary>
        /// <param name="failuresAccessor">The Interface class that provides access to the failure information. </param>
        /// <returns></returns>
        public FailureProcessingResult PreprocessFailures(FailuresAccessor failuresAccessor)
        {
            IList<FailureMessageAccessor> fmas = failuresAccessor.GetFailureMessages();
            if (fmas.Count == 0)
            {
                return FailureProcessingResult.Continue;
            }

            String transactionName = failuresAccessor.GetTransactionName();
            if (transactionName.Equals("Warning_FailurePreproccessor"))
            {
                foreach (FailureMessageAccessor fma in fmas)
                {
                    FailureDefinitionId id = fma.GetFailureDefinitionId();
                    if (id == ElementsCreation.m_idWarning)
                    {
                        failuresAccessor.DeleteWarning(fma);
                    }
                }

                return FailureProcessingResult.ProceedWithCommit;
            }
            else if (transactionName.Equals("Warning_FailurePreproccessor_OverlappedWall"))
            {
                foreach (FailureMessageAccessor fma in fmas)
                {
                    FailureDefinitionId id = fma.GetFailureDefinitionId();
                    if (id == BuiltInFailures.OverlapFailures.WallsOverlap)
                    {
                        failuresAccessor.DeleteWarning(fma);
                    }
                }

                return FailureProcessingResult.ProceedWithCommit;
            }
            else
            {
                return FailureProcessingResult.Continue;
            }
        }
    }

    /// <summary>
    /// Implements the interface IFailuresProcessor
    /// </summary>
    public class FailuresProcessor : IFailuresProcessor
    {
        /// <summary>
        /// This method is being called in case of exception or document destruction to dismiss any possible pending failure UI that may have left on the screen 
        ///在异常或文档破坏的情况下调用此方法以消除可能留在屏幕上的任何可能的挂起失败 UI
        /// </summary>
        /// <param name="document">Document for which pending failures processing UI should be dismissed </param>
        public void Dismiss(Document document)
        {
        }

        /// <summary>
        /// Method that Revit will invoke to process failures at the end of transaction. 
        /// </summary>
        /// <param name="failuresAccessor">Provides all necessary data to perform the resolution of failures.</param>
        /// <returns></returns>
        public FailureProcessingResult ProcessFailures(FailuresAccessor failuresAccessor)
        {
            IList<FailureMessageAccessor> fmas = failuresAccessor.GetFailureMessages();
            if (fmas.Count == 0)
            {
                return FailureProcessingResult.Continue;
            }

            String transactionName = failuresAccessor.GetTransactionName();
            if (transactionName.Equals("Error_FailuresProcessor"))
            {
                foreach (FailureMessageAccessor fma in fmas)
                {
                    FailureDefinitionId id = fma.GetFailureDefinitionId();
                    if (id == ElementsCreation.m_idError)
                    {
                        failuresAccessor.ResolveFailure(fma);
                    }
                }
                return FailureProcessingResult.ProceedWithCommit;
            }
            else
            {
                return FailureProcessingResult.Continue;
            }
        }
    }
}
