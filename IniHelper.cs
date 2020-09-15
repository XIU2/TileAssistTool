using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace IniHelper_cs
{
    public class IniHelper
    {
        [DllImport("kernel32")] // 返回0表示失败，非0为成功
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")] // 返回取得字符串缓冲区的长度
        private static extern long GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        /// <summary>
        /// 读取INI配置文件
        /// </summary>
        /// <param name="Section">节</param>
        /// <param name="Key">键</param>
        /// <param name="defaultValue">默认值，找不到时返回该值</param>
        /// <param name="iniFilePath">配置文件路径</param>
        /// <returns>成功返回 值，失败返回 默认值</returns>
        public static string GetValue(string Section, string Key, string defaultValue, string iniFilePath)
        {
            if (File.Exists(iniFilePath))
            {
                StringBuilder temp = new StringBuilder(1024);
                GetPrivateProfileString(Section, Key, defaultValue, temp, 1024, iniFilePath);
                return temp.ToString();
            }
            else
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// 写出INI配置文件
        /// </summary>
        /// <param name="Section">节</param>
        /// <param name="Key">键</param>
        /// <param name="Value">值</param>
        /// <param name="iniFilePath">配置文件路径</param>
        /// <returns>成功返回 true，失败返回 false</returns>
        public static bool SetValue(string Section, string Key, string Value, string iniFilePath)
        {
            var pat = Path.GetDirectoryName(iniFilePath);
            if (Directory.Exists(pat) == false)
            {
                Directory.CreateDirectory(pat);
            }
            if (File.Exists(iniFilePath) == false)
            {
                File.Create(iniFilePath).Close();
            }
            long OpStation = WritePrivateProfileString(Section, Key, Value, iniFilePath);
            if (OpStation == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
