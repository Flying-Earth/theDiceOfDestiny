using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Net
{
    public class LoginRequest : Singleton<LoginRequest>
    {
        /// <summary>
        /// 发送登录请求
        /// </summary>
        public void SendLoginRequest(string username, string password)
        {
            Dictionary<byte, object> data = new Dictionary<byte, object>();
            data.Add(1, username);
            data.Add(2, password);
            PhotonEngine.Peer.OpCustom((byte)OperationCode.Login, data, true);
        }

        /// <summary>
        /// 发送注册请求
        /// </summary>
        public void SendRegisterRequest(string username, string password)
        {
            Dictionary<byte, object> data = new Dictionary<byte, object>();
            data.Add(1, username);
            data.Add(2, password);
            PhotonEngine.Peer.OpCustom((byte)OperationCode.Register, data, true);
        }
    }
}
