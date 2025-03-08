using System;
using System.Drawing;
using System.IO;
using System.Timers;
using System.Windows.Forms;

namespace ScreenCaptureApp
{
    public partial class Form1 : Form
    {
        private Rectangle captureRect;
        private System.Timers.Timer timer;
        private bool isAutoCaptureEnabled = false;
        private readonly string saveDirectory = "F:\\work\\bets\\crashgame_analises\\ScreenCaptureApp\\captura_crashgame";

        public Form1()
        {
            InitializeComponent();
            timer = new System.Timers.Timer(30000); // 30 segundos
            timer.Elapsed += OnTimedEvent;
            timer.AutoReset = true;
            timer.Enabled = false;
        }

        private void btnCaptureOnce_Click(object sender, EventArgs e)
        {
            StartSelection(false);
        }

        private void btnStartAutoCapture_Click(object sender, EventArgs e)
        {
            StartSelection(true);
        }

        private void StartSelection(bool startAutoCapture)
        {
            var overlayForm = new TransparentOverlayForm(this, startAutoCapture);
            overlayForm.Show();
        }

        public void SetCaptureRect(Rectangle rect, bool startAutoCapture)
        {
            captureRect = rect;
            CaptureAndSaveImage();

            if (startAutoCapture)
            {
                isAutoCaptureEnabled = true;
                timer.Enabled = true;
            }
        }

        private void btnStopAutoCapture_Click(object sender, EventArgs e)
        {
            isAutoCaptureEnabled = false;
            timer.Enabled = false;
        }

        private void OnTimedEvent(object? source, ElapsedEventArgs e)
        {
            if (isAutoCaptureEnabled)
            {
                CaptureAndSaveImage();
            }
        }

        private void CaptureAndSaveImage()
        {
            if (captureRect.IsEmpty) return;

            try
            {
                using (Bitmap bitmap = new Bitmap(captureRect.Width, captureRect.Height))
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    g.CopyFromScreen(captureRect.Location, Point.Empty, captureRect.Size);
                    Directory.CreateDirectory(saveDirectory);
                    string fileName = $"capture_{DateTime.Now:yyyyMMdd_HHmmss}.png";
                    string savePath = Path.Combine(saveDirectory, fileName);
                    bitmap.Save(savePath);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao capturar a tela: {ex.Message}");
            }
        }
    }
}