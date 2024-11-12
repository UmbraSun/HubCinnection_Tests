using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace ConsoleApp
{
    internal class IniFile
    {
        private readonly string _path;
        private readonly string _sectionName;

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        private static extern int GetPrivateProfileString(string section, string key, string @default,
            StringBuilder retVal, int size, string filePath);

        public IniFile(string iniPath, string sectionName)
        {
            _path = new FileInfo(iniPath).FullName;
            _sectionName = sectionName;
        }

        public string Read(string key)
        {
            var retVal = new StringBuilder(1000);
            GetPrivateProfileString(_sectionName, key, "", retVal, 1000, _path);

            return retVal.ToString();
        }
    }
}
