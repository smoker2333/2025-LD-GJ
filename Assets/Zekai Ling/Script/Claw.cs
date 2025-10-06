using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Claw : MonoBehaviour
{
   // public bool isEnabled = true;// צ���Ƿ����

    bool isClawed = false;// �Ƿ�ץȡ����Ʒ

    public GameObject clawedObject;// ץȡ������Ʒ

    private FixedJoint2D fixedJoint;

    public Transform clawedTransform;//��������������������λ��


    public float originalLinearDrag=0.15f;
    public float releaedLinearDrag = 4f;//�Ƿ���Ҫ�����Բ�ֵ���ı���������

  //  public ScriptableObject clawData;

    //   public LayerMask targetObjectLayerMask; // �����Layer

    public Collider2D collider2D;

    void Start()
    {
      /*  if (clawData is ClawData data)
        {
            originalLinearDrag = data.originalLinearDrag;
            releaedLinearDrag = data.releaedLinearDrag;
        }*/

        collider2D = GetComponent<Collider2D>();
        fixedJoint =gameObject.GetComponent<FixedJoint2D>();
        fixedJoint.enabled = false; // ��ʼʱ���� FixedJoint2D
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if (isEnabled)
        {
            if (collision.gameObject.CompareTag("Object") && !isClawed)
            {
                SoundManager.Instance.PlaySound(SoundManager.Instance.grapplingHookHooksPackageSound);
                
                SetClawedObject(collision.gameObject);
                // ץȡ����Ʒ����߼�
                Debug.Log("ץȡ����Ʒ: " + collision.gameObject.name);
            }
        }
      
    }

    public void SetClawedObject(GameObject obj)
    {
        clawedObject = obj;
        clawedObject.transform.SetParent(this.transform);
        clawedObject.transform.position = clawedTransform.position;

        clawedObject.transform.rotation = clawedTransform.rotation;

        fixedJoint.enabled = true; // ���� FixedJoint2D
        fixedJoint.connectedBody = clawedObject.GetComponent<Rigidbody2D>();

        gameObject.GetComponent<Rigidbody2D>().drag = originalLinearDrag;

        isClawed = true;
    }


    public void DisableClaw()
    {
        gameObject.GetComponent<Rigidbody2D>().drag = releaedLinearDrag;

        if (isClawed)
        {
            fixedJoint.enabled = false; // ���� FixedJoint2D
            clawedObject.transform.SetParent(null);
            clawedObject = null;         

            isClawed = false;
        }
    }

    void Update()
    {
        if (isClawed && clawedObject != null)
        {           
            clawedObject.transform.rotation = clawedTransform.rotation;

        }
        else
        {
            isClawed = false;
            if(clawedObject != null)
               DisableClaw();

        }
        
    }
  


}
