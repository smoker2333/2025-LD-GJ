using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeltPlatform : MonoBehaviour
{
    GameObject carryingObject; // ���ʹ��ϳ��ص�����
     
    public float speed = 2f;          // ���ʹ��ƶ��ٶ�
    //����⵽������봫�ʹ�ʱ������������Ϊ���ʹ�ƽ̨��������

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(speed, 0); // ���ô��ʹ��ĳ�ʼ�ٶ�
    }
    void Update()
    {
        rb.velocity = new Vector2(speed, 0); // �������ô��ʹ����ٶȣ�ȷ�����ƶ�
           }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //���������ǩΪDestroy�����壬���ٴ��ʹ�
        if (collision.gameObject.CompareTag("Destroy"))
        {
            Destroy(gameObject);
            return;
        }

        if (collision.gameObject.CompareTag("Object"))
        {
           /* if (carryingObject != null) return; // ����Ѿ��г������壬ֱ�ӷ���
            collision.transform.SetParent(transform);*/
            carryingObject = collision.gameObject;
        }
    }

    //����⵽�����뿪���ʹ�ʱ�����ٸô��ʹ�ƽ̨
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (carryingObject = collision.gameObject)       
        {
            //��ԭ����ĸ�����Ϊ��
          //  carryingObject.transform.SetParent(null);
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (carryingObject != null)
        {
           // carryingObject.transform.SetParent(null);
        }
    }



}
