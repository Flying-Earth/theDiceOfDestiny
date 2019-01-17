using ExitGames.Logging;
using ExitGames.Logging.Log4Net;
using log4net;
using log4net.Config;
using MyGameServer.Handler;
using MyGameServer.Manager;
using Photon.SocketServer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGameServer
{
    public enum ReturnCode
    {
        Success = 0,
        Failed = -1
    }

    public class MyServer : ApplicationBase
    {
        public static readonly ILogger log = ExitGames.Logging.LogManager.GetCurrentClassLogger();

        public new static MyServer Instance { get; private set; }

        // 当一个客户端请求连接的时候，服务器端就会调用这个方法
        // 我们使用peerbase,表示和一个客户端的链接,然后photon就会把这些链接管理起来
        protected override PeerBase CreatePeer(InitRequest initRequest)
        {
            log.Info("一个客户端连接进来了！");
            return new MyClient(initRequest);
        }
        //初始化(当整个服务器启动起来的时候调用这个初始化)
        protected override void Setup()
        {
            Instance = this;
            // 日志的初始化(定义配置文件log4net位置)

            // Path.Combine  表示连接目录和文件名，可以屏蔽平台的差异
            // Photon: ApplicationLogPath 就是配置文件里面路径定义的属性
            //this.ApplicationPath 表示可以获取photon的根目录,就是Photon-OnPremise-Server-SDK_v4-0-29-11263\deploy这个目录
            // 这一步是设置日志输出的文档文件的位置，这里我们把文档放在Photon-OnPremise-Server-SDK_v4-0-29-11263\deploy\bin_Win64\log里面
            log4net.GlobalContext.Properties["Photon:ApplicationLogPath"] = Path.Combine(Path.Combine(Path.Combine(this.ApplicationRootPath, "bin_win64")), "log");
            //this.BinaryPath表示可以获取的部署目录就是目录Photon-OnPremise-Server-SDK_v4-0-29-11263\deploy\MyGameServer\bin
            FileInfo configFileInfo = new FileInfo(Path.Combine(this.BinaryPath, "log4net.config"));// 告诉log4net日志的配置文件的位置
            // 如果这个配置文件存在
            if (configFileInfo.Exists)
            {
                ExitGames.Logging.LogManager.SetLoggerFactory(Log4NetLoggerFactory.Instance);// 设置photon我们使用哪个日志插件
                XmlConfigurator.ConfigureAndWatch(configFileInfo);// 让log4net这个插件读取配置文件
            }

            log.Info("Setup Completed!");// 最后利用log对象就可以输出了

            AddHandler();
        }

        // server端关闭的时候
        protected override void TearDown()
        {
            RemoveHandler();
        }

        #region 所有Handler
        LoginHandler loginHandler;
        SaveGameHandler saveGameHandler;
        //BagHandler bagHandler;
        //EquipmentHandler equipmentHandler;
        #endregion

        // 初始化所有Handler
        public void AddHandler()
        {
            loginHandler = new LoginHandler();
            loginHandler.AddListener();

            saveGameHandler = new SaveGameHandler();
            saveGameHandler.AddListener();
        }

        // 移除所有Handler
        void RemoveHandler()
        {
            loginHandler.RemoveListener();
            saveGameHandler.RemoveListener();
        }
    }
}

