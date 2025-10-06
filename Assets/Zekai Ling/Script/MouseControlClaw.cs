using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class MouseControlClaw : MonoBehaviour
{
    public ScriptableObject clawData;

    public RobotHead robotAnimation;

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
   float distance = 0f;         // ���ɳ�ʼ����
    public float frequency = 5f;        // ����ϵ����Խ��ԽӲ��
    public float dampingRatio = 0.5f;   // ���ᣨ0~1��Խ��ص�Խ�٣�

    public float maxDistance = 10f; // ����������

    public GameObject targetClaw;
    Claw clawScript;


    private Rigidbody2D rb;
    private SpringJoint2D spring;

    public bool isClawing = false;


    public GameObject clawVisual;//צ�ӵĿ��ӻ�����

    public LayerMask hitLayers;      // ���߼���
    void Start()
    {

        rb = targetClaw.GetComponent<Rigidbody2D>();
        clawScript = targetClaw.GetComponent<Claw>();
        LoadClawData();
        ChangeLineData();
    }

    void LoadClawData()
    {

        if (clawData != null)
        {
            ClawData data = clawData as ClawData;
            if (data != null)
            {
                frequency = data.frequency;
                dampingRatio = data.dampingRatio;
                maxDistance = data.maxDistance;

                //���ƴӵ�ǰ���嵽Ŀ��צ�ӵ��߶Σ�����Ϸ�����п��ӻ���  
                if (data.lineMaterial != null)
                    lineMaterial = data.lineMaterial;
                lineWidth = data.lineWidth;

                clawScript.originalLinearDrag = data.originalLinearDrag;
                clawScript.releaedLinearDrag = data.releaedLinearDrag;
            }
        }

    }


    void EnableClaw()
    {
        isClawing = true;
        clawScript.collider2D.enabled = true;

        robotAnimation.SetSearching();
        
    }

    void DisableClaw()
    {
        isClawing = false;
        clawScript.DisableClaw();
        clawScript.collider2D.enabled = false;

        robotAnimation.CancleSearching();
    }

    //���ƴӵ�ǰ���嵽Ŀ��צ�ӵ��߶Σ�����Ϸ�����п��ӻ���  
    public Material lineMaterial;  // �߲���
    public float lineWidth = 0.1f;

    public LineRenderer lr;

    void ChangeLineData()
    {       

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
            //�������Ļ��ƾ���
            /*if(isClawing)
            {
                Vector2 direction = targetClaw.transform.position - transform.position;
                float currentDistance = direction.magnitude;
                if (currentDistance > maxDistance)
                {
                    direction = direction.normalized * maxDistance;
                }
                lr.SetPosition(1, (Vector2)transform.position + direction);
                return;
            }*/
            lr.SetPosition(1, targetClaw.transform.position);
        }
       
    }

    void UpdateRay(Vector3 start, Vector3 end)
    {
        if (targetClaw == null)
            return;      
        Vector2 direction = (end - start).normalized;
        float distance = Vector2.Distance(start, end);

        // �������߼��
        RaycastHit2D hit = Physics2D.Raycast(start, direction, distance, hitLayers);

        if (hit.collider != null)
        {
            // ���߻���������
            Destroy(spring);
            DisableClaw();
            Debug.DrawLine(start, hit.point, Color.red); // Debug ���ӻ�
            Debug.Log($"Ray hit: {hit.collider.name}");
        }
        else
        {         
            Debug.DrawLine(start, end, Color.green);
        }
    }

    void ChangeClawAnimaton()
    {
        if (clawScript.isClawed)
        {
            robotAnimation.SetCatching();
        }
        else
        {
            robotAnimation.CancleCatching();
        }
    }


    void MouseControl()
    {

        // ����������
        if (Input.GetMouseButtonDown(0))
        {
            // ���� SpringJoint2D
            // 播放玩家拉扯绳子音效
            SoundManager.Instance.PlaySound(SoundManager.Instance.playerPullsRopeSound);
            
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
            // 播放玩家释放绳子音效
            SoundManager.Instance.PlaySound(SoundManager.Instance.playerReleasesRopeSound);
            
            Destroy(spring);
            DisableClaw();
           
        }
    }


    public void LimitClawMaxDistance()
    {
        //����targetClaw�뵱ǰ�����������
        if (targetClaw != null && isClawing)
        {
            float currentDistance = Vector2.Distance(transform.position, targetClaw.transform.position);
            if (currentDistance > maxDistance)
            {
              //������ê��λ������Ϊ������λ��
              Vector2 direction = targetClaw.transform.position - transform.position;
                    direction = direction.normalized * maxDistance;
                    if (spring != null)
                {
                        spring.connectedAnchor = (Vector2)transform.position + direction;
                    }
               
            }           
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
        LimitClawMaxDistance();
        UpdateRay(transform.position, targetClaw.transform.position);
        ChangeClawAnimaton();
    }

   
}
