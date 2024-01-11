using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TreeView;

namespace LAB2_RWP
{
    public partial class MainForm : Form
    {
        Bitmap bmp;
        int bgType=0;
        Color bgColor=Color.White;
        string bgText = "None";
        string pathBgFile;
        Color graphColor=Color.Black;
        int graphType=0;
        float scale = (float)1;
        bool mouseOffset = false;
        bool graphMove = false;
        bool graphOffset = false;
        Point graphShift;
        int shiftX;
        int shiftY;
        Point prevMousePosition;
        PointF maxPoint;
        PointF minPoint;
        //Rain
        bool goRain = false;
        List<Rain> particles = new List<Rain>();
        Random random = new Random();
        float percentOfFill = 0;
        float percentOfFillRect = 0;
        int localeCountTick = 0;
        bool goWind = false;
        int windDirection = 0;
        float prevWindMouseLocation;
        bool windLocation = false;

        bool pointActive=false;

        public MainForm()
        {
            InitializeComponent();
            //буферизация
            typeof(Panel).InvokeMember("DoubleBuffered", BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic, null, panel1, new object[] { true });
            bmp = new Bitmap(panel1.ClientSize.Width, panel1.ClientSize.Height);
            buttonRain.Image = Properties.Resources.dropOn;
            buttonWind.Image = Properties.Resources.miniWind;
            this.panel1.MouseWheel += Panel1_MouseWheel;
            panel1.Paint += panel1_Paint;
        }


//--------------------BACKGROUND RENDER--------------------------------------------------------------------------------------------------------------
        private void BackgroundRendering()
        {
            switch(bgType)
            {
                case 1:
                    {
                        ColorRendering();
                        break;
                    }
                case 2:
                    {
                        ImageRendering();
                        break;
                    }
                case 3:
                    {
                        HatchRendering();
                        break;
                    }
                case 4:
                    {
                        GradientRendering();
                        break;
                    }
                case 5:
                    {
                        TextBackgroundRendering();
                        break;
                    }
                case 6:
                    {
                        GradientAndHatchRendering();
                        break;
                    }
                default: 
                    {
                        ColorRendering();
                        break;
                    }
            }
        }

        private void ImageRendering()
        {
            Bitmap initialImage = new Bitmap(pathBgFile);
            Bitmap convertImage = new Bitmap(initialImage, panel1.ClientSize.Width, panel1.ClientSize.Height);
            Graphics graphics = Graphics.FromImage(bmp);
            graphics.DrawImageUnscaledAndClipped(convertImage, panel1.ClientRectangle);
        }

        private void ColorRendering()
        {
            SolidBrush sBrush = new SolidBrush(bgColor);
            Graphics graph = Graphics.FromImage(bmp);
            graph.FillRectangle(sBrush, panel1.ClientRectangle);
        }

        private void HatchRendering()
        {
            HatchBrush hBrush = new HatchBrush(HatchStyle.ForwardDiagonal, Color.DeepPink, Color.DeepSkyBlue);
            Graphics graph = Graphics.FromImage(bmp);
            graph.FillRectangle(hBrush, panel1.ClientRectangle);
        }

        private void GradientRendering()
        {
            LinearGradientBrush gBrush = new LinearGradientBrush(panel1.ClientRectangle, Color.Pink, Color.PeachPuff, 45);
            Graphics graph = Graphics.FromImage(bmp);
            graph.FillRectangle(gBrush, panel1.ClientRectangle);
        }

        private void TextBackgroundRendering()
        {
            Graphics graph = Graphics.FromImage(bmp);
            string text = bgText;
            Font font = new Font("Arial", 14f*scale);
            Brush brush = new SolidBrush(Color.Red);
            SolidBrush solidBrush = new SolidBrush(Color.White);
            graph.FillRectangle(solidBrush, panel1.ClientRectangle);
            for (int i = 0; i < (panel1.ClientSize.Height+10); i+=(text.Length+50*(int)scale))
            {
                for(int j = 0; j <= (panel1.ClientSize.Width+10); j+= (text.Length + (int)scale*50))
                {
                    graph.DrawString(text, font, brush, j, i);
                }
                
            }

        }

