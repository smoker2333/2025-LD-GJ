using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Claw : MonoBehaviour
{
   // public bool isEnabled = true;// 爪子是否可用

    bool isClawed = false;// 是否抓取到物品

    public GameObject clawedObject;// 抓取到的物品

    private FixedJoint2D fixedJoint;

    public Transform clawedTransform;//勾到物体后物体会锁定的位置


    public float originalLinearDrag=0.15f;
    public float releaedLinearDrag = 4f;//是否需要靠线性插值来改变线性阻力

  //  public ScriptableObject clawData;

    //   public LayerMask targetObjectLayerMask; // 物体的Layer

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
        fixedJoint.enabled = false; // 初始时禁用 FixedJoint2D
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if (isEnabled)
        {
            if (collision.gameObject.CompareTag("Object") && !isClawed)
            {
                SetClawedObject(collision.gameObject);
                // 抓取到物品后的逻辑
                Debug.Log("抓取到物品: " + collision.gameObject.name);
            }
        }
      
    }

    public void SetClawedObject(GameObject obj)
    {
        clawedObject = obj;
        clawedObject.transform.SetParent(this.transform);
        clawedObject.transform.position = clawedTransform.position;

        clawedObject.transform.rotation = clawedTransform.rotation;

        fixedJoint.enabled = true; // 启用 FixedJoint2D
        fixedJoint.connectedBody = clawedObject.GetComponent<Rigidbody2D>();

        gameObject.GetComponent<Rigidbody2D>().drag = originalLinearDrag;

        isClawed = true;
    }


    public void DisableClaw()
    {
        gameObject.GetComponent<Rigidbody2D>().drag = releaedLinearDrag;

        if (isClawed)
        {
            fixedJoint.enabled = false; // 禁用 FixedJoint2D
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
        
    }
  


}
