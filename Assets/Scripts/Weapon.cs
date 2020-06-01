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
    [SerializeField] Ammo ammoSlot;
    [SerializeField] float timeBetweenShoots = 0.5f;
    [SerializeField] float damage=25f;
    bool canShoot = true;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();    
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && canShoot)
        {
            
            StartCoroutine(Shoot());
        }
    }

    IEnumerator Shoot()
    {
        canShoot = false;
        if (ammoSlot.GetAmmoAmount() > 0)
        {
            anim.SetTrigger("Shoot");
            muzzleVFX.Play();
            ProcessRayCast();
            ammoSlot.ReduceAmmo();
        }
        yield return new WaitForSeconds(timeBetweenShoots);
        canShoot = true;
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
        Destroy(shotHit, 0.5f);
    }
}
