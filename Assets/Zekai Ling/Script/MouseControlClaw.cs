using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class MouseControlClaw : MonoBehaviour
{
    /* public float forceStrength = 10f;  // 吸引力强度
     public float maxDistance = 5f;     // 影响范围

     public Rigidbody2D targetObject;

     void Start()
     {

     }

     void Update()
     {
         if (Input.GetMouseButton(0)) // 鼠标左键按住
         {
             // 将鼠标屏幕坐标转为世界坐标
             Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

             // 计算方向
             Vector2 dir = mouseWorldPos - targetObject.position;
             float distance = dir.magnitude;

             // 限制范围
             if (distance < maxDistance)
             {
                 dir.Normalize();
                 // 吸引力大小随距离变化
                 Vector2 force = dir * forceStrength * distance;
                 targetObject.AddForce(force);
             }
         }

     }*/
    public float distance = 0f;         // 弹簧初始长度
    public float frequency = 5f;        // 弹性系数（越大越硬）
    public float dampingRatio = 0.5f;   // 阻尼（0~1，越大回弹越少）

    public GameObject targetClaw;
    Claw clawScript;


    private Rigidbody2D rb;
    private SpringJoint2D spring;

    public bool isClawing = false;


    public GameObject clawVisual;//爪子的可视化对象

    void Start()
    {
        rb = targetClaw.GetComponent<Rigidbody2D>();
        clawScript = targetClaw.GetComponent<Claw>();
        ChangeLineData();
    }


    void EnableClaw()
    {
        isClawing = true;
        clawScript.isEnabled = true;
        
    }

    void DisableClaw()
    {
        isClawing = false;
        clawScript.DisableClaw();
        clawScript.isEnabled = false;
    }

    //绘制从当前物体到目标爪子的线段（在游戏界面中可视化）  
    public Material lineMaterial;  // 线材质
    public float lineWidth = 0.1f;

    private LineRenderer lr;

    void ChangeLineData()
    {
        lr = GetComponent<LineRenderer>();

        // 设置材质
        if (lineMaterial != null)
            lr.material = lineMaterial;

        // 设置线宽
        lr.startWidth = lineWidth;
        lr.endWidth = lineWidth;

        // 线段有两个点
        lr.positionCount = 2;
    }

    void UpdateLinePosition()
    {
        if ( targetClaw != null)
        {
            lr.SetPosition(0,transform.position);
            lr.SetPosition(1, targetClaw.transform.position);
        }
    }
    void MouseControl()
    {

        // 按下鼠标左键
        if (Input.GetMouseButtonDown(0))
        {
            // 添加 SpringJoint2D
            spring = targetClaw.AddComponent<SpringJoint2D>();
            spring.autoConfigureDistance = false;
            spring.distance = distance;
            spring.frequency = frequency;
            spring.dampingRatio = dampingRatio;

            // 连接到鼠标位置
            Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            spring.connectedAnchor = mouseWorldPos;

            EnableClaw();
        }

        // 拖动时更新锚点位置
        if (Input.GetMouseButton(0) && spring != null)
        {
            Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            spring.connectedAnchor = mouseWorldPos;
        }

        // 松开鼠标，销毁 SpringJoint
        if (Input.GetMouseButtonUp(0) && spring != null)
        {
            Destroy(spring);
            DisableClaw();
            Debug.Log("释放爪子" + clawScript.isEnabled);
        }
    }


    public void SetAngel()
    {
        //设置爪子的角度,targetClaw角度相对于该物体位置
        Vector2 direction = targetClaw.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        clawVisual.transform.rotation = Quaternion.AngleAxis(angle+90, Vector3.forward);
    }

    void Update()
    {
        UpdateLinePosition();
        MouseControl();
        SetAngel();
    }

   
}
