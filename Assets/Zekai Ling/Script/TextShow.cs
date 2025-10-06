using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextShow : MonoBehaviour
{
    public float showDuration = 3f; //展示时间

    public float targetScale = 1.2f; //目标缩放比例

    public float scaleSpeed = 2f; //缩放速度

    public float targetRisingDistance = 1f; //目标上升距离

    void Start()
    {
        StartCoroutine(TextShowUp());
    }

    //该物体展示3秒后销毁
    private void Update()
    {
        showDuration -= Time.deltaTime;
        if (showDuration <= 0f)
        {
            Destroy(gameObject);
        }
    }

    public IEnumerator TextShowUp()
    {
        //该物体以一定速度上升并放大
        Vector3 originalPosition = transform.position;
        Vector3 targetPosition = originalPosition + new Vector3(0, targetRisingDistance, 0);
        Vector3 originalScale = transform.localScale;
        Vector3 targetScaleVector = originalScale * targetScale;
        float elapsedTime = 0f;
        while (elapsedTime < showDuration)
        {
            float t = elapsedTime / showDuration;
            transform.position = Vector3.Lerp(originalPosition, targetPosition, t*1.5f);
            transform.localScale = Vector3.Lerp(originalScale, targetScaleVector, t);
            elapsedTime += Time.deltaTime * scaleSpeed;
            yield return null;
        }
        transform.position = targetPosition;
        transform.localScale = targetScaleVector;
       // Destroy(gameObject);
    }
    

}
