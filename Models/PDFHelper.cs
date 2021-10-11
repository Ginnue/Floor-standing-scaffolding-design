using Autodesk.Revit.UI;
using Spire.Pdf;
using Spire.Pdf.Annotations;
using Spire.Pdf.Graphics;
using System;
using System.Drawing;

namespace Floor_standing_scaffolding_design_software.Models
{
    class PDFHelper
    {
        private double height;
        private double lateralDistance;//横距
        private double longitudinalDistance;//纵距
        private double floorDistance;
        private string xiaohengganqiangdu;
        public string Xiaohengganqiangdu
        {
            get
            {
                return xiaohengganqiangdu;
            }
            set
            {
                xiaohengganqiangdu = value;
            }
        }
        private string xiaohenggannaodu;
        public string Xiaohenggannaodu
        {
            get
            {
                return xiaohenggannaodu;
            }
            set
            {
                xiaohenggannaodu = value;
            }
        }
        private string dahengganqiangdu;
        public string Dahengganqiangdu
        {
            get
            {
                return dahengganqiangdu;
            }
            set
            {
                dahengganqiangdu = value;
            }
        }
        private string dahenggannaodu;
        public string Dahenggannaodu
        {
            get
            {
                return dahenggannaodu;
            }
            set
            {
                dahenggannaodu = value;
            }
        }
        private string goujiankanghuayi;
        public string Goujiankanghuayi
        {
            get
            {
                return goujiankanghuayi;
            }
            set
            {
                goujiankanghuayi = value;
            }
        }
        private string changxibi;
        public string Changxibi
        {
            get
            {
                return changxibi;
            }
            set
            {
                changxibi = value;
            }
        }
        private string nfwdx;
        public string Nfwdx
        {
            get
            {
                return nfwdx;
            }
            set
            {
                nfwdx = value;
            }
        }
        private string fwdx;
        public string Fwdx
        {
            get
            {
                return fwdx;
            }
            set
            {
                fwdx = value;
            }
        }
        private string lianqiangjian;
        public string Lianqiangjian
        {
            get
            {
                return lianqiangjian;
            }
            set
            {
                lianqiangjian = value;
            }
        }
        private string djczl;
        public string Djczl
        {
            get
            {
                return djczl;
            }
            set
            {
                djczl = value;
            }
        }


