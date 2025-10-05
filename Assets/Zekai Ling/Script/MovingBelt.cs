using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBelt : MonoBehaviour
{
    public BeltPlatform beltPlatform; // �´��ʹ�ƽ̨Ԥ�Ƽ�

    public float speed = 2f;          // ���ʹ��ƶ��ٶ�


   // public float forceAmount = 5f;    // ʩ�ӵ����Ĵ�С
    public Vector2 direction = Vector2.right; // �ƶ������ң�

   /* private void OnCollisionStay2D(Collision2D collision)
    {
        Rigidbody2D rb = collision.rigidbody;
        if (rb != null)
        {
            // ���Ŵ��ʹ�����ʩ���ٶ�
            rb.velocity = new Vector2(direction.x * speed, rb.velocity.y);
        }
       *//* Rigidbody2D rb = collision.rigidbody;
        if (rb != null)
        {
            // ����ʩ��һ�����ҵ��������������޸��ٶ�
            rb.AddForce(Vector2.right * forceAmount, ForceMode2D.Force);
        }*//*
    }*/
     

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // ��������봫�ʹ�ʱ���ڷ�����ײ�Ķ�Ӧλ���·�����һ���µĴ��ʹ�ƽ̨
        if (collision.gameObject.CompareTag("Object"))
        {
            Vector3 spawnPosition = new Vector3(collision.transform.position.x, transform.position.y, transform.position.z);
           
            BeltPlatform newPlatForm= Instantiate(beltPlatform, spawnPosition, Quaternion.identity);

            newPlatForm.speed = speed; // �����´��ʹ�ƽ̨���ٶ�
        }
    }

}
