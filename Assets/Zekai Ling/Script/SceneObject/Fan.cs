using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan :MonoBehaviour
{
    //���_isStart��Ϊtrueʱ������һ��AnitGravity��Ч
    private bool isPlayed = false; // �Ƿ��Ѿ����Ź���Ч
    public void SwitchTheFan()
    {
        //   if (isStart)

        if (!isPlayed)
        {


            isPlayed = true; // ����Ϊ�Ѳ���
        }

        else
        {
            isPlayed = false; // ����Ϊδ����״̬
        }
    }
    private void Update()
    {
        // ������ر�����������SwitchTheFan����       
        SwitchTheFan();
    }


}
