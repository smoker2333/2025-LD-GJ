using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    public ObjectDataCount[] objectPrefabs; //Ҫ���ɵ�����Ԥ�Ƽ�
     
    public MovingBelt movingBelt; //���ʹ��ű�����

    int index;


    public float spawnInterval = 2f; //���ɼ��ʱ��
    public Transform spawnPoint; //����λ��

    private float timer;

    private void Start()
    {
        timer = spawnInterval; //��ʼ����ʱ��
    }

    private void Update()
    {
        timer -= Time.deltaTime; //���ټ�ʱ��
        if (timer <= 0f&&index<objectPrefabs.Length)
        {
            Spawn(); //��������
            timer = spawnInterval; //���ü�ʱ��
        }
    }

    private void Spawn()
    {
        //��0��objectPrefabs.Length-1���μ��ʱ����������
        Instantiate(objectPrefabs[index], spawnPoint.position, Quaternion.identity);
        index++;
        //���index�ﵽ���鳤���������Ӻ�����        
    }
}
