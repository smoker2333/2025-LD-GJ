using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    //��������
    public float currentGameScore;

    public float scoreToWinGame;

  //  public Action resumeGame;
    public Action pauseGame;
    public Action loseGameEvent;
    public Action winGameEvent;
    public Action<float, Vector3> addScoreEvent;
    public event Action<float> OnTimeChanged;// ʱ��仯�¼�

    public float timeLimitInSeconds = 60f; // ��Ϸʱ�����ƣ���λΪ��
    private float currentTime;

    public bool isPaused = false;

    public bool IsGameWined { get; private set; } = false;

    private Coroutine countTimeCoroutine;

    void Awake()
    {
        EventHub.OnGameRestart += RestartTheGame;
        EventHub.OnGamePause += PauseTheGame;
        EventHub.OnGameResume += ResumeTheGame;
        EventHub.NextLevelEvent += LoadNextLevel;

        if (instance == null)
        {
            instance = this;
          //  DontDestroyOnLoad(gameObject);
            Debug.Log("GameManager initialized.");
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        StarTheGame();
    }
    private void OnDisable()
    {
        EventHub.OnGameRestart -= RestartTheGame;
        EventHub.OnGamePause -= PauseTheGame;
        EventHub.OnGameResume -= ResumeTheGame;
        EventHub.NextLevelEvent -= LoadNextLevel;

    }

    IEnumerator CountdownRoutine()
    {
        while (currentTime > 0)
        {
            // ��ͣʱ���ݼ�
            while (isPaused)
                yield return null;

            currentTime -= Time.deltaTime;         
            yield return null; // ÿִ֡��һ��                              
            OnTimeChanged?.Invoke(currentTime);// ֪ͨ UI ����ʱ��
        }
       GameLose();
    }



    //�ӷ��¼�
    public void AddScore(float scoreToAdd, Vector3 position)
    {
        currentGameScore += scoreToAdd;
        addScoreEvent?.Invoke(scoreToAdd,position);
        CheckScore();
    }


    public void CheckScore()
    {
        if (currentGameScore >= scoreToWinGame)
        {
            GameWin();
        }

    }

    public void ResetScore()
    {
        currentGameScore = 0; 
    }


    public void ResumeGame()
    {
        pauseGame?.Invoke();
    }

    public void StarTheGame()
    {
        ResetData();
        
        // 播放游戏主场景背景音乐
        MusicManager.Instance.PlayMusic(MusicManager.Instance.mainGameSceneMusic);
        
        if (countTimeCoroutine != null)
        {
            StopCoroutine(countTimeCoroutine);
            countTimeCoroutine = null;
            countTimeCoroutine = StartCoroutine(CountdownRoutine());
        }
        else
        {
            countTimeCoroutine = StartCoroutine(CountdownRoutine());
        }
       
        
    }
    public void ResetData()
    {
        ResetScore();
        currentTime = timeLimitInSeconds;
        isPaused = false;
        Time.timeScale = 1f;

    }


    public void PauseTheGame()
    {
        if (countTimeCoroutine != null)
        {
           isPaused=true;
        }
        Time.timeScale=0f;//��ͣ��Ϸ
    }

    public void ResumeTheGame()
    {     
        if (countTimeCoroutine != null)
        {
          isPaused=false;
        }
        Time.timeScale=1f;//�ָ���Ϸ
    }

    public void GameLose()
    {
        if (!IsGameWined)
        {
            PauseTheGame();
            
            // 播放游戏失败音效
            SoundManager.Instance.PlaySound(SoundManager.Instance.failSound);
            
            loseGameEvent?.Invoke();
        }       
    }

    public void GameWin()
    {
       // PauseTheGame();
       
       // 播放游戏成功音效
       SoundManager.Instance.PlaySound(SoundManager.Instance.successSound);
       
       winGameEvent?.Invoke();
        IsGameWined = true;
    }

    public void QuitTheGame()
    {
        Application.Quit();
    }

    public void LoadNextLevel()
    {
        //������һ������
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
            StarTheGame();
        }
        else
        {
            Debug.Log("�Ѿ������һ���������޷�������һ��������");
        }

    }

    public void RestartTheGame()
    {
        //���¿�ʼ��ǰ����
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        StarTheGame();
    }
}


