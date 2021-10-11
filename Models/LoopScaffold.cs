using System;
using System.Collections.Generic;
using System.Text;
using Autodesk.Revit;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.Creation;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;

namespace Floor_standing_scaffolding_design_software.Models
{
    [Transaction(TransactionMode.Manual)]
    class LoopScaffold : IExternalEventHandler
    {
        //public double Height;
        //public double LateralDistance; //横距
        //public double LongitudinalDistance;//纵距
        //public double FloorDistance;
        //public int WorkFloor;
        public void Execute(UIApplication app)
        {
            //1.获取当前文档
            UIDocument uiDoc = app.ActiveUIDocument;
            Autodesk.Revit.DB.Document doc = uiDoc.Document;
            //2.载入相关族
            LoadFamily lof = new LoadFamily(doc);
            lof.LoadLoopscaffold();
            lof.LoadCorner();
            //3.获取脚手架数据
            ScaffoldData SD = new ScaffoldData();
            SD.GetData();
            double Height = SD.Height;
            double LateralDistance = SD.LateralDistance;
            double LongitudinalDistance = SD.LongitudinalDistance;
            double FloorDistance = SD.FloorDistance;
            int WorkFloor = SD.WorkFloor;
            //4.创建封闭脚手架
            #region
            Selection selection = uiDoc.Selection;
            try
            {
                //用户自行选择元素(只能选择模型线)
                IList<Reference> referenceCollection = selection.PickObjects(ObjectType.Element, new ModelCurveFilter(), "请选择模型线");

                if (0 == referenceCollection.Count)
                {
                    TaskDialog.Show("Revit", "你没有选任何模型线");
                }
                else
                {
                    foreach (Reference reference in referenceCollection) //遍历用户所选模型线集合
                    {
                        ModelCurve mcurve = doc.GetElement(reference) as ModelCurve;//获取模型线
                        ElementsCreation gls = new ElementsCreation(doc, mcurve, Height, LateralDistance, LongitudinalDistance, FloorDistance, WorkFloor);

                        gls.CreateLoopscaffold();
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            #endregion
        }
        public string GetName()
        {
            return "LoopScaffold";
        }
    }
}
