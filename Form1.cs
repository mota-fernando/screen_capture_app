using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Timers;
using System.Windows.Forms;
using Tesseract;

namespace ScreenCaptureApp
{
    public partial class Form1 : Form
    {
        private Rectangle captureRect;
        private System.Timers.Timer timer;
        private bool isAutoCaptureEnabled = false;
        private readonly string saveDirectory = "F:\\work\\bets\\crashgame_analises\\ScreenCaptureApp\\captura_crashgame";
        private readonly string tessDataPath = "F:\\work\\bets\\crashgame_analises\\ScreenCaptureApp\\tessdata";
        private TesseractEngine tesseractEngine;

        public Form1()
        {
            InitializeComponent();
            timer = new System.Timers.Timer(30000); // 30 segundos
            timer.Elapsed += OnTimedEvent;
            timer.AutoReset = true;
            timer.Enabled = false;

            // Inicializa o Tesseract
            tesseractEngine = new TesseractEngine(tessDataPath, "eng", EngineMode.Default);
        }

        private void btnCaptureOnce_Click(object sender, EventArgs e)
        {
            StartSelection(false);
        }

        private void btnStartAutoCapture_Click(object sender, EventArgs e)
        {
            if (isAutoCaptureEnabled)
            {
                btnStartAutoCapture.Text = "Captura Contínua";
                btnStopAutoCapture_Click(sender, e);
            }
            else
            {
                btnStartAutoCapture.Text = "Parar Captura";
                StartSelection(true);
            }
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
            btnStartAutoCapture.Text = "Captura Contínua";
            TrainTesseract();
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

                    using (Bitmap grayBitmap = ConvertToGrayScale(bitmap))
                    {
                        Directory.CreateDirectory(saveDirectory);
                        string fileName = $"capture_{DateTime.Now:yyyyMMdd_HHmmss}.png";
                        string savePath = Path.Combine(saveDirectory, fileName);
                        grayBitmap.Save(savePath, System.Drawing.Imaging.ImageFormat.Png);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao capturar a tela: {ex.Message}");
            }
        }

        private Bitmap ConvertToGrayScale(Bitmap original)
        {
            Bitmap grayBitmap = new Bitmap(original.Width, original.Height);
            using (Graphics g = Graphics.FromImage(grayBitmap))
            {
                ColorMatrix colorMatrix = new ColorMatrix(new float[][]
                {
                    new float[] { 0.3f, 0.3f, 0.3f, 0, 0 },
                    new float[] { 0.59f, 0.59f, 0.59f, 0, 0 },
                    new float[] { 0.11f, 0.11f, 0.11f, 0, 0 },
                    new float[] { 0, 0, 0, 1, 0 },
                    new float[] { 0, 0, 0, 0, 1 }
                });

                using (ImageAttributes attributes = new ImageAttributes())
                {
                    attributes.SetColorMatrix(colorMatrix);
                    g.DrawImage(original, new Rectangle(0, 0, original.Width, original.Height), 0, 0, original.Width, original.Height, GraphicsUnit.Pixel, attributes);
                }
            }
            return grayBitmap;
        }

        private void TrainTesseract()
        {
            string[] imageFiles = Directory.GetFiles(saveDirectory, "*.png");
            foreach (string imagePath in imageFiles)
            {
                using (var img = Pix.LoadFromFile(imagePath))
                {
                    using (var page = tesseractEngine.Process(img))
                    {
                        string extractedText = page.GetText();
                        File.AppendAllText(Path.Combine(saveDirectory, "ocr_training_data.txt"), extractedText + "\n");
                    }
                }
            }
            Console.WriteLine("Treinamento do Tesseract concluído.");
        }
    }
} 
