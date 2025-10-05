using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    public ObjectDataCount[] objectPrefabs; //要生成的物体预制件
     
    public MovingBelt movingBelt; //传送带脚本引用

    int index;


    public float spawnInterval = 2f; //生成间隔时间
    public Transform spawnPoint; //生成位置

    private float timer;

    private void Start()
    {
        timer = spawnInterval; //初始化计时器
    }

    private void Update()
    {
        timer -= Time.deltaTime; //减少计时器
        if (timer <= 0f&&index<objectPrefabs.Length)
        {
            Spawn(); //生成物体
            timer = spawnInterval; //重置计时器
        }
    }

    private void Spawn()
    {
        //从0到objectPrefabs.Length-1依次间隔时间生成物体
        Instantiate(objectPrefabs[index], spawnPoint.position, Quaternion.identity);
        index++;
        //如果index达到数组长度则不再增加和生成        
    }
}
