﻿using Microsoft.Win32;

namespace ServiceTelecomConnect
{
    class CheacReggedit
    {
        #region проверка реестра на наличе записи
        /// <summary>
        /// для проверки реестра на наличие записи
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        public static bool ValueExists(RegistryKey Key, string Value)
        {
            try
            {
                return Key.GetValue(Value) != null && Key.GetValue(Value) != "";
            }
            catch
            {
                return false;
            }
        }
        #endregion
    }
}
