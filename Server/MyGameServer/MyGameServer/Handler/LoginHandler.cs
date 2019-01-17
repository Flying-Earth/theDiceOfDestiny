using MyGameServer.Manager;
using MyGameServer.Model;
using MyGameServer.Net;
using MyGameServer.Tools;
using Photon.SocketServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MyGameServer.Handler
{
    //处理登陆请求的类
    class LoginHandler : IHandlerBase
    {
        // 添加监听
        public void AddListener()
        {
            HandlerMediat.AddListener(OperationCode.Login, OnLoginReceived);
            HandlerMediat.AddListener(OperationCode.Register, OnRegisterReceived);
        }

        // 移除监听
        public void RemoveListener()
        {
            HandlerMediat.RemoveListener(OperationCode.Login, OnLoginReceived);
            HandlerMediat.RemoveListener(OperationCode.Register, OnRegisterReceived);
        }

        // 登陆请求的处理的代码
        void OnLoginReceived(MyClient peer, OperationRequest operationRequest, SendParameters sendParameters)
        {
            //根据发送过来的请求获得用户名和密码
            string username = DictTool.GetValue<byte, object>(operationRequest.Parameters, 1) as string;
            string password = DictTool.GetValue<byte, object>(operationRequest.Parameters, 2) as string;
            //连接数据库进行校验
            UserManager manager = new UserManager();
            bool isSuccess = manager.VerifyUser(username, password);
            OperationResponse response = new OperationResponse(operationRequest.OperationCode);
            //如果验证成功，把成功的结果利用response.ReturnCode返回成功给客户端
            if (isSuccess)
            {
                Dictionary<byte, object> data = new Dictionary<byte, object>();
                data.Add(1, username);
                response.SetParameters(data);
                response.ReturnCode = (short)ReturnCode.Success;
            }
            else//否则返回失败给客户端
            {
                response.ReturnCode = (short)ReturnCode.Failed;
            }
            //把上面的回应给客户端
            peer.SendOperationResponse(response, sendParameters);

        }

        // 注册请求的处理的代码
        void OnRegisterReceived(ClientPeer peer, OperationRequest operationRequest, SendParameters sendParameters)
        {
            string username = DictTool.GetValue<byte, object>(operationRequest.Parameters, 1) as string;
            string password = DictTool.GetValue<byte, object>(operationRequest.Parameters, 2) as string;
            UserManager manager = new UserManager();
            //User user = manager.GetByUsername(username);//根据username查询数据
            bool isRegister = manager.VerifyUserName(username);
            OperationResponse responser = new OperationResponse(operationRequest.OperationCode);
            //如果没有查询到代表这个用户没被注册过可用
            if (!isRegister)
            {
                //添加输入的用户和密码进数据库
                User user = new User() { Username = username, Password = password, Registerdate = DateTime.Now };
                manager.Add(user);
                responser.ReturnCode = (short)ReturnCode.Success;//返回成功

            }
            else//否者这个用户被注册了
            {
                responser.ReturnCode = (short)ReturnCode.Failed;//返回失败
            }
            // 把上面的结果给客户端
            peer.SendOperationResponse(responser, sendParameters);

        }
    }
}
