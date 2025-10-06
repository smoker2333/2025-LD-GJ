using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapBlock : MonoBehaviour
{
    //��Object������ײ�����TrapBlockʱ��Object������ٶȻ��Ϊ0
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Object"))
        {
            SoundManager.Instance.PlaySound(SoundManager.Instance.baffleItemTouchingPlateSound);
            
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = Vector2.zero; // ��������ٶ���Ϊ0
            }
        }
    }



  
}
