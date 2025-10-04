using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockAbleWind : MonoBehaviour
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
        areaEffector2D = GetComponent<AreaEffector2D>();
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

       // if (windControl.isStart)
        {
            areaEffector2D.enabled = true; // ����AreaEffector2D���
            particleSystem.Play();
            isPlaying = true;
        }
       /* else
        {
            areaEffector2D.enabled = false; // �ر�AreaEffector2D���
            particleSystem.Stop();
            isPlaying = false;
        }*/

    }
    WindCount myObject;
    void Update()
    {     

        // if (windControl.isStart)
         {
             if (!isPlaying)
             {
                 isPlaying = true;
                 particleSystem.Play();
             }
             /*if (blockAble == false)
             {
                 return;
             }*/
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
        /* else
         {
             if (isPlaying)
             {
                 isPlaying = false;
                 particleSystem.Stop();
             }
         }*/

    }

   

}
