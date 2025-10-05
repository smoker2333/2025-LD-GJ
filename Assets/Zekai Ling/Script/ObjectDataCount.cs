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
        //��ScriptableObject�л�ȡ����ļ�ֵ������
        if (objectData is ObjectData data)
        {
            objectValue = data.value;
            objectWeight = data.weight;
            gameObject.name = data.objectName; //�������������
        }

        //�������������
      Rigidbody2D rigidbody2D=GetComponent<Rigidbody2D>();
        if (rigidbody2D != null)
        {
            rigidbody2D.mass = objectWeight;
        }
    }

    //�������tagΪ��Destroy���������������Լ�
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Destroy"))
        {
            Destroy(gameObject);
        }
    }
}
