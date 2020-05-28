using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float playerHealth = 200;

    void Start()
    {
        
    }

    public void ReduceHealth(float damageReceived)
    {
        playerHealth -= damageReceived;
        if(playerHealth <= 0)
        {
            Debug.Log("You died.");
        }
    }
}