        private void GradientAndHatchRendering()
        { 
            LinearGradientBrush gBrush = new LinearGradientBrush(panel1.ClientRectangle, Color.LightSeaGreen, Color.BlueViolet, 45);
            Graphics graph = Graphics.FromImage(bmp);
            SolidBrush pens = new SolidBrush(Color.White);
            Random random = new Random();
            int maxWidth = panel1.ClientSize.Width;
            int maxHeight = panel1.ClientSize.Height;
            int countSky = 500;
            graph.FillRectangle(gBrush, panel1.ClientRectangle);
            for(int i = 0; i < countSky; i++)
            {
                graph.FillEllipse(pens, random.Next(0, maxWidth), random.Next(0, maxHeight), 5, 5);
            }
        }

//------------------AXIS----------------------------------------------------------------------------------------------------------------------
        private void DrawAxis(PointF start, PointF end)
        {
            Graphics graphics = Graphics.FromImage(bmp);
            Pen pen = new Pen(Color.Black, scale);
            graphics.DrawLine(pen, start, end);
            graphics.Dispose();
        }

        private void DrawEl()
        {
            Pen pen = new Pen(Color.Black, 2f);
            pen.DashStyle = DashStyle.Dash;
            //int deltaX=0, deltaY=0;
            Graphics graphics = Graphics.FromImage(bmp);
            if (scale > 1)
            {
                //deltaX = -shiftX + graphShift.X;
                //deltaY = -shiftY + graphShift.Y;
            }
            graphics.DrawEllipse(pen, -20*scale+panel1.ClientSize.Width/2 + shiftX, -20*scale+panel1.ClientSize.Height/2+shiftY, 40*scale, 40*scale);
            //тестовые единчные кубы размер 1 в пикселях 20
            //graphics.DrawRectangle(pen, -20*scale + panel1.ClientSize.Width / 2, -20*scale + panel1.ClientSize.Height / 2, 40*scale, 40*scale);
            //graphics.DrawRectangle(pen, -0 + panel1.ClientSize.Width / 2, -100 + panel1.ClientSize.Height / 2, 80, 80);
        }
//--------------FUNCTION RENDER------------------------------------------------------------------------------------------------------------------
        
        private void ParabolaRendering()
        {
            Graphics graph = Graphics.FromImage(bmp);
            PointF[] pointsPlus = GeneratedParabolaPoint(1.0f);
            PointF[] pointsMinus = GeneratedParabolaPoint(-1.0f);
            Pen pen = new Pen(graphColor, 2f);
            graph.DrawLines(pen, pointsPlus);
            graph.DrawLines(pen, pointsMinus);
            graph.Dispose();
        }

        private void SinusRendering()
        {
            Graphics graph = Graphics.FromImage(bmp);
            PointF[] pointsPlus = GeneratedSinusPoint(1.0f);
            PointF[] pointsMinus = GeneratedSinusPoint(-1.0f);
            Pen pen = new Pen(graphColor, 2f);
            graph.DrawLines(pen, pointsPlus);
            graph.DrawLines(pen, pointsMinus);
            graph.Dispose();
        }

        private void ParabolaCubeRendering()
        {
            Graphics graph = Graphics.FromImage(bmp);
            PointF[] pointsPlus = GeneratedParCubePoint(1.0f);
            PointF[] pointsMinus = GeneratedParCubePoint(-1.0f);
            Pen pen = new Pen(graphColor, 2f);
            graph.DrawLines(pen, pointsPlus);
            graph.DrawLines(pen, pointsMinus);
            graph.Dispose();
        }

        private void TanRendering()
        {
            Graphics graph = Graphics.FromImage(bmp);
            
            Pen pen = new Pen(graphColor, 2f);
            for(int k=0; k*20<=panel1.ClientSize.Width; k++)
            {
                //Правая часть
                PointF[] pointsPlus = GeneratedTanPoint(1.0f, k, 1.0f);
                PointF[] pointsMinus = GeneratedTanPoint(-1.0f, k, 1.0f);
                //Левая часть
                PointF[] leftpointsPlus = GeneratedTanPoint(1.0f, k, -1.0f);
                PointF[] leftpointsMinus = GeneratedTanPoint(-1.0f, k, -1.0f);
                //graph.DrawLines(pen, pointsPlus);
                //graph.DrawLines(pen, pointsMinus);
                //graph.DrawLines(pen, leftpointsPlus);
                //graph.DrawLines(pen, leftpointsMinus);
            }
            
            graph.Dispose();
        }       

