using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class MouseControlClaw : MonoBehaviour
{
    /* public float forceStrength = 10f;  // ������ǿ��
     public float maxDistance = 5f;     // Ӱ�췶Χ

     public Rigidbody2D targetObject;

     void Start()
     {

     }

     void Update()
     {
         if (Input.GetMouseButton(0)) // ��������ס
         {
             // �������Ļ����תΪ��������
             Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

             // ���㷽��
             Vector2 dir = mouseWorldPos - targetObject.position;
             float distance = dir.magnitude;

             // ���Ʒ�Χ
             if (distance < maxDistance)
             {
                 dir.Normalize();
                 // ��������С�����仯
                 Vector2 force = dir * forceStrength * distance;
                 targetObject.AddForce(force);
             }
         }

     }*/
    public float distance = 0f;         // ���ɳ�ʼ����
    public float frequency = 5f;        // ����ϵ����Խ��ԽӲ��
    public float dampingRatio = 0.5f;   // ���ᣨ0~1��Խ��ص�Խ�٣�

    public GameObject targetClaw;
    Claw clawScript;


    private Rigidbody2D rb;
    private SpringJoint2D spring;

    public bool isClawing = false;


    public GameObject clawVisual;//צ�ӵĿ��ӻ�����

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

    //���ƴӵ�ǰ���嵽Ŀ��צ�ӵ��߶Σ�����Ϸ�����п��ӻ���  
    public Material lineMaterial;  // �߲���
    public float lineWidth = 0.1f;

    private LineRenderer lr;

    void ChangeLineData()
    {
        lr = GetComponent<LineRenderer>();

        // ���ò���
        if (lineMaterial != null)
            lr.material = lineMaterial;

        // �����߿�
        lr.startWidth = lineWidth;
        lr.endWidth = lineWidth;

        // �߶���������
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

        // ����������
        if (Input.GetMouseButtonDown(0))
        {
            // ��� SpringJoint2D
            spring = targetClaw.AddComponent<SpringJoint2D>();
            spring.autoConfigureDistance = false;
            spring.distance = distance;
            spring.frequency = frequency;
            spring.dampingRatio = dampingRatio;

            // ���ӵ����λ��
            Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            spring.connectedAnchor = mouseWorldPos;

            EnableClaw();
        }

        // �϶�ʱ����ê��λ��
        if (Input.GetMouseButton(0) && spring != null)
        {
            Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            spring.connectedAnchor = mouseWorldPos;
        }

        // �ɿ���꣬���� SpringJoint
        if (Input.GetMouseButtonUp(0) && spring != null)
        {
            Destroy(spring);
            DisableClaw();
            Debug.Log("�ͷ�צ��" + clawScript.isEnabled);
        }
    }


    public void SetAngel()
    {
        //����צ�ӵĽǶ�,targetClaw�Ƕ�����ڸ�����λ��
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
