using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    //��������
    public float currentGameScore;

    public float scoreToWinGame;


    public Action loseGameEvent;
    public Action winGameEvent;

    //�ӷ��¼�
    public void AddScore(float scoreToAdd)
    {
        currentGameScore += scoreToAdd;
    }

    public void ResetScore()
    {
        currentGameScore = 0; 
    }


    public void GameLose()
    {

    }


    public void GameWin()
    {

    }
}
