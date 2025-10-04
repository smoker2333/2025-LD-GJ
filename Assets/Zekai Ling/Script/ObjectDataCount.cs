using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDataCount : MonoBehaviour
{
    public float objectValue;
    public float objectWeight;

    void Start()
    {
      Rigidbody2D rigidbody2D=GetComponent<Rigidbody2D>();
        if (rigidbody2D != null)
        {
            rigidbody2D.mass = objectWeight;
        }
    }

    //如果碰到tag为“Destroy”的物体则销毁自己
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Destroy"))
        {
            Destroy(gameObject);
        }
    }
}
