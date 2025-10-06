using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotHead : MonoBehaviour
{
    
    public Animator headAnimator;

    public Animator ClawAnimator;

    public void SetCatching()
    {
        headAnimator.SetBool("isCatching", true);
        ClawAnimator.SetBool("isCaught", true);
       
    }

    public void CancleCatching()
    {
        headAnimator.SetBool("isCatching", false);
        ClawAnimator.SetBool("isCaught", false);
    }


    public void SetSearching()
    {
        headAnimator.SetBool("isSearching", true);
    }

    public void CancleSearching()
    {
        headAnimator.SetBool("isSearching", false);
    }

}
