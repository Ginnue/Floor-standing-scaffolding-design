using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Floor_standing_scaffolding_design_software.Models
{
    class SafeCalculation
    {
        private double Height;
        private double LateralDistance; //横距
        private double LongitudinalDistance;
        private double FloorDistance;

        private double LiveLoad;
        private double PlankLoad;
        private double PlankFloors;
        private double RailingLoad;
        private double SafetyNetLoad;

        private double W0;
        private double Uz;
        private double Us;
        private double fak;
        private double Ag = 0.25;
        private double mf = 0.4;

        private double phi;
        public void Data()
        {
            SafeCalculationData SD = new SafeCalculationData();
            SD.GetData();
            LiveLoad = SD.LiveLoad;
            PlankLoad = SD.Plankload;
            PlankFloors = SD.Plankfloors;
            RailingLoad = SD.Railingload;
            SafetyNetLoad = SD.SafetynetLoad;
            W0 = SD.W0;
            Uz = SD.Uz;
            Us = SD.Us;
            fak = SD.Fak;
            ScaffoldData SD1 = new ScaffoldData();
            SD1.GetData();
            Height = SD1.Height;
            LateralDistance = SD1.LateralDistance;
            LongitudinalDistance = SD1.LongitudinalDistance;
            FloorDistance = SD1.FloorDistance;
        }
        double A = 4.241 * Math.Pow(10, -4);//m2
        double W = 4.493;//cm2
        double ff = 205;
        double GammaG = 1.2;
        double GammaQ = 1.4;
        double Gamma0 = 1.0;
        //连墙件n步m跨
        int n = 2;
        int m = 3;
        double ii = 1.6;//cm  计算立杆回转半径

        private double XSigma;
        public double XSIGMA { get => XSigma; set => XSigma = value; }
        

        //1.小横杆计算
        public string  CalculationXHGQD()
        {
            double XP1 = 0.038;
            double XP2 = PlankLoad * LongitudinalDistance / 2;
            double XQ = LiveLoad * LongitudinalDistance / 2;
            double Xq = GammaG * XP1 + GammaG * XP2 + GammaQ * XQ;
            double XM = Xq * Math.Pow(LateralDistance, 2) / 8;
            XSigma = 1 * XM * Math.Pow(10, 6) / (W * Math.Pow(10, 3));
            string a;
            if (XSigma < 205)
            {
                a = "小横杆的计算强度小于205.0N/mm2,满足要求!";
            }
            else
            {
                a = "小横杆强度不满足要求！（建议调整排距、纵距或增加主节点间的横杆根数）";
            }
            return a;
        }
        private double XV;
        public double XV1 { get => XV; set => XV = value; }
        public string CalculationXHGND()
        {
            double XP1 = 0.038;
            double XP2 = PlankLoad * LongitudinalDistance / 2;
            double XQ = LiveLoad * LongitudinalDistance / 2;
            double XNq = XP1 + XP2 + XQ;//KN/m
            XV = 5 * XNq * Math.Pow(LateralDistance * 1000, 4) / (384 * 2.06 * Math.Pow(10, 5) * 107831.2);//mm,107831.2有待考究
            string b;
            if (XV < (LateralDistance * 1000 / 150) && XV < 10)
            {
                b = "小横杆的最大挠度小于" + LateralDistance * 1000 / 150 + "与10mm,满足要求!";
            }
            else
            {
                b = "小横杆挠度不满足要求！（建议调整排距、纵距或增加主节点间的横杆根数）";
            }
            return b;
        }
        private double DSigma;
        public double DSIGMA { get => DSigma; set => DSigma = value; }
        public string CalculationDHGQD()
        {
            double XP1 = 0.038;
            double DP1 = XP1 * LateralDistance;
            double DP2 = PlankLoad * LateralDistance * LongitudinalDistance / 2;
            double DQ = LiveLoad * LateralDistance * LongitudinalDistance / 2;
            double DP = (1.2 * DP1 + 1.2 * DP2 + 1.4 * DQ) / 2;
            double DM = 0.08 * (1.2 * XP1) * Math.Pow(LongitudinalDistance, 2) + 0.175 * DP * LongitudinalDistance;
            DSigma = 1 * DM * Math.Pow(10, 6) / (W * Math.Pow(10, 3));
            string c;
            if (DSigma < 205)
            {
                c = "大横杆的计算强度小于205.0N/mm2,满足要求!";
            }
            else
            {
                c = "大横杆不满足要求！（建议调整排距、纵距或增加主节点间的横杆根数）";
            }
            return c;
        }
        private double DV;
        public double DVND { get => DV; set => DV = value; }
        public string CalculationDHGND()
        {
            double XP1 = 0.038;
            double XP2 = PlankLoad * LongitudinalDistance / 2;
            double XQ = LiveLoad * LongitudinalDistance / 2;
            double DP1 = XP1 * LateralDistance;
            double DP2 = PlankLoad * LateralDistance * LongitudinalDistance / 2;
            double DQ = LiveLoad * LateralDistance * LongitudinalDistance / 2;
            double DP = (1.2 * DP1 + 1.2 * DP2 + 1.4 * DQ) / 2;
            double DV1 = 0.677 * XP1 * Math.Pow(LongitudinalDistance * 1000, 4) / (100 * 2.06 * Math.Pow(10, 5) * 107831.2);
            double DVP = (DP1 + DP2 + DQ) / 2;
            double DV2 = 1.146 * DVP * 1000 * Math.Pow(LongitudinalDistance * 1000, 3) / (100 * 2.06 * Math.Pow(10, 5) * 107831.2);
            DV = DV1 + DV2;
            double XNq = XP1 + XP2 + XQ;//KN/m
            double XV = 5 * XNq * Math.Pow(LateralDistance * 1000, 4) / (384 * 2.06 * Math.Pow(10, 5) * 107831.2);//mm,107831.2有待考究
            string d;
            if (DV < (LongitudinalDistance * 1000 / 150) && XV < 10)
            {
                d = "大横杆的最大挠度小于" + LongitudinalDistance * 1000 / 150 + "与10mm,满足要求!";
            }
            else
            {
                d = "大横杆挠度不满足要求！（建议调整排距、纵距或增加主节点间的横杆根数）";
            }
            return d;
        }
        private double HY;
        public double GJHY { get => HY; set => HY = value; }
        public string CalculationKHY()
        {
            //3.构件抗滑移计算
            double XP1 = 0.038;
            double HYP1 = XP1 * LongitudinalDistance;
            double HYP2 = PlankLoad * LateralDistance * LongitudinalDistance / 2;
            double HYQ = LiveLoad * LateralDistance * LongitudinalDistance / 2;
            HY = Gamma0 * (GammaG * HYP1 + GammaG * HYP2 + GammaQ * HYQ);
            string e;
            if (HY < 20)
            {
                e = "扣件抗滑承载力的设计计算满足要求!";
            }
            else
            {
                e = "扣件抗滑承载力的设计计算不满足要求";
            }
            return e;
        }
        private double NG;
        public double HZNG { get => NG; set => NG = value; }
        private double WK;
        public double HZWK { get => WK; set => WK = value; }
        private double N1;
        public double HZN1 { get => N1; set => N1 = value; }
        private double MW;
        public double HZMW { get => MW; set => MW = value; }
        public string CalculationCXB()
        {
            //4.脚手架荷载标准值
            double NG1 = 0.1072 * Height;//0.1072需要考究
            double NG2 = PlankLoad * PlankFloors * LongitudinalDistance * (LateralDistance + 0.3) / 2;//LateralDistance脚手架层数；0.3内排架距结构距离
            double NG3 = RailingLoad * LongitudinalDistance * PlankFloors;
            double NG4 = SafetyNetLoad * LongitudinalDistance * Height;
            NG = NG1 + NG2 + NG3 + NG4;
            double NQ = LiveLoad * 2 * LongitudinalDistance * LateralDistance / 2;
            WK = W0 * Uz * Us;//有计算，需完善
            N1 = GammaG * NG + GammaQ * NQ;
            MW = 1.4 * 0.6 * 0.05 * 0.6 * WK * LongitudinalDistance * Math.Pow(FloorDistance * n, 2);
            //5.立杆稳定性计算
            //不考虑风荷载
            double l0 = 1.155 * LongitudinalDistance * FloorDistance;
            double Lambda = (l0 * 1000) / (ii * 10);
            double Lambda0 = (Gamma0 * 1.5 * FloorDistance * 1000) / (ii * 10);
            try
            {
                SafeCalculationData CX = new SafeCalculationData();
                CX.GetPHI(Lambda);
                phi = CX.PHI;
            }
            catch
            {
                phi = 0;
            }
            string f;
            if (Lambda < 210 && Lambda0 < 210)
            {
                f = "长细比验算满足要求！";
            }
            else
            {
                f = "长细比验算不满足要求！";
            }
            return f;
        }
        private double sigma;
        public double Sigma { get => sigma; set => sigma = value; }
        public string CalculationWFWDX()
        {
            //4.脚手架荷载标准值
            double NG1 = 0.1072 * Height;//0.1072需要考究
            double NG2 = PlankLoad * PlankFloors * LongitudinalDistance * (LateralDistance + 0.3) / 2;//LateralDistance脚手架层数；0.3内排架距结构距离
            double NG3 = RailingLoad * LongitudinalDistance * PlankFloors;
            double NG4 = SafetyNetLoad * LongitudinalDistance * Height;
            double NG = NG1 + NG2 + NG3 + NG4;
            double NQ = LiveLoad * 2 * LongitudinalDistance * LateralDistance / 2;
            double WK = W0 * Uz * Us;//有计算，需完善
            double N1 = GammaG * NG + GammaQ * NQ;
            double MW = 1.4 * 0.6 * 0.05 * 0.6 * WK * LongitudinalDistance * Math.Pow(FloorDistance * n, 2);
            sigma = Gamma0 * N1 * 1000 / (phi * A * Math.Pow(10, 6));
            string g;
            if (sigma < 205)
            {
                g = "不考虑风荷载时，立杆稳定性满足要求！";
            }
            else
            {
                g = "不考虑风荷载时，立杆稳定性不满足要求！（建议调整立杆间距或步距）";
            }
            return g;
        }
        private double sigma1;
        public double Sigma1 { get => sigma1; set => sigma1 = value; }
        public string CalculationYFWDX()
        {
            double NG1 = 0.1072 * Height;//0.1072需要考究
            double NG2 = PlankLoad * PlankFloors * LongitudinalDistance * (LateralDistance + 0.3) / 2;//LateralDistance脚手架层数；0.3内排架距结构距离
            double NG3 = RailingLoad * LongitudinalDistance * PlankFloors;
            double NG4 = SafetyNetLoad * LongitudinalDistance * Height;
            double NG = NG1 + NG2 + NG3 + NG4;
            double NQ = LiveLoad * 2 * LongitudinalDistance * LateralDistance / 2;
            double WK = W0 * Uz * Us;//有计算，需完善
            double N1 = GammaG * NG + GammaQ * NQ;
            double MW = 1.4 * 0.6 * 0.05 * 0.6 * WK * LongitudinalDistance * Math.Pow(FloorDistance * n, 2);
            double sigma = Gamma0 * N1 * 1000 / (phi * A * Math.Pow(10, 6));
            //考虑风荷载
            sigma1 = Gamma0 * N1 * 1000 / (phi * A * Math.Pow(10, 6)) + Gamma0 * MW * Math.Pow(10, 6) / (W * Math.Pow(10, 3));
            string h;
            if (sigma1 < 205)
            {
                h = "考虑风荷载时，立杆稳定性满足要求！";
            }
            else
            {
                h = "考虑风荷载时，立杆稳定性不满足要求！（建议调整立杆间距或步距）";
            }
            return h;
        }
        private double h;
        public double H { get => h; set => h = value; }
        public string CalculationDSGD()
        {
            double NG1 = 0.1072 * Height;//0.1072需要考究
            double NG2 = PlankLoad * PlankFloors * LongitudinalDistance * (LateralDistance + 0.3) / 2;//LateralDistance脚手架层数；0.3内排架距结构距离
            double NG3 = RailingLoad * LongitudinalDistance * PlankFloors;
            double NG4 = SafetyNetLoad * LongitudinalDistance * Height;
            double NG = NG1 + NG2 + NG3 + NG4;
            double NQ = LiveLoad * 2 * LongitudinalDistance * LateralDistance / 2;
            double WK = W0 * Uz * Us;//有计算，需完善
            double N1 = GammaG * NG + GammaQ * NQ;
            double MW = 1.4 * 0.6 * 0.05 * 0.6 * WK * LongitudinalDistance * Math.Pow(FloorDistance * n, 2);
            double sigma = Gamma0 * N1 * 1000 / (phi * A * Math.Pow(10, 6));
            //6.最大搭设高度计算
            double NG2K = NG2 + NG3 + NG4;
            double NQK = NQ;
            double gK = 0.1072;
            double NXie = -0.001;//轴向力钢丝绳卸荷，有待考究。
            double sigmaSJ = 205;
            //不考虑风荷载时
            double HNF = (phi * A * sigmaSJ * Math.Pow(10, 3) - (GammaG * NG2K + GammaQ * NQK - NXie)) / (GammaG * gK);
            //考虑风荷载时
            double MWK = 0.161;//计算立杆段由风荷载标准值产生的弯矩，有待考究。
            double HF = (
                            phi * A * sigmaSJ * Math.Pow(10, 3) -
                                (GammaG * NG2K + 0.9 * GammaQ *
                                    (NQK + phi * A * Math.Pow(10, 4) * MWK * 100 / (W * Math.Pow(10, 3))
                                              ) - NXie)) / (GammaG * gK);//这个数有问题
            string i;
            if (HNF < HF)
            {
                string HNFString = HNF.ToString("0.0");
                h = HNF;
                i = $"脚手架允许搭设高度[H]={HNFString}m";
            }
            else
            {
                string HFString = HF.ToString("0.0");
                h = HF;
                i = $"脚手架允许搭设高度[H]={HFString}m";
            }
            return i;
        }

        public string CalculationLQJ()
        {
            double NG1 = 0.1072 * Height;//0.1072需要考究
            double NG2 = PlankLoad * PlankFloors * LongitudinalDistance * (LateralDistance + 0.3) / 2;//LateralDistance脚手架层数；0.3内排架距结构距离
            double NG3 = RailingLoad * LongitudinalDistance * PlankFloors;
            double NG4 = SafetyNetLoad * LongitudinalDistance * Height;
            double NG = NG1 + NG2 + NG3 + NG4;
            double NQ = LiveLoad * 2 * LongitudinalDistance * LateralDistance / 2;
            double WK = W0 * Uz * Us;//有计算，需完善
            //7.连墙件计算
            double AW = n * FloorDistance * m * LongitudinalDistance;
            double NlW = 1.4 * WK * AW;
            double No = 3;
            double Nl = NlW + No;
            double Ac = 4.24 * Math.Pow(10, -4);
            double Aa = 18.1;//cm2,毛截面面积
            double Nf1 = 0.85 * Ac * ff * Math.Pow(10, 3) / Gamma0;
            double phi2 = 0.95;//此系数需进一步核实
            double Nf2 = 0.85 * phi2 * Aa * Math.Pow(10, -4) * ff * Math.Pow(10, 3) / Gamma0;//这个数有问题
            string j;
            if (Nf1 > Nl && Nf2 > Nl)
            {
                j = "连墙件的设计计算满足强度设计要求";
            }
            else
            {
                j = "连墙件双扣件不满足要求!可以考虑调整连墙件设置或其他方式！";
            }
            return j;
        }
        public string CalculationDJCZL()
        {
            double NG1 = 0.1072 * Height;//0.1072需要考究
            double NG2 = PlankLoad * PlankFloors * LongitudinalDistance * (LateralDistance + 0.3) / 2;//LateralDistance脚手架层数；0.3内排架距结构距离
            double NG3 = RailingLoad * LongitudinalDistance * PlankFloors;
            double NG4 = SafetyNetLoad * LongitudinalDistance * Height;
            double NG = NG1 + NG2 + NG3 + NG4;
            double NQ = LiveLoad * 2 * LongitudinalDistance * LateralDistance / 2;
            double WK = W0 * Uz * Us;//有计算，需完善
            double N1 = GammaG * NG + GammaQ * NQ;
            //8.立杆的地基承载力计算
            double GammaU = 1.254;
            double pk = N1 / Ag;
            double fa = mf * fak;
            string k;
            if (pk <= GammaU * fa)
            {
                k = "地基承载力的计算满足要求！";
            }
            else
            {
                k = "地基承载力的计算不满足要求！（建议处理地基，提高地基承载力或增加立杆）";
            }
            return k;
        }
    }
}
