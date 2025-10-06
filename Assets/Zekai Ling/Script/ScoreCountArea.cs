using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCountArea : MonoBehaviour
{

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
            
            GameManager.instance.AddScore(objectData.objectValue, collision.transform.position);
                Debug.Log("��ǰ����: " + GameManager.instance.currentGameScore);
               
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
                GameManager.instance.AddScore(-objectData.objectValue, collision.transform.position);
                Debug.Log("��ǰ����: " + GameManager.instance.currentGameScore);

            }
        }
        
    }






}
