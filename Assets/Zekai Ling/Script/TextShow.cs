using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextShow : MonoBehaviour
{
    public float showDuration = 3f; //չʾʱ��

    //������չʾ3�������
    private void Update()
    {
        showDuration -= Time.deltaTime;
        if (showDuration <= 0f)
        {
            Destroy(gameObject);
        }
    }
    
}
