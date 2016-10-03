using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace MoveZoomPolygon
{
    public class MyClass
    {
        static Point[] pts;

        public static Point[] Pts
        {
            get { return pts; } }

        public static void Draw(Graphics g, int sidesNumber, double zoom, int xCentre, int yCentre)

        { 
            Color cl = Color.FromArgb(255, 255, 255);
            g.Clear(cl);

            g.SmoothingMode = SmoothingMode.AntiAlias;
            
            double r = 70 * zoom;
            Pen pen = new Pen(Color.Black);
            pts = new Point[1];
            int i = 0;

            int angle = 360 / sidesNumber;
            int x0 = (int)(xCentre + r * Math.Cos(angle + Math.PI * 2 * i / sidesNumber));
            int y0 = (int)(yCentre + r * Math.Sin(angle + Math.PI * 2 * i / sidesNumber));

            for (i = 0; i < sidesNumber; i++)
            {
                Array.Resize<Point>(ref pts, pts.Length + 1);
                int x = (int)(xCentre + r * Math.Cos(angle + Math.PI * 2 * i / sidesNumber));
                int y = (int)(yCentre + r * Math.Sin(angle + Math.PI * 2 * i / sidesNumber));
                pts[i] = new Point(x, y);
            }

            pts[i] = new Point(x0, y0);
            g.DrawPolygon(pen, pts);

        }


        public static bool IsPointInPolygon4(Point[] polygon, Point testPoint)
        {
            bool result = false;
            int j = polygon.Count() - 1;
            for (int i = 0; i < polygon.Count(); i++)
            {
                if (polygon[i].Y < testPoint.Y && polygon[j].Y >= testPoint.Y || polygon[j].Y < testPoint.Y && polygon[i].Y >= testPoint.Y)
                {
                    if (polygon[i].X + (testPoint.Y - polygon[i].Y) / (polygon[j].Y - polygon[i].Y) * (polygon[j].X - polygon[i].X) < testPoint.X)
                    {
                        result = !result;
                    }
                }
                j = i;
            }
            return result;
        }

     

    }
}
