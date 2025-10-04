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

    AreaEffector2D areaEffector2D; // AreaEffector2D组件

    public bool blockAble = false; //是否可以阻挡
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
            areaEffector2D.enabled = true; // 开启AreaEffector2D组件
            particleSystem.Play();
            isPlaying = true;
        }
       /* else
        {
            if (areaEffector2D != null)
                areaEffector2D.enabled = false; // 关闭AreaEffector2D组件
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
         else
         {
             if (isPlaying)
             {
                 isPlaying = false;
                 particleSystem.Stop();
             }
         }*/
        //发射射线进行检测，如果是玩家或场景物品，则将目标物体绑定的所有Collider2D组件的usedByEffector属性设置为false
        //关闭目标物体上的WindCount组件
        RaycastHit2D hit = Physics2D.Raycast(origin, raycastDirection, raycastLength, targetLayerMask);
        Debug.DrawLine(origin, origin + raycastDirection * raycastLength, Color.red);
        if (hit.collider != null)
        {
           
            //如果目标物体的tag为Player或者SceneObject，才能继续执行，用comparetag方法
            if (hit.collider.CompareTag("Player") || hit.collider.CompareTag("SceneObject"))
            {
                if (hit.collider.CompareTag("Player") || hit.collider.CompareTag("SceneObject"))
                {
                    WindCount windCount = hit.collider.GetComponent<WindCount>();
                    if (windCount != null)
                    {
                        windCount.enabled = false; // 关闭WindCount组件
                        foreach (Collider2D col in hit.collider.GetComponents<Collider2D>())
                        {
                            col.usedByEffector = false; // 设置usedByEffector为false
                        }
                    }
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

    //如果 if (windControl.isStart)，则开启自身的areaEffect2D组件,且粒子系统开始播放
    //如果 if (!windControl.isStart)，则关闭自身的areaEffect2D组件,且粒子系统停止播放
    private void SwitchPower()
    {
       // if (windControl.isStart)
        {
            if (!isPlaying)
            {
                isPlaying = true;
                if (areaEffector2D != null)
                    areaEffector2D.enabled = true; // 开启AreaEffector2D组件
                particleSystem.Play();
            }
        }
        /*else
        {
            if (isPlaying)
            {
                isPlaying = false;
                if (areaEffector2D != null)
                    areaEffector2D.enabled = false; // 关闭AreaEffector2D组件
                particleSystem.Stop();
            }
        }*/
    }

    //进入trigger时，将目标物体绑定的所有Collider2D组件的usedByEffector属性设置为false
    //关闭目标物体上的WindCount组件
    private void OnTriggerEnter2D(Collider2D collision)
    {
       
    }
    //离开trigger时，将目标物体绑定的所有Collider2D组件的usedByEffector属性设置为true
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("SceneObject"))
        {
            WindCount windCount = collision.GetComponent<WindCount>();
            if (windCount != null)
            {
                windCount.enabled = true; // 开启WindCount组件
                foreach (Collider2D col in collision.GetComponents<Collider2D>())
                {
                    col.usedByEffector = true; // 设置usedByEffector为true
                }
            }
        }
    }

}
