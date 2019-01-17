using UnityEngine;
using System.Collections;
using ExitGames.Client.Photon;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

namespace Net
{
    public class LoginHandler : HandlerBase
    {
        public Text loginPassageText;
        public Text registerPassageText;

        /// <summary>
        /// 注册监听事件
        /// </summary>
        public override void AddListener()
        {
            HandlerMediat.AddListener(OperationCode.Login, OnLoginReceived);
            HandlerMediat.AddListener(OperationCode.Register, OnRegisterReceived);
        }

        /// <summary>
        /// 移除监听事件
        /// </summary>
        public override void RemoveListener()
        {
            HandlerMediat.RemoveAllListener(OperationCode.Login);
            HandlerMediat.RemoveAllListener(OperationCode.Register);
        }

        /// <summary>
        /// 收到登录消息
        /// </summary>
        void OnLoginReceived(OperationResponse response)
        {
            ReturnCode returnCode = (ReturnCode)response.ReturnCode;
            string passage = "";
            if (returnCode == ReturnCode.Success)
            {
                //验证成功，跳转到下一个场景
                passage = "用户名和密码验证成功";
                Debug.Log(passage);
                loginPassageText.text = passage;
                Dictionary<byte, object> data = response.Parameters;
                object user;
                data.TryGetValue(1, out user);
                string userName = user.ToString();
                PlayerPrefs.SetString("UserName", userName);
                SceneManager.LoadScene("GameStoryFirst");
            }
            else if (returnCode == ReturnCode.Failed)
            {
                passage = "用户名或密码错误";
                Debug.LogError(passage);
                loginPassageText.text = passage;
            }
        }

        /// <summary>
        /// 收到注册消息
        /// </summary>
        void OnRegisterReceived(OperationResponse response)
        {
            ReturnCode returnCode = (ReturnCode)response.ReturnCode;
            string passage = "";
            if (returnCode == ReturnCode.Success)
            {
                passage = "注册成功，请返回登陆";
                Debug.Log(passage);
                registerPassageText.text = passage;
            }
            else if (returnCode == ReturnCode.Failed)
            {
                passage = "所用的用户名已被注册，请更改用户名";
                Debug.LogError(passage);
                registerPassageText.text = passage;
            }
        }
    }
}