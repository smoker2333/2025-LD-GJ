using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThankYouForPlaying : MonoBehaviour
{
    public void StartTheGame()
    {
        Debug.Log("Start The Game");
        //加载下一个场景
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + 1);
    }


    //退出游戏按钮功能
    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