        private void LinearFuncRendering()
        {
            Graphics graph = Graphics.FromImage(bmp);
            PointF[] pointsPlus = GeneratedLinearPoint(1.0f);
            PointF[] pointsMinus = GeneratedLinearPoint(-1.0f);
            Pen pen = new Pen(graphColor, 2f);
            graph.DrawLines(pen, pointsPlus);
            graph.DrawLines(pen, pointsMinus);
            graph.Dispose();
        }

        private void FunctionRendering()
        {
            switch(graphType)
            {
                case 1:
                    {
                        ParabolaRendering();
                        break;
                    }
                case 2:
                    {
                        //ExtremumRendering();
                        SinusRendering();
                        break;
                    }
                case 3:
                    {
                        ParabolaCubeRendering();
                        break;
                    }
                case 4:
                    {
                        TanRendering(); 
                        break;
                    }
                case 5:
                    {
                        LinearFuncRendering();
                        break;
                    }
                default: { break; }
            }
        }
        private void ExtremumRendering()
        {
            Graphics graph = Graphics.FromImage(bmp);
            SolidBrush pens = new SolidBrush(Color.Blue);
            float centreY = panel1.ClientSize.Height / 2;
            float centreX = panel1.ClientSize.Width / 2;
            float minY = centreY * 2;
            float maxY = 0;
            PointF[] points = new PointF[1000];
            CSin func = new CSin();
            int pointcounts = 0;
            int k = 14;
            for (float x = 20 * ((float)Math.PI / 2) + 20 * ((float)Math.PI) - 5 * k; x <= 5 * scale * 20 * ((float)Math.PI / 2) - 20 * ((float)Math.PI) + 5 * k; x += 1f)
            {
                points[pointcounts] = new PointF(2 * 20 * (float)Math.PI + x * scale + centreX + shiftX + graphShift.X, centreY + shiftY + graphShift.Y - scale * 20 * func.Calc(x / 20));
                if (pointcounts > 0) graph.DrawLine(Pens.Red, points[pointcounts - 1], points[pointcounts]);
                pointcounts++;

            }
            if (pointcounts > 0)
            {
                PointF[] pointsPolygon = new PointF[pointcounts];
                for (int i = 0; i < pointcounts; i++)
                {
                    pointsPolygon[i] = points[i];
                }
                graph.FillPolygon(pens, pointsPolygon);
            }

            //MessageBox.Show(maxPoint.ToString());
        }

//-------------POINT GENERATED------------------------------------------------------------------------------------------------------------------
        private PointF[] GeneratedParabolaPoint(float sign)
        {
            float centreY = panel1.ClientSize.Height / 2;
            float centreX = panel1.ClientSize.Width / 2;
            PointF[] points = new PointF[30000];
            CParabola func = new CParabola();
            int pointcounts = 0;
            for (float x = 0.0f; pointcounts < points.Length; x += 0.01f)
            {
                points[pointcounts] = new PointF(sign * x*scale + centreX+shiftX+graphShift.X, centreY + shiftY+graphShift.Y - scale*(1.0f / 20.0f) * func.Calc(x));
                pointcounts++;

            }
            return points;
        }
        private PointF[] GeneratedSinusPoint(float sign)
        {
            float centreY = panel1.ClientSize.Height / 2;
            float centreX = panel1.ClientSize.Width / 2;
            float minY = centreY*2;
            float maxY = 0;
            PointF[] points = new PointF[10000];
            CSin func = new CSin();
            int pointcounts = 0;
            for (float x = 0.0f; pointcounts < points.Length; x += 1f)
            {
                points[pointcounts] = new PointF(sign * x * scale + centreX + shiftX + graphShift.X, centreY + shiftY + graphShift.Y - scale*20 * func.Calc(sign * x / 20));
                //points[pointcounts] = new PointF(-x+centreX, centreY - 20*func.Calc(-x));
                if (points[pointcounts].Y < maxY)
                {
                    maxY = points[pointcounts].Y;
                    minPoint = points[pointcounts];
                }
                if (points[pointcounts].Y <= minY)
                {
                    minY = points[pointcounts].Y;
                    maxPoint = points[pointcounts];
                }
                pointcounts++;

            }
            
            return points;
        }
        private PointF[] GeneratedLinearPoint(float sign)
        {
            float centreY = panel1.ClientSize.Height / 2;
            float centreX = panel1.ClientSize.Width / 2;
            PointF[] points = new PointF[30000];
            CLinearFunc func = new CLinearFunc();
            int pointcounts = 0;
            for (float x = 0.0f; pointcounts < points.Length; x += 0.1f)
            {
                points[pointcounts] = new PointF(sign * x*scale + centreX+ shiftX + graphShift.X, centreY + shiftY + graphShift.Y - scale*func.Calc(sign * x));
                pointcounts++;

            }
            return points;
        }
        private PointF[] GeneratedTanPoint(float sign, int k, float signK)
        {
            float centreY = panel1.ClientSize.Height / 2;
            float centreX = panel1.ClientSize.Width / 2;
            Graphics graph = Graphics.FromImage(bmp);
            Pen pen = new Pen(graphColor, 2f);
            PointF[] points = new PointF[1000];//32
            CTan func = new CTan();
            int pointcounts = 0;
            float closeBreakX = 0; ;
            for (float x = 0.0f; pointcounts < points.Length; x += 1.0f)
            {
                if (x*scale >= scale*20 * (float)Math.PI / 2)
                {
                    closeBreakX = x-1f*scale;
                    break;
                }
                points[pointcounts] = new PointF(sign * x*scale + centreX + shiftX+graphShift.X +scale*signK*k*20*(float)Math.PI, centreY+ shiftY+ graphShift.Y - scale*20 * func.Calc(sign * x / 20));
                if (pointcounts > 0) graph.DrawLine(pen, points[pointcounts - 1], points[pointcounts]);
                pointcounts++;

            }
            //отрисовка вблизи точек разрыва
            for (float x = closeBreakX; pointcounts < points.Length; x += 0.01f)
            {
                if (x*scale >= scale * 20 * (float)Math.PI / 2)
                {
                    graph.Dispose();
                   return points;
                }
                points[pointcounts] = new PointF(sign * x * scale + centreX + shiftX + graphShift.X + scale * signK * k * 20 * (float)Math.PI, centreY + shiftY + graphShift.Y - scale * 20 * func.Calc(sign * x / 20));
                if (pointcounts > 0) graph.DrawLine(pen, points[pointcounts - 1], points[pointcounts]);
                pointcounts++;

            }
            graph.Dispose();
            return points;
        }
        private PointF[] GeneratedParCubePoint(float sign)
        {
            float centreY = panel1.ClientSize.Height / 2;
            float centreX = panel1.ClientSize.Width / 2;
            PointF[] points = new PointF[30000];
            CParabolaCube func = new CParabolaCube();
            int pointcounts = 0;
            for (float x = 0.0f; pointcounts < points.Length; x += 0.01f)
            {
                points[pointcounts] = new PointF(sign * x * scale + centreX + shiftX + graphShift.X, centreY + shiftY + graphShift.Y - scale * (1.0f / 400.0f) * func.Calc(sign * x));
                pointcounts++;

            }
            return points;
        }

//---------------SCALE--------------------------------------------------------------------------------------------------------------------------
        private void ScaleBarRendering()
        {
            Graphics graph = Graphics.FromImage(bmp);
            Pen pen = new Pen(Color.Black, 2f);
            Font font = new Font("Arial", 12f);
            Brush brush = new SolidBrush(Color.Black);
            graph.DrawString("Масштаб", font, brush, panel1.ClientSize.Width - 80, 20);
            graph.DrawString(scale.ToString("0.0"), font, brush, panel1.ClientSize.Width - 80, 40);
            
        }

