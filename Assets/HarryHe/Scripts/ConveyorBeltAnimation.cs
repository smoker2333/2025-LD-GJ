using UnityEngine;

public class ConveyorBeltAnimation : MonoBehaviour
{
    [Header("动画设置")]
    public float scrollSpeed = 1f; // 滚动速度
    public Vector2 scrollDirection = Vector2.right; // 滚动方向
    
    private Material beltMaterial;
    private Vector2 currentOffset;
    
    void Start()
    {
        // 获取履带的Renderer组件
        Renderer beltRenderer = GetComponent<Renderer>();
        if (beltRenderer != null)
        {
            // 创建材质实例（避免修改原始材质）
            beltMaterial = beltRenderer.material;
        }
        else
        {
            Debug.LogError("ConveyorBeltAnimation: 找不到Renderer组件！");
        }
    }
    
    void Update()
    {
        if (beltMaterial != null)
        {
            // 计算UV偏移
            currentOffset += scrollDirection * scrollSpeed * Time.deltaTime;
            
            // 应用UV偏移到材质
            beltMaterial.mainTextureOffset = currentOffset;
        }
    }
    
    // 设置滚动速度
    public void SetScrollSpeed(float speed)
    {
        scrollSpeed = speed;
    }
    
    // 设置滚动方向
    public void SetScrollDirection(Vector2 direction)
    {
        scrollDirection = direction.normalized;
    }
    
    // 停止动画
    public void StopAnimation()
    {
        scrollSpeed = 0f;
    }
    
    // 开始动画
    public void StartAnimation(float speed)
    {
        scrollSpeed = speed;
    }
}
