using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Joystick))]
public class PCJoystickAdaptor : MonoBehaviour
{
    private Joystick joystick;

    private void Awake()
    {
        joystick = GetComponent<Joystick>();
    }

    private void Update()
    {
        var y = Input.GetAxis("Vertical");
        var x = Input.GetAxis("Horizontal");

        joystick.InputOverride(new Vector2(x, y));
    }
}