        private void Panel1_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0 && scale < 4.9f)
            {
                scale += 0.1f;
                panel1.Invalidate();
            }
            else
            {
                if (scale > 1)
                {
                    scale -= 0.1f;
                    panel1.Invalidate();
                }
            }
        }

//----------------PANEL-------------------------------------------------------------------------------------------------------------------------

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            int deltaX=0, deltaY=0;
            float centreY = panel1.ClientSize.Height / 2;
            float centreX = panel1.ClientSize.Width / 2;
            bmp = new Bitmap(panel1.ClientSize.Width, panel1.ClientSize.Height);
            //Отрисовываем фон
            BackgroundRendering();
            //заполнение водой
            if (percentOfFill>0) WaterRendering();
            if(percentOfFill>12) WaterFillRect();
            if (goRain) RainRendering();
            //Отрисовываем Оси
            DrawAxis(new PointF(0, centreY+ shiftY+deltaY), new PointF(centreX*2, centreY + shiftY+ deltaY));
            DrawAxis(new PointF(centreX + shiftX + deltaX, 0), new PointF(centreX+shiftX + deltaX, centreY * 2));
            //Отрисовываем пунктирную окружность
            DrawEl();
            //Отрисовываем график если он есть
            FunctionRendering();
            //Scale bar
            if(scale>1) ScaleBarRendering();
            e.Graphics.DrawImageUnscaled(bmp, Point.Empty);
        }

        private void panel1_SizeChanged(object sender, EventArgs e)
        {
            LoadParticles();
            panel1.Invalidate();
        }
