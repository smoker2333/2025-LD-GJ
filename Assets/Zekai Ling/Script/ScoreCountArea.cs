using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCountArea : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //如果碰撞到的物体的标签是"Object"
        if (collision.CompareTag("Object"))
        {
            //获取碰撞物体上的ObjectDataCount组件
            ObjectDataCount objectData = collision.GetComponent<ObjectDataCount>();
            if (objectData != null)
            {
                //将物体的分数加到游戏管理器的当前分数中
                GameManager.instance.AddScore(objectData.objectValue, collision.transform.position);
                Debug.Log("当前分数: " + GameManager.instance.currentGameScore);
               
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //如果碰撞到的物体的标签是"Object"
        if (collision.CompareTag("Object"))
        {
            //获取碰撞物体上的ObjectDataCount组件
            ObjectDataCount objectData = collision.GetComponent<ObjectDataCount>();
            if (objectData != null)
            {
                //将物体的分数加到游戏管理器的当前分数中
                GameManager.instance.AddScore(-objectData.objectValue, collision.transform.position);
                Debug.Log("当前分数: " + GameManager.instance.currentGameScore);

            }
        }
        
    }






}
