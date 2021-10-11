using Autodesk.Revit.DB;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Floor_standing_scaffolding_design_software.Models
{
    class ScaffoldFilter : ISelectionFilter
    {
        private Document m_doc;

        public bool AllowElement(Element elem)
        {
            FilteredElementCollector collector = new FilteredElementCollector(m_doc);
            collector.OfCategory(BuiltInCategory.OST_GenericModel).OfClass(typeof(FamilyInstance));
            List<Element> element = new List<Element>();
            foreach(var item in collector)
            {
                if(item.Name== "一字型落地脚手架")
                {
                    return true;
                }
               else if (item.Name == "闭合型脚手架（转角90度）")
                {
                    return true;
                }
               else if (item.Name == "端点立杆90")
                {
                    return true;
                }
            }
            return false;
        }

        public bool AllowReference(Reference reference, XYZ position)
        {
            return false;
        }
    }
}
