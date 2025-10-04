using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapBlock : MonoBehaviour
{
    //当Object物体碰撞到这个TrapBlock时，Object物体的速度会变为0
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Object"))
        {
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = Vector2.zero; // 将物体的速度设为0
            }
        }
    }



  
}
