using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraTarget : MonoBehaviour
{
    float reach = 15;

    public void LookDown(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            transform.localPosition = new Vector2(0, -reach);
        }
        else if (ctx.canceled)
        {
            transform.localPosition = new Vector2(0, 0);
        }
    }
}
