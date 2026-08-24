using System;
using TMPro;
using UnityEngine;

public class InteractionPromptUI : MonoBehaviour
{
    private Camera _mainCam;
    [SerializeField] private TextMeshProUGUI prompt;
    [SerializeField] private GameObject uiPannel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        uiPannel.SetActive(false);
        _mainCam = Camera.main;
    }

    private void LateUpdate()
    {
        var rotation = _mainCam.transform.rotation;
        transform.LookAt(transform.position + rotation * Vector3.forward,
            rotation * Vector3.up);
    }

    public bool isDisplayed = false;

    public void SetUp(string promptText)
    {
        prompt.text = promptText;
        uiPannel.SetActive(true);
        isDisplayed = true;
    }

    public void Close()
    {
        isDisplayed = false;
        uiPannel.SetActive(false);
    }
}
