using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    //分数部分
    public float currentGameScore;

    public float scoreToWinGame;


    public Action loseGameEvent;
    public Action winGameEvent;

    //加分事件
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
