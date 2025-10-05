using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan :MonoBehaviour
{
    //���_isStart��Ϊtrueʱ������һ��AnitGravity��Ч
    private bool isPlayed = false; // �Ƿ��Ѿ����Ź���Ч

    public BlockAbleWind[] blockAbleWinds; // �洢���� BlockAbleWind ���������

    public ScriptableObject fanData; // �������ݽű�����

    private void Start()
    {
        LoadWindData(); // ���ط�������
    
    }

    void LoadWindData()
    {
        FanData data = fanData as FanData; // �� ScriptableObject ת��Ϊ FanData ����
        if (data != null)
        {
            foreach (BlockAbleWind wind in blockAbleWinds)
            {
                wind.raycastDirection = data.windDirection; // ���÷�ķ���
                wind.areaEffector2D.forceMagnitude = data.windStrength; // ���÷��ǿ��
            }
        }      
    }

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
