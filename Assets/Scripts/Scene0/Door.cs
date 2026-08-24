using System;
using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] private string prompt;
    public float openAngle = -90f;
    public float openSpeed = 2f;
    public bool isOpen = false;
    
    private Quaternion openRotation;
    private Quaternion closedRotation;
    private Coroutine currentRoutine;
    
    public string InteractionPrompt => prompt;

    private void Start()
    {
        closedRotation = transform.rotation;
        openRotation = Quaternion.Euler(transform.eulerAngles + new Vector3(0, openAngle, 0));
    }

    public bool Interact(Interactor interactor)
    {
        if (currentRoutine != null) StopCoroutine(currentRoutine);
        currentRoutine = StartCoroutine(ToggleDoor());
        return true;
    }

    private IEnumerator ToggleDoor()
    {
        Quaternion targetRotation;
        if (isOpen)
            targetRotation = closedRotation;
        else
            targetRotation = openRotation;
        
        isOpen = !isOpen;
        
        while (Quaternion.Angle(transform.rotation, targetRotation) > 0.1f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * openSpeed);
            yield return null;
        }
        
        transform.rotation = targetRotation;

        if (isOpen)
        {
            prompt = "Close Door";
        }
        else prompt = "Open Door";
    }
}