//-------------BUTTON---------------------------------------------------------------------------------------------------------------------------
        private void buttonRandomFunction_Click(object sender, EventArgs e)
        {
            Random random = new Random();
            graphType = 2;
            //graphType = random.Next(1, 6);
            scale = 1.0f;
            shiftX = 0;
            shiftY = 0;
            graphShift.X = 0;
            graphShift.Y= 0;
            percentOfFill = 0;
            percentOfFillRect = 0;
            goRain = false;
            goWind = false;
            buttonRain.Image = Properties.Resources.dropOn;
            buttonWind.Image = Properties.Resources.miniWind;
            windDirection = 0;
            timerRain.Stop();
            panel1.Invalidate();
        }
        private void buttonBackgroundStyle_Click(object sender, EventArgs e)
        {
            ChangeBackground changeBackground = new ChangeBackground();
            if (changeBackground.ShowDialog(this) == DialogResult.OK)
            {
                switch (changeBackground.Style)
                {
                    case 1:
                        {
                            bgType = 1;
                            bgColor = changeBackground.ColorRGB;
                            panel1.Invalidate();
                            break;
                        }
                    case 2:
                        {
                            bgType = 2;
                            pathBgFile = changeBackground.FileName;
                            panel1.Invalidate();
                            break;
                        }
                    case 3:
                        {
                            bgType = 3;
                            panel1.Invalidate();
                            break;

                        }
                    case 4:
                        {
                            bgType = 4;
                            panel1.Invalidate();
                            break;
                        }
                    case 5:
                        {
                            bgType = 5;
                            bgText = changeBackground.TextBackGround;
                            panel1.Invalidate();
                            break;
                        }
                    case 6:
                        {
                            bgType = 6;
                            panel1.Invalidate();
                            break;
                        }
                    default: { break; }
                }
            }
            changeBackground.Close();
        }
        private void buttonSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.OverwritePrompt = true;
            saveDialog.CheckPathExists = true;
            saveDialog.Filter =
                "Bitmap File(*.bmp)|*.bmp|" +
                "GIF File(*.gif)|*.gif|" +
                "JPEG File(*.jpg)|*.jpg|" +
                "TIF File(*.tif)|*.tif|" +
                "PNG File(*.png)|*.png";
            saveDialog.ShowHelp = true;
            if(saveDialog.ShowDialog() == DialogResult.OK)
            {
                string fileName = saveDialog.FileName;
                string strFilExtn =
                    fileName.Remove(0, fileName.Length - 3);
                
                switch (strFilExtn)
                {
                    case "bmp":
                        bmp.Save(fileName, System.Drawing.Imaging.ImageFormat.Bmp);
                        break;
                    case "jpg":
                        bmp.Save(fileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                        break;
                    case "gif":
                        bmp.Save(fileName, System.Drawing.Imaging.ImageFormat.Gif);
                        break;
                    case "tif":
                        bmp.Save(fileName, System.Drawing.Imaging.ImageFormat.Tiff);
                        break;
                    case "png":
                        bmp.Save(fileName, System.Drawing.Imaging.ImageFormat.Png);
                        break;
                    default:
                        break;
                }
                
                MessageBox.Show("Файл успешно сохранен!");
            }
            
        }
        private void buttonColorGraph_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            colorDialog.ShowDialog();
            graphColor = colorDialog.Color;
            panel1.Invalidate();

        }
