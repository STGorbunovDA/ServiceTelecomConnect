using ServiceTelecomConnect.Classes.Other;
using System;
using System.Threading;
using System.Windows.Forms;

namespace ServiceTelecomConnect
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.ThreadException += new ThreadExceptionEventHandler(Exception_global);
            Application.Run(new LoginForm());
        }

        static void Exception_global(object sender, ThreadExceptionEventArgs e)
        {
            LogUser.LogExceptionUserSaveFilePC(e.Exception.Message);
            LogUser.LogExceptionUserSaveFilePC(e.Exception.ToString());
            MessageBox.Show(e.Exception.Message);
        }
    }
}
