using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileController : MonoBehaviour
{
    [SerializeField] private GameObject vfx_HitExplosion;
    [SerializeField] private Transform target;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed = 2f;
    [SerializeField] private float rotateSpeed = 1000f;

    public System.Action OnTargetHit;

    public void SetTarget(Transform target)
    {
        if (target != null)
        {
            this.target = target;
        }
        else
        {
            Destroy(this.gameObject);
        }        
    }

    private void FixedUpdate()
    {
        if(target != null)
        {
            HomingMovement();
        }        
    }

    private void HomingMovement()
    {
        Vector2 direction = (Vector2)target.position - (Vector2)transform.position;
        direction.Normalize();
        float rotateAmount = Vector3.Cross(direction, transform.up).z;
        rb.angularVelocity = -rotateAmount * rotateSpeed;
        rb.velocity = transform.up * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform == target)
        {
            TargetHit();
        }
    }

    protected void TargetHit()
    {
        OnTargetHit?.Invoke();
        Debug.Log(target.name + " gets hit by the projectile.");
        Instantiate(vfx_HitExplosion, this.transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnValidate()
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();
    }
}
