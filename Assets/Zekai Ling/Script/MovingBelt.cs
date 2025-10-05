using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBelt : MonoBehaviour
{
    [Header("生成设置")]
    public Transform beltSpawnPoint;
    public BeltPlatform beltPlatformPrefab;

    [Header("传送带参数")]
    public float spawnInterval = 0.5f;
    public float speed = 2f;

    [Header("对象池设置")]
    public int poolSize = 10; // 预先创建的数量

    private List<BeltPlatform> platformPool;
    private int currentIndex = 0;

    private void Start()
    {
        //初始化对象池
        InitializePool();

        //启动循环生成
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
            newPlatform.gameObject.SetActive(false);  // 先隐藏
            platformPool.Add(newPlatform);
        }
    }

    private void SpawnPlatform(Vector3 newPosition)
    {
        //获取一个未使用的平台
        BeltPlatform platform = GetPooledPlatform();
        if (platform == null)
        {
            // 池中全用完就扩容（可选）
            platform = Instantiate(beltPlatformPrefab);
            platformPool.Add(platform);
        }
        SetNewPosistion(platform, newPosition);
    }

    public void SetNewPosistion(BeltPlatform platform, Vector3 newPosition)
    {
        // 激活并设置初始状态
        platform.transform.position = newPosition;
        platform.speed = speed;
        platform.gameObject.SetActive(true);
    }

    //当Object碰到传送带时，在对应的X轴位置生成一个传送带平台，Y轴位置与生成点相同
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Object"))
        {
            Vector3 spawnPlatformPosition = new Vector3(collision.transform.position.x, beltSpawnPoint.position.y, 0);
            Debug.Log("在X轴位置" + spawnPlatformPosition.x + "生成传送带");
            SpawnPlatform(spawnPlatformPosition);
        }
    }
    
    

    private BeltPlatform GetPooledPlatform()
    {
        // 从池中找一个未激活的
        foreach (var plat in platformPool)
        {
            if (!plat.gameObject.activeInHierarchy)
            {
                return plat;
            }
        }
        return null; // 池子全被占用
    }

    // public float forceAmount = 5f;    // 施加的力的大小
    //public Vector2 direction = Vector2.right; // 移动方向（右）

    /* private void OnCollisionStay2D(Collision2D collision)
     {
         Rigidbody2D rb = collision.rigidbody;
         if (rb != null)
         {
             // 沿着传送带方向施加速度
             rb.velocity = new Vector2(direction.x * speed, rb.velocity.y);
         }
        *//* Rigidbody2D rb = collision.rigidbody;
         if (rb != null)
         {
             // 持续施加一个向右的推力，而不是修改速度
             rb.AddForce(Vector2.right * forceAmount, ForceMode2D.Force);
         }*//*
     }*/



}
