using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestMVC.Models;
using System.Drawing.Imaging;
using System.Drawing;

namespace TestMVC.Controllers
{
    public class VeggieController : Controller
    {
        VeggieModel veggieModel = null;
        
        //Constructor
        public VeggieController()
        {
            InitialVeggie();
            ColorArray = InitialColorList(veggieModel.veggies.Count);
        }
        //InitialData
        public void InitialVeggie()
        {
            veggieModel = new VeggieModel();
            veggieModel.AddVeggie("MushRoom", 8);
            veggieModel.AddVeggie("Onion", 3);
            veggieModel.AddVeggie("Olive", 2);
            veggieModel.AddVeggie("Tomato", 6);
            veggieModel.AddVeggie("Carrot", 5);
            veggieModel.AddVeggie("Corn", 7);
            veggieModel.AddVeggie("Potato", 4);
        }

        #region Controllers
        //
        // GET: /Veggie/
        public ActionResult Index()
        {
            return View();
        }
        //
        // GET: /Veggie/PieChart
        public ActionResult PieChart()
        {
            FileContentResult result;
            result = DrawPieChart(veggieModel, 200, 200);
            return result;
        }
        //
        // GET: /Veggie/BarChart
        public ActionResult BarChart()
        {
            FileContentResult result;
            result = DrawBarChart(veggieModel, 200, 200);
            return result;
        }
        #endregion

        #region DrawChart
        List<Color> ColorArray;
        //PieChart
        private FileContentResult DrawPieChart(VeggieModel v,int bitMapW,int bitMapH)
        {
            //Creat Bitmap for Web
            Bitmap bitMap = new Bitmap(bitMapW, bitMapH);
            
            //Create Graphic for Drawing
            Graphics g = Graphics.FromImage(bitMap);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            
            //SumTotal
            int total = 0;
            for (int i = 0; i < v.veggies.Count; i++)
            {
                total += v.veggies[i].Qty;
            }
            //Variable for DrawPie
            float angle = 0;
            float sweep = 0;
            float radiusX = bitMapW * 0.6f;
            float radiusY = bitMapH * 0.6f;
            float pointX = (bitMapW - radiusX) * 0.75f;
            float pointY = (bitMapH - radiusY) * 0.66f;
            //Variable for DrawText
            float y = 0;
            for (int i = 0; i < v.veggies.Count; i++)
            {
                int val = v.veggies[i].Qty;
                sweep = 360f * val / total;
                //Set Color
                Color paintColor = this.ColorArray[i];
                SolidBrush brush = new SolidBrush(paintColor);
                g.FillPie(brush, pointX, pointY, radiusX, radiusY, angle, sweep);
                //NextStartAngle
                angle += sweep;
                //DrawText;
                Font font = new Font("Tahoma", 10, FontStyle.Regular, GraphicsUnit.Pixel);
                g.DrawString(v.veggies[i].Name, font, brush, 0, y);
                y += 10;
            }
            //Create Image MemoryStream
            System.IO.MemoryStream memStream = new System.IO.MemoryStream();
            bitMap.Save(memStream, ImageFormat.Png);
            return this.File(memStream.GetBuffer(), "image/png"); ;
        }

        //BarChart
        private FileContentResult DrawBarChart(VeggieModel v, int bitMapW, int bitMapH)
        {
            //Creat Bitmap for Web
            Bitmap bitMap = new Bitmap(bitMapW, bitMapH);

            //Create Graphic for Drawing
            Graphics g = Graphics.FromImage(bitMap);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            //FindMax
            int max = v.veggies[0].Qty; ;
            for (int i = 0; i < v.veggies.Count; i++)
            {
                if (v.veggies[i].Qty > max)
                {
                    max = v.veggies[i].Qty;
                }
            }
            //Variable for Chart
            float barWidth = bitMapW / v.veggies.Count;
            float barHeight = bitMapH * 0.5f;
            float newY = 0;
            //Variable for DrawText
            float y = 0;
            for (int i = 0; i < v.veggies.Count; i++)
            {
                int val = v.veggies[i].Qty;
                //Set Color
                Color paintColor = this.ColorArray[i];
                SolidBrush brush = new SolidBrush(paintColor);
                newY = bitMapH - (barHeight * v.veggies[i].Qty / max);
                g.FillRectangle(brush, (i * barWidth), newY, barWidth, barHeight * v.veggies[i].Qty / max);
                //DrawText;
                Font font = new Font("Tahoma", 10, FontStyle.Regular, GraphicsUnit.Pixel);
                g.DrawString(v.veggies[i].Name, font, brush, 0, y);
                y += 10;
            }
            //Create Image MemoryStream
            System.IO.MemoryStream memStream = new System.IO.MemoryStream();
            bitMap.Save(memStream, ImageFormat.Png);
            return this.File(memStream.GetBuffer(), "image/png"); ;
        }
        #endregion

        #region Util
        public List<Color> InitialColorList(int count)
        {
            List<Color> tmpColorArray = new List<Color>();
            HashSet<Color> hs = new HashSet<Color>();
            Random rmdColor = new Random();
            for (int i = 0; i < count; i++)
            {
                Color tmpColor;
                while (!hs.Add(tmpColor = Color.FromArgb(rmdColor.Next(0, 255), rmdColor.Next(0, 255), rmdColor.Next(0, 255)))) ;
                tmpColorArray.Add(tmpColor);
            }
            return tmpColorArray;
        }
        #endregion
    }
}
