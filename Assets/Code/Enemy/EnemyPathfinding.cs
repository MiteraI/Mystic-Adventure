using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathfinding : MonoBehaviour
{
    /*[SerializeField] public float moveSpeed = 2f;

    private Rigidbody2D rb;
    private Vector2 moveDir;
    private Knockback knockback;
    private Transform targetTransform;

    private void Awake()
    {
        knockback = GetComponent<Knockback>();
        rb = GetComponent<Rigidbody2D>();
        targetTransform = null;
    }

    private void FixedUpdate()
    {
        if (knockback.GettingKnockedBack) { return; }

        rb.MovePosition(rb.position + moveDir * (moveSpeed * Time.fixedDeltaTime));
        if (targetTransform != null)
        {
            MoveTowardsTarget();
        }
    }

    public void MoveTo(Vector2 targetPosition)
    {
        moveDir = targetPosition;
        targetTransform = null;
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.fixedDeltaTime);
    }
    public void MoveTowardsTarget()
    {
        if (targetTransform != null)
        {
            Vector2 targetPosition = new Vector2(targetTransform.position.x, targetTransform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.fixedDeltaTime);
        }
    }
    public void SetTarget(Transform target)
    {
        targetTransform = target;
    }*/
    [SerializeField] public float moveSpeed = 2f;

    private Rigidbody2D rb;
    private Vector2 moveDir;
    private Knockback knockback;
    private SpriteRenderer spriteRenderer;
    private Transform targetTransform;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        knockback = GetComponent<Knockback>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (knockback.GettingKnockedBack) { return; }

        rb.MovePosition(rb.position + moveDir * (moveSpeed * Time.fixedDeltaTime));

        if (moveDir.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }

    public void MoveTo(Vector2 targetPosition)
    {
        moveDir = targetPosition;
    }

    public void MoveTowardsTarget()
    {
        if (targetTransform != null)
        {
            Vector2 targetPosition = new Vector2(targetTransform.position.x, targetTransform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.fixedDeltaTime);
        }
    }
    public void SetTarget(Transform target)
    {
        targetTransform = target;
    }
}
