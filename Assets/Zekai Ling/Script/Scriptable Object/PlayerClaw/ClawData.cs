using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Claw Data", menuName = "Scriptable Object/Claw Data")]
public class ClawData : ScriptableObject
{
    public float originalLinearDrag = 0.15f;//��ʼ��������
    public float releaedLinearDrag = 4f;//�ͷź����������

    public float frequency = 5f;        // ����ϵ����Խ��ԽӲ��
    public float dampingRatio = 0.5f;   // ���ᣨ0~1��Խ��ص�Խ�٣�

    public float maxDistance = 10f; // ����������

    //���ƴӵ�ǰ���嵽Ŀ��צ�ӵ��߶Σ�����Ϸ�����п��ӻ���  
    public Material lineMaterial;  // �߲���
    public float lineWidth = 0.1f;


}
