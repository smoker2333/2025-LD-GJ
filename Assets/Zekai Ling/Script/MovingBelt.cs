using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBelt : MonoBehaviour
{
    [Header("��������")]
    public Transform beltSpawnPoint;
    public BeltPlatform beltPlatformPrefab;

    [Header("���ʹ�����")]
    public float spawnInterval = 0.5f;
    public float speed = 2f;

    [Header("���������")]
    public int poolSize = 10; // Ԥ�ȴ���������

    private List<BeltPlatform> platformPool;
    private int currentIndex = 0;

    private void Start()
    {
        //��ʼ�������
        InitializePool();

        //����ѭ������
        MusicManager.Instance.PlayAmbientSound(MusicManager.Instance.conveyorBeltAmbientSound);
        
        InvokeRepeating(nameof(SpawnPlatformTick), 0f, spawnInterval);

    }

    private void SpawnPlatformTick()
    {
        SpawnPlatform(beltSpawnPoint.position);
    }

    private void InitializePool()
    {
        platformPool = new List<BeltPlatform>();

        for (int i = 0; i < poolSize; i++)
        {
            BeltPlatform newPlatform = Instantiate(beltPlatformPrefab);
            newPlatform.gameObject.SetActive(false);  // ������
            platformPool.Add(newPlatform);
        }
    }

    private void SpawnPlatform(Vector3 newPosition)
    {
        //��ȡһ��δʹ�õ�ƽ̨
        BeltPlatform platform = GetPooledPlatform();
        if (platform == null)
        {
            // ����ȫ��������ݣ���ѡ��
            platform = Instantiate(beltPlatformPrefab);
            platformPool.Add(platform);
        }
        SetNewPosistion(platform, newPosition);
    }

    public void SetNewPosistion(BeltPlatform platform, Vector3 newPosition)
    {
        // ������ó�ʼ״̬
        platform.transform.position = newPosition;
        platform.speed = speed;
        platform.gameObject.SetActive(true);
    }

    //��Object�������ʹ�ʱ���ڶ�Ӧ��X��λ������һ�����ʹ�ƽ̨��Y��λ�������ɵ���ͬ
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Object"))
        {
            Vector3 spawnPlatformPosition = new Vector3(collision.transform.position.x, beltSpawnPoint.position.y, 0);
            Debug.Log("��X��λ��" + spawnPlatformPosition.x + "���ɴ��ʹ�");
            SpawnPlatform(spawnPlatformPosition);
        }
    }
    
    

    private BeltPlatform GetPooledPlatform()
    {
        // �ӳ�����һ��δ�����
        foreach (var plat in platformPool)
        {
            if (!plat.gameObject.activeInHierarchy)
            {
                return plat;
            }
        }
        return null; // ����ȫ��ռ��
    }

    // public float forceAmount = 5f;    // ʩ�ӵ����Ĵ�С
    //public Vector2 direction = Vector2.right; // �ƶ������ң�

    /* private void OnCollisionStay2D(Collision2D collision)
     {
         Rigidbody2D rb = collision.rigidbody;
         if (rb != null)
         {
             // ���Ŵ��ʹ�����ʩ���ٶ�
             rb.velocity = new Vector2(direction.x * speed, rb.velocity.y);
         }
        *//* Rigidbody2D rb = collision.rigidbody;
         if (rb != null)
         {
             // ����ʩ��һ�����ҵ��������������޸��ٶ�
             rb.AddForce(Vector2.right * forceAmount, ForceMode2D.Force);
         }*//*
     }*/



}
