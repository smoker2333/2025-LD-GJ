using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FanData", menuName = "Scriptable Object/FanData")]
public class FanData : ScriptableObject
{
    public Vector2 windDirection; // 风的方向
    public float windStrength; // 风的强度
   
}
   

