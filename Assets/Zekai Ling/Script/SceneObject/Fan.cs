using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan :MonoBehaviour
{
    //如果_isStart变为true时，播放一次AnitGravity音效
    private bool isPlayed = false; // 是否已经播放过音效

    public BlockAbleWind[] blockAbleWinds; // 存储所有 BlockAbleWind 组件的数组

    public ScriptableObject fanData; // 风扇数据脚本对象

    private void Start()
    {
        LoadWindData(); // 加载风扇数据
    
    }

    void LoadWindData()
    {
        FanData data = fanData as FanData; // 将 ScriptableObject 转换为 FanData 类型
        if (data != null)
        {
            foreach (BlockAbleWind wind in blockAbleWinds)
            {
                wind.raycastDirection = data.windDirection; // 设置风的方向
                wind.areaEffector2D.forceMagnitude = data.windStrength; // 设置风的强度
            }
        }      
    }

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
