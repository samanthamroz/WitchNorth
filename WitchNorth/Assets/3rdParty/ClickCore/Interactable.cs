using UnityEngine;

public class Interactable : MonoBehaviour, IInteractable
{
    public bool ValidInteractableConfiguration() {
        if (!TryGetComponent<Collider>(out _)) {
            Debug.LogError($"ClickCore >> {gameObject.name} does not include a Collider.");
            return false;
        }

        return true;
    }
}