//-------------OFFSET_GRAPH----------------------------------------------------------------------------------------------------------------
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (!graphOffset) mouseOffset = true;
                if (graphOffset) graphMove = true;
            }
            if(e.Button == MouseButtons.Right)
            {
                windLocation = true;
                prevWindMouseLocation = e.Location.X;
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseOffset = false;
                graphMove = false;
            }
            if (e.Button == MouseButtons.Right)
            {
                windLocation = false;
                if((e.Location.X - prevWindMouseLocation) > 0) { windDirection = 1; }
                else if ((e.Location.X - prevWindMouseLocation) < 0) { windDirection = -1; }
            }


        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            
            if(mouseOffset)
            {
                shiftX += (e.Location.X - prevMousePosition.X);
                shiftY += (e.Location.Y - prevMousePosition.Y);
                //LoadParticles();
                panel1.Invalidate();
                
            }
            if(graphMove)
            {
                graphShift.X += e.Location.X - prevMousePosition.X;
                graphShift.Y += e.Location.Y - prevMousePosition.Y;
                panel1.Invalidate();
            }
            prevMousePosition = e.Location;
        }

        private void buttonGraphOffset_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Offset graph?", "caption", MessageBoxButtons.OKCancel) == DialogResult.OK)
            { graphOffset = true; }
            else { graphOffset = false; }
        }
