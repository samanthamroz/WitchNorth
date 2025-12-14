using UnityEngine;
using UnityEngine.InputSystem;

public class MouseController {
    private IMouse _mouse;
    private InputActionAsset _inputActions;
    private IInteractionTriggerer _triggerer;

    public MouseController(IInteractionTriggerer triggerer, IMouse mouse, InputActionAsset actionAsset)
    {
        _triggerer = triggerer;

        _mouse = mouse;
        _inputActions = actionAsset;
        
        var mouseMap = _inputActions.FindActionMap("Main");

        //Subscribe methods to trigger when an input action event occurs
        //Performed = any change in ctx
        //Started = when ctx becomes 1
        //Cancelled = when ctx becomes 0
        mouseMap.FindAction("MouseMove").performed += ctx => OnMouseMove(ctx);
        mouseMap.FindAction("LeftMouseButton").started += ctx => OnLeftButton(ctx);
        mouseMap.FindAction("LeftMouseButton").canceled += ctx => OnLeftButton(ctx);
        mouseMap.FindAction("RightMouseButton").started += ctx => OnRightButton(ctx);
        mouseMap.FindAction("RightMouseButton").canceled += ctx => OnRightButton(ctx);
        mouseMap.FindAction("MiddleMouseButton").started += ctx => OnMiddleButton(ctx);
        mouseMap.FindAction("MiddleMouseButton").canceled += ctx => OnMiddleButton(ctx);
        
        //Start listening for input
        mouseMap.Enable();
    }

    void OnMouseMove(InputAction.CallbackContext context) {
        Vector2 mouseScreenPosition = context.ReadValue<Vector2>();
        _mouse.SetScreenPosition(mouseScreenPosition);
    }

    void OnLeftButton(InputAction.CallbackContext context) {
        bool isButtonDown = context.ReadValue<float>() == 1;
        
        if (isButtonDown) { //click down
            _mouse.SetButtonDown(MouseButton.left);
            _triggerer.DoLeftClick(_mouse.ScreenPosition);
        } else { //release
            _mouse.SetButtonUp(MouseButton.left);
            _triggerer.DoLeftRelease();
        }
    }
    
    void OnRightButton(InputAction.CallbackContext context) {
        bool isButtonDown = context.ReadValue<float>() == 1;

        if (isButtonDown) { //click down
            _mouse.SetButtonDown(MouseButton.right);
            _triggerer.DoRightClick(_mouse.ScreenPosition);
        } else { //release
            _mouse.SetButtonUp(MouseButton.right);
            _triggerer.DoRightRelease();
        }
    }

    void OnMiddleButton(InputAction.CallbackContext context) {
        bool isButtonDown = context.ReadValue<float>() == 1;

        if (isButtonDown) { //click down
            _mouse.SetButtonDown(MouseButton.middle);
            _triggerer.DoMiddleClick(_mouse.ScreenPosition);
        } else { //release
            _mouse.SetButtonUp(MouseButton.middle);
            _triggerer.DoMiddleRelease();
        }
    }
}