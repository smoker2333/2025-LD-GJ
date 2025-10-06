using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalDoor : MonoBehaviour
{
    public GameObject topDoor;
    public GameObject bottomDoor;

    public float openDuration;//开门动作所需的时间
    public float closeDuration;//关门动作所需的时间
    public float openInterval;//开门后到关门的间隔时间
    public float closeInterval;//关门后到开门的间隔时间
    public float doorOpenDistance;
    
    private Vector3 topDoorStartPos;
    private Vector3 bottomDoorStartPos;
    // Start is called before the first frame update
    void Start()
    {
        topDoorStartPos = topDoor.transform.position;
        bottomDoorStartPos = bottomDoor.transform.position;
        DoorMovement();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Coroutine DoorMovement()
    {
        return StartCoroutine(DoorAnimation());
    }

    private IEnumerator DoorAnimation()
    {
        while (true)
        {
            // 开门动作
            yield return StartCoroutine(OpenDoor());
            
            // 开门后间隔
            yield return new WaitForSeconds(openInterval);
            
            // 关门动作
            yield return StartCoroutine(CloseDoor());
            
            // 关门后间隔
            yield return new WaitForSeconds(closeInterval);
        }
    }

    private IEnumerator OpenDoor()
    {
        Vector3 topTarget = topDoorStartPos + transform.up * doorOpenDistance;
        Vector3 bottomTarget = bottomDoorStartPos - transform.up * doorOpenDistance;
        
        float elapsedTime = 0;
        while (elapsedTime < openDuration)
        {
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / openDuration;
            
            topDoor.transform.position = Vector3.Lerp(topDoorStartPos, topTarget, progress);
            bottomDoor.transform.position = Vector3.Lerp(bottomDoorStartPos, bottomTarget, progress);
            
            yield return null;
        }
    }

    private IEnumerator CloseDoor()
    {
        Vector3 topCurrent = topDoor.transform.position;
        Vector3 bottomCurrent = bottomDoor.transform.position;
        
        float elapsedTime = 0;
        while (elapsedTime < closeDuration)
        {
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / closeDuration;
            
            topDoor.transform.position = Vector3.Lerp(topCurrent, topDoorStartPos, progress);
            bottomDoor.transform.position = Vector3.Lerp(bottomCurrent, bottomDoorStartPos, progress);
            
            yield return null;
        }
    }
}
