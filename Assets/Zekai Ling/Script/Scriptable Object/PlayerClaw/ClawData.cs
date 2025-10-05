using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Claw Data", menuName = "Scriptable Object/Claw Data")]
public class ClawData : ScriptableObject
{
    public float originalLinearDrag = 0.15f;//初始线性阻力
    public float releaedLinearDrag = 4f;//释放后的线性阻力

    public float frequency = 5f;        // 弹性系数（越大越硬）
    public float dampingRatio = 0.5f;   // 阻尼（0~1，越大回弹越少）

    public float maxDistance = 10f; // 最大距离限制

    //绘制从当前物体到目标爪子的线段（在游戏界面中可视化）  
    public Material lineMaterial;  // 线材质
    public float lineWidth = 0.1f;


}
