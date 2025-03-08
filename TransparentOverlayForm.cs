using System;
using System.Drawing;
using System.Windows.Forms;

namespace ScreenCaptureApp
{
    public class TransparentOverlayForm : Form
    {
        private Rectangle captureRect;
        private Point startPoint;
        private bool isDrawing = false;
        private readonly Form1 mainForm;
        private readonly bool startAutoCapture;

        public TransparentOverlayForm(Form1 form, bool autoCapture)
        {
            mainForm = form;
            startAutoCapture = autoCapture;
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Color.Black;
            this.Opacity = 0.5;
            this.WindowState = FormWindowState.Maximized;
            this.TopMost = true;
            this.DoubleBuffered = true;
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            isDrawing = true;
            startPoint = e.Location;
            base.OnMouseDown(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (isDrawing)
            {
                int x = Math.Min(startPoint.X, e.X);
                int y = Math.Min(startPoint.Y, e.Y);
                int width = Math.Abs(startPoint.X - e.X);
                int height = Math.Abs(startPoint.Y - e.Y);
                captureRect = new Rectangle(x, y, width, height);
                this.Invalidate();
            }
            base.OnMouseMove(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            isDrawing = false;
            this.Close();
            mainForm.SetCaptureRect(captureRect, startAutoCapture);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (isDrawing)
            {
                using (Pen pen = new Pen(Color.Red, 2))
                {
                    e.Graphics.DrawRectangle(pen, captureRect);
                }
            }
            base.OnPaint(e);
        }
    }
}