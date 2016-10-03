using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace MoveZoomPolygon
{
    public partial class Form1 : Form
    {
        private Graphics gScreen;
        private Graphics gBitmap;
        private Bitmap bitmap;
        private double zoom = 1;
        private bool mouseDown = false;

        private int xCentre = 305;
        private int yCentre = 289;
        private int xRoute = 0;
        private int yRoute = 0;
        private int xDelta = 0;
        private int yDelta = 0;

        public Form1()
        {
            InitializeComponent();
            gBitmap = this.CreateGraphics();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            gScreen = CreateGraphics();
            bitmap = new Bitmap(ClientRectangle.Width, ClientRectangle.Height);
            gBitmap = Graphics.FromImage(bitmap); //?
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            xCentre -= xDelta;
            yCentre -= yDelta;

            xRoute -= xDelta;
            yRoute -= yDelta;

            xDelta = 0;
            yDelta = 0;

            MyClass.Draw(gBitmap, Convert.ToInt32(numericUpDown.Value), zoom, xCentre, yCentre);
            e.Graphics.DrawImage(bitmap, ClientRectangle);

        }

        private void Mouse_Move(object sender, MouseEventArgs e)
        {
            this.Text = " " + e.X + " " + e.Y;
            if (mouseDown)
            {
                xDelta = xRoute - e.X;
                yDelta = yRoute - e.Y;

                this.Refresh();
            }
        }

        private void paint_Btn_Click(object sender, EventArgs e)
        {
            xCentre = 305;
            yCentre = 289;
            this.Refresh();
        }

        private void Mouse_Down(object sender, MouseEventArgs e)
        {
            Point point = new Point(e.X, e.Y);
            if (MyClass.IsPointInPolygon4(MyClass.Pts, point))// MessageBox.Show("yes!!!!");
            {
                mouseDown = true;
                xRoute = e.X;
                yRoute = e.Y;
            }
        }

        private void Mouse_Wheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0) zoom += 0.1;
            if (e.Delta < 0 && zoom - 0.1 >= 0) zoom -= 0.1;

            this.Refresh();
        }

        private void Mouse_Up(object sender, MouseEventArgs e)
        {
            mouseDown = false;
            xRoute = 0;
            yRoute = 0;
        }

    }
}
