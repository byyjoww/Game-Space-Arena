using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private ScoreCounter scoreCounter;
    public Vector2 movementForce;
    public bool playerHit = false;
    public float speed = 2f;
    [SerializeField] private Transform target;
    [SerializeField] private float rotateSpeed = 1000f;

    private void Start()
    {
        scoreCounter = FindObjectOfType<ScoreCounter>();
        if (scoreCounter == null) Debug.Log("Can't find score counter");

        target = FindObjectOfType<MapPositions>().Player;
        if (target == null) Debug.Log("Can't find player");

        rb = GetComponent<Rigidbody2D>();
        if (rb == null) Debug.Log("Can't find rigidbody");

        if (gameObject.name == "pfProjectile(Clone)") StaticMovement();


        StateMachine.OnGameEnd += Explode;

        Destroy(this.gameObject, 60f);
    }

    private void FixedUpdate()
    {
        if (gameObject.name == "pfMissile(Clone)") HomingMovement();
    }

    private void StaticMovement ()
    {
        rb.AddForce(movementForce);
    }

    private void HomingMovement()
    {
        Vector2 direction = (Vector2)target.position - rb.position;
        direction.Normalize();
        float rotateAmount = Vector3.Cross(direction, transform.up).z;
        rb.angularVelocity = -rotateAmount * rotateSpeed;
        rb.velocity = transform.up * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == target.gameObject)
        {
            Debug.Log("Player Was Hit!");
            FindObjectOfType<StateMachine>().DeathCheck();
            Instantiate(Resources.Load("vfx_DeathExplosion"),transform.position,Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.tag == "Power")
        {
            Debug.Log("A power hit an enemy.");
            scoreCounter.AddEnemyKilledCount();
            Explode();
        }
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