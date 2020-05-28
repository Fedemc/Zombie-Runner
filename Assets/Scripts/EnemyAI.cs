using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float chaseRange = 5f;
    [SerializeField] float attackRange = 0.5f;
    [SerializeField] Animator animController;
    [SerializeField] float turnSpeed = 5f;

    NavMeshAgent navMeshAgent;
    float distanceToTarget = Mathf.Infinity;
    bool isProvoked = false;
    public enum EnemyState
    {
        alive,
        dead
    }
    public EnemyState enemyState;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.stoppingDistance = attackRange;
        animController = GetComponent<Animator>();
        enemyState = EnemyState.alive;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.75f);
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }

    // Update is called once per frame
    void Update()
    {
        distanceToTarget = Vector3.Distance(target.position, transform.position);
        
        if(enemyState == EnemyState.alive)
        {
            if (isProvoked)
            {
                EngageTarget();
            }
            else if (distanceToTarget <= chaseRange)
            {
                isProvoked = true;
                //navMeshAgent.SetDestination(target.position);
            }
        }
    }

    private void EngageTarget()
    {
        bool isOnAttackRange = distanceToTarget <= navMeshAgent.stoppingDistance;
        FaceTarget();

        if (!isOnAttackRange)
        {
            ChaseTarget();
        }
        
        if(isOnAttackRange)
        {
            AttackTarget();
        }

    }

    private void ChaseTarget()
    {
        animController.SetBool("onAttackRange", false);
        animController.SetTrigger("Move");
        navMeshAgent.SetDestination(target.position);
    }

    private void AttackTarget()
    {
        //Debug.Log(gameObject.name + " attacking target");
        animController.SetBool("onAttackRange", true);
    }

    private void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
    }

    public void Die()
    {
        navMeshAgent.isStopped = true;
        enemyState = EnemyState.dead;
        animController.SetTrigger("Die");
    }
}
