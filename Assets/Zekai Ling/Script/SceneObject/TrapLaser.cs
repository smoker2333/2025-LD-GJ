using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapLaser : MonoBehaviour
{
    public float reflectMultiplier = 1.2f; // �����ٶȱ��ʣ�>1 ��ʾ��ǿ��
    public ScriptableObject reflectLaserData;

    public float soundLimitation = 5f;//如果弹射速度小于这个值就不播放音效

    public float maxSpeed = 20f; 
    private void Start()
    {
        if (reflectLaserData != null)
        {
            ReflectLaserData data = reflectLaserData as ReflectLaserData;
            if (data != null)
            {
                reflectMultiplier = data.multiplier;
                maxSpeed = data.maxSpeed;
            }
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
       
        
        if(collision.collider.CompareTag("Object") == false)
            return;
        Rigidbody2D rb = collision.rigidbody;
        if (rb != null && collision.contacts.Length > 0)
        {
            // ��ȡ��ײ��ķ��߷���
            Vector2 normal = collision.contacts[0].normal;

            // ��ǰ�ٶȷ���
            Vector2 incoming = rb.velocity;

            // �������� = �����������ݷ��߷���
            Vector2 reflect = Vector2.Reflect(incoming, normal);

            //反射的速度不能超过maxSpeed，如果超过则按比例缩小到maxSpeed
            if(reflect.magnitude * reflectMultiplier > maxSpeed)
            {
                reflect = reflect.normalized * (maxSpeed / reflectMultiplier);
            }

            // �����µ��ٶȣ��ɼӱ���
            rb.velocity = reflect * reflectMultiplier;
            if(rb.velocity.magnitude < soundLimitation)
                return;

            // 播放激光触发音效和陷阱绳子回收音效
            SoundManager.Instance.PlaySound(SoundManager.Instance.laserTriggerSound);
           // SoundManager.Instance.PlaySound(SoundManager.Instance.trapRopeRetrievalSound);
        }
    }


   /* public float force = 50f; // ���ɵ�����
    public ScriptableObject reflectLaserData;

    private void Start()
    {
        if (reflectLaserData != null)
        {
            ReflectLaserData data = reflectLaserData as ReflectLaserData;
            if (data != null)
            {
                force = data.multiplier;
            }
        }
    }

    //������������ʱ����������ͷ��߷�����㷴�������������ݷ���ĽǶȸ�������һ���̶�����force
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //������������ʱ����������ͷ��߷�����㷴�������������ݷ���ĽǶȸ�������һ���̶�����force
        Debug.Log("Laser collided with: " + collision.gameObject.name);
        if (collision.gameObject.CompareTag("Object"))
        {
            Debug.Log("Laser hit object");
            Rigidbody2D rb = collision.rigidbody;
            if (rb != null && collision.contacts.Length > 0)
            {
                // ��ȡ��ײ��ķ��߷���
                Vector2 normal = collision.contacts[0].normal;

                // ��ǰ�ٶȷ���
                Vector2 incoming = rb.velocity;

                // �������� = �����������ݷ��߷���
                Vector2 reflect = Vector2.Reflect(incoming, normal);

                rb.AddForce(reflect.normalized * force, ForceMode2D.Impulse);
                Debug.Log("Laser hit object, applied force: " + reflect.normalized * force);

            }
        }



    }*/
}
