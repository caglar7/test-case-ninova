

using System;
using Template;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    public Action<float, float> onHorizontalVertical; 
    public Action<float, float> onMouseXY;
    public Action<Vector2> onMouseHover;
    public Action<Vector2> onMouseDrag;
    public Action<Vector2> onMouseDown;
    public Action<Vector2> onMouseUp;
    public Action<KeyCode> onKeyDown, onKeyUp;
    private KeyCode keyDown, keyUp;

    private void Update()
    {
        // disable enable all inputs 
        // ...

        onHorizontalVertical?.Invoke(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        onMouseXY?.Invoke(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        onMouseHover?.Invoke(Camera.main.ScreenToWorldPoint(Input.mousePosition));

        if(Input.GetMouseButton(0)) onMouseDrag?.Invoke(Camera.main.ScreenToWorldPoint(Input.mousePosition));

        if(Input.GetMouseButtonDown(0)) onMouseDown?.Invoke(Camera.main.ScreenToWorldPoint(Input.mousePosition));

        if(Input.GetMouseButtonUp(0)) onMouseUp?.Invoke(Camera.main.ScreenToWorldPoint(Input.mousePosition));

        // ----------------
        if (Input.GetKeyDown(KeyCode.Alpha1)) keyDown = KeyCode.Alpha1;
        if (Input.GetKeyDown(KeyCode.Alpha2)) keyDown = KeyCode.Alpha2;
        if (Input.GetKeyDown(KeyCode.Alpha3)) keyDown = KeyCode.Alpha3;
        if (Input.GetKeyDown(KeyCode.Alpha4)) keyDown = KeyCode.Alpha4;
        if (Input.GetKeyDown(KeyCode.Alpha5)) keyDown = KeyCode.Alpha5;
        if (Input.GetKeyDown(KeyCode.Alpha6)) keyDown = KeyCode.Alpha6;
        if (Input.GetKeyDown(KeyCode.Alpha7)) keyDown = KeyCode.Alpha7;
        if (Input.GetKeyDown(KeyCode.Alpha8)) keyDown = KeyCode.Alpha8;
        if (Input.GetKeyDown(KeyCode.Alpha9)) keyDown = KeyCode.Alpha9;
        if (Input.GetKeyDown(KeyCode.Alpha0)) keyDown = KeyCode.Alpha0;
        if (Input.GetKeyDown(KeyCode.Space)) keyDown = KeyCode.Space;
        if (Input.GetKeyDown(KeyCode.E)) keyDown = KeyCode.E;
        if (Input.GetKeyDown(KeyCode.Q)) keyDown = KeyCode.Q;
        if (Input.GetKeyDown(KeyCode.R)) keyDown = KeyCode.R;
        if (Input.GetKeyDown(KeyCode.Escape)) keyDown = KeyCode.Escape;
        if (Input.GetKeyDown(KeyCode.LeftShift)) keyDown = KeyCode.LeftShift;
        if (Input.GetKeyDown(KeyCode.LeftControl)) keyDown = KeyCode.LeftControl;

        if(keyDown != KeyCode.None)
            onKeyDown?.Invoke(keyDown);
            
        keyDown = KeyCode.None;

        // --------------
        if(Input.GetKeyUp(KeyCode.LeftShift)) keyUp = KeyCode.LeftShift;
        if(Input.GetKeyUp(KeyCode.LeftControl)) keyUp = KeyCode.LeftControl;

        if(keyUp != KeyCode.None)
            onKeyUp?.Invoke(keyUp);

        keyUp = KeyCode.None;
    }
}
