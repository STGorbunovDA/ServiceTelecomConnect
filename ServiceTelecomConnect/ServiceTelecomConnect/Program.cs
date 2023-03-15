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
            Application.ThreadException += new ThreadExceptionEventHandler(ExceptionGlobal);
            Application.Run(new LoginForm());
        }

        static void ExceptionGlobal(object sender, ThreadExceptionEventArgs e)
        {
            LogUser.LogExceptionUserSaveFilePC(e.Exception.Message);
            LogUser.LogExceptionUserSaveFilePC(e.Exception.ToString());
            MessageBox.Show(e.Exception.Message);
        }
    }
}
