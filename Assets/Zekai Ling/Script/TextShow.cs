using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextShow : MonoBehaviour
{
    public float showDuration = 3f; //展示时间

    //该物体展示3秒后销毁
    private void Update()
    {
        showDuration -= Time.deltaTime;
        if (showDuration <= 0f)
        {
            Destroy(gameObject);
        }
    }
    
}
