using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interactor : MonoBehaviour
{
    [SerializeField] private Transform _interactionPoint;
    [SerializeField] private float _interactionRadius = 0.5f;
    [SerializeField] private LayerMask _interactableMask;

    private readonly Collider[] _colliders = new Collider[3];
    [SerializeField] private int _numFound;
    
    [SerializeField] private InteractionPromptUI _interactionPromptUI;
    private IInteractable _interactable;

    private void Update()
    {
        _numFound = Physics.OverlapSphereNonAlloc(_interactionPoint.position, _interactionRadius, _colliders,
            _interactableMask);

        if (_numFound > 0)
        {
            // get any monobehaviour implementing IInteractable
            _interactable = _colliders[0].GetComponentInParent<IInteractable>();

            if (_interactable != null)
            {
                if (!_interactionPromptUI.isDisplayed) _interactionPromptUI.SetUp(_interactable.InteractionPrompt);
                if (Keyboard.current.fKey.wasPressedThisFrame)
                {
                    // player is the interactor interacting with the interactable collider we found
                    _interactable.Interact(this);
                }
            }
        }
        else
        {
            _interactable = null;
            if (_interactionPromptUI.isDisplayed) _interactionPromptUI.Close();
        }
    }
}
