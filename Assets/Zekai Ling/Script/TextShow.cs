using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextShow : MonoBehaviour
{
  //该物体展示3秒后销毁
    private void Start()
    {
        Destroy(gameObject, 3f);
    }
}
