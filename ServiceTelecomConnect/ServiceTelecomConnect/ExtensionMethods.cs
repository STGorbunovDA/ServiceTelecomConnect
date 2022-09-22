using System;
using System.Reflection;
using System.Windows.Forms;

namespace ServiceTelecomConnect
{
    public static class ExtensionMethods
    {
        public static void DoubleBuffered(this DataGridView dgw, bool setting)
        {
            Type dgvType = dgw.GetType();
            PropertyInfo pi = dgvType.GetProperty("DoubleBuffered",
                BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(dgw, setting, null);
        }

        public static void DoubleBufferedForm(this Form dgw, bool setting)
        {
            Type dgvType = dgw.GetType();
            PropertyInfo pi = dgvType.GetProperty("DoubleBuffered",
                BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(dgw, setting, null);
        }
    }
}
