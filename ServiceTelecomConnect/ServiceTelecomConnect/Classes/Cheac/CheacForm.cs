using System.Windows.Forms;

namespace ServiceTelecomConnect
{
    internal class CheacForm
    {
        static volatile CheacForm Class;
        static object SyncObject = new object();
        public static CheacForm GetInstance
        {
            get
            {
                if (Class == null)
                    lock (SyncObject)
                    {
                        if (Class == null)
                            Class = new CheacForm();
                    }
                return Class;
            }
        }
        public bool CheckIfFormIsOpen(string formname)
        {

            FormCollection fc = Application.OpenForms;
            foreach (Form frm in fc)
            {
                if (frm.Name == formname)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
