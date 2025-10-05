using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIDataDisplay : MonoBehaviour
{
    public GameObject scoreTextObject;

    public TextMeshProUGUI currentScoreText;

    public TextMeshProUGUI timeText;

    public GameObject loseUI;
    public GameObject winUI;

    private void Start()
    {
        // 订阅事件，参数是名字 + 位置
        GameManager.instance.addScoreEvent += ShowObjectInfo;
        GameManager.instance.OnTimeChanged+= UpdateUITime;
        GameManager.instance.loseGameEvent += ShowLoseUI;
        GameManager.instance.winGameEvent += ShowWinUI;
    }

    private void OnDisable()
    {
        // 取消订阅
        GameManager.instance.addScoreEvent -= ShowObjectInfo;
        GameManager.instance.OnTimeChanged -= UpdateUITime;
        GameManager.instance.loseGameEvent -= ShowLoseUI;
        GameManager.instance.winGameEvent -= ShowWinUI;
    }

    private void ShowObjectInfo(float score, Vector3 pos)
    {
        // 如果游戏正在退出或场景卸载，不执行
        if (!this || !gameObject.scene.isLoaded) return;
        GameObject spawnedObj = Instantiate(scoreTextObject, pos, Quaternion.identity);
        spawnedObj.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = score.ToString("F0");      

        currentScoreText.text = "Score: " + GameManager.instance.currentGameScore.ToString("F0");
    }

    void UpdateUITime(float timeRemaining)
    {
        if(timeRemaining>0)
        {
            int minutes = Mathf.FloorToInt(timeRemaining / 60f);
            int seconds = Mathf.FloorToInt(timeRemaining % 60f);
            timeText.text = string.Format("Time: {0:00}:{1:00}", minutes, seconds);
        }

    }


    void ShowLoseUI()
    {
        loseUI.SetActive(true);
    }

    void ShowWinUI()
    {
        winUI.SetActive(true);
        
    }

    public void OnRestartButton()
    {
  /*      loseUI.SetActive(false);
        GameManager.instance.StarTheGame();*/
        EventHub.OnGameRestart?.Invoke();
    }

    public void LoadNextLevelButton()
    {
        EventHub.NextLevelEvent?.Invoke();
    }

}