        private double XSigma;
        public double XSIGMA
        {
            get
            {
                return XSigma;
            }
            set
            {
                XSigma = value;
            }
        }
        private double XV;
        public double XV1
        {
            get
            {
                return XV;
            }
            set
            {
                XV = value;
            }
        }
        private double DSigma;
        public double DSIGMA
        {
            get
            {
                return DSigma;
            }
            set
            {
                DSigma = value;
            }
        }
        private double DV;
        public double DVND
        {
            get
            {
                return DV;
            }
            set
            {
                DV = value;
            }
        }
        private double HY;
        public double GJHY
        {
            get
            {
                return HY;
            }
            set
            {
                HY = value;
            }
        }
        private double NG;
        public double HZNG
        {
            get
            {
                return NG;
            }
            set
            {
                NG = value;
            }
        }
        private double WK;
        public double HZWK
        {
            get
            {
                return WK;
            }
            set
            {
                WK = value;
            }
        }
        private double N1;
        public double HZN1
        {
            get
            {
                return N1;
            }
            set
            {
                N1 = value;
            }
        }
        private double MW;
        public double HZMW
        {
            get
            {
                return MW;
            }
            set
            {
                MW = value;
            }
        }
        private double sigma;
        public double Sigma
        {
            get
            {
                return sigma;
            }
            set
            {
                sigma = value;
            }
        }
        private double sigma1;
        public double Sigma1
        {
            get
            {
                return sigma1;
            }
            set
            {
                sigma1 = value;
            }
        }
        private double h;
        public double H
        {
            get
            {
                return h;
            }
            set
            {
                h = value;
            }
        }
        public void GenerateCalculations()
        {
            //创建PdfDocument类对象，加载测试文档
            PdfDocument doc = new PdfDocument();
            try
            {
                doc.LoadFromFile(@"C:\Users\l'L\Desktop\calculation.pdf");
            }
            catch
            {
                TaskDialog.Show("Revit", "找不到指定文件！");
            }
            
            ScaffoldData SD1 = new ScaffoldData();
            SD1.GetData();
            height = SD1.Height;
            lateralDistance = SD1.LateralDistance;
            longitudinalDistance = SD1.LongitudinalDistance;
            floorDistance = SD1.FloorDistance;

            SafeCalculation SCD = new SafeCalculation();
            SCD.Data();
            SCD.CalculationXHGQD();

            #region pg1
            PdfPageBase page = doc.Pages[0];
            //初始化RectangleF类，指定注释添加的位置、注释图标大小
            RectangleF rect00 = new RectangleF(213, 312, 20, 12);
            PdfFreeTextAnnotation textAnnotation = new PdfFreeTextAnnotation(rect00);
            //添加注释内容
            textAnnotation.Text = height.ToString();
            //设置注释属性，包括字体、字号、注释边框粗细、边框颜色、填充颜色等
            PDFSet(textAnnotation, page);

            RectangleF rect01 = new RectangleF(145, 335, 20, 12);
            PdfFreeTextAnnotation textAnnotation01 = new PdfFreeTextAnnotation(rect01);
            textAnnotation01.Text = longitudinalDistance.ToString();
            PDFSet(textAnnotation01, page);

            RectangleF rect02 = new RectangleF(246, 335, 20, 12);
            PdfFreeTextAnnotation textAnnotation02 = new PdfFreeTextAnnotation(rect02);
            textAnnotation02.Text = lateralDistance.ToString();
            PDFSet(textAnnotation02, page);

            RectangleF rect03 = new RectangleF(485, 335, 20, 12);
            PdfFreeTextAnnotation textAnnotation03 = new PdfFreeTextAnnotation(rect03);
            textAnnotation03.Text = floorDistance.ToString();
            PDFSet(textAnnotation03, page);
            #endregion
            #region pg2
            PdfPageBase page1 = doc.Pages[1];

            RectangleF rect11 = new RectangleF(110, 140, 250, 15);
            PdfFreeTextAnnotation textAnnotation11 = new PdfFreeTextAnnotation(rect11);
            textAnnotation11.Text = xiaohengganqiangdu;
            PDFSet(textAnnotation11, page1);

            RectangleF rect12 = new RectangleF(110, 318, 250, 15);
            PdfFreeTextAnnotation textAnnotation12 = new PdfFreeTextAnnotation(rect12);
            textAnnotation12.Text = xiaohenggannaodu;
            PDFSet(textAnnotation12, page1);

            RectangleF rect13 = new RectangleF(110, 686, 250, 15);
            PdfFreeTextAnnotation textAnnotation13 = new PdfFreeTextAnnotation(rect13);
            textAnnotation13.Text = dahengganqiangdu;
            PDFSet(textAnnotation13, page1);

            RectangleF rect14 = new RectangleF(172, 118, 30, 15);
            PdfFreeTextAnnotation textAnnotation14 = new PdfFreeTextAnnotation(rect14);
            textAnnotation14.Text = XSigma.ToString();
            PDFSet(textAnnotation14, page1);

            RectangleF rect15 = new RectangleF(123, 298, 30, 15);
            PdfFreeTextAnnotation textAnnotation15 = new PdfFreeTextAnnotation(rect15);
            textAnnotation15.Text = XV.ToString();
            PDFSet(textAnnotation15, page1);

            RectangleF rect16 = new RectangleF(172, 662, 30, 15);
            PdfFreeTextAnnotation textAnnotation16 = new PdfFreeTextAnnotation(rect16);
            textAnnotation16.Text = DSigma.ToString();
            PDFSet(textAnnotation16, page1);
            #endregion
            #region pg3

            PdfPageBase page2 = doc.Pages[2];

            RectangleF rect21 = new RectangleF(110, 238, 250, 15);
            PdfFreeTextAnnotation textAnnotation21 = new PdfFreeTextAnnotation(rect21);
            textAnnotation21.Text = dahenggannaodu;
            PDFSet(textAnnotation21, page2);

            RectangleF rect22 = new RectangleF(110, 465, 250, 15);
            PdfFreeTextAnnotation textAnnotation22 = new PdfFreeTextAnnotation(rect22);
            textAnnotation22.Text = goujiankanghuayi;
            PDFSet(textAnnotation22, page2);

            RectangleF rect23 = new RectangleF(160, 212, 40, 15);
            PdfFreeTextAnnotation textAnnotation23 = new PdfFreeTextAnnotation(rect23);
            textAnnotation23.Text = DV.ToString();
            PDFSet(textAnnotation23, page2);

            RectangleF rect24 = new RectangleF(200, 440, 50, 15);
            PdfFreeTextAnnotation textAnnotation24 = new PdfFreeTextAnnotation(rect24);
            textAnnotation24.Text = HY.ToString();
            PDFSet(textAnnotation24, page2);

            RectangleF rect25 = new RectangleF(350, 709, 50, 15);
            PdfFreeTextAnnotation textAnnotation25 = new PdfFreeTextAnnotation(rect25);
            textAnnotation25.Text = NG.ToString();
            PDFSet(textAnnotation25, page2);

            #endregion
            #region pg4
            PdfPageBase page3 = doc.Pages[3];

            RectangleF rect31 = new RectangleF(180, 220, 50, 15);
            PdfFreeTextAnnotation textAnnotation31 = new PdfFreeTextAnnotation(rect31);
            textAnnotation31.Text = WK.ToString();
            PDFSet(textAnnotation31, page3);

            RectangleF rect32 = new RectangleF(130, 413, 50, 15);
            PdfFreeTextAnnotation textAnnotation32 = new PdfFreeTextAnnotation(rect32);
            textAnnotation32.Text = N1.ToString();
            PDFSet(textAnnotation32, page3);

            RectangleF rect33 = new RectangleF(130, 613, 50, 15);
            PdfFreeTextAnnotation textAnnotation33 = new PdfFreeTextAnnotation(rect33);
            textAnnotation33.Text = MW.ToString();
            PDFSet(textAnnotation33, page3);

            #endregion
            #region pg5
            PdfPageBase page4 = doc.Pages[4];

            RectangleF rect41 = new RectangleF(390, 235, 250, 15);
            PdfFreeTextAnnotation textAnnotation41 = new PdfFreeTextAnnotation(rect41);
            textAnnotation41.Text = changxibi;
            PDFSet(textAnnotation41, page4);

            RectangleF rect42 = new RectangleF(100, 383, 250, 15);
            PdfFreeTextAnnotation textAnnotation42 = new PdfFreeTextAnnotation(rect42);
            textAnnotation42.Text = nfwdx;
            PDFSet(textAnnotation42, page4);

            RectangleF rect43 = new RectangleF(350, 670, 250, 15);
            PdfFreeTextAnnotation textAnnotation43 = new PdfFreeTextAnnotation(rect43);
            textAnnotation43.Text = changxibi;
            PDFSet(textAnnotation43, page4);

            RectangleF rect44 = new RectangleF(123, 363, 200, 15);
            PdfFreeTextAnnotation textAnnotation44 = new PdfFreeTextAnnotation(rect44);
            textAnnotation44.Text = sigma.ToString();
            PDFSet(textAnnotation44, page4);

            #endregion

            #region pg6
            PdfPageBase page5 = doc.Pages[5];

            RectangleF rect51 = new RectangleF(195, 100, 30, 15);
            PdfFreeTextAnnotation textAnnotation51 = new PdfFreeTextAnnotation(rect51);
            textAnnotation51.Text = sigma1.ToString();
            PDFSet(textAnnotation51, page5);

            RectangleF rect52 = new RectangleF(100, 122, 250, 15);
            PdfFreeTextAnnotation textAnnotation52 = new PdfFreeTextAnnotation(rect52);
            textAnnotation52.Text = fwdx;
            PDFSet(textAnnotation52, page5);

            RectangleF rect53 = new RectangleF(395, 552, 30, 15);
            PdfFreeTextAnnotation textAnnotation53 = new PdfFreeTextAnnotation(rect53);
            textAnnotation53.Text = h.ToString();
            PDFSet(textAnnotation53, page5);
            #endregion
            #region pg7
            PdfPageBase page6 = doc.Pages[6];
            RectangleF rect61 = new RectangleF(105, 217, 250, 15);
            PdfFreeTextAnnotation textAnnotation61 = new PdfFreeTextAnnotation(rect61);
            textAnnotation61.Text = lianqiangjian;
            PDFSet(textAnnotation61, page6);

            #endregion
            #region pg8
            PdfPageBase page7 = doc.Pages[7];

            RectangleF rect71 = new RectangleF(105, 146, 250, 15);
            PdfFreeTextAnnotation textAnnotation71 = new PdfFreeTextAnnotation(rect71);
            textAnnotation71.Text = djczl;
            PDFSet(textAnnotation71, page7);
            #endregion
            //保存并打开文档
            doc.SaveToFile(@"C:\Users\l'L\Desktop\calculation.pdf", FileFormat.PDF);
        }



        public void PDFSet(PdfFreeTextAnnotation text, PdfPageBase page)
        {
            PdfFont font = new PdfFont(PdfFontFamily.TimesRoman, 12);
            PdfAnnotationBorder border = new PdfAnnotationBorder(0.75f);
            text.Font = font;
            text.Border = border;
            text.BorderColor = Color.Red;
            text.LineEndingStyle = PdfLineEndingStyle.Circle;
            text.Color = Color.Transparent;
            text.Opacity = 0.8f;

            page.AnnotationsWidget.Add(text);
        }
    }
}
