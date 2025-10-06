using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextShow : MonoBehaviour
{
    public float showDuration = 3f; //չʾʱ��

    public float targetScale = 1.2f; //Ŀ�����ű���

    public float scaleSpeed = 2f; //�����ٶ�

    public float targetRisingDistance = 1f; //Ŀ����������

    void Start()
    {
        StartCoroutine(TextShowUp());
    }

    //������չʾ3�������
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
        //��������һ���ٶ��������Ŵ�
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
