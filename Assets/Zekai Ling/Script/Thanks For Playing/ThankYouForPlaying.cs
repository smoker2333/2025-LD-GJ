using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThankYouForPlaying : MonoBehaviour
{
    public void StartTheGame()
    {
        Debug.Log("Start The Game");
        //������һ������
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + 1);
    }


    //�˳���Ϸ��ť����
    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
