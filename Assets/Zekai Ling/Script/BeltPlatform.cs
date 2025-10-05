using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeltPlatform : MonoBehaviour
{
    GameObject carryingObject; // 传送带上承载的物体
     
    public float speed = 2f;          // 传送带移动速度
    //当检测到物体进入传送带时，将该物体作为传送带平台的子物体

    Rigidbody2D rb;

    public float forceAmount = 5f;    // 施加的力的大小

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(speed, 0); // 设置传送带的初始速度
    }
    void Update()
    {
        rb.velocity = new Vector2(speed, 0); // 持续设置传送带的速度，确保其移动
           }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //如果碰到标签为Destroy的物体，销毁传送带
        if (collision.gameObject.CompareTag("Destroy"))
        {
            Destroy(gameObject);
            return;
        }

        if (collision.gameObject.CompareTag("Object"))
        {
           /* if (carryingObject != null) return; // 如果已经有承载物体，直接返回
            collision.transform.SetParent(transform);*/
            carryingObject = collision.gameObject;
        }
    }

    //当检测到物体离开传送带时，销毁该传送带平台
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (carryingObject = collision.gameObject)       
        {
            //还原物体的父物体为空
          //  carryingObject.transform.SetParent(null);
           carryingObject = null;
                        
        }
    }

    private void OnDestroy()
    {
        if (carryingObject != null)
        {
            //对目标物体施加一个向右的推力
            Rigidbody2D objRb = carryingObject.GetComponent<Rigidbody2D>();
            if (objRb != null)
            {
                objRb.AddForce(Vector2.right * forceAmount, ForceMode2D.Impulse);
            }

           // carryingObject.transform.SetParent(null);
        }
    }



}
