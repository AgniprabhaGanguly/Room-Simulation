using UnityEngine;

public class Lights : MonoBehaviour, IInteractable
{
    [SerializeField] private string prompt = "Toggle Light";
    public string InteractionPrompt => prompt;
    
    [SerializeField] private GameObject lightBulb;
    [SerializeField] private MeshRenderer lightBulbRenderer;
    
    private bool isLightOn = true;
    private Color glowColor;
    
    void Start()
    {
        // Calling .material creates an instance, perfectly solving your shared material issue.
        glowColor = lightBulbRenderer.material.GetColor("_EmissionColor");
    }

    public bool Interact(Interactor interactor)
    {
        // Instantly toggle the light without coroutine overhead
        ToggleLight();
        return true;
    }

    private void ToggleLight()
    {
        isLightOn = !isLightOn;
        
        // Toggle the GameObject holding your Point Light
        lightBulb.SetActive(isLightOn);
        
        if (isLightOn)
        {
            lightBulbRenderer.material.SetColor("_EmissionColor", glowColor);
        }
        else
        {
            lightBulbRenderer.material.SetColor("_EmissionColor", Color.black);
        }
    }
}