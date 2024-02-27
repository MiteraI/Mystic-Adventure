using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float roamChangeDirFloat = 2f;
    [SerializeField] private float attackRange = 0.1f;
    [SerializeField] private float chaseRange = 3f;
    [SerializeField] private float chaseSpeed = 3f;
    [SerializeField] private MonoBehaviour enemyType;
    [SerializeField] private float attackCooldown = 2f;
    [SerializeField] private bool stopMovingWhileAttacking = false;

    private bool canAttack = true;

    private Vector2 roamPosition;
    private float timeRoaming = 0f;

    private EnemyPathfinding enemyPathfinding;  

    private void Awake()
    {
        enemyPathfinding = GetComponent<EnemyPathfinding>();
    }

    private void Start()
    {
        roamPosition = GetRoamingPosition();
    }

    private void Update()
    {
        MovementStateControl();
    }

    private void MovementStateControl()
    {
        var distanceToPlayer = Vector2.Distance(transform.position, PlayerController.Instance.transform.position);

        if (distanceToPlayer < attackRange)
        {
             Attacking();
        } else if (distanceToPlayer < chaseRange)
        {
            Chasing();
		} else
        {
			Roaming();
		}
    }

    private void Roaming()
    {
        timeRoaming += Time.deltaTime;

        FlipSprite(roamPosition);

        enemyPathfinding.MoveTo(roamPosition);

        if (timeRoaming > roamChangeDirFloat)
        {
            roamPosition = GetRoamingPosition();
        }
    }

    private void Attacking()
    {
   
        if (attackRange != 0 && canAttack)
        {

            canAttack = false;
            (enemyType as IEnemy).Attack();

            if (stopMovingWhileAttacking)
            {
                enemyPathfinding.StopMoving();
            }
            else
            {
                enemyPathfinding.MoveTo(roamPosition);
            }

            StartCoroutine(AttackCooldownRoutine());
        }
    }

    private void Chasing()
    {
        var playerPosition = PlayerController.Instance.transform.position;
        FlipSprite(playerPosition);
		transform.position = Vector2.MoveTowards(transform.position, playerPosition, chaseSpeed * Time.deltaTime);
	}

    private void FlipSprite(Vector3 target)
    {
        // Flip the sprite based on the direction of the player
        if (target.x < transform.position.x)
        {
			transform.localScale = new Vector3(1, 1, 1);
		}
		else
        {
			transform.localScale = new Vector3(-1, 1, 1);
		}
    }

    private IEnumerator AttackCooldownRoutine()
    {
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    private Vector2 GetRoamingPosition()
    {
        timeRoaming = 0f;
        return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }
}
