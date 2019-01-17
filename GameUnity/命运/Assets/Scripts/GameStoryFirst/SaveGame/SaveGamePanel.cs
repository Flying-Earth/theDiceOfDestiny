using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.GameStoryFirst.SaveGame
{
    class SaveGamePanel : MonoBehaviour
    {
        /// <summary>
        /// 保存游戏按钮事件，外部挂载
        /// </summary>
        public void OnSaveGameButtonEvent()
        {
            string userName = PlayerPrefs.GetString("UserName");
            string saveGameName = userName + "Save";
            Player player = Player.instance.GetPlayer();
            int cardNum = CardManager.instance.GetCurrentIndex() - 1;
            if (cardNum == -1) cardNum = 0;
            List<int> cardSequence = CardManager.instance.GetCardSequence();
            bool transitionA = GameManager.instance.GetTransA();
            bool transitionB = GameManager.instance.GetTransB();
            Net.SaveGameRequest.Instance.SendSaveGameRequest(saveGameName, userName, player, cardNum, cardSequence, transitionA, transitionB);
        }
    }
}
