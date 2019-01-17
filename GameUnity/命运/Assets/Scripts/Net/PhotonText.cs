using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonText : MonoBehaviour {

    void Update()
    {
        //这里我们没点击一次就向服务器发起一次请求
        if (Input.GetMouseButtonDown(0))
        {
            SendRequest();
        }
    }
    void SendRequest()
    {
        Dictionary<byte, object> data = new Dictionary<byte, object>();
        data.Add(1, 100);
        data.Add(2, "ads你好啊");
        // 参数1 opCode表示发起请求的编号，接收的时候便于找到  参数二客户端需要向服务器传递的数据，是否稳定（如果为false表示数据丢失一两条也没啥）
        Net.PhotonEngine.Peer.OpCustom(1, data, true);//这里我们测试所以先传递空数据过去
    }
}
