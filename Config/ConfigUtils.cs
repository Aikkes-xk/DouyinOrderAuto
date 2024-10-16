using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DyOrderAuto.Config
{
    internal class ConfigUtils
    {
         string Path;
         string EXE = Assembly.GetExecutingAssembly().GetName().Name;
        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        static extern long WritePrivateProfileString(string Section, string Key, string Value, string FilePath);
        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        static extern int GetPrivateProfileString(string Section, string Key, string Default, StringBuilder RetVal, int Size, string FilePath);
        internal ConfigUtils(string IniPath = null)
        {
            Path = new FileInfo(IniPath ?? EXE + ".ini").FullName;
        }
        /// <summary>
        /// 读取配置项
        /// </summary>
        /// <param name="Key">需要读取的key</param>
        /// <param name="Section">读取的节点</param>
        /// <returns></returns>
        internal string Read(string Section ,string Key)
        {
            var RetVal = new StringBuilder(255);
            GetPrivateProfileString(Section ?? EXE, Key, "", RetVal, 255, Path);
            return RetVal.ToString();
        }
        /// <summary>
        /// 写入配置项
        /// </summary>
        /// <param name="Key">写入key</param>
        /// <param name="Value">写入值</param>
        /// <param name="Section">写入节点，空的话默认程序名字</param>
        internal void Write(string Section,string Key, string Value)
        {
            WritePrivateProfileString(Section ?? EXE, Key, Value, Path);
        }
        /// <summary>
        /// 删除指定节点的配置项
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="Section"></param>
        internal void DeleteKey(string Key, string Section = null)
        {
            Write(Key, null, Section ?? EXE);
        }
        /// <summary>
        /// 删除整个节点的配置
        /// </summary>
        /// <param name="Section"></param>
        internal void DeleteSection(string Section = null)
        {
            Write(null, null, Section ?? EXE);
        }
        /// <summary>
        /// 判断配置文件的KEY值是否存在
        /// </summary>
        /// <param name="Key">要判断的KEY</param>
        /// <param name="Section">需要检查的节点</param>
        /// <returns></returns>
        internal bool KeyExists(string Key, string Section = null)
        {
            return Read(Key, Section).Length > 0;
        }
    }
}
