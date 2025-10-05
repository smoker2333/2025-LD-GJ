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

    public Action loseGameEvent;
    public Action winGameEvent;
    public Action<float, Vector3> addScoreEvent;
    public event Action<float> OnTimeChanged;// ʱ��仯�¼�

    public float timeLimitInSeconds = 60f; // ��Ϸʱ�����ƣ���λΪ��
    private float currentTime;

    public bool isPaused = false;

    public bool IsGameOver { get; private set; } = false;

    private Coroutine countTimeCoroutine;

    void Awake()
    {
        EventHub.OnGameRestart += RestartTheGame;


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

    public void StarTheGame()
    {
        ResetData();
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

    }


    public void PauseTheGame()
    {
        if (countTimeCoroutine != null)
        {
           isPaused=true;
        }
    }

    public void ResumeTheGame()
    {
        if (countTimeCoroutine != null)
        {
          isPaused=false;
        }
    }

    public void GameLose()
    {
        PauseTheGame();
        loseGameEvent?.Invoke();

    }

    public void GameWin()
    {
        PauseTheGame();
       winGameEvent?.Invoke();
    }



    public void RestartTheGame()
    {
        //���¿�ʼ��ǰ����
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        StarTheGame();
    }
}


