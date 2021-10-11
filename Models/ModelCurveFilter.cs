using Autodesk.Revit.DB;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Floor_standing_scaffolding_design_software.Models
{
    class ModelCurveFilter : ISelectionFilter
    {
        public bool AllowElement(Element element)
        {
            if (element is ModelCurve)
            {
                return true;
            }
            return false;
        }
        public bool AllowReference(Reference refer, XYZ point)
        {
            return false;
        }
    }
}
