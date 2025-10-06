using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class Wind : MonoBehaviour
{
    public float raycastLength = 100f;
    // public bool isOn = false;
    public Vector2 raycastDirection;
    Vector2 origin;
    public LayerMask targetLayerMask;

    public Fan windControl;
    private bool isPlaying = false;
    private ParticleSystem particleSystem;

    AreaEffector2D areaEffector2D; // AreaEffector2D���

    public bool blockAble = false; //�Ƿ�����赲
    private void Start()
    {
        areaEffector2D= GetComponent<AreaEffector2D>();
        particleSystem = GetComponent<ParticleSystem>();
        //groundLayerMask = LayerMask.GetMask("Player");
        origin = transform.position;
        /*if (up == false)
        {
            raycastDirection = Vector2.right;
        }
        else
        {
            raycastDirection = Vector2.up;
        }*/

       // if(windControl.isStart)
        {
            if(areaEffector2D != null)
            areaEffector2D.enabled = true; // ����AreaEffector2D���
            SoundManager.Instance.PlaySound(SoundManager.Instance.windFieldAmbientSound);
            
            particleSystem.Play();
            isPlaying = true;
        }
       /* else
        {
            if (areaEffector2D != null)
                areaEffector2D.enabled = false; // �ر�AreaEffector2D���
            particleSystem.Stop();
            isPlaying = false;
        }*/

    }
    WindCount myObject;
    void Update()
    {
        SwitchPower();

        /* if (windControl.isStart)
         {
             if (!isPlaying)
             {
                 isPlaying = true;
                 particleSystem.Play();
             }
             if (blockAble == false)
             {
                 return;
             }
             //����Raycast2D���ߣ�����Ƿ������屻���߼�⵽���������layer
             RaycastHit2D hit = Physics2D.Raycast(origin, raycastDirection, raycastLength, targetLayerMask);
             //����Raycast����
             Debug.DrawLine(origin, origin + raycastDirection * raycastLength, Color.red);

             //����Ƿ������屻Raycast��⵽
             if (hit.collider != null)
             {
                 //�����⵽��ΪGround���򷵻�
                 if (hit.collider.CompareTag("Ground"))
                 {
                     return;
                 }

                 //���Ŀ�������tagΪPlayer����Enemy������SceneObject�����ܼ���ִ�У���comparetag����
                 else if (hit.collider.CompareTag("Player") || hit.collider.CompareTag("SceneObject"))
                 {
                     if(myObject==null)
                     {
                         myObject = hit.collider.gameObject.GetComponent<WindCount>();
                     }                  
                     if (myObject.count < myObject.maxCount)
                     {
                         myObject.count += 2;
                     }
                 }

             }
             else
             {
                 //���û�м�⵽���壬��ִ���κβ���
                 if (myObject != null)
                 {
                     myObject = null;
                 }
             }
         }
         else
         {
             if (isPlaying)
             {
                 isPlaying = false;
                 particleSystem.Stop();
             }
         }*/
        //�������߽��м�⣬�������һ򳡾���Ʒ����Ŀ������󶨵�����Collider2D�����usedByEffector��������Ϊfalse
        //�ر�Ŀ�������ϵ�WindCount���
        RaycastHit2D hit = Physics2D.Raycast(origin, raycastDirection, raycastLength, targetLayerMask);
        Debug.DrawLine(origin, origin + raycastDirection * raycastLength, Color.red);
        if (hit.collider != null)
        {
           
            //���Ŀ�������tagΪPlayer����SceneObject�����ܼ���ִ�У���comparetag����
            if (hit.collider.CompareTag("Player") || hit.collider.CompareTag("SceneObject"))
            {
                if (hit.collider.CompareTag("Player") || hit.collider.CompareTag("SceneObject"))
                {
                    WindCount windCount = hit.collider.GetComponent<WindCount>();
                    if (windCount != null)
                    {
                        windCount.enabled = false; // �ر�WindCount���
                        foreach (Collider2D col in hit.collider.GetComponents<Collider2D>())
                        {
                            col.usedByEffector = false; // ����usedByEffectorΪfalse
                        }
                    }
                }
            }
        }
        else
        {
            //���û�м�⵽���壬��ִ���κβ���
            if (myObject != null)
            {
                myObject = null;
            }
        }
    }

    //��� if (windControl.isStart)������������areaEffect2D���,������ϵͳ��ʼ����
    //��� if (!windControl.isStart)����ر�������areaEffect2D���,������ϵͳֹͣ����
    private void SwitchPower()
    {
       // if (windControl.isStart)
        {
            if (!isPlaying)
            {
                isPlaying = true;
                if (areaEffector2D != null)
                    areaEffector2D.enabled = true; // ����AreaEffector2D���
                particleSystem.Play();
            }
        }
        /*else
        {
            if (isPlaying)
            {
                isPlaying = false;
                if (areaEffector2D != null)
                    areaEffector2D.enabled = false; // �ر�AreaEffector2D���
                particleSystem.Stop();
            }
        }*/
    }

    //����triggerʱ����Ŀ������󶨵�����Collider2D�����usedByEffector��������Ϊfalse
    //�ر�Ŀ�������ϵ�WindCount���
    private void OnTriggerEnter2D(Collider2D collision)
    {
       
    }
    //�뿪triggerʱ����Ŀ������󶨵�����Collider2D�����usedByEffector��������Ϊtrue
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("SceneObject"))
        {
            WindCount windCount = collision.GetComponent<WindCount>();
            if (windCount != null)
            {
                windCount.enabled = true; // ����WindCount���
                foreach (Collider2D col in collision.GetComponents<Collider2D>())
                {
                    col.usedByEffector = true; // ����usedByEffectorΪtrue
                }
            }
        }
    }

}
