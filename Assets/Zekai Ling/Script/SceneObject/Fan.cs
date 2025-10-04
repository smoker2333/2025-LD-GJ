using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan :MonoBehaviour
{
    //如果_isStart变为true时，播放一次AnitGravity音效
    private bool isPlayed = false; // 是否已经播放过音效
    public void SwitchTheFan()
    {
        //   if (isStart)

        if (!isPlayed)
        {


            isPlayed = true; // 设置为已播放
        }

        else
        {
            isPlayed = false; // 重置为未播放状态
        }
    }
    private void Update()
    {
        // 如果开关被触发，调用SwitchTheFan方法       
        SwitchTheFan();
    }


}
