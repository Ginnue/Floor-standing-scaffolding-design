using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Floor_standing_scaffolding_design_software.Models
{
    class CreateSchedule
    {
        public ICollection<ViewSchedule> CreateSchedules(UIDocument uiDocument)
        {
            Document document = uiDocument.Document;
            Transaction t = new Transaction(document, "Create Schedules");
            t.Start();
            List<ViewSchedule> schedules = new List<ViewSchedule>();
            //创建常规模型类别的空明细表。     
            ViewSchedule schedule = ViewSchedule.CreateSchedule(document, new ElementId(BuiltInCategory.OST_GenericModel), ElementId.InvalidElementId);
            schedule.Name = "脚手架材料统计表";            
            schedules.Add(schedule);
            List<string> filedName = new List<string>();
            filedName.Add("类型");
            filedName.Add("族");
            filedName.Add("族与类型");
            filedName.Add("立杆总长(m)");
            filedName.Add("大横杆总长(m)");
            filedName.Add("小横杆总长(m)");
            filedName.Add("扣件数量");
            filedName.Add("脚手板面积");
            filedName.Add("钢板网面积");
            //遍历从常规模型视图明细表中获取的所有可调度字段。
            foreach (SchedulableField schedulableField in schedule.Definition.GetSchedulableFields())
            {
                string s = schedulableField.GetName(document);
                bool isContain = filedName.Contains(s);
                if (isContain == true)
                {
                    ElementId parameterId = schedulableField.ParameterId;
                    ScheduleField field = schedule.Definition.AddField(schedulableField);

                    if (Enum.IsDefined(typeof(BuiltInParameter), parameterId.IntegerValue))
                    {
                        BuiltInParameter bip = (BuiltInParameter)parameterId.IntegerValue;
                        //获得数据类型
                        StorageType st = document.get_TypeOfStorage(bip);
                        if (st == StorageType.String || st == StorageType.ElementId)
                        {
                            field.GridColumnWidth = 3 * field.GridColumnWidth;
                            field.HorizontalAlignment = ScheduleHorizontalAlignment.Left;
                        }
                        else
                        {
                            field.HorizontalAlignment = ScheduleHorizontalAlignment.Center;
                        }
                    }
                    if (field.ParameterId == new ElementId(BuiltInParameter.HOST_VOLUME_COMPUTED))
                    {
                        double volumeFilterInCubicFt = 0.8 * Math.Pow(3.2808399, 3.0);
                        ScheduleFilter filter = new ScheduleFilter(field.FieldId, ScheduleFilterType.GreaterThan, volumeFilterInCubicFt);
                        schedule.Definition.AddFilter(filter);
                    }

                    if (field.ParameterId == new ElementId(BuiltInParameter.ELEM_TYPE_PARAM))
                    {
                        ScheduleSortGroupField sortGroupField = new ScheduleSortGroupField(field.FieldId);
                        sortGroupField.ShowHeader = true;
                        schedule.Definition.AddSortGroupField(sortGroupField);
                    }
                }
            }
            t.Commit();
            uiDocument.ActiveView = schedule;
            return schedules;
        }
    }
}
