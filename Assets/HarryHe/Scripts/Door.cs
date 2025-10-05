using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject leftDoor;
    public GameObject rightDoor;

    public float openDuration;//开门动作所需的时间
    public float closeDuration;//关门动作所需的时间
    public float openInterval;//开门后到关门的间隔时间
    public float closeInterval;//关门后到开门的间隔时间
    public float doorOpenDistance;
    
    private Vector3 leftDoorStartPos;
    private Vector3 rightDoorStartPos;
    // Start is called before the first frame update
    void Start()
    {
        leftDoorStartPos = leftDoor.transform.position;
        rightDoorStartPos = rightDoor.transform.position;
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
        Vector3 leftTarget = leftDoorStartPos + Vector3.left * doorOpenDistance;
        Vector3 rightTarget = rightDoorStartPos + Vector3.right * doorOpenDistance;
        
        float elapsedTime = 0;
        while (elapsedTime < openDuration)
        {
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / openDuration;
            
            leftDoor.transform.position = Vector3.Lerp(leftDoorStartPos, leftTarget, progress);
            rightDoor.transform.position = Vector3.Lerp(rightDoorStartPos, rightTarget, progress);
            
            yield return null;
        }
    }

    private IEnumerator CloseDoor()
    {
        Vector3 leftCurrent = leftDoor.transform.position;
        Vector3 rightCurrent = rightDoor.transform.position;
        
        float elapsedTime = 0;
        while (elapsedTime < closeDuration)
        {
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / closeDuration;
            
            leftDoor.transform.position = Vector3.Lerp(leftCurrent, leftDoorStartPos, progress);
            rightDoor.transform.position = Vector3.Lerp(rightCurrent, rightDoorStartPos, progress);
            
            yield return null;
        }
    }
}
