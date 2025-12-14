using UnityEngine;
using System;

public class InteractionTriggerer : IInteractionTriggerer {
    private IInteractable currentInteractableBeingClicked;
    private IInteractable lastInteractableClicked;
    private Camera cam;

    public InteractionTriggerer() {
        cam = Camera.main;
    }
    
    private bool TryUpdateCurrentInteractable(Vector2 mouseScreenPosition) {
        Ray ray = cam.ScreenPointToRay(mouseScreenPosition);

        //If any collider was hit
        if (Physics.Raycast(ray, out RaycastHit hit)) {
            //If the object that collider is attached to has a component that is a type of Interactable
            if (hit.collider.gameObject.TryGetComponent(out IInteractable foundInteractable)) {
                currentInteractableBeingClicked = foundInteractable;
                return true;
            }
        }

        //No interactable found at that point
        return false;
    }
    

    private void HandleClick<TClick, TClickAway>(Vector2 screenPosition, Action<TClick> clickAction, Action<TClickAway> clickAwayAction) {
        // Handle click-away on last interactable
        if (lastInteractableClicked is TClickAway clickAwayObj) {
            clickAwayAction(clickAwayObj);
        }

        // Try to find new interactable
        if (!TryUpdateCurrentInteractable(screenPosition)) return;

        // Handle click on current interactable
        if (currentInteractableBeingClicked is TClick clickObj) {
            clickAction(clickObj);
        }
    }

    private void HandleRelease<TRelease>(Action<TRelease> releaseAction) {
        if (currentInteractableBeingClicked is TRelease releaseObj) {
            releaseAction(releaseObj);
        }

        lastInteractableClicked = currentInteractableBeingClicked;
        currentInteractableBeingClicked = null;
    }


    public void DoLeftClick(Vector2 screenPositionClicked) {
        HandleClick<ILeftClick, ILeftClickAway>(screenPositionClicked, obj => obj.DoLeftClick(), obj => obj.DoLeftClickAway());
    }

    public void DoRightClick(Vector2 screenPositionClicked) {
        HandleClick<IRightClick, IRightClickAway>(screenPositionClicked, obj => obj.DoRightClick(), obj => obj.DoRightClickAway());
    }

    public void DoMiddleClick(Vector2 screenPositionClicked) {
        HandleClick<IMiddleClick, IMiddleClickAway>(screenPositionClicked, obj => obj.DoMiddleClick(), obj => obj.DoMiddleClickAway());
    }


    public void DoLeftRelease() {
        HandleRelease<ILeftRelease>(obj => obj.DoLeftRelease());
    }
    
    public void DoRightRelease() {
        HandleRelease<IRightRelease>(obj => obj.DoRightRelease());
    }
    
    public void DoMiddleRelease() {
        HandleRelease<IMiddleRelease>(obj => obj.DoMiddleRelease());
    }
}