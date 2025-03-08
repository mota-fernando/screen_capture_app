namespace ScreenCaptureApp
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private Button btnStartAutoCapture;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.btnStartAutoCapture = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnStartAutoCapture
            // 
            this.btnStartAutoCapture.Location = new System.Drawing.Point(20, 20);
            this.btnStartAutoCapture.Name = "btnStartAutoCapture";
            this.btnStartAutoCapture.Size = new System.Drawing.Size(150, 30);
            this.btnStartAutoCapture.TabIndex = 0;
            this.btnStartAutoCapture.Text = "Iniciar Captura Automática";
            this.btnStartAutoCapture.UseVisualStyleBackColor = true;
            this.btnStartAutoCapture.Click += new System.EventHandler(this.btnStartAutoCapture_Click);
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnStartAutoCapture);
            this.Name = "Form1";
           // this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.ResumeLayout(false);
        }
    }
}