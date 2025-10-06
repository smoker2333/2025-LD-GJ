using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
     Animator spawnAnimator; //���ɶ���������

     TextMeshProUGUI remainingText; //�����ı�

    public float spawnAnimationDuration = 0.5f; //���ɶ�������ʱ��

    public ObjectDataCount[] objectPrefabs; //Ҫ���ɵ�����Ԥ�Ƽ�
     
    public MovingBelt movingBelt; //���ʹ��ű�����

    int index;


    public float spawnInterval = 2f; //���ɼ��ʱ��
    public Transform spawnPoint; //����λ��

    private float timer;

    private void Start()
    {
        //��Ѱ�������������������ΪNumText��TextMeshProUGUI���
        remainingText = transform.Find("NumText").GetComponent<TextMeshProUGUI>();

        spawnAnimator = GetComponent<Animator>();
        timer = spawnInterval; //��ʼ����ʱ��

        remainingText.text = (objectPrefabs.Length - index).ToString();
    }

    private void Update()
    {
        timer -= Time.deltaTime; //���ټ�ʱ��
        if (timer <= 0f&&index<objectPrefabs.Length)
        {
           // Spawn(); //��������
           StartCoroutine(Spawning());
            timer = spawnInterval; //���ü�ʱ��
        }
    }

    IEnumerator Spawning()
    {
        //�Ȳ������ɶ���
        spawnAnimator.SetTrigger("Spawn");

        //�ȴ���������ʱ��
        yield return new WaitForSeconds(spawnAnimationDuration);
        //��������
        Spawn();
    }

    private void Spawn()
    {
        //��0��objectPrefabs.Length-1���μ��ʱ����������
        Instantiate(objectPrefabs[index], spawnPoint.position, Quaternion.identity);
        index++;
        remainingText.text = (objectPrefabs.Length - index).ToString();
        //���index�ﵽ���鳤���������Ӻ�����        
    }
}
