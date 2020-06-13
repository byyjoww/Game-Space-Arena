using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Joystick joystick;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float multiplierX;
    [SerializeField] private float multiplierY;

    private void Start()
    {
        joystick = FindObjectOfType<Joystick>();
        rb = GetComponent<Rigidbody2D>();
        StateMachine.OnGameEnd += Explode;
    }

    private void Update()
    {
        rb.AddForce(new Vector2(joystick.Horizontal * multiplierX, joystick.Vertical * multiplierY) * Time.deltaTime);
    }

    private void Explode()
    {
        Instantiate(Resources.Load("vfx_DeathExplosion"), transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        StateMachine.OnGameEnd -= Explode;
    }
}