using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrounded : MonoBehaviour
{
    private int whatIsGround;
    private static bool grounded;

    public static bool Grounded
    {
        get { return grounded; }
    }

    void Start()
    {
        whatIsGround = LayerMask.NameToLayer("Ground");
    }

    ///updates the state of grounded
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.layer == whatIsGround) grounded = true;
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.layer == whatIsGround)
        {
            grounded = false;
            StateControl.Running = false;
        }
    }
}
