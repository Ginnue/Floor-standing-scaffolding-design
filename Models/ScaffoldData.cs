using Autodesk.Revit.UI;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Floor_standing_scaffolding_design_software.Models
{
    class ScaffoldData
    {
        private double height;
        public double Height
        {
            get
            {
                return height;
            }
            set
            {
                height = value;
            }
        }
        private double lateralDistance;//横距
        public double LateralDistance
        {
            get
            {
                return lateralDistance;
            }
            set
            {
                lateralDistance = value;
            }
        }
        private double longitudinalDistance;//纵距
        public double LongitudinalDistance
        {
            get
            {
                return longitudinalDistance;
            }
            set
            {
                longitudinalDistance = value;
            }
        }

        private double floorDistance;
        public double FloorDistance
        {
            get
            {
                return floorDistance;
            }
            set
            {
                floorDistance = value;
            }
        }
        private int workFloor;
        public int WorkFloor
        {
            get
            {
                return workFloor;
            }
            set
            {
                workFloor = value;
            }
        }
        public void GetData()
        {
            string sql = "select * from datainfo where data_id=1";
            try
            {
                MySqlDataReader reader = MySQLHelper.GetReader(sql);
                if (reader.Read())
                {
                    height = Convert.ToDouble(reader["data_Height"]);
                    lateralDistance = Convert.ToDouble(reader["data_LateralDistance"]);
                    longitudinalDistance = Convert.ToDouble(reader["data_LongitudinalDistance"]);
                    floorDistance = Convert.ToDouble(reader["data_FloorDistance"]);
                    workFloor = Convert.ToInt32(reader["data_WorkFloor"]);
                }
            }
            catch
            {
                TaskDialog.Show("Revit", "无法连接数据库！");
            }
            
        }
    }
}