//-------------------RAIN----------------------------------------------------------------------------------------------------------------
        private void timerRain_Tick(object sender, EventArgs e)
        {
           CSin func = new CSin();
            localeCountTick++;
            if (localeCountTick % 10 == 0 && percentOfFill < 13) percentOfFill++;
            if(localeCountTick % 10 == 0 && percentOfFill > 12 && percentOfFillRect<300) percentOfFillRect++;

            float centreY = panel1.ClientSize.Height/2;
            float centreX = panel1.ClientSize.Width/2;
            int countPart = 0;
            foreach (Rain part in particles.ToList())
            {
                part.SetWind(windDirection);
                if (countPart < particles.Count / 2)
                {
                    if (part.posY + part.size[1] >= ( centreY + shiftY - scale * 20 * func.Calc(part.posX / 20)))
                    {
                        part.posY = 0;
                        if (part.windDirection != 0) { part.posX = part.startPosX; }
                    }
                    part.SetWindPosX();
                    part.NewParticlesPosition(centreY + shiftY - scale * 20 * func.Calc(part.posX / 20));
                }
                else
                {
                    if (part.posY + part.size[1] >= (centreY + shiftY - scale * 20 * func.Calc(-part.posX / 20)))
                    {
                        part.posY = 0;
                        if (part.windDirection != 0) { part.posX = part.startPosX; }
                    }
                    part.SetWindPosX();
                    part.NewParticlesPosition(centreY + shiftY - scale * 20 * func.Calc(-part.posX / 20));
                }
                countPart++;

            }
            panel1.Invalidate();
        }
        private void LoadParticles()
        {
            int countPart = 200;
            float centreX = panel1.ClientSize.Width/2;
            particles.Clear();
            for (int i = 0; i < countPart; i++) 
            {
                if(shiftX<=0) particles.Add(new Rain(random.Next(-300*5+0+shiftX, panel1.ClientSize.Width/2 - shiftX+300*5), 0, random.Next(10, 20), random.Next(0, 2)));
                else if(shiftX>0) particles.Add(new Rain(random.Next(-300*5+ 0 - shiftX, panel1.ClientSize.Width / 2 + shiftX+300*5), 0, random.Next(10, 20), random.Next(0, 2)));
            }
            
        }
        private void RainRendering()
        {
            Graphics graph = Graphics.FromImage(bmp);
            SolidBrush pens = new SolidBrush(Color.Blue);
            float centreX = panel1.ClientSize.Width / 2;
            int countParticles = 0;
            foreach (Rain part in particles.ToList())
            {
                graph.FillRectangle(pens, scale * part.posX + centreX + shiftX, part.posY, part.size[0], part.size[1]);
                //if (countParticles < particles.Count/2)
                //{
                //    graph.FillRectangle(pens, scale*part.posX + centreX+shiftX , part.posY , part.size[0], part.size[1]);
                //}
                //else
                //{
                //    graph.FillRectangle(pens, -part.posX*scale + centreX+shiftX, part.posY ,  part.size[0], part.size[1]);
                //}
                countParticles++;
            }
            
        }
        private void WaterFillRect()
        {
            Graphics graph = Graphics.FromImage(bmp);
            SolidBrush pens = new SolidBrush(Color.Blue);
            float centreY = panel1.ClientSize.Height / 2;
            float centreX = panel1.ClientSize.Width / 2;
            CSin func = new CSin();
            float x = 20 * ((float)Math.PI / 2);
            float localePercent = percentOfFillRect;
            graph.FillRectangle(pens, 0 , centreY + shiftY - scale*20 * func.Calc(x / 20) - 3*localePercent+2/scale, centreX*2, 3*localePercent);
        }
        private void WaterRendering()
        {
            for (int k = 0; k * 20 <= panel1.ClientSize.Width; k++)
            {
                WaterFillingCell(k, 1.0f);
                WaterFillingCell(k, -1.0f);
            }
        }
        private void WaterFillingCell(float k, float signK)
        {
            Graphics graph = Graphics.FromImage(bmp);
            SolidBrush pens = new SolidBrush(Color.Blue);
            float centreY = panel1.ClientSize.Height / 2;
            float centreX = panel1.ClientSize.Width / 2;
            PointF[] points = new PointF[1000];
            CSin func = new CSin();
            int pointcounts = 0;
            for (float x = 20 * ((float)Math.PI / 2) +  20 * ((float)Math.PI) - 5 * percentOfFill; x <= 5 * 20 * ((float)Math.PI / 2) - 20 * ((float)Math.PI) + 5 * percentOfFill; x += 1f)
            {
                points[pointcounts] = new PointF(signK*k * 2 * scale * 20 * (float)Math.PI + x * scale + centreX + shiftX + graphShift.X, centreY + shiftY + graphShift.Y - scale * 20 * func.Calc(x / 20));
                pointcounts++;

            }
            if (pointcounts > 0)
            {
                PointF[] pointsPolygon = new PointF[pointcounts];
                for (int i = 0; i < pointcounts; i++)
                {
                    pointsPolygon[i] = points[i];
                }
                graph.FillPolygon(pens, pointsPolygon);
            }
        }
        private void buttonRain_Click(object sender, EventArgs e)
        {
            if (graphType == 2)
            {
                if (!goRain && MessageBox.Show("Включить дождь?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    buttonRain.Image = Properties.Resources.dropOff;
                    goRain = true;
                    LoadParticles();
                    timerRain.Start();
                }
                else if(goRain && MessageBox.Show("Выключить дождь?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    buttonRain.Image = Properties.Resources.dropOn;
                    goRain = false;
                    localeCountTick = 0;
                    timerRain.Stop();
                    panel1.Invalidate();
                }
            }
            else
            {
                MessageBox.Show("Не выбрана нужная функция!", "Caution");
            }
        }

        private void buttonWind_Click(object sender, EventArgs e)
        {
            if(graphType==2 && goRain)
            {
                if(!goWind && MessageBox.Show("Включить ветер?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    goWind = true;
                    buttonWind.Image = Properties.Resources.windOff;
                    panel1.Invalidate();
                }
                else if(goWind && MessageBox.Show("Выключить ветер?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    goWind = false;
                    buttonWind.Image = Properties.Resources.miniWind;
                    windDirection = 0;
                    panel1.Invalidate();
                }
            }
            else
            {
                MessageBox.Show("Включите дождь!", "Caution");
            }
        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            float centreX = panel1.ClientSize.Width / 2;
            float centreY = panel1.ClientSize.Height / 2;
            pointActive = true;
            if (PointOnGraph(e.Location.X, e.Location.Y)) MessageBox.Show("Yes popal");
        }

        private bool PointOnGraph(float xAbsolut, float yAbsolut)
        {
            CSin func = new CSin();
            //switch ccase->func, shag, koef
            float centreX = panel1.ClientSize.Width / 2;
            float centreY = panel1.ClientSize.Height / 2;
            float x = (xAbsolut-centreX-shiftX)/scale;
            float inaccuracy = 3.0f;
            if(Math.Abs(yAbsolut - (centreY + shiftY - scale*20*func.Calc(x/20))) <= inaccuracy)
            {
                return true;
            }
            else
            {
                MessageBox.Show(yAbsolut.ToString() + ";"+ (centreY + shiftY - scale * 20 * func.Calc(x / 20)).ToString());
                return false;
            }

        }
    }
}
