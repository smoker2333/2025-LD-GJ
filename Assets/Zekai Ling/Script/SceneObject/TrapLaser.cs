using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapLaser : MonoBehaviour
{
    public float reflectMultiplier = 1.2f; // 反弹速度倍率（>1 表示更强）
    public ScriptableObject reflectLaserData;

    private void Start()
    {
        if (reflectLaserData != null)
        {
            ReflectLaserData data = reflectLaserData as ReflectLaserData;
            if (data != null)
            {
                reflectMultiplier = data.multiplier;
            }
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Object") == false)
            return;
        Rigidbody2D rb = collision.rigidbody;
        if (rb != null && collision.contacts.Length > 0)
        {
            // 获取碰撞点的法线方向
            Vector2 normal = collision.contacts[0].normal;

            // 当前速度方向
            Vector2 incoming = rb.velocity;

            // 反射向量 = 入射向量根据法线反射
            Vector2 reflect = Vector2.Reflect(incoming, normal);

            // 设置新的速度（可加倍）
            rb.velocity = reflect * reflectMultiplier;
        }
    }
}
