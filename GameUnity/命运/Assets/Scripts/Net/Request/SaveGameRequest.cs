using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Net
{
    public class SaveGameRequest : Singleton<SaveGameRequest>
    {
        /// <summary>
        /// 发送存储请求
        /// </summary>
        public void SendSaveGameRequest(string saveGameName, string userName, Player player, int cardNum, List<int> cardSequence, 
            bool transitionA, bool transitionB)
        {
            Dictionary<byte, object> data = SaveGameManager.instance.SetSaveStatus(saveGameName, userName, player, cardNum, transitionA, transitionB);
            Dictionary<byte, object> card = SaveGameManager.instance.SetSaveCardSequence(cardSequence, saveGameName);
            PhotonEngine.Peer.OpCustom((byte)OperationCode.SaveGame, data, true);
            PhotonEngine.Peer.OpCustom((byte)OperationCode.SaveCardSequence, card, true);
        }

        /// <summary>
        /// 发送加载请求
        /// </summary>
        public void SendLoadGameRequest(string saveGameName, string userName)
        {
            Dictionary<byte, object> data = new Dictionary<byte, object>();
            data.Add(1, saveGameName);
            data.Add(2, userName);
            PhotonEngine.Peer.OpCustom((byte)OperationCode.LoadGame, data, true);
            PhotonEngine.Peer.OpCustom((byte)OperationCode.LoadCardSequence, data, true);
        }
    }
}
