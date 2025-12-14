using UnityEngine;

public struct InteractionData
{
    public Vector2 screenPosition;
    public MouseButton button;
    public InteractionType interactionType;
    public float timestamp;
    public IInteractable interactable;
    public GameObject target;

    public InteractionData(Vector2 position, MouseButton mouseButton, InteractionType type, IInteractable interactable)
    {
        screenPosition = position;
        button = mouseButton;
        interactionType = type;
        timestamp = Time.time;
        this.interactable = interactable;
        target = (interactable as MonoBehaviour).gameObject;
    }

    public override string ToString() {
        return $"{timestamp} | Interaction: {button}, {interactionType} | Screen Position: {screenPosition} (Object: {target.name})";
    }

    public float TimeSinceInteraction()
    {
        return Time.time - timestamp;
    }
}