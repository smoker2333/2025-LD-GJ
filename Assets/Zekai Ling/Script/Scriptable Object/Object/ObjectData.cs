using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Object Data", menuName = "Scriptable Object/Object Data")]
public class ObjectData : ScriptableObject
{
    public string objectName;
    //public Sprite objectSprite;
    //public GameObject objectPrefab;
    
    public float value;
    public float weight;
}
   

