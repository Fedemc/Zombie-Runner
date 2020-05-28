using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] PlayerHealth target;
    [SerializeField] float attackDmg = 40f;


    void Start()
    {
        target = FindObjectOfType<PlayerHealth>();
    }

    public void AttackHitEvent()
    {
        if(!target)
        {
            return;
        }
        target.ReduceHealth(attackDmg);
        Debug.Log("Hit " + target.name + " for " + attackDmg +" damage.");
    }

}
