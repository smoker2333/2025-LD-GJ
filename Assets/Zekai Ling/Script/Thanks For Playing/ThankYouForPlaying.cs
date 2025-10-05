using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThankYouForPlaying : MonoBehaviour
{
    //退出游戏按钮功能
    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
