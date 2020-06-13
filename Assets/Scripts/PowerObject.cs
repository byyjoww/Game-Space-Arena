using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerObject : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private ScriptablePower power;

    private void Start()
    {
        target = FindObjectOfType<MapPositions>().Player;
        StateMachine.OnGameEnd += Explode;
    }

    public void SetPower(ScriptablePower power)
    {
        this.power = power;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform == target)
        {
            Debug.Log("Player picked up " + gameObject);
            target.GetComponent<PlayerPowers>().AddPower(power);
            Destroy(gameObject);
        }

        Destroy(gameObject, 20f);
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
