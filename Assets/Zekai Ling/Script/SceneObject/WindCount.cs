using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindCount : MonoBehaviour
{
    public int count = 0;
    public int maxCount = 10;
    //�����������2D��ײ��
   Collider2D selfCollider;
    //��ȡ�������2D��ײ��
  //  public bool isTestOn = false;
    private void Start()
    {
        selfCollider = GetComponent<Collider2D>();
    }
    void Update()
    {
        if (count > 0)
        {
            if (selfCollider.usedByEffector == true)
            {
                selfCollider.usedByEffector = false;
                //���selfCollider.contactCaptureLayers�е�Default��
                selfCollider.contactCaptureLayers |= LayerMask.GetMask("Default");//���Default��


            }
        }
        else
        {
            if (selfCollider.usedByEffector == false)
            {
                selfCollider.usedByEffector = true;
                //�Ƴ�selfCollider.contactCaptureLayers�е�Default��
                selfCollider.contactCaptureLayers &= ~LayerMask.GetMask("Default");//�Ƴ�Default��
            }

        }
    }
    private void FixedUpdate()
    {
        //��windCount����0ʱ�����ϼ�һ
        if (count > 0)
        {
            count--;
        }
    }
}
