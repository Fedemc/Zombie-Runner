using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float hitPoints = 100f;

    // method to reduce HP by amount of dmg (from weapon)
    public void ReduceHealth(float damage)
    {
        hitPoints -= damage;
        BroadcastMessage("OnDamageReceived");
        if(hitPoints <= 0)
        {
            GetComponent<EnemyAI>().Die();
            Destroy(gameObject, 3);
        }
    }
}
