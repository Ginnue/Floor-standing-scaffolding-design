using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Floor_standing_scaffolding_design_software.ViewModels
{
    class UIViewModel
    {
        public List<string> ScaffoldTypes()
        {
            List<string> ScaffoldType = new List<string>();
            ScaffoldType.Add("双排落地脚手架");
            return ScaffoldType;
        }
        public List<string> LoopTypes()
        {
            List<string> LoopType = new List<string>();
            LoopType.Add("一字型脚手架");
            LoopType.Add("封闭型脚手架");
            return LoopType;
        }
        public List<string> OperationFloors()
        {
            List<string> OperationFloor = new List<string>();
            OperationFloor.Add("两步一设");
            OperationFloor.Add("三步一设");
            return OperationFloor;
        }        
    }
}
