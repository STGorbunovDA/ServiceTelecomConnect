using System.Threading;

namespace ServiceTelecomConnect
{
    class InstanceChecker
    {
        static readonly Mutex mutex = new Mutex(false, "ServiceTelecomConnect");
        public static bool TakeMemory()
        {
            return mutex.WaitOne();
        }
    }
}
