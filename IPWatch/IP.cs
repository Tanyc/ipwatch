// =================================================================== 
// IP位置信息查找【IPWatch】项目
// ===================================================================
// 文件：MainFrm.cs
// 项目名称：IP位置信息查找项目
// 创建时间：2015-09-09 12:33:54
// 负责人：TanYucheng
// 类介绍：IP库文件读取类，参考 http://www.ipip.net/download.html 里的C#版本
// https://github.com/17mon/csharp
// ===================================================================

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Net;

namespace IPWatch
{
    class IP
    {
        public static bool EnableFileWatch = false;

        private static int offset;
        private static uint[] index = new uint[256];
        private static byte[] dataBuffer;
        private static byte[] indexBuffer;
        private static long lastModifyTime = 0L;
        private static string ipFile;
        private static readonly object @lock = new object();

        public static void Load(string filename)
        {
            ipFile = new FileInfo(filename).FullName;
            Load();
            if (EnableFileWatch)
            {
                Watch();
            }
        }

        public static string[] Find(string ip)
        {
            lock (@lock)
            {
                var ips = ip.Split('.');
                var ip_prefix_value = int.Parse(ips[0]);
                long ip2long_value = BytesToLong(byte.Parse(ips[0]), byte.Parse(ips[1]), byte.Parse(ips[2]),
                    byte.Parse(ips[3]));
                var start = index[ip_prefix_value];
                var max_comp_len = offset - 1028;
                long index_offset = -1;
                var index_length = -1;
                byte b = 0;
                for (start = start * 8 + 1024; start < max_comp_len; start += 8)
                {
                    if (
                        BytesToLong(indexBuffer[start + 0], indexBuffer[start + 1], indexBuffer[start + 2],
                            indexBuffer[start + 3]) >= ip2long_value)
                    {
                        index_offset = BytesToLong(b, indexBuffer[start + 6], indexBuffer[start + 5],
                            indexBuffer[start + 4]);
                        index_length = 0xFF & indexBuffer[start + 7];
                        break;
                    }
                }
                var areaBytes = new byte[index_length];
                Array.Copy(dataBuffer, offset + (int)index_offset - 1024, areaBytes, 0, index_length);
                return Encoding.UTF8.GetString(areaBytes).Split('\t');
            }
        }

        private static void Watch()
        {
            var file = new FileInfo(ipFile);
            if (file.DirectoryName == null) return;
            var watcher = new FileSystemWatcher(file.DirectoryName, file.Name) { NotifyFilter = NotifyFilters.LastWrite };
            watcher.Changed += (s, e) =>
            {
                var time = File.GetLastWriteTime(ipFile).Ticks;
                if (time > lastModifyTime)
                {
                    Load();
                }
            };
            watcher.EnableRaisingEvents = true;
        }

        private static void Load()
        {
            lock (@lock)
            {
                var file = new FileInfo(ipFile);
                lastModifyTime = file.LastWriteTime.Ticks;
                try
                {
                    dataBuffer = new byte[file.Length];
                    using (var fin = new FileStream(file.FullName, FileMode.Open, FileAccess.Read))
                    {
                        fin.Read(dataBuffer, 0, dataBuffer.Length);
                    }

                    var indexLength = BytesToLong(dataBuffer[0], dataBuffer[1], dataBuffer[2], dataBuffer[3]);
                    indexBuffer = new byte[indexLength];
                    Array.Copy(dataBuffer, 4, indexBuffer, 0, indexLength);
                    offset = (int)indexLength;

                    for (var loop = 0; loop < 256; loop++)
                    {
                        index[loop] = BytesToLong(indexBuffer[loop * 4 + 3], indexBuffer[loop * 4 + 2],
                            indexBuffer[loop * 4 + 1],
                            indexBuffer[loop * 4]);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        private static uint BytesToLong(byte a, byte b, byte c, byte d)
        {
            return ((uint)a << 24) | ((uint)b << 16) | ((uint)c << 8) | d;
        }

        /// <summary>
        /// 在线查询IP地理位置
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        internal static string[] OnlineFind(string ip)
        {
            String baseUrl = "https://sp0.baidu.com/8aQDcjqpAAV3otqbppnN2DJv/api.php";
            baseUrl += "?" + "query="+ip;
            baseUrl += "&" + "co=";
            baseUrl += "&" + "resource_id=6006";
            baseUrl += "&" + "t="+Base.Common.GetTimeStamp();
            baseUrl += "&" + "ie=utf8";
            baseUrl += "&" + "oe=utf8";
            baseUrl += "&" + "cb=op_aladdin_callback";
            baseUrl += "&" + "format=json";
            baseUrl += "&" + "tn=baidu";
            baseUrl += "&" + "_="+Base.Common.GetTimeStamp();

            Regex reg = new Regex(@"""location"":""(?<location>.*?)"", ""titlecont""");

            HttpWebResponse resp =  HttpHelper.CreateGetHttpResponse(baseUrl, 30, "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/49.0.2623.87 Safari/537.36", null);
            
            Stream stream = resp.GetResponseStream();

            StreamReader sr = new StreamReader(stream, Encoding.UTF8);
            string html = sr.ReadToEnd();
            sr.Close();
            stream.Close();

            string tempLocation = reg.Match(html).Result("${location}");
            

            //Thread t1 = new Thread(new ParameterizedThreadStart(CollectIpInfoMethod));
            //t1.IsBackground = true;
            //t1.Start(ip);


            return tempLocation.Split(' ');
        }

        //public static void CollectIpInfoMethod(object data)
        //{
        //    try
        //    {
        //        IDictionary<string, string> parameters = (IDictionary<string, string>)data;
        //        HttpHelper.CreatePostHttpResponse(Base.JunqiChallengerLiteUserInfoUrl, parameters, Encoding.UTF8,
        //            30, "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/49.0.2623.87 Safari/537.36", null);
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}
    }
}
