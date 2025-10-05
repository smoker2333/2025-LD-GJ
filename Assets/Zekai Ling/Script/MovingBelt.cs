using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBelt : MonoBehaviour
{
    public float speed = 2f;          // 传送带移动速度


   // public float forceAmount = 5f;    // 施加的力的大小
    public Vector2 direction = Vector2.right; // 移动方向（右）

    private void OnCollisionStay2D(Collision2D collision)
    {
        Rigidbody2D rb = collision.rigidbody;
        if (rb != null)
        {
            // 沿着传送带方向施加速度
            rb.velocity = new Vector2(direction.x * speed, rb.velocity.y);
        }
        /*  Rigidbody2D rb = collision.rigidbody;
          if (rb != null)
          {
              // 持续施加一个向右的推力，而不是修改速度
              rb.AddForce(Vector2.right * forceAmount, ForceMode2D.Force);
          }*/
    }
}
