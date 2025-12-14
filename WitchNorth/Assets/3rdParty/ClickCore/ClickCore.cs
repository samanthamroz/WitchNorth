using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PlayerInput))]
public class ClickCore : MonoBehaviour
{
    public static ClickCore self;
    private MouseController mouseController;
    private IValidater validater;
    private IMouse mouse;
    private IInteractionTriggerer interactionTriggerer;
    private IActionConductor actionConductor;
    
    void Awake() { //This is called automatically by Unity at the very beginning of every run of the game
        if (self == null) {
            self = this;

            validater = new ClickCoreValidater();
            mouse = new Mouse();
            interactionTriggerer = new InteractionTriggerer();
            actionConductor = new ActionConductor();

            InputActionAsset inputActions = Resources.Load<InputActionAsset>("ClickCoreInputActions");
            mouseController = new MouseController(interactionTriggerer, mouse, inputActions);

            DoSceneValidation();
            SceneManager.sceneLoaded += SceneStart; //subscribe to this function for every load of the scene

            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }
    private void SceneStart(Scene scene, LoadSceneMode mode) { //Required Unity Function Signature
        DoSceneValidation();
    }
    private void DoSceneValidation() {
        if (!validater.AllInteractablesValid()) {
            Debug.LogError("ClickCore >> Some Interactables are not configured properly");
        }
    }

    //Access to mouse status
    public Vector2 GetMousePosition() {
        return mouse.ScreenPosition;
    }
    public bool IsMouseButtonDown(MouseButton button) {
        return mouse.IsButtonDown(button);
    }

    //Template functions
    public void DoDelayedStartAction(Action delayedAction, float timeToDelaySeconds) {
        StartCoroutine(actionConductor.DelayedAction(delayedAction, timeToDelaySeconds));
    }

    public void DoDelayedSequence(Action preDelayAction, Action delayedAction, float timeToDelaySeconds) {
        StartCoroutine(actionConductor.DelayedSequence(preDelayAction, delayedAction, timeToDelaySeconds));
    }
}

