// =================================================================== 
// IP位置信息查找【IPWatch】项目
// ===================================================================
// 文件：MainFrm.cs
// 项目名称：IP位置信息查找项目
// 创建时间：2015-09-09 12:33:54
// 负责人：TanYucheng
// 类介绍：主窗体功能逻辑实现主类
// ===================================================================

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO;
using System.Net;
using System.Threading;

namespace IPWatch
{
    public partial class MainFrm : Form
    {

        #region 版本信息 tag:base-1
        static public MainFrm Instance;
        #endregion

        public static MainFrm mainFrm;

        public MainFrm()
        {
            InitializeComponent();

            #region 版本信息 tag:base-1
            Instance = this;
            Instance.Text = Text = Base.Common.getVersion(Instance, new string[4] { "IPWatch", "", "", "" });
            notifyIcon1.Text = Base.Common.VERSION;
            #endregion
        }


        #region 窗体状态及托盘图标事件 tag:base-2
        /// <summary>
        /// 托盘图标单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (WindowState == FormWindowState.Minimized)
                {
                    Visible = true;
                    WindowState = FormWindowState.Normal;
                    ShowInTaskbar = true;
                }
                else
                {
                    WindowState = FormWindowState.Minimized;
                    Visible = false;
                }
            }
        }

        /// <summary>
        /// 窗体状态发生改变时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainFrm_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                this.Hide();
                ShowInTaskbar = true;
            }
        }
        #endregion

        int searchType = 0;//查询类型:0,离线；1,在线（百度）；

        /// <summary>
        /// 离线查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnIPSeach_Click(object sender, EventArgs e)
        {
            searchType = 0;
            if (txtIPList.Text == "")
            {
                MessageBox.Show("请输入要查找的IP地址");
                return;
            }

            //校验IP列表的合法性
            Regex reg=new Regex(@"((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)");

            //按每一行进行查找
            IP.EnableFileWatch = true;
            IP.Load("17monipdb.dat");

            string resultMsg = "";
            string[] ipArr=txtIPList.Text.Split('\n');
            for (int i = 0; i < ipArr.Length; i++) {
                if (ipArr[i] != "")
                {
                    try
                    {
                        string ip = reg.Match(ipArr[i]).Value;

                        resultMsg += ip + "," + string.Join(",", IP.Find(ip)) + "\r\n";
                    }
                    catch
                    {
                        resultMsg += ipArr[i]+",不合法的IP地址,,,\r\n";
                    }
                }
                else {
                    resultMsg += ",,,,\r\n";
                }
            }
            txtResult.Text = resultMsg;
                
        }

        /// <summary>
        /// 带表头导出CSV文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnWithHeadExp_Click(object sender, EventArgs e)
        {
            string header = "";
            switch (searchType)
            { //查询类型:0,离线；1,在线（百度）；
                case 1:
                    header = "IP,省市县,运营商\r\n";
                    File.WriteAllText(Application.StartupPath + @"\IPWatchResultWithHeader_" +
                        string.Format("{0:yyyyMMddHHmmssffff}", DateTime.Now) + ".csv",
                        header + txtResult.Text, Encoding.UTF8);
                    break;
                default:
                    header = "IP,国家,省份,市县,地址\r\n";
                    File.WriteAllText(Application.StartupPath + @"\IPWatchResultWithHeader_" +
                        string.Format("{0:yyyyMMddHHmmssffff}", DateTime.Now) + ".csv",
                        header + txtResult.Text, Encoding.UTF8);
                    break;
            }
            
        }

        /// <summary>
        /// 不带表头导出CSV文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNoHeadExp_Click(object sender, EventArgs e)
        {
            File.WriteAllText(Application.StartupPath+ @"\IPWatchResultNoHeader_" + string.Format("{0:yyyyMMddHHmmssffff}", DateTime.Now) + ".csv", txtResult.Text, Encoding.UTF8);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnCls_Click(object sender, EventArgs e)
        {
            txtResult.Text = "";
            txtIPList.Text = "";
        }

        /// <summary>
        /// 更新离线数据库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdateIPDB_Click(object sender, EventArgs e)
        {
            string getIPDBUrl = "http://api.ipip.net/api.php?a=ipdb";

            WebRequest webReq = WebRequest.Create(getIPDBUrl);
            WebResponse webResp = webReq.GetResponse();
            Stream stream = webResp.GetResponseStream();

            StreamReader sr = new StreamReader(stream, Encoding.UTF8);
            string html = sr.ReadToEnd();
            sr.Close();
            stream.Close();

            txtResult.Text = "当前版本：非常遗憾,还没有找到接口获取当前版本信息。\r\n\r\n"+
                "最新版本："+html.Split('|')[1]+"\r\n"+
                "下载地址："+html.Split('|')[2]+"\r\n\r\n"+
                "时间关系，程序不实现自动下载了，自行下载【17monipdb.dat】文件替换当前目录下的同名文件，即可实现更新。";
        }

        /// <summary>
        /// 查询本机IP
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLocalIP_Click(object sender, EventArgs e)
        {
            //http://api.ipip.net/api.php?a=ipdb
            //113.119.134.38|20150801|http://s.qdcdn.com/17mon/17monipdb.dat

            IP.EnableFileWatch = true;
            IP.Load("17monipdb.dat");
            string getIPDBUrl = "http://api.ipip.net/api.php?a=ipdb";

            WebRequest webReq = WebRequest.Create(getIPDBUrl);
            WebResponse webResp = webReq.GetResponse();
            Stream stream = webResp.GetResponseStream();

            StreamReader sr = new StreamReader(stream, Encoding.UTF8);
            string html = sr.ReadToEnd();
            sr.Close();
            stream.Close();

            string ip = html.Split('|')[0];
            txtResult.Text = ip+"," + string.Join(",", IP.Find(ip)) + "\r\n";
        }

        /// <summary>
        /// 关于按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show("花了小点时间，写此工具方便批量查询IP地址用。\r\n            -by:Tanyc");
        }

        /// <summary>
        /// 在线查询IP地址（百度IP搜索接口）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnIPSearchOnline_Click(object sender, EventArgs e)
        {
            searchType = 1;
            if (txtIPList.Text == "")
            {
                MessageBox.Show("请输入要查找的IP地址");
                return;
            }
            string[] ipArr = txtIPList.Text.Split('\n');

            onlineSearchProgressBar.Maximum = ipArr.Length;
            txtResult.Text = "";

            //起一个后台线程跑
            Thread t1 = new Thread(new ParameterizedThreadStart(OnlineFindIPInfoThread));
            t1.IsBackground = true;
            t1.Start(ipArr);
        }

        /// <summary>
        /// 在线查询线程调用方法
        /// </summary>
        /// <param name="ipArr"></param>
        private void OnlineFindIPInfoThread(object ipArr) 
        {
            OnlineFindIPInfo((string[])ipArr);
        }

        /// <summary>
        /// 在线批量查询IP地址
        /// </summary>
        /// <param name="ipArr"></param>
        private void OnlineFindIPInfo(object ipArr)
        {
            string[] tempIpArr = (string[])ipArr;
            setBtnEnable(false);
            //onlineSearchProgressBar.Value = 0;
            setProgressBarValue(onlineSearchProgressBar,0);
            //校验IP列表的合法性
            Regex reg = new Regex(@"((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)");

            //按每一行进行查找
            string resultMsg = "";
            for (int i = 0; i < tempIpArr.Length; i++)
            {

                setProgressBarValue(onlineSearchProgressBar, i+1);
                log("查询进行中[ " + i + "/" + tempIpArr.Length + " ]...");
                if (tempIpArr[i] != "")
                {
                    try
                    {
                        string ip = reg.Match(tempIpArr[i]).Value;

                        string tempStr = ip + "," + string.Join(",", IP.OnlineFind(ip));
                        for (int __count = Regex.Matches(tempStr, @",").Count; __count < 2; __count++)
                        {
                            tempStr += ",";
                        }
                        tempStr += "\r\n";

                        resultMsg += tempStr;
                    }
                    catch
                    {
                        resultMsg += tempIpArr[i] + ",不合法的IP地址,\r\n";
                    }
                }
                else
                {
                    resultMsg += ",,\r\n";
                }
            }
            addResult(resultMsg);

            setBtnEnable(true);

            setProgressBarValue(onlineSearchProgressBar, 0);
        }

        

        /// <summary>
        /// 设置按钮是否可用
        /// </summary>
        /// <param name="flag">true，可用；false，不可用</param>
        private void setBtnEnable(bool flag)
        {
            setCtrlEnabled(btnCls, flag);
            setCtrlEnabled(btnLocalIP, flag);
            setCtrlEnabled(btnUpdateIPDB, flag);
            setCtrlEnabled(btnIPSeach, flag);
            setCtrlEnabled(btnIPSearchOnline, flag);
            setCtrlEnabled(btnWithHeadExp, flag);
            setCtrlEnabled(btnNoHeadExp, flag);
        }

        /// <summary>
        /// 委托 设置控件是否可用
        /// </summary>
        /// <param name="ctrl">控件</param>
        /// <param name="l">true，可用；false，不可用</param>
        delegate void SetCtrlEnabledDelegate(Control ctrl, bool l);//定义委托
        private void setCtrlEnabled(Control ctrl,bool flag)
        {
            if (ctrl.InvokeRequired)//等待异步
            {
                SetCtrlEnabledDelegate setCtrlEnabledDelegate = new SetCtrlEnabledDelegate(setCtrlEnabled);
                ctrl.Invoke(setCtrlEnabledDelegate, ctrl, flag);//通过代理调用方法
            }
            else
            {
                ctrl.Enabled = flag;
            }
        }

        /// <summary>
        /// 委托 输出日志信息
        /// </summary>
        /// <param name="l">日志内容</param>
        delegate void LogDelegate(string l);//定义委托
        private void log(string l)
        {
            if (this.txtResult.InvokeRequired)//等待异步
            {
                LogDelegate writelog = new LogDelegate(log);
                this.txtResult.Invoke(writelog, l);//通过代理调用方法
            }
            else
            {
                int removeCount = txtResult.Text.IndexOf("\r\n") + 1;
                //日志输出行数超过20行，则删除前面的
                if (txtResult.GetLineFromCharIndex(txtResult.Text.Length) > 20)
                {
                    txtResult.Text = txtResult.Text.Remove(0, removeCount);
                }

                txtResult.AppendText(DateTime.Now.ToString("HH:mm:ss ") + l + "\r\n");
            }
        }

        /// <summary>
        /// 写入结果
        /// </summary>
        /// <param name="resultStr"></param>
        private void addResult(string resultStr)
        {
            if (this.txtResult.InvokeRequired)//等待异步
            {
                LogDelegate writelog = new LogDelegate(addResult);
                this.txtResult.Invoke(writelog, resultStr);//通过代理调用方法
            }
            else
            {
               
                txtResult.Text=resultStr;
            }
        }

        /// <summary>
        /// 委托 设置进度条进度值
        /// </summary>
        /// <param name="progressBar1"></param>
        /// <param name="value"></param>
        delegate void SetProgressBarValueDelegate(ProgressBar progressBar1, int value);
        private void setProgressBarValue(ProgressBar progressBar1, int value)
        {
            if (progressBar1.InvokeRequired)
            {
                object[] parameters = new object[] {progressBar1,value };
                progressBar1.Invoke(new SetProgressBarValueDelegate(setProgressBarValue), parameters);
            }
            else
                progressBar1.Value = value;
        }

        private void MainFrm_Load(object sender, EventArgs e)
        {
            mainFrm = this;
        }



    }
}
