using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyGameServer.Net;
using Photon.SocketServer;
using PhotonHostRuntimeInterfaces;

namespace MyGameServer
{
    public class MyClient : ClientPeer
    {
        public MyClient(InitRequest initRequest) : base(initRequest)
        {
        }

        protected override void OnDisconnect(DisconnectReason reasonCode, string reasonDetail)
        {
            
        }

        //处理客户端的请求
        protected override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters)
        {
            //OperationRequest封装了请求的信息
            //SendParameters 参数，传递的数据

            HandlerMediat.Dispatch((OperationCode)operationRequest.OperationCode, this, operationRequest, sendParameters);  // 分发消息

            //通过operationRequest获取到opCode请求，在客户端发起请求的时候我们设置了opCode为1
            //因为客户端可能发起很多次请求，我们服务器要对每一次请求做相应的回应，所以需要相对应的opCode
            switch (operationRequest.OperationCode)
            {
                case 1:
                    MyServer.log.Info("收到一个客户端的请求");

                    //得到客户端的数据并打印到服务器的日志里面
                    //Dictionary<byte, object> data = operationRequest.Parameters;
                    //object intValue;
                    //data.TryGetValue(1, out intValue);
                    //object StringValue;
                    //data.TryGetValue(2, out StringValue);
                    //MyServer.log.Info("从客户端得到数据:" + intValue.ToString() + StringValue.ToString());

                    //从服务器发送数据给客户端
                    //OperationResponse opResponse = new OperationResponse(1);//给其指定一个opCode,区分不同的请求的响应

                    //Dictionary<byte, object> data2 = new Dictionary<byte, object>();
                    //data2.Add(1, 200);
                    //data2.Add(2, "谢谢，我很好！");
                    //opResponse.SetParameters(data2);

                    //这里接受到请求后需要给客户端一个回应
                    //SendOperationResponse(opResponse, sendParameters);//通过这个方法给客户端一个响应

                    //客户端没有发送请求给服务器，服务器直接发送数据给客户端
                    //EventData ed = new EventData(1);//客户端有opCode，那么服务器之间发送就需要eventCode，这我们也设置eventcode为1
                    //ed.Parameters = data2;
                    //SendEvent(ed, new SendParameters());
                    break;
                default:
                    break;
            }
        }
    }
}
