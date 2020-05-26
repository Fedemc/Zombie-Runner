using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private Animator anim;
    [SerializeField] Camera FPCamera;
    [SerializeField] float weaponRange = 100f;
    [SerializeField] ParticleSystem muzzleVFX;
    [SerializeField] GameObject shotHitVFX;
    float damage=25f;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();    
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Shoot();
            
        }
    }

    private void Shoot()
    {
        anim.SetTrigger("Shoot");
        muzzleVFX.Play();
        ProcessRayCast();

    }

    private void ProcessRayCast()
    {
        RaycastHit hit;
        if (Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out hit, weaponRange))
        {
            CreateHitImpact(hit);
            EnemyHealth enemyHealth = hit.transform.gameObject.GetComponent<EnemyHealth>();
            // If we hit enemy, reduce enemy health
            if (enemyHealth)
            {
                enemyHealth.ReduceHealth(damage);
            }
        }
        else
        {
            return;
        }
    }

    private void CreateHitImpact(RaycastHit hit)
    {
        GameObject shotHit = Instantiate(shotHitVFX, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(shotHit, 1f);
    }
}
