using System.Collections;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float roamChangeDirFloat = 2f;
    [SerializeField] private float detectionRadius = 5f;
    [SerializeField] private float attackRange = 1.5f;
    private enum State
    {
        Roaming,
        Chasing,
        Attacking
    }

    private State state;
    private EnemyPathfinding enemyPathfinding;
    private Transform playerTransform;
    private Animator enemyAnimator;
    private bool isChasing = true;

    [SerializeField] private EnemyType enemyType;
    private void Awake()
    {
        enemyPathfinding = GetComponent<EnemyPathfinding>();
        state = State.Roaming;
        enemyAnimator = GetComponent<Animator>();
    }

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(RoamingRoutine());
    }

    private IEnumerator RoamingRoutine()
    {
        while (true)
        {
            if (Vector2.Distance(transform.position, playerTransform.position) < detectionRadius)
            {
                state = State.Chasing;
                enemyPathfinding.SetTarget(playerTransform);
                break; // Exit the loop when chasing begins
            }

            Vector2 roamPosition = GetRoamingPosition();
            enemyPathfinding.MoveTo(roamPosition);
            yield return new WaitForSeconds(roamChangeDirFloat);
        }

        StartCoroutine(ChasingRoutine());
    }

    private IEnumerator ChasingRoutine()
    {
        while (isChasing && state == State.Chasing)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

            if (enemyType == EnemyType.Skeleton && distanceToPlayer < attackRange)
            {
                state = State.Attacking;
                if (enemyAnimator != null)
                {
                    Debug.Log("Distance to Player: " + distanceToPlayer);
                    Debug.Log("Attack Range: " + attackRange);
                    Debug.Log("Triggering Skeleton_Attack animation");
                    enemyAnimator.SetBool("Skeleton_Attack", true);
                }
            }
            else
            {
                if (enemyType == EnemyType.Skeleton && enemyAnimator != null)
                {
                    enemyAnimator.SetBool("Skeleton_Attack", false);
                }
                Vector2 directionToPlayer = (playerTransform.position - transform.position).normalized;
                FlipSprite(directionToPlayer.x);
                if (Vector2.Distance(transform.position, playerTransform.position) < detectionRadius)
                {
                    transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, enemyPathfinding.moveSpeed * Time.deltaTime);
                }
                else
                {
                    // If the player is outside the detection radius, go back to roaming
                    state = State.Roaming;
                    StartCoroutine(RoamingRoutine());
                    yield break;
                }
            }


            yield return null;
        }
    }


    private void FlipSprite(float direction)
    {
        // Assuming your sprite renderer is attached to the same GameObject
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        if (enemyType == EnemyType.DarkBlueSlime)
        {
            // For DarkBlueSlime, check if moving right, flip the sprite
            if (direction > 0)
            {
                spriteRenderer.flipX = true;
            }
            // For DarkBlueSlime, check if moving left, flip the sprite back
            else if (direction < 0)
            {
                spriteRenderer.flipX = false;
            }
            // If direction is 0, the sprite orientation remains unchanged
        }
        else
        {
            // For other enemy types (e.g., Skeleton), use the original logic
            if (direction < 0)
            {
                spriteRenderer.flipX = true;
            }
            else if (direction > 0)
            {
                spriteRenderer.flipX = false;
            }
        }
    }
    private Vector2 GetRoamingPosition()
    {
        return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }
    public enum EnemyType
    {
        Skeleton,
        BlueSlime,
        DarkBlueSlime
    }
}