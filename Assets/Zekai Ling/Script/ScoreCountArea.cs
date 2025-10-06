using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCountArea : MonoBehaviour
{

    public List<GameObject> containedObject = new List<GameObject>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameManager.instance.isPaused) return;
        //�����ײ��������ı�ǩ��"Object"
        if (collision.CompareTag("Object"))
        {
            //��ȡ��ײ�����ϵ�ObjectDataCount���
            ObjectDataCount objectData = collision.GetComponent<ObjectDataCount>();
            if (objectData != null)
            {
                //������ķ����ӵ���Ϸ�������ĵ�ǰ������
                SoundManager.Instance.PlaySound(SoundManager.Instance.packagePlacedInBasketSound);
            
        
                Debug.Log("��ǰ����: " + GameManager.instance.currentGameScore);
               
            }

            //如果碰撞的物体不在列表中，则添加到列表
            if (!containedObject.Contains(collision.gameObject))
            {
                containedObject.Add(collision.gameObject);
                GameManager.instance.AddScore(objectData.objectValue, collision.transform.position);
            }

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (GameManager.instance.isPaused) return;
        Debug.Log("�����뿪�Ʒ���");
       
        //�����ײ��������ı�ǩ��"Object"
        if (collision.CompareTag("Object"))
        {
            //��ȡ��ײ�����ϵ�ObjectDataCount���
            ObjectDataCount objectData = collision.GetComponent<ObjectDataCount>();
            if (objectData != null)
            {
                //������ķ����ӵ���Ϸ�������ĵ�ǰ������
               
                Debug.Log("��ǰ����: " + GameManager.instance.currentGameScore);

            }
            //如果碰撞的物体在列表中，则从列表中移除
            if (containedObject.Contains(collision.gameObject))
            {
                GameManager.instance.AddScore(-objectData.objectValue, collision.transform.position);
                containedObject.Remove(collision.gameObject);
            }
        }
        
    }






}
