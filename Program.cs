using System;
using System.Windows.Forms;

namespace ScreenCaptureApp
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao iniciar a aplicação: {ex.Message}");
            }
        }
    }
}