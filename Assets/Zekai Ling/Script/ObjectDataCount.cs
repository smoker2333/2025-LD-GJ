using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDataCount : MonoBehaviour
{
    public float objectValue;
    public float objectWeight;
    public ScriptableObject objectData;

    void Start()
    {
        //从ScriptableObject中获取物体的价值和重量
        if (objectData is ObjectData data)
        {
            objectValue = data.value;
            objectWeight = data.weight;
            gameObject.name = data.objectName; //设置物体的名称
        }

        //设置物体的质量
      Rigidbody2D rigidbody2D=GetComponent<Rigidbody2D>();
        if (rigidbody2D != null)
        {
            rigidbody2D.mass = objectWeight;
        }
    }

    //如果碰到tag为“Destroy”的物体则销毁自己
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Destroy"))
        {
            Destroy(gameObject);
        }
    }
}
