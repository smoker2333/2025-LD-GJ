using UnityEngine;

public class ConveyorBeltSpriteAnimation : MonoBehaviour
{
    [Header("Sprite动画设置")]
    public Sprite[] beltSprites; // 履带动画帧
    public float animationSpeed = 10f; // 动画播放速度
    
    private SpriteRenderer spriteRenderer;
    private int currentFrame = 0;
    private float timer = 0f;
    
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("ConveyorBeltSpriteAnimation: 找不到SpriteRenderer组件！");
        }
    }
    
    void Update()
    {
        if (beltSprites == null || beltSprites.Length == 0) return;
        
        // 动画计时器
        timer += Time.deltaTime * animationSpeed;
        
        if (timer >= 1f)
        {
            timer = 0f;
            currentFrame = (currentFrame + 1) % beltSprites.Length;
            spriteRenderer.sprite = beltSprites[currentFrame];
        }
    }
    
    // 设置动画速度
    public void SetAnimationSpeed(float speed)
    {
        animationSpeed = speed;
    }
    
    // 停止动画
    public void StopAnimation()
    {
        animationSpeed = 0f;
    }
}
