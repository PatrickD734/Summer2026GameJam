using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InteractionDetector : MonoBehaviour
{
    private IInteractable interactableInRange = null; //Clsest Interactable
    public GameObject interactionText;
    void Start()
    {
        interactionText.SetActive(false);
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        Debug.Log("OnInteract called, performed: " + context.performed);
        if (context.performed)
        {
            interactableInRange?.Interact();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IInteractable interactable) && interactable.CanInteract())
        {
            interactableInRange = interactable;
            interactionText.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out IInteractable interactable) && interactable == interactableInRange)
        {
            interactableInRange = null;
            interactionText.SetActive(false);
        }
    }
}
