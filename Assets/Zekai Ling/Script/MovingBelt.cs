using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBelt : MonoBehaviour
{
    public BeltPlatform beltPlatform; // 新传送带平台预制件

    public float speed = 2f;          // 传送带移动速度


   // public float forceAmount = 5f;    // 施加的力的大小
    public Vector2 direction = Vector2.right; // 移动方向（右）

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
     

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 当物体进入传送带时，在发生碰撞的对应位置下方生成一个新的传送带平台
        if (collision.gameObject.CompareTag("Object"))
        {
            Vector3 spawnPosition = new Vector3(collision.transform.position.x, transform.position.y, transform.position.z);
           
            BeltPlatform newPlatForm= Instantiate(beltPlatform, spawnPosition, Quaternion.identity);

            newPlatForm.speed = speed; // 设置新传送带平台的速度
        }
    }

}
