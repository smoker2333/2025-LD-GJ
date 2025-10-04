using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindCount : MonoBehaviour
{
    public int count = 0;
    public int maxCount = 10;
    //声明该物体的2D碰撞体
   Collider2D selfCollider;
    //获取该物体的2D碰撞体
  //  public bool isTestOn = false;
    private void Start()
    {
        selfCollider = GetComponent<Collider2D>();
    }
    void Update()
    {
        if (count > 0)
        {
            if (selfCollider.usedByEffector == true)
            {
                selfCollider.usedByEffector = false;
                //添加selfCollider.contactCaptureLayers中的Default层
                selfCollider.contactCaptureLayers |= LayerMask.GetMask("Default");//添加Default层


            }
        }
        else
        {
            if (selfCollider.usedByEffector == false)
            {
                selfCollider.usedByEffector = true;
                //移除selfCollider.contactCaptureLayers中的Default层
                selfCollider.contactCaptureLayers &= ~LayerMask.GetMask("Default");//移除Default层
            }

        }
    }
    private void FixedUpdate()
    {
        //当windCount大于0时，不断减一
        if (count > 0)
        {
            count--;
        }
    }
}
