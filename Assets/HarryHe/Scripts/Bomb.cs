using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float explosionDelay;//爆炸倒计时
    public float explosionMaxRadius;//最大爆炸半径
    public float explosionMinRadius;//最小爆炸半径
    public float force;//爆炸的力的大小
    public float forceMultiplier;//爆炸倍率(随距离炸弹的范围线性变化？)
    
    private float timer;//当前倒计时
    
    // Start is called before the first frame update
    void Start()
    {
        timer = explosionDelay;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        
        if (timer <= 0)
        {
            Explode();
            enabled = false;
        }
    }
    
    private void Explode()
    {
        // 检测爆炸范围内的所有物体
        Collider2D[] objectsInRange = Physics2D.OverlapCircleAll(transform.position, explosionMaxRadius);
        
        foreach (Collider2D obj in objectsInRange)
        {
            Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                // 计算距离
                float distance = Vector2.Distance(transform.position, obj.transform.position);
                
                // 计算力的大小
                float explosionForce = CalculateExplosionForce(distance);
                
                // 施加力
                Vector2 direction = (obj.transform.position - transform.position).normalized;
                rb.AddForce(direction * explosionForce, ForceMode2D.Impulse);
            }
        }
        
        Debug.Log("炸弹爆炸");
        Destroy(gameObject);
    }
    
    private float CalculateExplosionForce(float distance)
    {
        // 在最小半径内，施加最大的力？
        if (distance <= explosionMinRadius)
        {
            return force * forceMultiplier;
        }
        else
        {
            // 在最小半径和最大半径之间线性插值
            float ratio = (distance - explosionMinRadius) / (explosionMaxRadius - explosionMinRadius);
            return Mathf.Lerp(force * forceMultiplier, force, ratio);
        }
    }
    
    //绘制爆炸的范围
    void OnDrawGizmosSelected()
    {
        //最大爆炸半径
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionMaxRadius);
        
        //最小爆炸半径
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, explosionMinRadius);
    }
}
