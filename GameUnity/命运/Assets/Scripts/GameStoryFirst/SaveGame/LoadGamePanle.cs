using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.GameStoryFirst.SaveGame
{
    class LoadGamePanle : MonoBehaviour
    {
        /// <summary>
        /// 保存游戏按钮事件，外部挂载
        /// </summary>
        public void OnLoadGameButtonEvent()
        {           
            string userName = PlayerPrefs.GetString("UserName");
            string saveGameName = userName + "Save";
            Net.SaveGameRequest.Instance.SendLoadGameRequest(saveGameName, userName);
        }
    }
}
