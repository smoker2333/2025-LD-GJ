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

    AreaEffector2D areaEffector2D; // AreaEffector2D组件

    public bool blockAble = false; //是否可以阻挡
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
            areaEffector2D.enabled = true; // 开启AreaEffector2D组件
            particleSystem.Play();
            isPlaying = true;
        }
       /* else
        {
            areaEffector2D.enabled = false; // 关闭AreaEffector2D组件
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
             //发射Raycast2D射线，检测是否有物体被射线检测到，检测所有layer
             RaycastHit2D hit = Physics2D.Raycast(origin, raycastDirection, raycastLength, targetLayerMask);
             //绘制Raycast线条
             Debug.DrawLine(origin, origin + raycastDirection * raycastLength, Color.red);

             //检测是否有物体被Raycast检测到
             if (hit.collider != null)
             {
                 //如果检测到的为Ground，则返回
                 if (hit.collider.CompareTag("Ground"))
                 {
                     return;
                 }

                 //如果目标物体的tag为Player或者Enemy，或者SceneObject，才能继续执行，用comparetag方法
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
                 //如果没有检测到物体，则不执行任何操作
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
