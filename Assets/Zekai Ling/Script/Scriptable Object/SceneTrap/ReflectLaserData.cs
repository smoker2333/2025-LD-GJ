using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ReflectLaserData", menuName = "Scriptable Object/ReflectLaserData")]
public class ReflectLaserData : ScriptableObject
{
   public float multiplier = 1f;//反弹的倍率

    public float maxSpeed = 20f;//反弹的最大速度

}
