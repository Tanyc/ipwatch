// =================================================================== 
// IP位置信息查找【IPWatch】项目
// ===================================================================
// 文件：Base.cs
// 项目名称：IP位置信息查找项目
// 创建时间：2015-09-09 12:33:54
// 负责人：TanYucheng
// 类介绍：基础方法类
// ===================================================================

using System;
using System.Collections.Generic;
using System.Text;

#region 版本信息 tag:base-1
using System.Reflection;
using System.IO;
using System.Xml;
#endregion

namespace IPWatch
{
    class Base
    {
        /// <summary>
        /// 公共类方法、属性、常量集
        /// </summary>
        public class Common
        {
            #region 版本信息 tag:base-1

            /// <summary>
            /// 程序版本信息 程序名-版本号[编译时间-发布版本类型]（作者信息）-其他信息
            /// </summary>
            public static string VERSION = "";

            /// <summary>
            /// 程序名
            /// </summary>
            public static string APPLICATION_NAME = "双人行工作室";

            /// <summary>
            /// 程序发布类型
            /// </summary>
            public static string PUBLISH_TYPE = "内部测试版";

            /// <summary>
            /// 作者信息
            /// </summary>
            public static string AUTHINFO = "双人行工作室 tanyc";

            /// <summary>
            /// 获取程序发布版本信息 
            ///     注：该方法应该至少在主窗体的构造方法里调用一次
            /// </summary>
            /// <param name="frmObj">调用对象，这里通常是程序主窗体</param>
            /// <param name="args">字符串数组
            ///      依次信息为：程序名-->程序发布类型-->作者信息-->其他描述
            ///      new string[4] { "XX工具", "", "", "" }
            /// </param>
            /// <returns>程序版本信息 程序名-版本号[编译时间-发布版本类型]（作者信息）-其他信息</returns>
            public static string getVersion(Object frmObj, string[] args)
            {
                if (VERSION == null || VERSION == "")
                {
                    //初始化版本信息
                    AssemblyName aName = frmObj.GetType().Assembly.GetName();
                    Version v = aName.Version;
                    //int rev = Convert.ToInt32(svnid.Split(' ')[1]);
                    DateTime CompileTime = GetPe32Time(frmObj.GetType().Assembly.Location);
                    if (CompileTime == DateTime.MinValue)
                        CompileTime = File.GetLastWriteTime(frmObj.GetType().Assembly.Location);
                    VERSION = string.Format("{0} {1} [{2} {3}] {4} {5}", args[0] == null || args[0] == "" ? APPLICATION_NAME : APPLICATION_NAME = args[0],
                        v, CompileTime.ToString("yyyy-MM-dd"), args[1] == null || args[1] == "" ? PUBLISH_TYPE : PUBLISH_TYPE = args[1],
                         args[2] == null || args[2] == "" ? AUTHINFO : AUTHINFO = args[2],
                         args[3] == null || args[3] == "" ? "" : args[3]);

                }

                return VERSION;
            }



            /// <summary>
            /// 获取程序的编译时间
            /// </summary>
            /// <param name="fileName">程序文件路径</param>
            /// <returns>编译时间</returns>
            static DateTime GetPe32Time(string fileName)
            {
                int seconds;
                using (BinaryReader br = new BinaryReader(new FileStream(fileName, FileMode.Open, FileAccess.Read)))
                {
                    byte[] bs = br.ReadBytes(2);
                    if (bs.Length != 2) return DateTime.MinValue;
                    if (bs[0] != 'M' || bs[1] != 'Z') return DateTime.MinValue;
                    br.BaseStream.Seek(0x3c, SeekOrigin.Begin);
                    byte offset = br.ReadByte();
                    br.BaseStream.Seek(offset, SeekOrigin.Begin);
                    bs = br.ReadBytes(4);
                    if (bs.Length != 4) return DateTime.MinValue;
                    if (bs[0] != 'P' || bs[1] != 'E' || bs[2] != 0 || bs[3] != 0) return DateTime.MinValue;
                    bs = br.ReadBytes(4);
                    if (bs.Length != 4) return DateTime.MinValue;
                    seconds = br.ReadInt32();
                }
                return DateTime.SpecifyKind(new DateTime(1970, 1, 1), DateTimeKind.Utc).
                        AddSeconds(seconds).ToLocalTime();
            }

            /// <summary>
            /// 获取时间戳 
            /// </summary>
            /// <returns>时间戳符串</returns>
            public static String GetTimeStamp()
            {
                DateTime timeStamp = new DateTime(1970, 1, 1); //得到1970年的时间戳 
                long a = (DateTime.UtcNow.Ticks - timeStamp.Ticks) / 10000000; //注意这里有时区问题，用now就要减掉8个小时
                String time = a.ToString() + DateTime.UtcNow.Millisecond;//加上毫秒数
                //MessageBox.Show(time);
                return time;
            }
            #endregion

        }
    }
}