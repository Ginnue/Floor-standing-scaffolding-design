using Autodesk.Revit.UI;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Floor_standing_scaffolding_design_software.Models
{
    class SafeCalculationData
    {
        private double liveload;
        public double LiveLoad
        {
            get
            {
                return liveload;
            }
            set
            {
                liveload = value;
            }
        }


        private double plankload;
        public double Plankload
        {
            get
            {
                return plankload;
            }
            set
            {
                plankload = value;
            }
        }
        private double plankfloors;
        public double Plankfloors

        {
            get
            {
                return plankfloors;
            }
            set
            {
                plankfloors = value;
            }
        }
        private double railingload;
        public double Railingload
        {
            get
            {
                return railingload;
            }
            set
            {
                railingload = value;
            }
        }
        private double safetynetLoad;
        public double SafetynetLoad
        {
            get
            {
                return safetynetLoad;
            }
            set
            {
                safetynetLoad = value;
            }
        }
        private double w0;
        public double W0
        {
            get
            {
                return w0;
            }
            set
            {
                w0 = value;
            }
        }
        private double uz;
        public double Uz
        {
            get
            {
                return uz;
            }
            set
            {
                uz = value;
            }
        }
        private double us;
        public double Us
        {
            get
            {
                return us;
            }
            set
            {
                us = value;
            }
        }

        private double fak;
        public double Fak
        {
            get
            {
                return fak;
            }
            set
            {
                fak = value;
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
                    liveload = Convert.ToDouble(reader["data_LiveLoad"]);
                    plankload = Convert.ToDouble(reader["data_PlankLoad"]);
                    plankfloors = Convert.ToDouble(reader["data_PlankFloors"]);
                    railingload = Convert.ToDouble(reader["data_RailingLoad"]);
                    safetynetLoad = Convert.ToDouble(reader["data_SafetyNetLoad"]);
                    w0 = Convert.ToDouble(reader["data_W0"]);
                    uz = Convert.ToDouble(reader["data_Uz"]);
                    us = Convert.ToDouble(reader["data_Us"]);
                    fak = Convert.ToDouble(reader["data_fak"]);
                }
            }
            catch
            {
                TaskDialog.Show("Revit", "无法连接数据库！");
            }

        }
        private double phi;
        public double PHI { get => phi; set => phi = value; }

        private double changxibi;
        public void GetPHI(double ChangXiBi)
        {
            changxibi = ChangXiBi;

            int qian = Convert.ToInt32(changxibi / 10) * 10;
            int ge = Convert.ToInt32(changxibi % 10);
            string sql = $"select * from gb50018 where Ten={qian}";
            try
            {
                MySqlDataReader reader = MySQLHelper.GetReader2(sql);
                if (reader.Read())
                {
                    phi = Convert.ToDouble(reader[$"Ones_{ge}"]);
                }
            }
            catch
            {
                TaskDialog.Show("Revit", "无法连接数据库！");
            }

        }
    }
}
