using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExitGames.Client.Photon;
using UnityEngine;

namespace Net
{
    public class SaveGameHandler : HandlerBase
    {
        /// <summary>
        /// 注册监听事件
        /// </summary>
        public override void AddListener()
        {
            HandlerMediat.AddListener(OperationCode.SaveGame, OnSaveGameReceived);
            HandlerMediat.AddListener(OperationCode.LoadGame, OnLoadGameReceived);
            HandlerMediat.AddListener(OperationCode.SaveCardSequence, OnSaveCardSequenceReceived);
            HandlerMediat.AddListener(OperationCode.LoadCardSequence, OnLoadCardSequenceReceived);
        }

        /// <summary>
        /// 移除监听事件
        /// </summary>
        public override void RemoveListener()
        {
            HandlerMediat.RemoveAllListener(OperationCode.SaveGame);
            HandlerMediat.RemoveAllListener(OperationCode.LoadGame);
            HandlerMediat.RemoveAllListener(OperationCode.SaveCardSequence);
            HandlerMediat.RemoveAllListener(OperationCode.LoadCardSequence);
        }

        /// <summary>
        /// 收到存储消息
        /// </summary>
        /// <param name="response"></param>
        private void OnSaveGameReceived(OperationResponse response)
        {
            ReturnCode returnCode = (ReturnCode)response.ReturnCode;
            string passage = "";
            if (returnCode == ReturnCode.Success)
            {
                //存储成功
                passage = "存储成功";
                Debug.Log(passage);
            }
            else if (returnCode == ReturnCode.Failed)
            {
                //修改成功
                passage = "修改成功";
                Debug.LogError(passage);
            }
        }

        /// <summary>
        /// 收到加载信息
        /// </summary>
        /// <param name="response"></param>
        private void OnLoadGameReceived(OperationResponse response)
        {
            ReturnCode returnCode = (ReturnCode)response.ReturnCode;
            string passage = "";
            if (returnCode == ReturnCode.Success)
            {
                //加载成功
                passage = "加载成功";
                Debug.Log(passage);

                Dictionary<byte, object> data = response.Parameters;
                SaveGameManager.instance.SetLoadStatus(data);
            }
            else if (returnCode == ReturnCode.Failed)
            {
                //加载失败
                passage = "加载失败";
                Debug.LogError(passage);
            }
        }

        private void OnSaveCardSequenceReceived(OperationResponse response)
        {
            ReturnCode returnCode = (ReturnCode)response.ReturnCode;
            string passage = "";
            if (returnCode == ReturnCode.Success)
            {
                //存储成功
                passage = "保存成功";
                Debug.Log(passage);
            }
            else if (returnCode == ReturnCode.Failed)
            {
                //修改成功
                passage = "更改成功";
                Debug.LogError(passage);
            }
        }


        private void OnLoadCardSequenceReceived(OperationResponse response)
        {
            ReturnCode returnCode = (ReturnCode)response.ReturnCode;
            string passage = "";
            if (returnCode == ReturnCode.Success)
            {
                //读取成功
                passage = "读取成功";
                Debug.Log(passage);

                Dictionary<byte, object> card = response.Parameters;
                SaveGameManager.instance.SetLoadCardSequence(card);


            }
            else if (returnCode == ReturnCode.Failed)
            {
                //读取失败
                passage = "读取失败";
                Debug.LogError(passage);
            }
        }
    }
}
