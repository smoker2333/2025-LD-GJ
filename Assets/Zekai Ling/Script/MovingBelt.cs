using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBelt : MonoBehaviour
{
    public float speed = 2f;          // ���ʹ��ƶ��ٶ�
    public Vector2 direction = Vector2.right; // �ƶ������ң�

    private void OnCollisionStay2D(Collision2D collision)
    {
        Rigidbody2D rb = collision.rigidbody;
        if (rb != null)
        {
            // ���Ŵ��ʹ�����ʩ���ٶ�
            rb.velocity = new Vector2(direction.x * speed, rb.velocity.y);
        }
    }
}
