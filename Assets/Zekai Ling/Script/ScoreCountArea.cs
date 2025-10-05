using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCountArea : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //�����ײ��������ı�ǩ��"Object"
        if (collision.CompareTag("Object"))
        {
            //��ȡ��ײ�����ϵ�ObjectDataCount���
            ObjectDataCount objectData = collision.GetComponent<ObjectDataCount>();
            if (objectData != null)
            {
                //������ķ����ӵ���Ϸ�������ĵ�ǰ������
                GameManager.instance.AddScore(objectData.objectValue, collision.transform.position);
                Debug.Log("��ǰ����: " + GameManager.instance.currentGameScore);
               
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //�����ײ��������ı�ǩ��"Object"
        if (collision.CompareTag("Object"))
        {
            //��ȡ��ײ�����ϵ�ObjectDataCount���
            ObjectDataCount objectData = collision.GetComponent<ObjectDataCount>();
            if (objectData != null)
            {
                //������ķ����ӵ���Ϸ�������ĵ�ǰ������
                GameManager.instance.AddScore(-objectData.objectValue, collision.transform.position);
                Debug.Log("��ǰ����: " + GameManager.instance.currentGameScore);

            }
        }
        
    }






}
