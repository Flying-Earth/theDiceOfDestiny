using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RegisterPanel : MonoBehaviour
{

    public GameObject loginPanel;

    public InputField usernameInput;
    public InputField passwordInput;
    public InputField repasswordInput;
    public Button registerButton;
    public Button returnButton;
    public Text passageText;

    /// <summary>
    /// 注册按钮事件，外部挂载
    /// </summary>
    public void OnRegisterButtonEvent()
    {
        string username = usernameInput.text;
        string password = passwordInput.text;
        string repassword = repasswordInput.text;
        string passage = "";

        if (!password.Equals(repassword))
        {
            passage = "两次密码输入不一致，请重新输入";
            passageText.text = passage;
        }else
            Net.LoginRequest.Instance.SendRegisterRequest(username, password);

    }

    public void OnReturnButtonEvent()
    {
        loginPanel.SetActive(true);
        gameObject.SetActive(false);
    }
}
