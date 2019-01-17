using UnityEngine;
using UnityEngine.SceneManagement;

public class UnNetEnter : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SceneChange()
    {
        PlayerPrefs.SetString("UserName", "UnNet");
        SceneManager.LoadScene("GameStoryFirst");
    }
}