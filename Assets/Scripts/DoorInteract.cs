using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteract : MonoBehaviour
{
    [SerializeField] private GameObject doorGameObject;
    private bossDoor bossdoor;
    private bool isTriggered = false;

    private void Awake()
    {
        bossdoor = doorGameObject.GetComponent<bossDoor>();
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.GetComponent<PlayerController>() != null && isTriggered == false)
        {
            bossdoor.OpenDoor();
            isTriggered = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.GetComponent<PlayerController>() != null)
        {
            bossdoor.CloseDoor();
        }
    }
}
